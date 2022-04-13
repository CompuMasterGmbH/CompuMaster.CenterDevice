using CenterDevice.Rest.Exceptions;
using CenterDevice.Rest.Utils;
using log4net;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.ResponseHandler
{
    public class BaseResponseHandler
    {
        private static ILog logger = LogManager.GetLogger(typeof(BaseResponseHandler));

        public virtual void ValidateResponse(RestResponse result)
        {
            if (result.ErrorException != null && result.StatusCode == HttpStatusCode.NoContent && result.ErrorException is System.Runtime.Serialization.SerializationException)
            {
                result.ErrorMessage = null;
                result.ErrorException = null;
            }
            else if (result.ErrorException != null)
            {
                if (result.ErrorException is SerializationException)
                {
                    logger.ErrorFormat("Serialization exception caught. Status code {0}, Content type {1}, Content {2}", result.StatusCode, result.ContentType, result.Content);

                    throw new InvalidResponseDataException(result.ErrorMessage, result.ErrorException);
                }

                logger.ErrorFormat("Serialization exception caught. Status code {0}, Content type {1}, Content {2}", result.StatusCode, result.ContentType, result.Content);
                throw new RestClientException(result.ErrorMessage, result.ErrorException, HeadersToString(result.Headers), result.Content);
            }
        }

        private static string HeadersToString(IReadOnlyCollection<HeaderParameter> headers)
        {
            string Result = null;
            foreach (Parameter p in headers)
            {
                Result = p.Name + ": " + p.Value + System.Environment.NewLine;
                /*
                if (Result == null)
                    Result = p.Name + ": " + p.Value;
                else
                    Result = System.Environment.NewLine + p.Name + ": " + p.Value;
                */
            }
            return Result;
        }

        protected static RestClientException CreateDefaultException(HttpStatusCode expected, RestResponse result)
        {
            return CreateDefaultException(new List<HttpStatusCode> { expected }, result);
        }

        protected static RestClientException CreateDefaultException(List<HttpStatusCode> expected, RestResponse result)
        {
            var statusCode = result?.StatusCode;
            var content = result?.Content;
            var errorException = result.ErrorException;
            return RestClientExceptionUtils.CreateDefaultException(expected, statusCode, content, errorException);
        }


    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element