using System.Text.Json.Serialization;
using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents.Metadata
{
    public class DocumentBasicMetadata : IDocumentBasicMetadata
    {
        public string Id { get; set; }

        public string Filename { get; set; }

        public long Version { get; set; }

        public long Size { get; set; }

        [JsonPropertyName(RestApiConstants.UPLOAD_DATE)]
        public DateTime UploadDate { get; set; }

        [JsonPropertyName(RestApiConstants.VERSION_DATE)]
        public DateTime VersionDate { get; set; }

        [JsonPropertyName(RestApiConstants.DOCUMENT_DATE)]
        public DateTime? DocumentDate { get; set; }

        [JsonPropertyName(RestApiConstants.ARCHIVED_DATE)]
        public DateTime? ArchivedDate { get; set; }

        public string Uploader { get; set; }

        public string Owner { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element