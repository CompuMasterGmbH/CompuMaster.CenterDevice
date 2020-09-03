﻿using RestSharp;
using System.Net;

namespace CenterDevice.Rest.Clients.HealthCheck
{
    public class HealthCheckRestClient : CenterDeviceRestClient, IHealthCheckRestClient
    {
        private const string URI_RESOURCE = "healthcheck";

        public HealthCheckRestClient(IRestClientConfiguration configuration, IRestClientErrorHandler errorHandler) : base(null, configuration, errorHandler, null) { }

        public bool IsConnectionWorking(bool useDefaultProxy, string userName, string password)
        {
            var testClient = new RestClient(GetBaseAddress());
            testClient.UserAgent = client.UserAgent;

            if (useDefaultProxy)
            {
                testClient.Proxy = WebRequest.GetSystemWebProxy();
            }
            else
            {
                testClient.Proxy = null;
            }

            if (testClient.Proxy != null)
            {
                if (userName != null && password != null)
                {
                    testClient.Proxy.Credentials = new NetworkCredential(userName, password);
                }
                else
                {
                    testClient.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                }
            }

            return testClient.Execute(CreateRestRequest(URI_RESOURCE, Method.GET, ContentType.APPLICATION_JSON)).StatusCode == HttpStatusCode.OK;
        }
    }
}
