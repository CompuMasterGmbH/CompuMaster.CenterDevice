using CenterDevice.Rest.Clients.OAuth;
using CenterDevice.Rest.Exceptions;
using CenterDevice.Rest.ResponseHandler;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CenterDevice.Rest.Clients
{
    public abstract class CenterDeviceRestClient
    {
        protected const string AUTHORIZATION = "Authorization";
        private const string CONTENT_TYPE = "Content-Type";
        private const string BEARER = "Bearer ";
        private const string PARAMETERS = "Parameters";

        private const string UNKNOWN_OR_EXPIRED_TOKEN = "Unknown or expired token";
        private const string EXPIRED_TENANT = "Tenant has expired";
        private const string OPERATION_TIMED_OUT_EXCEPTION = "The operation has timed out";

        protected readonly RestClient client;
        protected readonly IOAuthInfoProvider oAuthInfoProvider;
        protected readonly TimeSpan defaultDelay = TimeSpan.FromMinutes(1);

        protected const string RETRY_AFTER = "Retry-After";
        protected const int TOO_MANY_REQUESTS = 429;

        private OfflineModeSimulator offlineModeSimulator = new OfflineModeSimulator();
        private readonly IRestClientErrorHandler errorHandler;

        public void DisableOfflineModeSimulation()
        {
            offlineModeSimulator = null;
        }

        public CenterDeviceRestClient(IOAuthInfoProvider oauthInfo, IRestClientConfiguration configuration, IRestClientErrorHandler errorHandler)
        {
            this.errorHandler = errorHandler;
            oAuthInfoProvider = oauthInfo;
            client = new RestClient(configuration.BaseAddress);
            client.UserAgent = configuration.UserAgent;
        }

        protected string GetBaseAddress()
        {
            return client.BaseUrl.AbsoluteUri;
        }

        protected virtual IRestResponse Execute(OAuthInfo oAuthInfo, IRestRequest request)
        {
            PrepareRequest(oAuthInfo, request);

            return HandleResponseSync(oAuthInfo, request, client.Execute(request));
        }

        protected virtual IRestResponse<T> Execute<T>(OAuthInfo oAuthInfo, IRestRequest request) where T : new()
        {
            PrepareRequest(oAuthInfo, request);

            return HandleResponseSync(oAuthInfo, request, client.Execute<T>(request));
        }

        private void PrepareRequest(OAuthInfo oAuthInfo, IRestRequest request)
        {
            offlineModeSimulator?.ThrowIfOffline();

            AddAuthorizationHeader(oAuthInfo, request);
        }

        protected TimeSpan? ExtractDelay(IRestResponse result)
        {
            return ExtractDelay((string)result.Headers.FirstOrDefault(parameter => parameter.Name.Equals(RETRY_AFTER, StringComparison.OrdinalIgnoreCase))?.Value);
        }

        protected TimeSpan? ExtractDelay(string value)
        {
            try
            {
                if (value != null)
                {
                    return TimeSpan.FromSeconds(int.Parse(value));
                }
            }
            catch (Exception)
            {
                // Nothing to do
            }
            return null;
        }

        private bool IsRateLimitExceeded(IRestResponse result)
        {
            return (int)result.StatusCode == TOO_MANY_REQUESTS;
        }

        private IRestResponse<T> HandleResponseSync<T>(OAuthInfo oAuthInfo, IRestRequest request, IRestResponse<T> result) where T : new()
        {
            if (IsExpiredToken(result))
            {
                var refreshOAuthInfo = errorHandler?.RefreshToken(oAuthInfo);
                if (refreshOAuthInfo == null)
                {
                    return result;
                }

                SwapAuthorizationHeader(refreshOAuthInfo, request);

                return client.Execute<T>(request);
            }
            else if (IsRateLimitExceeded(result))
            {
                throw new TooManyRequestsException(ExtractDelay(result));
            }
            else if (IsNotConnected(result))
            {
                throw new NotConnectedException(result.ErrorMessage, result.ErrorException);
            }
            else if (IsOperationTimedOut(result))
            {
                throw new OperationTimedOutException(result.ErrorMessage, result.ErrorException);
            }
            else
            {
                return result;
            }
        }

        private IRestResponse HandleResponseSync(OAuthInfo oAuthInfo, IRestRequest request, IRestResponse result)
        {
            if (IsExpiredToken(result))
            {
                var refreshOAuthInfo = errorHandler?.RefreshToken(oAuthInfo);
                if (refreshOAuthInfo == null)
                {
                    return result;
                }

                SwapAuthorizationHeader(refreshOAuthInfo, request);

                return client.Execute(request);
            }
            else if (IsRateLimitExceeded(result))
            {
                throw new TooManyRequestsException(ExtractDelay(result));
            }
            else if (IsNotConnected(result))
            {
                throw new NotConnectedException(result.ErrorMessage, result.ErrorException);
            }
            else if (IsOperationTimedOut(result))
            {
                throw new OperationTimedOutException(result.ErrorMessage, result.ErrorException);
            }
            else
            {
                return result;
            }
        }

        private bool IsNotConnected(IRestResponse result)
        {
            var exception = result.ErrorException as WebException;
            if (exception == null)
            {
                return false;
            }

            return exception.Status == WebExceptionStatus.ConnectFailure || exception.Status == WebExceptionStatus.NameResolutionFailure;
        }

        protected void ValidateResponse(IRestResponse result, BaseResponseHandler handler)
        {
            errorHandler?.ValidateResponse(result);

            handler.ValidateResponse(result);
        }

        protected T UnwrapResponse<T>(IRestResponse<T> result, DataResponseHandler<T> handler)
        {
            errorHandler?.ValidateResponse(result);

            handler.ValidateResponse(result);

            return handler.UnwrapResponse(result);
        }

        protected RestRequest CreateRestRequest(string path, Method method, string contentType)
        {
            return AddContentTypeHeader(CreateRestRequest(path, method), contentType);
        }

        protected RestRequest CreateRestRequest(string path, Method method)
        {
            return new RestRequest(path, method);
        }

        protected OAuthInfo GetOAuthInfo(string userId)
        {
            return oAuthInfoProvider.GetOAuthInfo(userId);
        }

        protected RestRequest AddContentTypeHeader(RestRequest request, string contentType)
        {
            request.AddHeader(CONTENT_TYPE, contentType);
            return request;
        }

        protected string GetAuthorizationBearer(string userId)
        {
            return GetAuthorizationBearer(oAuthInfoProvider.GetOAuthInfo(userId));
        }

        protected string GetAuthorizationBearer(OAuthInfo oAuthInfo)
        {
            return BEARER + oAuthInfo.access_token;
        }

        private void AddAuthorizationHeader(OAuthInfo oAuthInfo, IRestRequest request)
        {
            request.AddHeader(AUTHORIZATION, GetAuthorizationBearer(oAuthInfo));
        }

        private void SwapAuthorizationHeader(OAuthInfo newOAuthInfo, IRestRequest request)
        {
            RemoveAuthorizationHeader(request);
            AddAuthorizationHeader(newOAuthInfo, request);
        }

        private void RemoveAuthorizationHeader(IRestRequest request)
        {
            ((List<Parameter>)((RestRequest)request).GetType().GetProperty(PARAMETERS).GetValue(request))
                .RemoveAll(parameter => parameter.Name == AUTHORIZATION);
        }

        private bool IsExpiredToken(IRestResponse result)
        {
            return result.StatusCode == HttpStatusCode.Unauthorized
                && (result.Content == UNKNOWN_OR_EXPIRED_TOKEN || result.Content == EXPIRED_TENANT);
        }

        private bool IsOperationTimedOut(IRestResponse result)
        {
            if (result.ErrorMessage != null)
            {
                return result.ErrorMessage.Contains(OPERATION_TIMED_OUT_EXCEPTION);
            }

            return false;
        }
    }
}
