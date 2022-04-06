using Newtonsoft.Json;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents
{
    public class DocumentSearchQuery
    {
        [JsonProperty(RestApiConstants.TEXT)]
        public string Text { get; set; }
    }

    public class DocumentSearchFolder
    {
        [JsonProperty(RestApiConstants.IDS)]
        public List<string> Ids { get; set; }

        [JsonProperty(RestApiConstants.DEEP)]
        public bool Deep { get; set; }
    }

    public class DocumentSearchNamed
    {
        [JsonProperty(RestApiConstants.NAME)]
        public string Name { get; set; }

        [JsonProperty(RestApiConstants.PARAMS)]
        public string Params { get; set; }
    }

    public class DocumentSearchFilter
    {
        [JsonProperty(RestApiConstants.IDS)]
        public List<string> Ids { get; set; }

        [JsonProperty(RestApiConstants.FILENAMES)]
        public List<string> Filenames { get; set; }

        [JsonProperty(RestApiConstants.EXTENSION)]
        public List<string> Extensions { get; set; }

        [JsonProperty(RestApiConstants.MIMETYPES)]
        public List<string> MimeTypes { get; set; }

        [JsonProperty(RestApiConstants.FOLDERS)]
        public DocumentSearchFolder Folders { get; set; }

        [JsonProperty(RestApiConstants.COLLECTIONS)]
        public List<string> Collections { get; set; }
    }

    public class DocumentSearchRequest
    {
        [JsonProperty(RestApiConstants.QUERY)]
        public DocumentSearchQuery Query { get; set; }

        [JsonProperty(RestApiConstants.FILTER)]
        public DocumentSearchFilter Filter { get; set; }

        [JsonProperty(RestApiConstants.NAMED)]
        public List<DocumentSearchNamed> Named { get; set; }

        [JsonProperty(RestApiConstants.OFFSET)]
        public int? Offset { get; set; }

        [JsonProperty(RestApiConstants.ROWS)]
        public int? Rows { get; set; }

        [JsonProperty(RestApiConstants.SORT)]
        public DocumentSortOrder Sort { get; set; }
    }

    public class DocumentSortOrder
    {
        [JsonProperty(RestApiConstants.FIELD)]
        public string Field { get; set; }

        [JsonProperty(RestApiConstants.ORDER)]
        public string Order { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element