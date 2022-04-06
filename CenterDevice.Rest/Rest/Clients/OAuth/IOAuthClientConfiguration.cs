#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.OAuth
{
    public interface IOAuthClientConfiguration : IRestClientConfiguration
    {
        string ClientId { get; }

        string ClientSecret { get; }

        string RedirectUri { get; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element