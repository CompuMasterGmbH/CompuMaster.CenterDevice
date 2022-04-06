#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents.Metadata
{
    public class DocumentSharingMetadata : IDocumentSharingMetadata
    {
        public SharingInfo Collections { get; set; }

        public CenterDevice.Rest.Clients.Common.Sharings Groups { get; set; }

        public CenterDevice.Rest.Clients.Common.Sharings Users { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element