using System;
using System.Collections.Generic;
using System.Text;

namespace CenterDevice.Model.Exceptions
{
    /// <summary>
    /// Thrown when a required file can't be found
    /// </summary>
    public class FileNotFoundException : System.Exception
    {
        /// <summary>
        /// Thrown when a required file can't be found
        /// </summary>
        /// <param name="remotePath"></param>
        public FileNotFoundException(string remotePath) : base("File not found: " + remotePath)
        {
            this.RemotePath = remotePath;
        }

        /// <summary>
        /// Thrown when a required file can't be found
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="innerException"></param>
        public FileNotFoundException(string remotePath, Exception innerException) : base("File not found: " + remotePath, innerException)
        {
            this.RemotePath = remotePath;
        }

        /// <summary>
        /// The path of the missing file
        /// </summary>
        public string RemotePath { get; set; }
    }
}
