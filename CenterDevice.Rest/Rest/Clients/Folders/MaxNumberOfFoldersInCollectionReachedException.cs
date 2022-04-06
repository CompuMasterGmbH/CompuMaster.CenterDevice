using CenterDevice.Model.Registry;
using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Folders
{
    [Serializable]
    public class MaxNumberOfFoldersInCollectionReachedException : Exceptions.PermanentException
    {
        public MaxNumberOfFoldersInCollectionReachedException() : base() { }

        public MaxNumberOfFoldersInCollectionReachedException(string message, Exception e) : base(message, e) { }

        public override RegistryStatus GetErrorCode()
        {
            return RegistryStatus.ERROR_MAX_FOLDERS;
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element