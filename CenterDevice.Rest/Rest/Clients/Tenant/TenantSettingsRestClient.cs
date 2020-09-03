﻿using CenterDevice.Rest.Clients.OAuth;
using CenterDevice.Rest.ResponseHandler;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace CenterDevice.Rest.Clients.Tenant
{
    public class TenantSettingsRestClient : CenterDeviceRestClient, ITenantSettingsRestClient
    {
        private string URI_RESOURCE
        {
            get
            {
                return this.ApiVersionPrefix + "tenant/{0}/settings";
            }
        }

        public TenantSettingsRestClient(IOAuthInfoProvider oauthInfo, IRestClientConfiguration configuration, IRestClientErrorHandler errorHandler, string apiVersionPrefix) : base(oauthInfo, configuration, errorHandler, apiVersionPrefix)
        {
        }

        public TenantSettings GetTenantSettings(string userId, string tenantId)
        {
            var tenantRequest = CreateRestRequest(string.Format(URI_RESOURCE, tenantId), Method.GET, ContentType.APPLICATION_JSON);

            var response = Execute<TenantSettings>(GetOAuthInfo(userId), tenantRequest);
            return UnwrapResponse(response, new StatusCodeResponseHandler<TenantSettings>(HttpStatusCode.OK));
        }

        public void UpdateTenantSettings(string userId, string tenantId, string settingName, IEnumerable<string> users, IEnumerable<string> roles)
        {
            var tenantRequest = CreateRestRequest(string.Format(URI_RESOURCE, tenantId), Method.PUT, ContentType.APPLICATION_JSON);

            JObject parameter = new JObject();
            if (users != null)
            {
                parameter[RestApiConstants.USERS] = JArray.FromObject(users);
            }
            if (roles != null)
            {
                parameter[RestApiConstants.ROLES] = JArray.FromObject(roles);
            }

            var settingsUpdate = new JObject
            {
                [settingName] = parameter
            };
            tenantRequest.AddParameter(ContentType.APPLICATION_JSON, settingsUpdate.ToString(), ParameterType.RequestBody);

            var response = Execute<TenantSettings>(GetOAuthInfo(userId), tenantRequest);
            ValidateResponse(response, new StatusCodeResponseHandler<TenantSettings>(HttpStatusCode.NoContent));
        }
    }
}
