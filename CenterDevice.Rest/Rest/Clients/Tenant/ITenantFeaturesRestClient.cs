#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Tenant
{
    public interface ITenantFeaturesRestClient
    {
        TenantFeatures GetTenantFeatures(string userId, string tenantId);
        void EnableTenantFeature(string userId, string tenantId, string feature);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element