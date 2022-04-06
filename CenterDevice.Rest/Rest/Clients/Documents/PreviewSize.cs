#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Documents
{
    public enum PreviewSize : int
    {
        _120 = 120,
        _240 = 240,
        _480 = 480,
        _720 = 720,
        _1080 = 1080,
        _2560 = 2560
    };

    public static class PreviewSizeExtension
    {
        public static string ToApiParameter(this PreviewSize size)
        {
            return (int)size + "x" + (int)size;
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element