using CenterDevice.Rest.Exceptions;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.OAuth
{
    public class AuthorizationException : TemporaryException
    {
        public AuthorizationException(string message) : base(message) { }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element