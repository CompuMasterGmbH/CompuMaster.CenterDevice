using RestSharp.Deserializers;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents.Metadata
{
    public class DocumentFullMetadata : DocumentBasicMetadata, IDocumentSharingMetadata, IDocumentFolderMetadata, IDocumentLinkMetadata, IDocumentLockMetadata
    {
        public List<string> Locks { get; set; }

        [DeserializeAs(Name = RestApiConstants.LOCKED_BY)]
        public string LockedBy { get; set; }

        public SharingInfo Collections { get; set; }

        public List<string> Folders { get; set; }

        public string Link { get; set; }

        public CenterDevice.Rest.Clients.Common.Sharings Groups { get; set; }

        public CenterDevice.Rest.Clients.Common.Sharings Users { get; set; }

        public bool IsShared
        {
            get
            {
                return (Users != null && Users.HasSharing) || (Groups != null && Groups.HasSharing) || Link != null;
            }
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element