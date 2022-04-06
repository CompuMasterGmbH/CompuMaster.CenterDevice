using System.IO;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest
{
    /// <summary>
    /// The common interface for a stream wrapper, allowing custom implementations for download/upload transfer limit
    /// </summary>
    public interface IStreamWrapper
    {
        Stream WrapDownloadStream(Stream stream);

        Stream WrapUploadStream(Stream stream);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element