using CenterDevice.Rest.Clients.OAuth;
using RestSharp;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients
{
    public interface IRestClientErrorHandler
    {
        void ValidateResponse(IRestResponse result);
        OAuthInfo RefreshToken(OAuthInfo oAuthInfo);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element