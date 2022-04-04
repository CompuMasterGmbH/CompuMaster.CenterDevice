using System.Text.Json.Serialization;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents.Metadata
{
    public class DocumentLockMetadata : DocumentBasicMetadata, IDocumentLockMetadata
    {
        public List<string> Locks { get; set; }

        [JsonPropertyName(RestApiConstants.LOCKED_BY)]
        public string LockedBy { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element