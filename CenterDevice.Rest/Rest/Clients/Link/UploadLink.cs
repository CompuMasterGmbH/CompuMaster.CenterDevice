using RestSharp.Deserializers;
using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class UploadLink
    {
        [DeserializeAs(Name = RestApiConstants.ID)]
        public string Id { get; set; }

        [DeserializeAs(Name = RestApiConstants.CREATOR)]
        public string Creator { get; set; }

        [DeserializeAs(Name = RestApiConstants.NAME)]
        public string Name { get; set; }

        [DeserializeAs(Name = RestApiConstants.CREATION_DATE)]
        public DateTime? CreationDate { get; set; }

        [DeserializeAs(Name = RestApiConstants.EXPIRY_DATE)]
        public DateTime? ExpiryDate { get; set; }

        [DeserializeAs(Name = RestApiConstants.MAX_DOCUMENTS)]
        public int? MaxDocuments { get; set; }

        [DeserializeAs(Name = RestApiConstants.MAX_BYTES)]
        public long? MaxBytes { get; set; }

        [DeserializeAs(Name = RestApiConstants.COLLECTION)]
        public string Collection { get; set; }

        [DeserializeAs(Name = RestApiConstants.TAGS)]
        public System.Collections.Generic.List<string> Tags { get; set; }

        [DeserializeAs(Name = RestApiConstants.UPLOADS)]
        public int? UploadsMade { get; set; }

        [DeserializeAs(Name = RestApiConstants.UPLOADED_BYTES)]
        public long? UploadedBytes { get; set; }

        [DeserializeAs(Name = RestApiConstants.PASSWORD)]
        public string Password { get; set; }

        [DeserializeAs(Name = RestApiConstants.EMAIL_CREATOR_ON_UPLOAD)]
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