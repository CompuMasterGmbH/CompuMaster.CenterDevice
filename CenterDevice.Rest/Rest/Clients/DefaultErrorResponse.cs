using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients
{
    public class DefaultErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public string ToErrorMessage()
        {
            var message = "Got status code \"" + Code + "\" with message \"" + Message + "\"";
            if (Data != null && Data.ContainsKey("explanation"))
            {
                message += " and explanation \"" + Data["explanation"] + "\"";
            }
            return message;
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element