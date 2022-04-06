using CenterDevice.Rest.Clients.Folders;
using CenterDevice.Rest.Utils;
using RestSharp;
using System.Collections.Generic;
using System.Net;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.ResponseHandler
{
    public class GetFoldersResponseHandler : BaseResponseHandler, DataResponseHandler<FoldersResponse>
    {
        public FoldersResponse UnwrapResponse(IRestResponse<FoldersResponse> result)
        {
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
#pragma warning disable IDE0017 // Initialisierung von Objekten vereinfachen
                var foldersResponse = new FoldersResponse();
#pragma warning restore IDE0017 // Initialisierung von Objekten vereinfachen
                foldersResponse.Folders = new List<Folder>();
                return foldersResponse;
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Data;
            }

            if (RestClientExceptionUtils.GetDefaultErrorResponse(result?.Content)?.Code == (int)ReturnCode.INVALID_STATE)
            {
                throw new FolderParentNotFoundException();
            }

            throw CreateDefaultException(HttpStatusCode.OK, result);
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element