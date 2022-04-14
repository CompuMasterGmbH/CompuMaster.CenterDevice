Option Explicit On
Option Strict On

Imports NUnit.Framework

<TestFixture, NonParallelizable> Public Class DirectoriesExtendedTest
    Inherits TestBase

    Private Const RemoteTestDirPath As String = "ZZZ_UnitTests_CenterDevice"

    <OneTimeSetUp> Public Sub InitTests()
        CleanupTestData()
        'Create required test directories
        Dim RequiredDirPath As String
        RequiredDirPath = RemoteTestDirPath & "/Test/Summen- und Saldenliste/2020"
        If Me.IOClient.RootDirectory.DirectoryExists(RequiredDirPath) <> True Then
            Me.IOClient.RootDirectory.CreateDirectoryStructure(RequiredDirPath)
        End If
        'Create required test file
        Dim RequiredFileName As String
        RequiredDirPath = RemoteTestDirPath & "/Test/Summen- und Saldenliste/2020"
        RequiredFileName = "test.txt.txt"
        Dim Dir As CenterDevice.IO.DirectoryInfo
        Dir = Me.IOClient.RootDirectory.OpenDirectoryPath(RequiredDirPath)
        If Dir.FileExists(RequiredFileName) = False Then
            Dir.UploadAndCreateNewFile(TestFileForUploadTests, RequiredFileName)
        End If
    End Sub

    <OneTimeTearDown> Public Sub CleanupTestData()
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        Dim OpenedCleanupDir As CenterDevice.IO.DirectoryInfo
        BaseTestPath = Me.IOClient.RootDirectory
        If BaseTestPath.DirectoryExists(RemoteTestDirPath) Then
            OpenedCleanupDir = BaseTestPath.OpenDirectoryPath(RemoteTestDirPath)
            OpenedCleanupDir.Delete()
        End If
    End Sub

    Protected Function TestFileForUploadTests() As String
        Dim LocalFilePath As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Me.TestAssembly.Location), "TestFile.txt")
        If System.IO.File.Exists(LocalFilePath) = False Then Throw New System.IO.FileNotFoundException("File not found: " & LocalFilePath)
        Return LocalFilePath
    End Function

    <Test> Public Sub RootDirBrowsing()

        Me.IOClient.RootDirectory.ResetDirectoriesCache()
        Me.IOClient.RootDirectory.ResetFilesCache()

        System.Console.WriteLine(System.Environment.NewLine & "## Initial directory listing - flat")
        System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(False, True))
        Assert.AreEqual("/", Me.IOClient.RootDirectory.ToStringListing(False, True))

        System.Console.WriteLine(System.Environment.NewLine & "## Initial directory listing - recursive - without display of files")
        System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, False))
        Assert.AreEqual("/" & System.Environment.NewLine & "    [Dirs:?]/", Me.IOClient.RootDirectory.ToStringListing(True, False))

        System.Console.WriteLine(System.Environment.NewLine & "## Full directory listing - after GetDirectories(0)")
        Me.IOClient.RootDirectory.GetDirectories(0, False)
        Assert.NotZero(Me.IOClient.RootDirectory.GetDirectories(0, False).Length)

        If False Then 'Currenlty DEACTIVATED for performance reasons
            System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, True))
            System.Console.WriteLine(System.Environment.NewLine & "## Full directory listing - after GetDirectories(1)")

            Me.IOClient.RootDirectory.GetDirectories(1, True)
            System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, True))

            System.Console.WriteLine(System.Environment.NewLine & "## Full directory listing - after GetDirectories(2)")
            Me.IOClient.RootDirectory.GetDirectories(2, True)

            System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, True))
            System.Console.WriteLine(System.Environment.NewLine & "## Full directory listing - after GetDirectories(10)")

            Me.IOClient.RootDirectory.GetDirectories(10, True)
            System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, True))
        End If
    End Sub

    <Test> Public Sub NavigateAndOpenSpecificPaths()
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        Dim OpenTestPath As String
        Dim OpenedTestDir As CenterDevice.IO.DirectoryInfo
        Dim OpenedTestFile As CenterDevice.IO.FileInfo
        BaseTestPath = Me.IOClient.RootDirectory

        OpenTestPath = "/"
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - " & OpenTestPath & " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = ""
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - """ + OpenTestPath + """" + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = "."
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = RemoteTestDirPath & "/Test/Summen- und Saldenliste\2020/"
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = RemoteTestDirPath & "/Test/Summen- und Saldenliste\2020/test.txt.txt"
        System.Console.WriteLine(System.Environment.NewLine & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)

        BaseTestPath = OpenedTestDir
        OpenTestPath = "test.txt.txt"
        System.Console.WriteLine(System.Environment.NewLine & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)

        OpenTestPath = "/"
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = ""
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - """ + OpenTestPath + """" + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = "."
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = ".."
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = "test.txt.txt"
        System.Console.WriteLine(System.Environment.NewLine & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)

        OpenTestPath = "../test.txt.txt"
        System.Console.WriteLine(System.Environment.NewLine & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        Assert.Catch(Of System.IO.FileNotFoundException)(Sub()
                                                             OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
                                                             System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)
                                                         End Sub)

        OpenTestPath = "/" & RemoteTestDirPath & "/Test/test.txt.txt"
        System.Console.WriteLine(System.Environment.NewLine & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        Assert.Catch(Of System.IO.FileNotFoundException)(Sub()
                                                             OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
                                                             System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)
                                                         End Sub)

        OpenTestPath = "/test.txt.txt"
        System.Console.WriteLine(System.Environment.NewLine & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        Assert.Catch(Of System.IO.FileNotFoundException)(Sub()
                                                             OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
                                                             System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)
                                                         End Sub)
    End Sub

    <Test> Public Sub FileDetails()
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        Dim OpenTestPath As String
        Dim OpenedTestDir As CenterDevice.IO.DirectoryInfo
        Dim OpenedTestFile As CenterDevice.IO.FileInfo
        BaseTestPath = Me.IOClient.RootDirectory

        OpenTestPath = RemoteTestDirPath & "/Test/Summen- und Saldenliste\2020"
        System.Console.WriteLine(System.Environment.NewLine & "## Directory listing for path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        OpenedTestDir.GetDirectories(10, True)
        System.Console.WriteLine(OpenedTestDir.ToStringListing(True, True))
        System.Console.WriteLine(System.Environment.NewLine & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        System.Console.WriteLine(OpenedTestDir.ToStringDetails())

        BaseTestPath = IOClient.RootDirectory
        OpenTestPath = RemoteTestDirPath & "/Test/Summen- und Saldenliste\2020/test.txt.txt"
        System.Console.WriteLine(System.Environment.NewLine & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine(OpenedTestFile.ToStringDetails())
        Assert.AreEqual("/" & RemoteTestDirPath & "/Test/Summen- und Saldenliste/2020/test.txt.txt", OpenedTestFile.FullName)
    End Sub

    <Test> Public Sub OpenDirectory()
        Const ExistingDir As String = RemoteTestDirPath & "/Test/Summen- und Saldenliste\2020"
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        Dim OpenedTestDir As CenterDevice.IO.DirectoryInfo
        BaseTestPath = Me.IOClient.RootDirectory

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(ExistingDir)
        Assert.AreEqual("/" & ExistingDir.Replace("\", "/"), OpenedTestDir.FullName)

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(ExistingDir & "/")
        Assert.AreEqual("/" & ExistingDir.Replace("\", "/"), OpenedTestDir.FullName)

        OpenedTestDir = BaseTestPath.OpenDirectoryPath("/" & ExistingDir) 'test root with leading path separator "/"
        Assert.AreEqual("/" & ExistingDir.Replace("\", "/"), OpenedTestDir.FullName)

        Assert.Catch(Of Exception)(Sub()
                                       OpenedTestDir = BaseTestPath.OpenDirectoryPath(ExistingDir & "DIR-NOT-EXISTING")
                                   End Sub)
    End Sub

    Private Sub CreateRemoteTestFolderIfNotExisting(remotePath As String)
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        BaseTestPath = Me.IOClient.RootDirectory

        'Lookup status (and fill caches)
        BaseTestPath.DirectoryExists(remotePath)

        'Create the folder
        If BaseTestPath.DirectoryExists(remotePath) = False Then
            BaseTestPath.CreateDirectory(remotePath)
        End If

        'Lookup status again and check that old caches don't exist
        Assert.IsTrue(BaseTestPath.DirectoryExists(remotePath))
    End Sub

    Private Sub RemoveRemoteTestFolder(remotePath As String)
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        BaseTestPath = Me.IOClient.RootDirectory

        'Lookup status (and fill caches)
        Assert.IsTrue(BaseTestPath.DirectoryExists(remotePath))

        'Delete the folder
        BaseTestPath.OpenDirectoryPath(remotePath).Delete()

        'Lookup status again and check that old caches don't exist
        Assert.IsFalse(BaseTestPath.DirectoryExists(remotePath))

    End Sub

    <Test> Public Sub CreateCollectionOrFolderAndCleanup()
        Const RemoteTestFolderName As String = "ZZZ_UnitTest_CenterDevice_TempDir"
        Me.CreateRemoteTestFolderIfNotExisting(RemoteTestFolderName)
        Me.RemoveRemoteTestFolder(RemoteTestFolderName)
    End Sub

End Class
