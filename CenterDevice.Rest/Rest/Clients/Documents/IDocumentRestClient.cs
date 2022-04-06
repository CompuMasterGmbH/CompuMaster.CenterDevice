using CenterDevice.Rest.Clients.Documents.Metadata;
using System.IO;
using System.Threading;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents
{
    public interface IDocumentRestClient
    {
        DocumentFullMetadata GetDocumentMetadata(string userId, string id);

        T GetDocumentMetadata<T>(string userId, string id, long? version = null) where T : new();

        Stream DownloadPreview(string userId, string id, PreviewSize size, long? version);

        Stream DownloadDocument(string userId, string id);

        Stream DownloadDocument(string userId, string id, long? version, long? range);

        NewVersionUploadResponse UploadNewVersion(string userId, string id, string filename, string filepath);

        NewVersionUploadResponse UploadNewVersion(string userId, string id, string filename, string filepath, CancellationToken token);

        NewVersionUploadResponse RenameDocument(string userId, string id, string filename);

        DeleteDocumentsResponse DeleteDocument(string userId, string documentId);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element