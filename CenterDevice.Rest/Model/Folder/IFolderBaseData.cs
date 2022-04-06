#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Model.Folder
{
    public interface IFolderBaseData
    {
        string Id { get; set; }
        string Name { get; set; }
        string Collection { get; set; }
        bool IsShared { get; }
        bool? HasSubFolders { get; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element