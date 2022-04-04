using CenterDevice.Rest.Clients.OAuth;
using CenterDevice.Rest.ResponseHandler;
using Newtonsoft.Json.Linq;
using Ninject;
using RestSharp;
using System.Net;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class LinkRestClient : CenterDeviceRestClient, ILinkRestClient
    {
        private string URI_RESOURCE
        {
            get
            {
                return this.ApiVersionPrefix + "link/";
            }
        }

        [Inject]
        public LinkRestClient(IOAuthInfoProvider oauthInfo, IRestClientConfiguration configuration, IRestClientErrorHandler errorHandler, string apiVersionPrefix) : base(oauthInfo, configuration, errorHandler, apiVersionPrefix)
        {
        }

        public Link GetLink(string userId, string linkId)
        {
            var linkRequest = CreateRestRequest(URI_RESOURCE + linkId, Method.Get, ContentType.APPLICATION_JSON);

            var result = Execute<Link>(GetOAuthInfo(userId), linkRequest);
            return UnwrapResponse(result, new StatusCodeResponseHandler<Link>(HttpStatusCode.OK));
        }

        public void DeleteLink(string userId, string linkId)
        {
            var linkRequest = CreateRestRequest(URI_RESOURCE + linkId, Method.Delete, ContentType.APPLICATION_JSON);

            var result = Execute<Link>(GetOAuthInfo(userId), linkRequest);
            ValidateResponse(result, new StatusCodeResponseHandler(HttpStatusCode.NoContent));
        }

        public void UpdateLink(string userId, string linkId, LinkAccessControl accessControl)
        {
            var updateLinkRequest = CreateRestRequest(URI_RESOURCE + linkId, Method.Put, ContentType.APPLICATION_JSON);

            var parameters = new JObject();
            parameters[RestApiConstants.ACCESS_CONTROL] = AccessControlConverter.ToJsonObject(accessControl);

            updateLinkRequest.AddParameter(ContentType.APPLICATION_JSON, parameters.ToString(), ParameterType.RequestBody);

            var result = Execute<Link>(GetOAuthInfo(userId), updateLinkRequest);
            ValidateResponse(result, new StatusCodeResponseHandler(HttpStatusCode.NoContent));
        }


    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element