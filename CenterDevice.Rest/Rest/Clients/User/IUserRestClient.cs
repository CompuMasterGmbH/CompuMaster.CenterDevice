using CenterDevice.Rest.Clients.OAuth;
using CenterDevice.Rest.Clients.Tenant;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.User
{
    public interface IUserRestClient
    {
        ExtendedUserData GetLoggedInUserData(OAuthInfo oAuthInfo);

        BaseUserData GetUserData(string userId, string userIdToRequest);

        FullUserData GetUserDataField(string userId, string userIdToRequest, string field);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element