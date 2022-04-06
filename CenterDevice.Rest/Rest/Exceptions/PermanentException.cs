using CenterDevice.Model.Registry;
using CenterDevice.Rest.Clients;
using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Exceptions
{
    [Serializable]
    public abstract class PermanentException : RestClientException, ErrorCodedException
    {
        public PermanentException() : base() { }

        public PermanentException(string message, Exception e) : base(message, e) { }

        public PermanentException(string message, Exception e, DefaultErrorResponse data) : base(message, e, data) { }

        public abstract RegistryStatus GetErrorCode();
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element