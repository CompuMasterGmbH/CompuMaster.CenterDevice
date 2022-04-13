using RestSharp;
using System.IO;
using System.Threading;

namespace CenterDevice.Rest.Clients.Documents
{
    internal static class DocumentStreamUtils
    {
        private const int DEFAULT_COPY_BUFFER_SIZE = 81920;

        internal static void AddFileToUpload(RestRequest uploadRequest, string fileName, string filePath, IStreamWrapper streamWrapper, CancellationToken cancellationToken)
        {
            uploadRequest.AddFile(fileName, filePath);

            //System.Func<Stream> uploadStream = () => 
            //{
            //    var result = new System.IO.MemoryStream();
            //    var fileStream = WrapUploadStream(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete), streamWrapper);
            //    CopyTo(fileStream, result, cancellationToken);
            //    return (Stream)result;
            //};

            //uploadRequest.AddFile(fileName, uploadStream, Path.GetFileName(filePath));
        }

        internal static Stream WrapUploadStream(Stream stream, IStreamWrapper streamWrapper)
        {
            return streamWrapper?.WrapUploadStream(stream) ?? stream;
        }

        internal static Stream WrapDownloadStream(Stream stream, IStreamWrapper streamWrapper)
        {
            return streamWrapper?.WrapDownloadStream(stream) ?? stream;
        }

        public static void CopyTo(Stream source, Stream destination, CancellationToken cancellationToken)
        {
            var buffer = new byte[DEFAULT_COPY_BUFFER_SIZE];
            int count;
            while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                destination.Write(buffer, 0, count);
            }
        }
    }
}
