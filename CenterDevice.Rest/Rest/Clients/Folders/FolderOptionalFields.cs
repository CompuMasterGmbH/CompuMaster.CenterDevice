using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Folders
{
    public static class FolderFields
    {
        public static readonly string[] DEFAULT = new[] {
            RestApiConstants.ID,
            RestApiConstants.NAME,
            RestApiConstants.PARENT,
            RestApiConstants.COLLECTION,
            RestApiConstants.LINK
        };

        public static readonly string[] ID = new[] {
            RestApiConstants.ID
        };

        public static string[] GetDefaultWith(params FolderOptionalFields[] optionalFields)
        {
            if (!optionalFields.Any())
            {
                return DEFAULT;
            }
            return DEFAULT.Concat(ToRestApiConstants(optionalFields)).ToArray();
        }

        private static IEnumerable<string> ToRestApiConstants(FolderOptionalFields[] optionalFields)
        {
            foreach (var field in optionalFields)
            {
                switch (field)
                {
                    case FolderOptionalFields.PATH:
                        yield return RestApiConstants.PATH;
                        break;
                    case FolderOptionalFields.HAS_SUBFOLDERS:
                        yield return RestApiConstants.HAS_SUBFOLDERS;
                        break;
                    case FolderOptionalFields.SHARINGS:
                        yield return RestApiConstants.USERS;
                        yield return RestApiConstants.GROUPS;
                        break;
                }
            }
        }
    }

    public enum FolderOptionalFields
    {
        PATH, HAS_SUBFOLDERS, SHARINGS
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element