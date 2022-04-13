using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Collections
{
    public class CreateCollectionResponse
    {
        public string Id { get; set; }

        [JsonPropertyName("failed-documents")]
        public List<string> FailedDocuments { get; set; }

        [JsonPropertyName("failed-users")]
        public List<string> FailedUsers { get; set; }

        [JsonPropertyName("failed-groups")]
        public List<string> FailedGroups { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element