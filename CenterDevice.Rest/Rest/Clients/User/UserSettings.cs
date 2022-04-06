using RestSharp.Deserializers;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.User
{
    public class UserSettings
    {
        public LocaleSettings Locale { get; set; }

        [DeserializeAs(Name = RestApiConstants.TENANT_SETTINGS)]
        public UserTenantSettings TenantSettings { get; set; }
    }

    public class UserTenantSettings
    {
        public bool Unarchiving { get; set; }
    }

    public class LocaleSettings
    {
        public string Language { get; set; }
        public string Country { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element