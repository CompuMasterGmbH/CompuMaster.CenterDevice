using System.Text.Json.Serialization;
using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class UploadLink
    {
        [JsonPropertyName(RestApiConstants.ID)]
        public string Id { get; set; }

        [JsonPropertyName(RestApiConstants.CREATOR)]
        public string Creator { get; set; }

        [JsonPropertyName(RestApiConstants.NAME)]
        public string Name { get; set; }

        [JsonPropertyName(RestApiConstants.CREATION_DATE)]
        public DateTime? CreationDate { get; set; }

        [JsonPropertyName(RestApiConstants.EXPIRY_DATE)]
        public DateTime? ExpiryDate { get; set; }

        [JsonPropertyName(RestApiConstants.MAX_DOCUMENTS)]
        public int? MaxDocuments { get; set; }

        [JsonPropertyName(RestApiConstants.MAX_BYTES)]
        public long? MaxBytes { get; set; }

        [JsonPropertyName(RestApiConstants.COLLECTION)]
        public string Collection { get; set; }

        [JsonPropertyName(RestApiConstants.TAGS)]
        public System.Collections.Generic.List<string> Tags { get; set; }

        [JsonPropertyName(RestApiConstants.UPLOADS)]
        public int? UploadsMade { get; set; }

        [JsonPropertyName(RestApiConstants.UPLOADED_BYTES)]
        public long? UploadedBytes { get; set; }

        [JsonPropertyName(RestApiConstants.PASSWORD)]
        public string Password { get; set; }

        [JsonPropertyName(RestApiConstants.EMAIL_CREATOR_ON_UPLOAD)]
        public bool EMailCreatorOnUpload { get; set; }

        [NonSerialized]
        internal string UploadLinkBaseUrl;

        public string Web
        {
            get
            {
                if (this.UploadLinkBaseUrl == null)
                    throw new ArgumentNullException(nameof(UploadLinkBaseUrl));
                else
                    return this.UploadLinkBaseUrl + this.Id;
            }
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element