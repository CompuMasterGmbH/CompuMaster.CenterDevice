#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public interface ILinksRestClient
    {
        LinkCreationResponse CreateDocumentLink(string userId, string id, LinkAccessControl accessControl);

        LinkCreationResponse CreateFolderLink(string userId, string id, LinkAccessControl accessControl);

        LinkCreationResponse CreateCollectionLink(string userId, string id, LinkAccessControl accessControl);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element