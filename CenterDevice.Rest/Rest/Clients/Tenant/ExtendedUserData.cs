using CenterDevice.Rest.Clients.User;
using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Tenant
{
    public class ExtendedUserData : User.UserData
    {
        [JsonPropertyName("first-name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last-name")]
        public string LastName { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element