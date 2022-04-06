#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Model.Collection
{
    public interface CollectionBaseData
    {
        string Id { get; }
        string Name { get; }
        bool IsShared { get; }
        bool? HasFolders { get; }
        bool Archived { get; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element