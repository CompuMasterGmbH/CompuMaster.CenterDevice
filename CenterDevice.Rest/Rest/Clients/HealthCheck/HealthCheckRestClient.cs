using RestSharp;
using System.Net;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.HealthCheck
{
    public class HealthCheckRestClient : CenterDeviceRestClient, IHealthCheckRestClient
    {
        private const string URI_RESOURCE = "healthcheck";

        public HealthCheckRestClient(IRestClientConfiguration configuration, IRestClientErrorHandler errorHandler) : base(null, configuration, errorHandler, null) { }

        public bool IsConnectionWorking(bool useDefaultProxy, string userName, string password)
        {
            var options = new RestClientOptions(CustomOptionBaseAddress)
            {
                UserAgent = this.CustomOptionUserAgent
            };

            if (useDefaultProxy)
            {
                options.Proxy = WebRequest.GetSystemWebProxy();
            }
            else
            {
                options.Proxy = null;
            }

            if (options.Proxy != null)
            {
                if (userName != null && password != null)
                {
                    options.Proxy.Credentials = new NetworkCredential(userName, password);
                }
                else
                {
                    options.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                }
            }

            var testClient = new RestClient(options);

            return testClient.ExecuteAsync(CreateRestRequest(URI_RESOURCE, Method.Get, ContentType.APPLICATION_JSON)).Result.StatusCode == HttpStatusCode.OK;
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element