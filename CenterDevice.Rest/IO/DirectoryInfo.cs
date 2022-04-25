using System;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.IO
{
    public class DirectoryInfo
    {
        /// <summary>
        /// Create a directory instance representing the root directory
        /// </summary>
        /// <param name="client"></param>
        public DirectoryInfo(CenterDevice.IO.IOClientBase client)
        {
            this.ioClient = client;
            this.parentDirectory = null;
            this.restCollection = null;
            this.restFolder = null;
        }

        /// <summary>
        /// Create a directory instance representing a CenterDevice collection (=folder level below root level)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="parent"></param>
        /// <param name="collection"></param>
        public DirectoryInfo(CenterDevice.IO.IOClientBase client, CenterDevice.IO.DirectoryInfo parent, CenterDevice.Rest.Clients.Collections.Collection collection)
        {
            this.ioClient = client;
            this.parentDirectory = parent;
            this.restCollection = collection;
            this.restFolder = null;
        }

        /// <summary>
        /// Create a directory instance representing a CenterDevice folder
        /// </summary>
        /// <param name="client"></param>
        /// <param name="parent"></param>
        /// <param name="folder"></param>
        public DirectoryInfo(CenterDevice.IO.IOClientBase client, CenterDevice.IO.DirectoryInfo parent, CenterDevice.Rest.Clients.Folders.Folder folder)
        {
            this.ioClient = client;
            this.parentDirectory = parent;
            this.restFolder = folder;
            this.restCollection = null;
        }

        protected readonly CenterDevice.IO.IOClientBase ioClient;
        protected readonly CenterDevice.IO.DirectoryInfo parentDirectory;
        public CenterDevice.IO.DirectoryInfo ParentDirectory
        {
            get
            {
                return this.parentDirectory;
            }
        }
        protected readonly CenterDevice.Rest.Clients.Collections.Collection restCollection;
        protected readonly CenterDevice.Rest.Clients.Folders.Folder restFolder;

        public bool IsRootDirectory { get => ((this.parentDirectory == null) && (this.restCollection == null) && (this.restFolder == null)); }

        public void ResetFilesCache()
        {
            this.getFiles = null;
        }

        public void ResetDirectoriesCache()
        {
            this.getDirectories = null;
        }

        protected DirectoryInfo[] getDirectories = null;
        public DirectoryInfo[] GetDirectories()
        {
            if (this.getDirectories == null)
            {
                if (this.IsRootDirectory)
                {
                    //root directory
                    //List<CenterDevice.Rest.Clients.Collections.Collection> results = this.ioClient.ApiClient.Collections.GetCollections(this.ioClient.UserID).Collections;
                    List<CenterDevice.Rest.Clients.Collections.Collection> results = this.ioClient.LookupCollections();
                    var dirs = new List<DirectoryInfo>();
                    if (results != null)
                    {
                        foreach (Rest.Clients.Collections.Collection result in results)
                        {
                            dirs.Add(new DirectoryInfo(this.ioClient, this, result));
                        }
                        this.getDirectories = dirs.ToArray();
                    }
                }
                else if (this.restCollection != null)
                {
                    //List<Rest.Clients.Folders.Folder> results = this.ioClient.ApiClient.Folders.GetFolders(this.ioClient.UserID, this.restCollection.Id, CenterDevice.Rest.RestApiConstants.NONE, null).Folders;
                    this.ioClient.LookupCollectionFolders(this.restCollection);
                    List<Rest.Clients.Folders.Folder> results = this.restCollection.SubFolders;
                    var dirs = new List<DirectoryInfo>();
                    foreach (Rest.Clients.Folders.Folder result in results)
                    {
                        dirs.Add(new DirectoryInfo(this.ioClient, this, result));
                    }
                    this.getDirectories = dirs.ToArray();
                }
                else if (this.restFolder != null)
                {
                    //List<Rest.Clients.Folders.Folder> results = this.ioClient.ApiClient.Folders.GetFolders(this.ioClient.UserID, this.restFolder.Id, CenterDevice.Rest.RestApiConstants.NONE, null).Folders;
                    this.ioClient.LookupFolders(this.restFolder);
                    List<Rest.Clients.Folders.Folder> results = this.restFolder.SubFolders;
                    var dirs = new List<DirectoryInfo>();
                    foreach (Rest.Clients.Folders.Folder result in results)
                    {
                        dirs.Add(new DirectoryInfo(this.ioClient, this, result));
                    }
                    this.getDirectories = dirs.ToArray();
                }
                else
                    throw new System.NotImplementedException();
            }
            return this.getDirectories;
        }

        public DirectoryInfo[] GetDirectories(int recurseLevels)
        {
            return this.GetDirectories(recurseLevels, false);
        }

        public DirectoryInfo[] GetDirectories(int recurseLevels, bool browseForFiles)
        {
            if (recurseLevels >= 0)
            {
                if ((browseForFiles) && (this.getFiles == null))
                    this.GetFiles();
                this.getDirectories = this.GetDirectories();
                if (recurseLevels > 0)
                {
                    foreach (DirectoryInfo subDir in this.getDirectories)
                    {
                        subDir.GetDirectories(recurseLevels - 1, browseForFiles);
                    }
                }
                return this.getDirectories;
            }
            else
                return null;
        }

        /// <summary>
        /// Get file system information on a single directory
        /// </summary>
        /// <param name="directoryName">The name of the sub directory</param>
        /// <returns></returns>
        public DirectoryInfo GetDirectory(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName)) return this;
            if (directoryName == "/") return this.RootDirectory;
            DirectoryInfo[] AllDirs = this.GetDirectories();
            foreach (DirectoryInfo dir in AllDirs)
            {
                if (dir.Name == directoryName)
                    return dir;
            }
            throw new System.IO.DirectoryNotFoundException("Directory not found: " + directoryName);
        }

        /// <summary>
        /// Get file system information on a single directory
        /// </summary>
        /// <param name="path">A directory path represented as an array of sub folders</param>
        /// <returns></returns>
        /// <remarks>
        /// <p>The CenterDevice file system supports file and directory names with "/" or "\" which are typically not allowed on local file systems or website addresses since they are reserved characters for separating directory hierarchy levels.</p>
        /// <p>WARNING: This method will lead to exceptions when accessing directories containing these reserved characters in name at remote server because this method breaks down those names into separate dirctory hierarchy levels leading to invalid or wrong path references.</p>
        /// <p>RECOMMENDATION: Use overloaded method with path argument as string[]</p>
        /// </remarks>
        public DirectoryInfo OpenDirectoryPath(string path)
        {
            return this.OpenDirectoryPath(this.ioClient.Paths.SplitPath(path));
        }

        /// <summary>
        /// Get file system information on a single directory
        /// </summary>
        /// <param name="directoryNames">A directory path represented as an array of sub folders, whereas an element only containing DirectorySeparatorChar references the root directory</param>
        /// <returns></returns>
        public DirectoryInfo OpenDirectoryPath(string[] directoryNames)
        {
            if (directoryNames.Length == 0)
                return this;
            string NextSubDirName = directoryNames[0];
            List<string> RemainingSubDirsToOpen = new List<string>(directoryNames);
            RemainingSubDirsToOpen.RemoveAt(0);
            DirectoryInfo NextDirectory;
            if ((string.IsNullOrEmpty(NextSubDirName)) || (NextSubDirName == PathTransformations.DIRECTORY_NAME_CURRENT))
                NextDirectory = this;
            else if ((NextSubDirName == this.ioClient.Paths.DirectorySeparatorChar.ToString()) || (NextSubDirName == this.ioClient.Paths.AltDirectorySeparatorChar.ToString()))
                NextDirectory = this.RootDirectory;
            else if (NextSubDirName == PathTransformations.DIRECTORY_NAME_PARENT)
                if (parentDirectory == null)
                    throw new System.IO.DirectoryNotFoundException("There is no parent directory for " + this.FullName);
                else
                    NextDirectory = parentDirectory;
            else
                NextDirectory = this.GetDirectory(NextSubDirName);
            return NextDirectory.OpenDirectoryPath(RemainingSubDirsToOpen.ToArray());
        }

        /// <summary>
        /// Get file system information on a single file
        /// </summary>
        /// <param name="path">A directory path represented as an array of sub folders inclusive file name</param>
        /// <returns></returns>
        /// <remarks>
        /// <p>The CenterDevice file system supports file and directory names with "/" or "\" which are typically not allowed on local file systems or website addresses since they are reserved characters for separating directory hierarchy levels.</p>
        /// <p>WARNING: This method will lead to exceptions when accessing directories containing these reserved characters in name at remote server because this method breaks down those names into separate dirctory hierarchy levels leading to invalid or wrong path references.</p>
        /// <p>RECOMMENDATION: Use overloaded method with path argument as string[]</p>
        /// </remarks>
        public FileInfo OpenFilePath(string path)
        {
            return this.OpenFilePath(this.ioClient.Paths.SplitPath(path));
        }

        /// <summary>
        /// Get file system information on a single file
        /// </summary>
        /// <param name="directoryNamesWithFileName">A directory path represented as an array of sub folders inclusive the file name as the last array element, whereas an element only containing DirectorySeparatorChar references the root directory</param>
        /// <returns></returns>
        public FileInfo OpenFilePath(string[] directoryNamesWithFileName)
        {
            if ((directoryNamesWithFileName == null) || (directoryNamesWithFileName.Length == 0))
                throw new System.ArgumentNullException(nameof(directoryNamesWithFileName));
            string fileName = directoryNamesWithFileName[directoryNamesWithFileName.Length - 1];
            List<string> SubDirsToOpen = new List<string>(directoryNamesWithFileName);
            SubDirsToOpen.RemoveAt(directoryNamesWithFileName.Length - 1);
            return this.OpenFilePath(SubDirsToOpen.ToArray(), fileName);
        }

        /// <summary>
        /// Get file system information on a single file
        /// </summary>
        /// <param name="directoryNames">A directory path represented as an array of sub folders, whereas an element only containing DirectorySeparatorChar references the root directory</param>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        public FileInfo OpenFilePath(string[] directoryNames, string fileName)
        {
            if ((directoryNames == null) || (directoryNames.Length == 0))
                return this.GetFile(fileName);
            else
                return this.OpenDirectoryPath(directoryNames).GetFile(fileName);
        }


        internal protected FileInfo[] getFiles = null;
        /// <summary>
        /// Get the directory's files
        /// </summary>
        /// <returns></returns>
        public FileInfo[] GetFiles()
        {
            if (this.getFiles == null)
            {
                if (this.IsRootDirectory)
                {
                    //root directory can't contain files, only directories (collections)
                    this.getFiles = new FileInfo[] { };
                }
                else if (this.restCollection != null)
                {
                    this.ioClient.LookupCollectionDocuments(this.restCollection);
                    var files = new List<FileInfo>();
                    foreach (Rest.Clients.Documents.Metadata.DocumentFullMetadata result in this.restCollection.Documents)
                    {
                        files.Add(new FileInfo(this.ioClient, this, result));
                    }
                    this.getFiles = files.ToArray();
                }
                else if (this.restFolder != null)
                {
                    this.ioClient.LookupDocuments(this.restFolder);
                    var files = new List<FileInfo>();
                    foreach (Rest.Clients.Documents.Metadata.DocumentFullMetadata result in this.restFolder.Documents)
                    {
                        files.Add(new FileInfo(this.ioClient, this, result));
                    }
                    this.getFiles = files.ToArray();
                }
                else
                    throw new System.NotImplementedException();
            }
            return this.getFiles;
        }

        [Obsolete("Use ContainsCollidingDuplicateFiles instead"), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool? HasCollidingDuplicateFiles => this.ContainsCollidingDuplicateFiles;

        /// <summary>
        /// Are children files existing with the very same file name?
        /// </summary>
        public bool? ContainsCollidingDuplicateFiles
        {
            get
            {
                if (this.IsRootDirectory)
                {
                    return false;
                }
                else if (this.getFiles != null)
                {
                    foreach (FileInfo file in this.getFiles)
                    {
                        if (file.HasCollidingDuplicateFile)
                            return true;
                    }
                    return false;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Are children directories existing with the very same directory name?
        /// </summary>
        public bool? ContainsCollidingDuplicateDirectories
        {
            get
            {
                if (this.getDirectories != null)
                {
                    foreach (DirectoryInfo dir in this.getDirectories)
                    {
                        if (dir.HasCollidingDuplicateDirectory)
                            return true;
                    }
                    return false;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Get a single file or if not existing then throw a FileNotFoundException
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileInfo GetFile(string fileName)
        {
            foreach (FileInfo file in this.GetFiles())
            {
                if (file.FileName == fileName)
                    return file;
            }
            throw new System.IO.FileNotFoundException("File not found", this.ioClient.Paths.CombinePath(this.Path.ToArray(), fileName));
        }

        /// <summary>
        /// Get a single file or if not existing then return null (Nothing in VisualBasic)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileInfo TryGetFile(string fileName)
        {
            foreach (FileInfo file in this.GetFiles())
            {
                if (file.FileName == fileName)
                    return file;
            }
            return null;
        }

        /// <summary>
        /// Check if a file exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FileExists(string fileName)
        {
            foreach (FileInfo file in this.GetFiles())
            {
                if (file.FileName == fileName)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check if a directory exists
        /// </summary>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        public bool DirectoryExists(string directoryName)
        {
            foreach (DirectoryInfo dir in this.GetDirectories())
            {
                if (dir.Name == directoryName)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The root directory
        /// </summary>
        protected DirectoryInfo RootDirectory
        {
            get
            {
                if (this.parentDirectory == null)
                    return this;
                else
                    return this.parentDirectory.RootDirectory;
            }
        }

        /// <summary>
        /// The collection directory (=top level directory level below root)
        /// </summary>
        /// <remarks>A collection DirectoryInfo hasn't got a parent collection any more, so it will be null.</remarks>
        protected DirectoryInfo ParentCollection
        {
            get
            {
                if ((this.parentDirectory == null) || (this.restCollection != null))
                    return null;
                else if (this.parentDirectory.restCollection != null)
                    return this.parentDirectory;
                else
                    return this.parentDirectory.ParentCollection;
            }
        }

        /// <summary>
        /// The associated collection directory (=top level directory level below root)
        /// </summary>
        public DirectoryInfo AssociatedCollection
        {
            get
            {
                if (this.restCollection != null)
                    return this;
                else
                    return this.ParentCollection;
            }
        }

        /// <summary>
        /// The name of the directory (a CenterDevice collection or folder) or null if root directory
        /// </summary>
        public string Name
        {
            get
            {
                if (this.restCollection != null)
                    return this.restCollection.Name;
                else if (this.restFolder != null)
                    return this.restFolder.Name;
                else
                    //root folder
                    return null;
            }
        }

        /// <summary>
        /// Archiving date
        /// </summary>
        /// <remarks>Folders and files inherit their property value from their parent CenterDevice collection (the DirectoryInfo[] of the top level directory structure)</remarks>
        public DateTime? ArchivedDate
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.ArchivedDate;
                else if (this.ParentCollection != null)
                    //folder directory
                    return this.ParentCollection.ArchivedDate;
                else
                    //root folder
                    return null;
            }
        }

        /// <summary>
        /// Auditing property of the collection
        /// </summary>
        /// <remarks>Folders and files inherit their property value from their parent CenterDevice collection (the DirectoryInfo[] of the top level directory structure)</remarks>
        public bool Auditing
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.Auditing;
                else if (this.ParentCollection != null)
                    //folder directory
                    return this.ParentCollection.Auditing;
                else
                    //root folder
                    return false;
            }
        }

        /// <summary>
        /// IsIntelligent property of the collection
        /// </summary>
        /// <remarks>Folders and files inherit their property value from their parent CenterDevice collection (the DirectoryInfo[] of the top level directory structure)</remarks>
        public bool IsIntelligent
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.IsIntelligent;
                else if (this.ParentCollection != null)
                    //folder directory
                    return this.ParentCollection.IsIntelligent;
                else
                    //root folder
                    return false;
            }
        }

        /// <summary>
        /// NeedToOptIn property of the collection
        /// </summary>
        /// <remarks>Folders and files inherit their property value from their parent CenterDevice collection (the DirectoryInfo[] of the top level directory structure)</remarks>
        public bool? NeedToOptIn
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.NeedToOptIn;
                else if (this.ParentCollection != null)
                    //folder directory
                    return this.ParentCollection.NeedToOptIn;
                else
                    //root folder
                    return null;
            }
        }

        /// <summary>
        /// Public property of the collection
        /// </summary>
        /// <remarks>Folders and files inherit their property value from their parent CenterDevice collection (the DirectoryInfo[] of the top level directory structure)</remarks>
        public bool Public
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.Public;
                else if (this.ParentCollection != null)
                    //folder directory
                    return this.ParentCollection.Public;
                else
                    //root folder
                    return false;
            }
        }

        /// <summary>
        /// Shared property
        /// </summary>
        public bool IsShared
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.IsShared;
                else if (this.restFolder != null)
                    //folder directory
                    return this.restFolder.IsShared;
                else
                    //root folder
                    return false;
            }
        }

        /// <summary>
        /// Owner property of the collection
        /// </summary>
        /// <remarks>Folders and files inherit their property value from their parent CenterDevice collection (the DirectoryInfo[] of the top level directory structure)</remarks>
        public string Owner
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.Owner;
                else if (this.ParentCollection != null)
                    //folder directory
                    return this.ParentCollection.Owner;
                else
                    //root folder
                    return null;
            }
        }

        /// <summary>
        /// Link property  
        /// </summary>
        public string Link
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.Link;
                else if (this.restFolder != null)
                    //folder directory
                    return this.restFolder.Link;
                else
                    //root folder
                    return null;
            }
        }

        /// <summary>
        /// Group sharings
        /// </summary>
        public CenterDevice.Rest.Clients.Common.Sharings Groups
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.Groups;
                else if (this.restFolder != null)
                    //folder directory
                    return this.restFolder.Groups;
                else
                    //root folder
                    return null;
            }
        }

        /// <summary>
        /// User sharings
        /// </summary>
        public CenterDevice.Rest.Clients.Common.Sharings Users
        {
            get
            {
                if (this.restCollection != null)
                    //collection directory
                    return this.restCollection.Users;
                else if (this.restFolder != null)
                    //folder directory
                    return this.restFolder.Users;
                else
                    //root folder
                    return null;
            }
        }

        /// <summary>
        /// Unique collection ID for low level API
        /// </summary>
        /// <seealso cref="AssociatedCollection"/>
        public string CollectionID { get => this.restCollection?.Id; }

        /// <summary>
        /// Unique folder ID for low level API
        /// </summary>
        public string FolderID { get => this.restFolder?.Id; }

        /// <summary>
        /// The full path starting from root
        /// </summary>
        public string FullName
        {
            get
            {
                return this.ioClient.Paths.CombinePath(this.Path.ToArray());
            }
        }

        /// <summary>
        /// Create a nicely printed directory overview
        /// </summary>
        /// <param name="recursive"></param>
        /// <param name="includeFiles"></param>
        /// <returns></returns>
        public string ToStringListing(bool recursive, bool includeFiles)
        {
            string result = this.Name + this.ioClient.Paths.DirectorySeparatorChar;
            if (recursive)
            {
                if (this.getDirectories != null)
                {
                    foreach (DirectoryInfo subDir in this.getDirectories)
                    {
                        result += System.Environment.NewLine + Indent(subDir.ToStringListing(recursive, includeFiles));
                    }
                }
                else
                {
                    result += System.Environment.NewLine + Indent("[Dirs:?]" + this.ioClient.Paths.DirectorySeparatorChar);
                }
                if (includeFiles)
                {
                    if (this.getFiles != null)
                    {
                        foreach (FileInfo file in this.getFiles)
                        {
                            if (file.HasCollidingDuplicateFile)
                                result += System.Environment.NewLine + Indent(file.FileName + " [v" + file.Version + ", id " + file.ID.Substring(file.ID.Length - 4) + "]");
                            else
                                result += System.Environment.NewLine + Indent(file.FileName + " [v" + file.Version + "]");
                        }
                    }
                    else
                    {
                        result += System.Environment.NewLine + Indent("[Files:?]");
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Indent a string
        /// </summary>
        private string Indent(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            else
            {
                string result = "    " + value.Replace("\n", "\n    ");
                if (result.EndsWith("\n    "))
                    result = result.Substring(0, result.Length - ("\n    ".Length - 1));
                return result;
            }
        }

        /// <summary>
        /// The full path starting from root
        /// </summary>
        public List<string> Path
        {
            get
            {
                List<string> result;
                if (this.parentDirectory == null)
                    result = new List<string>();
                else
                    result = this.parentDirectory.Path;
                result.Add(this.Name ?? this.ioClient.Paths.DirectorySeparatorChar.ToString());
                return result;
            }
        }

        /// <summary>
        /// The full name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.FullName;
        }

        /// <summary>
        /// Print details on this entry
        /// </summary>
        /// <returns></returns>
        public string ToStringDetails()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(this.FullName + " {");
            sb.AppendLine(this.Indent("Name=" + this.Name));
            sb.AppendLine(this.Indent("CollectionID=" + this.CollectionID));
            sb.AppendLine(this.Indent("FolderID=" + this.FolderID));
            sb.AppendLine(this.Indent("Shared=" + this.IsShared));
            sb.AppendLine(this.Indent("Link=" + this.Link));
            sb.AppendLine(this.Indent("ArchivedDate=" + this.ArchivedDate));
            sb.AppendLine(this.Indent("Auditing=" + this.Auditing));
            sb.AppendLine(this.Indent("IsIntelligent=" + this.IsIntelligent));
            sb.AppendLine(this.Indent("NeedToOptIn=" + this.NeedToOptIn));
            sb.AppendLine(this.Indent("Public=" + this.Public));
            sb.AppendLine(this.Indent("Owner=" + this.Owner));
            sb.AppendLine(this.Indent("ContainsCollidingDuplicateFiles=" + this.ContainsCollidingDuplicateFiles));
            sb.AppendLine(this.Indent("ContainsCollidingDuplicateDirectories=" + this.ContainsCollidingDuplicateDirectories));
            sb.Append("}");
            return sb.ToString();
        }

        public enum UploadMode : byte
        {
            /// <summary>
            /// Create a new file (an existing file with equal file name will remain as collision file)
            /// </summary>
            CreateNewFile = 1,
            /// <summary>
            /// Create a new version for an existing file
            /// </summary>
            CreateNewVersion = 2,
            /// <summary>
            /// If the file already exists create a new version, otherwise create a new file
            /// </summary>
            CreateNewVersionOrNewFile = 3,
            /// <summary>
            /// Drop an existing old file (if it exists) incl. version history completely and create a new file
            /// </summary>
            DropExistingFileAndCreateNew = 4
        }

        public void Upload(string localPath, string fileName, UploadMode uploadMode)
        {
            switch (uploadMode)
            {
                case UploadMode.CreateNewFile:
                    this.UploadAndCreateNewFile(localPath, fileName);
                    break;
                case UploadMode.CreateNewVersion:
                    this.GetFile(fileName).UploadNewVersion(localPath);
                    break;
                case UploadMode.CreateNewVersionOrNewFile:
                    if (!this.FileExists(fileName))
                        this.UploadAndCreateNewFile(localPath, fileName);
                    else
                        this.GetFile(fileName).UploadNewVersion(localPath);
                    break;
                case UploadMode.DropExistingFileAndCreateNew:
                    while (this.FileExists(fileName))
                        this.GetFile(fileName).Delete();
                    this.UploadAndCreateNewFile(localPath, fileName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(uploadMode));
            }
            this.getFiles = null; //force reload on next request
        }

        public void Upload(System.Func<System.IO.Stream> fileDataStream, string fileName, UploadMode uploadMode)
        {
            switch (uploadMode)
            {
                case UploadMode.CreateNewFile:
                    this.UploadAndCreateNewFile(fileDataStream, fileName);
                    break;
                case UploadMode.CreateNewVersion:
                    this.GetFile(fileName).UploadNewVersion(fileDataStream);
                    break;
                case UploadMode.CreateNewVersionOrNewFile:
                    if (!this.FileExists(fileName))
                        this.UploadAndCreateNewFile(fileDataStream, fileName);
                    else
                        this.GetFile(fileName).UploadNewVersion(fileDataStream);
                    break;
                case UploadMode.DropExistingFileAndCreateNew:
                    while (this.FileExists(fileName))
                        this.GetFile(fileName).Delete();
                    this.UploadAndCreateNewFile(fileDataStream, fileName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(uploadMode));
            }
            this.getFiles = null; //force reload on next request
        }

        /// <summary>
        /// Upload a file (an existing file will remain as collision file)
        /// </summary>
        /// <param name="fileDataStream"></param>
        /// <param name="fileName"></param>
        public void UploadAndCreateNewFile(System.Func<System.IO.Stream> fileDataStream, string fileName)
        {
            if (this.IsRootDirectory)
                throw new System.InvalidOperationException("Upload to root directory not allowed");
            this.ioClient.ApiClient.Documents.UploadDocument(this.ioClient.CurrentAuthenticationContextUserID, fileName, fileDataStream, this.CollectionID ?? this.ParentCollection.CollectionID, this.FolderID, System.Threading.CancellationToken.None);
            this.getFiles = null; //force reload on next request
        }

        /// <summary>
        /// Upload a file (an existing file will remain as collision file)
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="fileName"></param>
        public void UploadAndCreateNewFile(string localPath, string fileName)
        {
            if (this.IsRootDirectory)
                throw new System.InvalidOperationException("Upload to root directory not allowed");
            this.ioClient.ApiClient.Documents.UploadDocument(this.ioClient.CurrentAuthenticationContextUserID, fileName, localPath, this.CollectionID ?? this.ParentCollection.CollectionID, this.FolderID, System.Threading.CancellationToken.None);
            this.getFiles = null; //force reload on next request
        }

        /// <summary>
        /// Create a new sub directory
        /// </summary>
        /// <param name="directoryName"></param>
        public void CreateDirectory(string directoryName)
        {
            if (this.IsRootDirectory)
                this.ioClient.ApiClient.Collections.CreateCollection(this.ioClient.CurrentAuthenticationContextUserID, directoryName);
            else
                this.ioClient.ApiClient.Folders.CreateFolder(this.ioClient.CurrentAuthenticationContextUserID, directoryName, this.restCollection?.Id, this.restFolder?.Id);
            this.getDirectories = null; //force reload on next request
        }

        /// <summary>
        /// Create a new sub directory
        /// </summary>
        /// <param name="directoryPath"></param>
        public void CreateDirectoryStructure(string directoryPath)
        {
            String[] DirLevels = directoryPath.Split(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
            if (DirLevels.Length == 0) return;
            if (DirLevels[0] == "") throw new ArgumentException("Must not start with a directory separator char", nameof (directoryPath));
            DirectoryInfo SubDir = this;
            foreach (string dirName in  DirLevels)
            {
                if (dirName == "") throw new ArgumentException("Must not contain empty sub directory names", nameof(directoryPath));
                if (!SubDir.DirectoryExists(dirName)) SubDir.CreateDirectory(dirName);
                SubDir = SubDir.OpenDirectoryPath(dirName);
            }
        }

        public enum DirectoryType : byte
        {
            Folder = 1,
            Collection = 2
        }

        /// <summary>
        /// Create a new sub directory
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="style"></param>
        public void CreateDirectory(string directoryName, DirectoryType style)
        {
            if (style == DirectoryType.Collection)
                this.ioClient.ApiClient.Collections.CreateCollection(this.ioClient.CurrentAuthenticationContextUserID, directoryName);
            else
                this.ioClient.ApiClient.Folders.CreateFolder(this.ioClient.CurrentAuthenticationContextUserID, directoryName, this.restCollection?.Id, this.restFolder?.Id);
            this.getDirectories = null; //force reload on next request
        }

        public DirectoryType Type
        {
            get
            {
                if ((this.IsRootDirectory) || (!string.IsNullOrEmpty(this.CollectionID)))
                    return DirectoryType.Collection;
                else
                    return DirectoryType.Folder;
            }
        }
        /// <summary>
        /// Delete this directory
        /// </summary>
        public void Delete()
        {
            if (this.IsRootDirectory)
                throw new NotSupportedException("Removing root directory is not supported");
            else if (this.restCollection != null)
                this.ioClient.ApiClient.Collection.DeleteCollection(this.ioClient.CurrentAuthenticationContextUserID, this.restCollection.Id);
            else if (this.restFolder != null)
                this.ioClient.ApiClient.Folder.DeleteFolder(this.ioClient.CurrentAuthenticationContextUserID, this.restFolder.Id);
            else
                throw new NotImplementedException("Delecte action for this directory type hasn't been implemented, yet");
            this.ParentDirectory.getDirectories = null; //force reload on next request
        }

        /// <summary>
        /// Move a directory (support only for folder directories, no support for collection directories)
        /// </summary>
        /// <param name="targetDirectory"></param>
        public void Move(DirectoryInfo targetDirectory)
        {
            if (this.IsRootDirectory)
                throw new NotSupportedException("Moving root directory is not supported");
            else if (this.restCollection != null)
                throw new NotSupportedException("Moving a collection Directory is not supported (collections must remain collections, folders must remain folders)");
            else if (this.restFolder != null)
                if (targetDirectory.restFolder != null)
                    this.ioClient.ApiClient.Folder.MoveFolder(this.ioClient.CurrentAuthenticationContextUserID, this.restFolder.Id, targetDirectory.FolderID, targetDirectory.AssociatedCollection.restCollection.Id);
                else
                    throw new NotSupportedException("Moving a folder Directory into into a collection Directory is not supported (collections must remain collections, folder must remain folders)");
            else
                throw new NotImplementedException("Delecte action for this directory type hasn't been implemented, yet");
            this.ParentDirectory.getDirectories = null; //force reload on next request
            targetDirectory.getDirectories = null; //force reload on next request
        }

        /// <summary>
        /// Rename a directory
        /// </summary>
        /// <param name="targetName"></param>
        public void Rename(string targetName)
        {
            if (this.IsRootDirectory)
                throw new NotSupportedException("Renaming root directory is not supported");
            else if (this.restCollection != null)
                this.ioClient.ApiClient.Collection.RenameCollection(this.ioClient.CurrentAuthenticationContextUserID, this.restCollection.Id, targetName);
            else if (this.restFolder != null)
                this.ioClient.ApiClient.Folder.RenameFolder(this.ioClient.CurrentAuthenticationContextUserID, this.restFolder.Id, targetName);
            else
                throw new NotImplementedException("Rename action for this directory type hasn't been implemented, yet");
            this.ParentDirectory.getDirectories = null; //force reload on next request
        }

        /// <summary>
        /// Create a new sharing
        /// </summary>
        public void AddSharing(string[] userIDs, string[] groupIDs)
        {
            if (this.IsRootDirectory)
                throw new NotSupportedException("Sharing of root directory is not supported");
            else if (this.restCollection != null)
                this.ioClient.ApiClient.Collection.ShareCollection(this.ioClient.CurrentAuthenticationContextUserID, this.restCollection.Id, userIDs, groupIDs);
            else if (this.restFolder != null)
                this.ioClient.ApiClient.Folder.ShareFolder(this.ioClient.CurrentAuthenticationContextUserID, this.restFolder.Id, userIDs, groupIDs);
            else
                throw new NotImplementedException("Share action for this directory type hasn't been implemented, yet");
            this.ParentDirectory.getDirectories = null; //force reload on next request
        }

        /// <summary>
        /// Remove a sharing
        /// </summary>
        public void RemoveSharing(string[] userIDs, string[] groupIDs)
        {
            if (this.IsRootDirectory)
                throw new NotSupportedException("Sharing of root directory is not supported");
            else if (this.restCollection != null)
                this.ioClient.ApiClient.Collection.UnshareCollection(this.ioClient.CurrentAuthenticationContextUserID, this.restCollection.Id, userIDs, groupIDs);
            else if (this.restFolder != null)
                this.ioClient.ApiClient.Folder.UnshareFolder(this.ioClient.CurrentAuthenticationContextUserID, this.restFolder.Id, userIDs, groupIDs);
            else
                throw new NotImplementedException("Share action for this directory type hasn't been implemented, yet");
            this.ParentDirectory.getDirectories = null; //force reload on next request
        }

        public static Rest.Clients.Link.LinkAccessControl CreateLinkAccessControlItem(DateTime expiryDateUtc, string password, int maxDownloads, bool allowView, bool allowEdit, bool allowRemove, bool allowUpload)
        {
            var linkControlInfo = new Rest.Clients.Link.LinkAccessControl
            {
                ExpiryDate = expiryDateUtc,
                Password = password,
                MaxDownloads = maxDownloads,
                ViewOnly = allowView
            };
            if (allowEdit || allowRemove) throw new NotSupportedException("Only allowView or allowUpload supported");
            if (allowView == allowUpload) throw new NotSupportedException("Must be different: allowView and allowUpload");
            return linkControlInfo;
        }

        /// <summary>
        /// Create a new sharing
        /// </summary>
        public Rest.Clients.Link.LinkCreationResponse AddExternalLinkSharing(DateTime expiryDateUtc, string password, int maxDownloads, bool allowView, bool allowEdit, bool allowRemove, bool allowUpload)
        {
            var result = this.AddExternalLinkSharing(CreateLinkAccessControlItem(expiryDateUtc, password, maxDownloads, allowView, allowEdit, allowRemove, allowUpload));
            this.ParentDirectory.getDirectories = null; //force reload of directories with modified share setup
            return result;
        }

        /// <summary>
        /// Create a new sharing
        /// </summary>
        public Rest.Clients.Link.LinkCreationResponse AddExternalLinkSharing(Rest.Clients.Link.LinkAccessControl settings)
        {
            if (this.IsRootDirectory)
                throw new NotSupportedException("Sharing of root directory is not supported");
            else if (this.restCollection != null)
            {
                var result = this.ioClient.ApiClient.Links.CreateCollectionLink(this.ioClient.CurrentAuthenticationContextUserID, this.restCollection.Id, settings);
                this.ParentDirectory.getDirectories = null; //force reload of directories with modified share setup
                return result;
            }
            else if (this.restFolder != null)
            {
                var result = this.ioClient.ApiClient.Links.CreateFolderLink(this.ioClient.CurrentAuthenticationContextUserID, this.restCollection.Id, settings);
                this.ParentDirectory.getDirectories = null; //force reload of directories with modified share setup
                return result;
            }
            else
                throw new NotImplementedException("Share action for this directory type hasn't been implemented, yet");
        }

        /// <summary>
        /// Remove a sharing
        /// </summary>
        public void RemoveExternalLinkSharing(Rest.Clients.Link.LinkCreationResponse link)
        {
            this.RemoveExternalLinkSharing(link.Id);
            this.ParentDirectory.getDirectories = null; //force reload of directories with modified share setup
        }

        /// <summary>
        /// Remove a sharing
        /// </summary>
        public void RemoveExternalLinkSharing(string linkId)
        {
            this.ioClient.ApiClient.Link.DeleteLink(this.ioClient.CurrentAuthenticationContextUserID, linkId);
            this.ParentDirectory.getDirectories = null; //force reload of directories with modified share setup
        }

        /// <summary>
        /// Update a sharing
        /// </summary>
        public void UpdateExternalLinkSharing(string linkId, DateTime expiryDateUtc, string password, int maxDownloads, bool allowView, bool allowEdit, bool allowRemove, bool allowUpload)
        {
            this.UpdateExternalLinkSharing(linkId, CreateLinkAccessControlItem(expiryDateUtc, password, maxDownloads, allowView, allowEdit, allowRemove, allowUpload));
            this.ParentDirectory.getDirectories = null; //force reload of directories with modified share setup
        }

        /// <summary>
        /// Update a sharing
        /// </summary>
        public void UpdateExternalLinkSharing(string linkId, Rest.Clients.Link.LinkAccessControl settings)
        {
            this.ioClient.ApiClient.Link.UpdateLink(this.ioClient.CurrentAuthenticationContextUserID, linkId, settings);
            this.ParentDirectory.getDirectories = null; //force reload of directories with modified share setup
        }

        /// <summary>
        /// Is there a directory collision because of multiple folders or collections created with equal names?
        /// </summary>
        public bool HasCollidingDuplicateDirectory
        {
            get
            {
                foreach (DirectoryInfo dir in this.parentDirectory.GetDirectories())
                {
                    if ((dir.CollectionID != this.CollectionID || dir.FolderID != this.FolderID) && (dir.Name == this.Name))
                        return true;
                }
                return false;
            }
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element