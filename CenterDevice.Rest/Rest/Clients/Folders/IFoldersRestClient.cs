using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Folders
{
    public interface IFoldersRestClient
    {
        FoldersResponse GetFolders(string userId, string collectionId, string parentId, string documentId, string path, IEnumerable<string> ids, bool onlySharedFolders, bool onlyTopMost, string[] fields);

        FoldersResponse GetFolders(string userId, string collectionId, string parentId, string documentId, string path, IEnumerable<string> ids, string[] fields);

        FoldersResponse GetFolders(string userId, string collectionId, string parentId, string documentId);

        FoldersResponse GetFolders(string userId, string collectionId, string path);

        FolderCreationResponse CreateFolder(string userId, string name, string collection, string parent);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element