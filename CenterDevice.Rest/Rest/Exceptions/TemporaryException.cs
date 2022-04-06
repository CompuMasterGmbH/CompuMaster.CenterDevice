using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Exceptions
{
    [Serializable]
    public class TemporaryException : RestClientException
    {
        public TemporaryException() : base() { }

        public TemporaryException(string message) : base(message) { }

        public TemporaryException(string message, Exception innerException) : base(message, innerException) { }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element