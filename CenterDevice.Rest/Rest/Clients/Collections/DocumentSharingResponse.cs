using System.Text.Json.Serialization;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Collections
{
    public class DocumentSharingResponse
    {
        [JsonPropertyName(RestApiConstants.FAILED_DOCUMENTS)]
        public List<string> FailedDocuments { get; set; }

        public bool HasFailedDocuments { get { return FailedDocuments != null && FailedDocuments.Count > 0; } }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element