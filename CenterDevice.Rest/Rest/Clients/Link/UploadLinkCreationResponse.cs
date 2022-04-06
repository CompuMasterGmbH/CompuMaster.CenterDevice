using RestSharp.Deserializers;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class UploadLinkCreationResponse
    {
        [DeserializeAs(Name = "web")]
        public string Web { get; set; }

        [DeserializeAs(Name = "id")]
        public string Id { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element