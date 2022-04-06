using CenterDevice.Rest.Clients.Collections;
using CenterDevice.Rest.Clients.Groups;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.User
{
    public class FullUserData : BaseUserData
    {
        public List<Collection> Collections { get; set; }

        public List<Group> Groups { get; set; }

    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element