using CenterDevice.Rest.Clients;
using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Exceptions
{
    [Serializable]
    public class BadRequestException : RestClientException
    {
        public BadRequestException() : base() { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }

        public BadRequestException(string message, Exception innerException, DefaultErrorResponse defaultErrorResponse) : base(message, innerException, defaultErrorResponse)
        {
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element