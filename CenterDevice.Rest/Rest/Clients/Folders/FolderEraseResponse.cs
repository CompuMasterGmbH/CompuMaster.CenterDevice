using System.Text.Json.Serialization;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Folders
{
    public class FolderEraseResponse
    {
        [JsonPropertyName(RestApiConstants.DOCUMENTS_NOT_DELETED)]
        public List<string> DocumentsRemainedInCollection { get; set; }

        [JsonPropertyName(RestApiConstants.FOLDERS_NOT_DELETED)]
        public List<string> FoldersRemainedCollection { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element