#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Link
{
    public interface ILinkRestClient
    {
        Link GetLink(string userId, string linkId);

        void DeleteLink(string userId, string linkId);

        void UpdateLink(string userId, string linkId, LinkAccessControl accessControl);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element