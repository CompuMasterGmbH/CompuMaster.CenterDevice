#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Model.Registry
{
    public enum IconStatus : int
    {
        SYNCED = 0x0,
        SYNCING = 0x1,
        CHANGED = 0x2,
        CONFLICT = 0x3,
        ERROR = 0x4,
        UNKNOWN = 0x5,
        NONE = 0x6,
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element