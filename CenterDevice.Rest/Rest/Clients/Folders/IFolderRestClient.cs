#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Folders
{
    public interface IFolderRestClient
    {
        Folder GetFolder(string userId, string id, string[] fields);

        void DeleteFolder(string userId, string id);

        FolderEraseResponse EraseFolder(string userId, string id, bool onlyOwnedDocuments);

        void RenameFolder(string userId, string folderId, string newName);

        void MoveFolder(string userId, string folderId, string targetFolderId, string targetCollectionId);

        void AddDocument(string userId, string documentId, string targetFolderId);

        void RemoveDocument(string userId, string documentId, string folderId, bool removeFromCollection);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element