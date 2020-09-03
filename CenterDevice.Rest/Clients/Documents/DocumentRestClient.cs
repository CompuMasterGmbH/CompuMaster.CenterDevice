﻿using CenterDevice.Rest.Clients.Documents.Metadata;
using CenterDevice.Rest.Clients.OAuth;
using CenterDevice.Rest.Exceptions;
using CenterDevice.Rest.ResponseHandler;
using CenterDevice.Rest.Utils;
using Ninject;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace CenterDevice.Rest.Clients.Documents
{
    public class DocumentRestClient : CenterDeviceRestClient, IDocumentRestClient
    {
        private const string URI_RESOURCE = "v2/document/";
        private const int PREVIEW_TIMEOUT = 10 * 1000;

        private readonly string userAgent;
        private readonly StreamWrapper streamWrapper;

        [Inject]
        public DocumentRestClient(IOAuthInfoProvider oauthInfoProvider, IRestClientConfiguration configuration, IRestClientErrorHandler errorHandler, StreamWrapper streamWrapper) : base(oauthInfoProvider, configuration, errorHandler)
        {
            userAgent = configuration.UserAgent;
            this.streamWrapper = streamWrapper;
        }

        public DocumentFullMetadata GetDocumentMetadata(string userId, string id)
        {
            return GetDocumentMetadata<DocumentFullMetadata>(userId, id, null);
        }

        public T GetDocumentMetadata<T>(string userId, string id, long? version = null) where T : new()
        {
            string path = URI_RESOURCE + id;
            if (version != null && version > 0)
            {
                path += ";" + RestApiConstants.VERSION + "=" + version;
            }

            var metadataRequest = CreateRestRequest(path, Method.GET, ContentType.APPLICATION_JSON);
            metadataRequest.AddQueryParameter(RestApiConstants.INCLUDES, FieldUtils.GetFieldIncludes(typeof(T)));

            var result = Execute<T>(GetOAuthInfo(userId), metadataRequest);
            return UnwrapResponse(result, new StatusCodeResponseHandler<T>(HttpStatusCode.OK));
        }

        public Stream DownloadPreview(string userId, string id, PreviewSize size, long? version)
        {
            var webRequest = CreatePreviewDocumentRequest(userId, id, size, version);
            webRequest.Timeout = PREVIEW_TIMEOUT;

            HttpWebResponse webResponse = null;
            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e) when ((e.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException(e.Message, e);
            }
            catch (WebException e) when ((e.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotAcceptable)
            {
                throw new NotAcceptableException(e.Message, e);
            }

            if (webResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new RestClientException("Cannot download preview for document '" + id + "', received status code " + webResponse.StatusCode + ".");
            }

            return DocumentStreamUtils.WrapDownloadStream(webResponse.GetResponseStream(), streamWrapper);
        }

        public Stream DownloadDocument(string userId, string id)
        {
            return DownloadDocument(userId, id, null, null);
        }

        public Stream DownloadDocument(string userId, string id, long? version, long? range)
        {
            HttpWebRequest webRequest = CreateDownloadDocumentRequest(userId, id, version, range);
            HttpWebResponse webResponse = null;
            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e) when ((e.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.RequestedRangeNotSatisfiable)
            {
                throw new RequestedRangeNotSatisfiableException("The requested range cannot be downloaded");
            }
            catch (WebException e) when ((e.Response as HttpWebResponse)?.StatusCode != HttpStatusCode.OK)
            {
                ThrowRestClientException(e, e.Response as HttpWebResponse);
            }

            ValidateResponse(webResponse, userId, range);
            return DocumentStreamUtils.WrapDownloadStream(webResponse.GetResponseStream(), streamWrapper);
        }

        private static void ThrowRestClientException(Exception e, HttpWebResponse errorResponse)
        {
            var content = (errorResponse?.GetResponseStream() != null) ?
                new StreamReader(errorResponse.GetResponseStream()).ReadToEnd()
                : null;

            throw RestClientExceptionUtils.CreateDefaultException(new List<HttpStatusCode> { HttpStatusCode.OK }, errorResponse?.StatusCode, content, e);
        }

        private HttpWebRequest CreateDownloadDocumentRequest(string userId, string id, long? version, long? range)
        {
            return CreateDownloadRequest(userId, range, GetDocumentDownloadUri(id, version));
        }

        private HttpWebRequest CreatePreviewDocumentRequest(string userId, string id, PreviewSize size, long? version)
        {
            var path = URI_RESOURCE + id;
            if (version != null)
            {
                path += ";" + RestApiConstants.VERSION + "=" + version;
            }
            path += ";preview=" + size.ToApiParameter() + ";pages=1?wait-for-generation=10&include-error-info=false";

            HttpWebRequest httpWebRequest = CreateDownloadRequest(userId, null, new Uri(new Uri(GetBaseAddress()), path));
            httpWebRequest.Accept = "image/png, image/jpeg";
            return httpWebRequest;
        }

        private HttpWebRequest CreateDownloadRequest(string userId, long? range, Uri requestUri)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            webRequest.Headers.Add("Authorization", GetAuthorizationBearer(userId));
            webRequest.Timeout = int.MaxValue;
            webRequest.KeepAlive = false;
            webRequest.ReadWriteTimeout = int.MaxValue;
            webRequest.UserAgent = userAgent;
            if (range != null)
            {
                webRequest.AddRange(range.Value);
            }
            return webRequest;
        }

        private Uri GetDocumentDownloadUri(string id, long? version)
        {
            var path = URI_RESOURCE + id;
            if (version != null)
            {
                path += ";" + RestApiConstants.VERSION + "=" + version;
            }
            return new Uri(new Uri(GetBaseAddress()), path);
        }

        private void ValidateResponse(HttpWebResponse webResponse, string userId, long? range)
        {
            if ((int)webResponse.StatusCode == TOO_MANY_REQUESTS)
            {
                throw new TooManyRequestsException(ExtractDelay(webResponse.Headers.Get(RETRY_AFTER)));
            }
            else if (webResponse.StatusCode != HttpStatusCode.OK && range == null)
            {
                throw new RestClientException("Received unexpected status code while trying to download document: " + webResponse.StatusCode);
            }
            else if (webResponse.StatusCode != HttpStatusCode.PartialContent && range != null)
            {
                throw new RestClientException("Received unexpected status code while trying to partially download document: " + webResponse.StatusCode);
            }
        }

        public NewVersionUploadResponse UploadNewVersion(string userId, string id, string filename, string filepath)
        {
            return UploadNewVersion(userId, id, filename, filepath, new CancellationToken());
        }

        public NewVersionUploadResponse UploadNewVersion(string userId, string id, string filename, string filepath, CancellationToken token)
        {
            RestRequest newVersionRequest = CreateRestRequest(URI_RESOURCE + id, Method.POST, ContentType.MULTIPART_FORM_DATA);
            newVersionRequest.AlwaysMultipartFormData = true;
            newVersionRequest.AddParameter(GenerateFormParameter("metadata", GetMetadata(filename, filepath), ContentType.APPLICATION_JSON));
            DocumentStreamUtils.AddFileToUpload(newVersionRequest, "document", filepath, streamWrapper, token);
            newVersionRequest.Timeout = int.MaxValue;
            newVersionRequest.ReadWriteTimeout = int.MaxValue; // Cannot use Timeout.Infinite here because resthsharp only uses this if > 0

            var result = Execute<NewVersionUploadResponse>(GetOAuthInfo(userId), newVersionRequest);
            return UnwrapResponse(result, new StatusCodeResponseHandler<NewVersionUploadResponse>(HttpStatusCode.Created));
        }

        private Parameter GenerateFormParameter(string name, object value, string contentType)
        {
            Parameter parameter = new Parameter();
            parameter.Name = name;
            parameter.Value = value;
            parameter.ContentType = contentType;
            parameter.Type = ParameterType.RequestBody;
            return parameter;
        }

        private string GetMetadata(string filename, string fileFullpath)
        {
            return "{ metadata: { document: {filename: \"" + filename + "\", size: \"" + GetFileSize(fileFullpath) + "\"} } }";
        }

        public NewVersionUploadResponse RenameDocument(string userId, string id, string filename)
        {
            RestRequest renameRequest = CreateRestRequest(URI_RESOURCE + id, Method.POST, ContentType.APPLICATION_JSON);
            renameRequest.AddJsonBody(new { action = RestApiConstants.RENAME, @params = new { filename = filename } });

            var result = Execute<NewVersionUploadResponse>(GetOAuthInfo(userId), renameRequest);
            return UnwrapResponse(result, new RenameDocumentResponseHandler());
        }

        public void AddLock(string userId, string id)
        {
            RestRequest renameRequest = CreateRestRequest(URI_RESOURCE + id, Method.POST, ContentType.APPLICATION_JSON);
            renameRequest.AddJsonBody(new { action = RestApiConstants.ADD_LOCK, @params = new { locks = new string[] { RestApiConstants.CREATE_NEW_VERSION } } });

            var result = Execute<NewVersionUploadResponse>(GetOAuthInfo(userId), renameRequest);
            ValidateResponse(result, new StatusCodeResponseHandler(HttpStatusCode.NoContent));
        }

        public void RemoveLock(string userId, string id)
        {
            RestRequest renameRequest = CreateRestRequest(URI_RESOURCE + id, Method.POST, ContentType.APPLICATION_JSON);
            renameRequest.AddJsonBody(new { action = RestApiConstants.REMOVE_LOCK, @params = new { locks = new string[] { RestApiConstants.CREATE_NEW_VERSION } } });

            var result = Execute<NewVersionUploadResponse>(GetOAuthInfo(userId), renameRequest);
            ValidateResponse(result, new StatusCodeResponseHandler(HttpStatusCode.NoContent));
        }

        private string GetFileName(string filepath)
        {
            return Path.GetFileName(filepath);
        }

        private long GetFileSize(string filepath)
        {
            return new FileInfo(filepath).Length;
        }

        public DeleteDocumentsResponse DeleteDocument(string userId, string documentId)
        {
            RestRequest delete = CreateRestRequest(URI_RESOURCE + documentId, Method.DELETE, ContentType.APPLICATION_JSON);

            var result = Execute<DeleteDocumentsResponse>(GetOAuthInfo(userId), delete);
            return UnwrapResponse(result, new StatusCodeResponseHandler<DeleteDocumentsResponse>(new List<HttpStatusCode> { HttpStatusCode.OK, HttpStatusCode.NoContent }));
        }
    }
}
