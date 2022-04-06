using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Exceptions
{
    public class NotAcceptableException : RestClientException
    {
        public NotAcceptableException() : base() { }

        public NotAcceptableException(string message) : base(message) { }

        public NotAcceptableException(string message, Exception cause) : base(message, cause) { }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element