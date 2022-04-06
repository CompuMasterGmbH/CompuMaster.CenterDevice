using CenterDevice.Rest.Exceptions;
using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents
{
    [Serializable]
    public class NameUnchangedException : RestClientException
    {
        public NameUnchangedException(string message) : base(message, null)
        {

        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element