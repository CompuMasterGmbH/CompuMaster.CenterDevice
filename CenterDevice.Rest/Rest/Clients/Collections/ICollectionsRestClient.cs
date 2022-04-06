using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Collections
{
    public interface ICollectionsRestClient
    {
        CollectionsResults GetCollections(string userId);

        CollectionsResults GetCollections(string userId, IEnumerable<string> ids, bool includeHasFolders);

        IEnumerable<string> GetCollectionIds(string userId, bool includePublic);

        CreateCollectionResponse CreateCollection(string userId, string collectionName);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element