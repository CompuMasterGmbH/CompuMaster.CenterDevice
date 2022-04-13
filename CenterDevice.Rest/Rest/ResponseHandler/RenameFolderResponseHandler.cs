using CenterDevice.Rest.Exceptions;
using CenterDevice.Rest.Utils;
using RestSharp;
using System.Net;

namespace CenterDevice.Rest.ResponseHandler
{
    class RenameFolderResponseHandler : BaseResponseHandler
    {

        public override void ValidateResponse(RestResponse result)
        {
            if (result.StatusCode != HttpStatusCode.OK)
            {
                var errorResponse = RestClientExceptionUtils.GetDefaultErrorResponse(result?.Content);

                if (result.StatusCode == HttpStatusCode.Forbidden
                    && errorResponse?.Code == (int)ReturnCode.INVALID_STATE)
                {
                    throw new FolderNameAlreadyExistsException();
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest
                    && errorResponse?.Code == (int)ReturnCode.INVALID_REQUEST_DATA)
                {
                    throw new InvalidFolderNameException();
                }

                throw CreateDefaultException(HttpStatusCode.OK, result);
            }

            base.ValidateResponse(result);
        }
    }
}
