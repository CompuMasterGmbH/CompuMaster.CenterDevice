using RestSharp.Deserializers;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Collections
{
    public class CollectionEraseResponse
    {
        [DeserializeAs(Name = RestApiConstants.REMOVED_FROM_COLLECTION)]
        public List<string> RemovedFromCollection { get; set; }

        [DeserializeAs(Name = RestApiConstants.DOCUMENTS_NOT_DELETED)]
        public List<string> DocumentsRemainedInCollection { get; set; }

        [DeserializeAs(Name = RestApiConstants.FOLDERS_NOT_DELETED)]
        public List<string> FoldersRemainedCollection { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element