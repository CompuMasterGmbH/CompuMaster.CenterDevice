using CenterDevice.Rest.Exceptions;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients
{
    public class OfflineModeSimulator
    {
        public static bool SimulateOfflineMode { get; set; } = false;

        public void ThrowIfOffline()
        {
            if (SimulateOfflineMode)
            {
                throw new NotConnectedException("Simulating offline mode", null);
            }
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element