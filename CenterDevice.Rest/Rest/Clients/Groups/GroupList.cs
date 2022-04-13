using System.Text.Json.Serialization;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Groups
{
    public class GroupList
    {
        public List<Group> Groups { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element