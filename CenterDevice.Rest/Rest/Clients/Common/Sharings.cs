using System.Text.Json.Serialization;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Common
{
    public class Sharings
    {
        public List<string> Visible { get; set; }

        [JsonPropertyName("not-visible-count")]
        public int NotVisibleCount { get; set; }

        public bool HasSharing
        {
            get
            {
                return NotVisibleCount != 0 || (Visible != null && Visible.Count > 0);
            }
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element