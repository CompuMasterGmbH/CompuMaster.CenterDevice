using RestSharp;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.OAuth
{
    public interface IOAuthRestClient
    {
        RestResponse<OAuthInfo> SwapToken(OAuthInfo oAuthInfo, string userId);

        RestResponse<OAuthInfo> SwapToken(OAuthInfo oAuthInfo, string email, string tenantId);

        RestResponse<OAuthInfo> RefreshToken(OAuthInfo oAuthInfo);

        RestResponse<OAuthInfo> DestroyToken(OAuthInfo oAuthInfo);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element