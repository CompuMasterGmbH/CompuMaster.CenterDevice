using System;
using System.Runtime.Serialization;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents
{
    [Serializable]
    public class RequestedRangeNotSatisfiableException : Exception
    {
        public RequestedRangeNotSatisfiableException()
        {
        }

        public RequestedRangeNotSatisfiableException(string message) : base(message)
        {
        }

        public RequestedRangeNotSatisfiableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequestedRangeNotSatisfiableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element