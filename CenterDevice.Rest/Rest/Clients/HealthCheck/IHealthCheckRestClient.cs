#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.HealthCheck
{
    public interface IHealthCheckRestClient
    {
        bool IsConnectionWorking(bool useDefaultProxy, string userName, string password);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element