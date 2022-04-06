using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.IO
{
    public class PathTransformations
    {
        public PathTransformations()
        {
        }

        /// <summary>
        /// Get the parent directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetDirectoryName(string path)
        {
            return this.CombinePath(this.GetDirectoryName(this.SplitPath(path)));
        }

        /// <summary>
        /// Get the parent directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
#pragma warning disable CA1822 // Mark members as static
        public string[] GetDirectoryName(string[] path)
#pragma warning restore CA1822 // Mark members as static
        {
            var Paths = new List<string>(path);
            Paths.RemoveAt(Paths.Count - 1);
            return Paths.ToArray();
        }

        /// <summary>
        /// Get the file name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetFileName(string path)
        {
            return this.GetFileName(this.SplitPath(path));
        }

        /// <summary>
        /// Get the file name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
#pragma warning disable CA1822 // Mark members as static
        public string GetFileName(string[] path)
#pragma warning restore CA1822 // Mark members as static
        {
            return path[path.Length - 1];
        }

        /// <summary>
        /// Split up a path into its directory names
        /// </summary>
        /// <param name="path"></param>
        /// <returns>A string array of directory names</returns>
        public string[] SplitPath(string path)
        {
            var PathDirs = new List<string>();
            if (path.StartsWith(this.DirectorySeparatorChar.ToString()) || path.StartsWith(this.AltDirectorySeparatorChar.ToString()))
            {
                PathDirs.Add(this.DirectorySeparatorChar.ToString());
                if (!string.IsNullOrEmpty(path.Substring(1)))
                    PathDirs.AddRange(path.Substring(1).Split(this.DirectorySeparatorChar, this.AltDirectorySeparatorChar));
            }
            else
                PathDirs.AddRange(path.Split(this.DirectorySeparatorChar, this.AltDirectorySeparatorChar));
            return PathDirs.ToArray();
        }

        /// <summary>
        /// Combine directory names and file name to a path
        /// </summary>
        /// <param name="directoryNames"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string CombinePath(string[] directoryNames, string fileName)
        {
#pragma warning disable IDE0028 // Initialisierung der Sammlung vereinfachen
            var combinedPaths = new List<string>(directoryNames);
#pragma warning restore IDE0028 // Initialisierung der Sammlung vereinfachen
            combinedPaths.Add(fileName);
            return this.CombinePath(combinedPaths.ToArray());
        }

        /// <summary>
        /// Combine directory names to a path
        /// </summary>
        /// <param name="directoryNames"></param>
        /// <returns></returns>
        public string CombinePath(string[] directoryNames)
        {
            var Path = new System.Text.StringBuilder();
            foreach (string pathPart in directoryNames)
            {
                if (Path.Length == 0)
                    //First path part
                    if ((pathPart == this.DirectorySeparatorChar.ToString()) || (pathPart == this.AltDirectorySeparatorChar.ToString()))
                        Path.Append(pathPart);
                    else
                        Path.Append(this.DirectorySeparatorChar + pathPart);
                else if ((Path.Length == 1) && (Path.ToString() == this.DirectorySeparatorChar.ToString()))
                    Path.Append(pathPart);
                else
                    Path.Append(this.DirectorySeparatorChar + pathPart);
            }
            return Path.ToString();
        }
        
        /// <summary>
        /// The directory separator used to split up or combine path hierarchies
        /// </summary>
        public char DirectorySeparatorChar { get; set; } = '/';

        /// <summary>
        /// An additional directory separator used to split up path hierarchies
        /// </summary>
        public char AltDirectorySeparatorChar { get; set; } = '\\';

        internal protected const string DIRECTORY_NAME_CURRENT = ".";
        internal protected const string DIRECTORY_NAME_PARENT = "..";
#pragma warning disable CA1819 // Properties should not return arrays
        public char[] InvalidPathChars { get; set; } = System.IO.Path.GetInvalidPathChars();
        public char[] InvalidFileNameChars { get; set; } = System.IO.Path.GetInvalidFileNameChars();
        public string[] InvalidPathNames { get; set; } = new string[] {DIRECTORY_NAME_CURRENT, DIRECTORY_NAME_PARENT};
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element