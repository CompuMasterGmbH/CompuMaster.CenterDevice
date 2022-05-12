using System;
using System.Collections.Generic;
using System.Text;

namespace CenterDevice.Model.Exceptions
{
    /// <summary>
    /// Thrown when a required directory (folder or collection) can't be found
    /// </summary>
    public class DirectoryNotFoundException : System.Exception
    {
        /// <summary>
        /// Thrown when a required directory (folder or collection) can't be found
        /// </summary>
        /// <param name="remotePath"></param>
        public DirectoryNotFoundException(string remotePath) : base("Directory not found: " + remotePath)
        {
            this.RemotePath = remotePath;
        }

        /// <summary>
        /// Thrown when a required directory (folder or collection) can't be found
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="innerException"></param>
        public DirectoryNotFoundException(string remotePath, Exception innerException) : base("Directory not found: " + remotePath, innerException)
        {
            this.RemotePath = remotePath;
        }

        /// <summary>
        /// The path of the missing directory
        /// </summary>
        public string RemotePath { get; set; }
    }
}
