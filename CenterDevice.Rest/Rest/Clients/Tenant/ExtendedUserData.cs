using CenterDevice.Rest.Clients.User;
using RestSharp.Deserializers;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Tenant
{
    public class ExtendedUserData : User.UserData
    {
        [DeserializeAs(Name = "first-name")]
        public string FirstName { get; set; }

        [DeserializeAs(Name = "last-name")]
        public string LastName { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element