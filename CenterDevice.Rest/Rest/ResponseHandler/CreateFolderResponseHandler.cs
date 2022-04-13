using CenterDevice.Rest.Clients;
using CenterDevice.Rest.Clients.Folders;
using CenterDevice.Rest.Utils;
using RestSharp;
using System.Net;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.ResponseHandler
{
    public class CreateFolderResponseHandler : BaseResponseHandler, DataResponseHandler<FolderCreationResponse>
    {
        private static readonly string EXPLANATION_KEY = "explanation";

        private static readonly string FOLDER_NOT_FOUND_MESSAGE = "not found";

        private static readonly string FOLDER_ALREADY_EXISTS_MESSAGE = "same name already exists";

        private static readonly string MAX_NUMBER_OF_FOLDERS_IN_COLLECTION_REACHED_MESSAGE = "maximum number of folders limit";

        private static readonly string MAX_DEPTH_OF_NESTED_FOLDERS_EXCEEDED_MESSAGE = "exceed the max depth";

        public FolderCreationResponse UnwrapResponse(RestResponse<FolderCreationResponse> result)
        {
            if (result.StatusCode == HttpStatusCode.Created)
            {
                return result.Data;
            }

            if (result.StatusCode == HttpStatusCode.Forbidden)
            {
                var error = RestClientExceptionUtils.GetDefaultErrorResponse(result?.Content);
                if (error != null && error.Code == 300)
                {
                    if (IsFolderNotFoundException(error))
                    {
                        throw new FolderParentNotFoundException();
                    }

                    if (IsFolderAlreadyExistsException(error))
                    {
                        throw new FolderAlreadyExistsException();
                    }

                    if (IsMaxNumberOfFoldersInCollectionReachedException(error))
                    {
                        throw new MaxNumberOfFoldersInCollectionReachedException();
                    }

                    if (IsMaxDepthOfNestedFoldersExceededException(error))
                    {
                        throw new MaxDepthOfNestedFoldersExceededException();
                    }
                }
            }

            throw CreateDefaultException(HttpStatusCode.OK, result);
        }

        private bool IsFolderNotFoundException(DefaultErrorResponse errorResponse)
        {
            return GetExplanation(errorResponse).Contains(FOLDER_NOT_FOUND_MESSAGE);
        }

        private bool IsFolderAlreadyExistsException(DefaultErrorResponse errorResponse)
        {
            return GetExplanation(errorResponse).Contains(FOLDER_ALREADY_EXISTS_MESSAGE);
        }

        private bool IsMaxNumberOfFoldersInCollectionReachedException(DefaultErrorResponse errorResponse)
        {
            return GetExplanation(errorResponse).Contains(MAX_NUMBER_OF_FOLDERS_IN_COLLECTION_REACHED_MESSAGE);
        }

        private bool IsMaxDepthOfNestedFoldersExceededException(DefaultErrorResponse errorResponse)
        {
            return GetExplanation(errorResponse).Contains(MAX_DEPTH_OF_NESTED_FOLDERS_EXCEEDED_MESSAGE);
        }

        private string GetExplanation(DefaultErrorResponse errorResponse)
        {
            var data = errorResponse.Data;
            if (data != null && data.Count > 0)
            {
                object explanation = null;
                data.TryGetValue(EXPLANATION_KEY, out explanation);
                return explanation != null ? explanation as string : string.Empty;
            }

            return string.Empty;
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element