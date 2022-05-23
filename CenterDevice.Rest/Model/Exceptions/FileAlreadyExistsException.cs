using System;
using System.Collections.Generic;
using System.Text;

namespace CenterDevice.Model.Exceptions
{
    /// <summary>
    /// Thrown when a required file already exists
    /// </summary>
    public class FileAlreadyExistsException : System.Exception
    {
        /// <summary>
        /// Thrown when a required file already exists
        /// </summary>
        /// <param name="remotePath"></param>
        public FileAlreadyExistsException(string remotePath) : base("File already existing: " + remotePath)
        {
            this.RemotePath = remotePath;
        }

        /// <summary>
        /// Thrown when a required file already exists
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="innerException"></param>
        public FileAlreadyExistsException(string remotePath, Exception innerException) : base("File already existing: " + remotePath, innerException)
        {
            this.RemotePath = remotePath;
        }

        /// <summary>
        /// The path of the missing file
        /// </summary>
        public string RemotePath { get; set; }
    }
}
