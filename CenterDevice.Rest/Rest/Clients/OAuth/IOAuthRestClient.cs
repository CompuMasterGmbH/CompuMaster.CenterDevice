using RestSharp;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.OAuth
{
    public interface IOAuthRestClient
    {
        IRestResponse<OAuthInfo> SwapToken(OAuthInfo oAuthInfo, string userId);

        IRestResponse<OAuthInfo> SwapToken(OAuthInfo oAuthInfo, string email, string tenantId);

        IRestResponse<OAuthInfo> RefreshToken(OAuthInfo oAuthInfo);

        IRestResponse<OAuthInfo> DestroyToken(OAuthInfo oAuthInfo);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element