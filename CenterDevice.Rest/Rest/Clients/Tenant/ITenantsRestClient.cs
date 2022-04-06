using CenterDevice.Rest.Clients.OAuth;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Tenant
{
    public interface ITenantsRestClient
    {
        TenantResponse GetTenants(OAuthInfo oAuthInfo);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element