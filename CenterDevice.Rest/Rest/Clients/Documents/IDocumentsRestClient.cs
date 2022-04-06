using CenterDevice.Rest.Clients.Documents.Metadata;
using System;
using System.Collections.Generic;
using System.Threading;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents
{
    public interface IDocumentsRestClient
    {
        DocumentSearchResults<DocumentFullMetadata> Search(string userId, string query, List<string> collections);

        DocumentSearchResults<T> Search<T>(string userId, string query, List<string> collections, int offset, int rows);

        DocumentSearchResults<T> Search<T>(string userId, DocumentSearchRequest request);

        DocumentSearchResults<T> Get<T>(string userId, string collectionId, IEnumerable<string> documentIds, string folderId, int offset, int rows) where T : new();

        DocumentSearchResults<T> Get<T>(string userId, string collectionId, IEnumerable<string> documentIds, string folderId, DateTime? lastChangeTo, int offset, int rows) where T : new();

        UploadDocumentResponse UploadDocument(string userId, string filename, string path, string collectionId, string parentId, CancellationToken cancellationToken);

        UploadDocumentResponse UploadDocument(string userId, string filename, string path, DateTime? documentDate, List<string> collectionIds, List<string> folderIds, CancellationToken cancellationToken);

        DeleteDocumentsResponse DeleteDocuments(string userId, List<string> ids);

        void MoveDocuments(string userId, IEnumerable<string> documentIds, string srcCollection, string srcFolder, string dstCollection, string dstFolder);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element