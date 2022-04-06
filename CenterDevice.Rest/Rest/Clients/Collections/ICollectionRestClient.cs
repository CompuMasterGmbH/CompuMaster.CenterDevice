#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Collections
{
    public interface ICollectionRestClient
    {
        DocumentSharingResponse RemoveDocumentFromCollection(string userId, string documentId, string collectionId);

        DocumentSharingResponse AddDocumentToCollection(string userId, string documentId, string collectionId);

        CollectionEraseResponse EraseCollection(string userId, string collectionId, bool onlyOwnedDocuments);

        Collection GetCollection(string userId, string collectionId);

        Collection GetCollection(string userId, string collectionId, RestRequestFields fields);

        void DeleteCollection(string userId, string collectionId);

        void RenameCollection(string userId, string collectionId, string newName);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element