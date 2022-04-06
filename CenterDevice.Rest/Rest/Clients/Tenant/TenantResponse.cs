using RestSharp.Deserializers;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Tenant
{
    public class TenantResponse
    {
        public List<TenantData> Tenants { get; set; }

        [DeserializeAs(Name = "default-tenant-id")]
        public string DefaultTenantId { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element