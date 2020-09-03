﻿using CenterDevice.Rest.Clients.OAuth;
using CenterDevice.Rest.ResponseHandler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using RestSharp;
using System.Net;
using System;
using System.Collections.Generic;

namespace CenterDevice.Rest.Clients.Link
{
    public class UploadLinkRestClient : CenterDeviceRestClient, IUploadLinkRestClient
    {
        private string URI_RESOURCE
        {
            get
            {
                return this.ApiVersionPrefix + "upload-link/";
            }
        }

        [Inject]
        public UploadLinkRestClient(IOAuthInfoProvider oauthInfo, IRestClientConfiguration configuration, IRestClientErrorHandler errorHandler, string apiVersionPrefix, string uploadLinkBaseUrl) : base(oauthInfo, configuration, errorHandler, apiVersionPrefix)
        {
            this.UploadLinkBaseUrl = uploadLinkBaseUrl;
        }

        private readonly string UploadLinkBaseUrl;

        public UploadLink GetLink(string userId, string linkId)
        {
            var linkRequest = CreateRestRequest(URI_RESOURCE + linkId, Method.GET, ContentType.APPLICATION_JSON);

            var result = Execute<UploadLink>(GetOAuthInfo(userId), linkRequest);
            UploadLink resultUnwrapped = UnwrapResponse(result, new StatusCodeResponseHandler<UploadLink>(HttpStatusCode.OK));
            resultUnwrapped.UploadLinkBaseUrl = this.UploadLinkBaseUrl;
            return resultUnwrapped;
        }

        public void DeleteLink(string userId, string linkId)
        {
            var linkRequest = CreateRestRequest(URI_RESOURCE + linkId, Method.DELETE, ContentType.APPLICATION_JSON);

            var result = Execute<Link>(GetOAuthInfo(userId), linkRequest);
            ValidateResponse(result, new StatusCodeResponseHandler(HttpStatusCode.NoContent));
        }

        public void UpdateLink(string userId, string linkId, string collectionId, string name, List<string> tags, DateTime? expiryDate, int? maxDocuments, string password, bool? emailCreatorOnUpload)
        {
            var updateLinkRequest = CreateRestRequest(URI_RESOURCE + linkId, Method.PUT, ContentType.APPLICATION_JSON);

#pragma warning disable IDE0028 // Initialisierung der Sammlung vereinfachen
            var parameters = new JObject();
#pragma warning restore IDE0028 // Initialisierung der Sammlung vereinfachen
            if (collectionId != null) parameters[RestApiConstants.COLLECTION] = collectionId;
            if (name != null) parameters[RestApiConstants.NAME] = name;
            if (tags != null) parameters[RestApiConstants.TAGS] = JsonConvert.SerializeObject(tags);
            if (expiryDate.HasValue) parameters[RestApiConstants.EXPIRY_DATE] = expiryDate;
            if (maxDocuments.HasValue) parameters[RestApiConstants.MAX_DOCUMENTS] = maxDocuments;
            if (password != null) parameters[RestApiConstants.PASSWORD] = password;
            if (emailCreatorOnUpload.HasValue) parameters[RestApiConstants.EMAIL_CREATOR_ON_UPLOAD] = emailCreatorOnUpload;

            updateLinkRequest.AddParameter(ContentType.APPLICATION_JSON, parameters.ToString(), ParameterType.RequestBody);

            var result = Execute<Link>(GetOAuthInfo(userId), updateLinkRequest);
            ValidateResponse(result, new StatusCodeResponseHandler(HttpStatusCode.NoContent));
        }


    }
}
