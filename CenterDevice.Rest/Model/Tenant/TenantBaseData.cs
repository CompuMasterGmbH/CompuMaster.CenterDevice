#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Model.Tenant
{
    public interface TenantBaseData
    {
        string Id { get; }

        string Name { get; }

        string Status { get; }

        bool Expired { get; }

    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element