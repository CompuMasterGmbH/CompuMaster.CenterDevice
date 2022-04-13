using System.Text.Json.Serialization;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class UploadLinks
    {
        [JsonPropertyName(RestApiConstants.UPLOAD_LINKS)]
        public List<UploadLink> UploadLinksList { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element