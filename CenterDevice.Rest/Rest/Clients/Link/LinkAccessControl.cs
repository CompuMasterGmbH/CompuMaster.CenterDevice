using System.Text.Json.Serialization;
using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class LinkAccessControl
    {
        [JsonPropertyName(RestApiConstants.EXPIRY_DATE)]
        public DateTime? ExpiryDate { get; set; }

        [JsonPropertyName(RestApiConstants.MAX_DOWNLOADS)]
        public int? MaxDownloads { get; set; }

        [JsonPropertyName(RestApiConstants.PASSWORD)]
        public string Password { get; set; }

        [JsonPropertyName(RestApiConstants.VIEW_ONLY)]
        public bool ViewOnly { get; set; }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + ExpiryDate?.GetHashCode() ?? 0;
            hash = (hash * 7) + MaxDownloads?.GetHashCode() ?? 0;
            hash = (hash * 7) + Password?.GetHashCode() ?? 0;
            hash = (hash * 7) + ViewOnly.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = obj as LinkAccessControl;
            return ExpiryDate == other.ExpiryDate
                && MaxDownloads == other.MaxDownloads
                && Password == other.Password
                && ViewOnly == other.ViewOnly;
        }

        public static bool operator ==(LinkAccessControl a, LinkAccessControl b)
        {
#pragma warning disable IDE0041 // "Ist NULL"-Prüfung verwenden
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }
#pragma warning restore IDE0041 // "Ist NULL"-Prüfung verwenden

            return a.Equals(b);
        }

        public static bool operator !=(LinkAccessControl a, LinkAccessControl b)
        {
            return !(a == b);
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element