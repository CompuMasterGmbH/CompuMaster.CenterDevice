using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class Link
    {
        [JsonPropertyName(RestApiConstants.WEB)]
        public string Web { get; set; }

        [JsonPropertyName(RestApiConstants.DOWNLOAD)]
        public string Download { get; set; }

        [JsonPropertyName(RestApiConstants.ID)]
        public string Id { get; set; }

        [JsonPropertyName(RestApiConstants.REST)]
        public string Rest { get; set; }

        [JsonPropertyName(RestApiConstants.ACCESS_CONTROL)]
        public LinkAccessControl AccessControl { get; set; }

        [JsonPropertyName(RestApiConstants.COLLECTION)]
        public string Collection { get; set; }

        [JsonPropertyName(RestApiConstants.VIEWS)]
        public System.Int64 Views { get; set; }

        [JsonPropertyName(RestApiConstants.DOWNLOADS)]
        public System.Int64 Downloads { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element