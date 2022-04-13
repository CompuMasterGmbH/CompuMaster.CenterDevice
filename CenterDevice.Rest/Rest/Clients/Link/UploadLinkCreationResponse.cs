using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class UploadLinkCreationResponse
    {
        [JsonPropertyName("web")]
        public string Web { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element