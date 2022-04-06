using RestSharp.Deserializers;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents.Metadata
{
    public class DocumentSyncMetadata : DocumentBasicMetadata, IDocumentLockMetadata
    {
        public List<string> Locks { get; set; }

        [DeserializeAs(Name = RestApiConstants.LOCKED_BY)]
        public string LockedBy { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element