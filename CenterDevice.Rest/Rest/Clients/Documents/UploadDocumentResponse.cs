using RestSharp.Deserializers;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents
{
    public class UploadDocumentResponse
    {
        public string Id { get; set; }

        [DeserializeAs(Name = RestApiConstants.UPLOAD_ACTIONS)]
        public UploadActions UploadActions { get; set; }

        // Duplicates are not handled since we don't use 
        // "check-for-duplicate" parameter when uploading new documents

        public bool HasUploadActions { get { return UploadActions != null; } }

        public bool HasFailedFolders
        {
            get { return HasUploadActions && UploadActions.HasAddToFolder && UploadActions.AddToFolder.HasFailedFolders; }
        }
    }

    public class UploadActions
    {
        [DeserializeAs(Name = RestApiConstants.ADD_TO_FOLDER)]
        public AddToFolder AddToFolder { get; set; }

        public bool HasAddToFolder { get { return AddToFolder != null; } }
    }

    public class AddToFolder
    {
        public List<string> Failed { get; set; }

        public bool HasFailedFolders { get { return Failed != null && Failed.Count > 0; } }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element