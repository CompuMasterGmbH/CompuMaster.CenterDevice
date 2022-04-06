using RestSharp.Deserializers;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public class Link
    {
        [DeserializeAs(Name = RestApiConstants.WEB)]
        public string Web { get; set; }

        [DeserializeAs(Name = RestApiConstants.DOWNLOAD)]
        public string Download { get; set; }

        [DeserializeAs(Name = RestApiConstants.ID)]
        public string Id { get; set; }

        [DeserializeAs(Name = RestApiConstants.REST)]
        public string Rest { get; set; }

        [DeserializeAs(Name = RestApiConstants.ACCESS_CONTROL)]
        public LinkAccessControl AccessControl { get; set; }

        [DeserializeAs(Name = RestApiConstants.ACCESS_CONTROL)]
        public string Collection { get; set; }

        [DeserializeAs(Name = RestApiConstants.VIEWS)]
        public System.Int64 Views { get; set; }

        [DeserializeAs(Name = RestApiConstants.DOWNLOADS)]
        public System.Int64 Downloads { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element