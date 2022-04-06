using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Exceptions
{
    [Serializable]
    public class InvalidResponseDataException : RestClientException
    {
        public InvalidResponseDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element