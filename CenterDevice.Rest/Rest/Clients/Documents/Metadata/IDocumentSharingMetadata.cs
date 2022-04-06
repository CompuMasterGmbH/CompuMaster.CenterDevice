#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents.Metadata
{
    public interface IDocumentSharingMetadata
    {
        SharingInfo Collections { get; set; }

        CenterDevice.Rest.Clients.Common.Sharings Groups { get; set; }

        CenterDevice.Rest.Clients.Common.Sharings Users { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element