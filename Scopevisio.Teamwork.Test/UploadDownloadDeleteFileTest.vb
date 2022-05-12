Imports NUnit.Framework

<TestFixture, NonParallelizable> Public Class UploadDownloadDeleteFileTest
    Inherits TestBase

    Private Const RemoteTestDirPath As String = "ZZZ_UnitTests"
    Private Const RemoteFileName As String = "UnitTest_TestFile.tmp"

    <SetUp> Public Sub InitTests()
        CleanupTestData()
        If Me.IOClient.RootDirectory.DirectoryExists(RemoteTestDirPath) <> True Then
            Me.IOClient.RootDirectory.CreateDirectory(RemoteTestDirPath, CenterDevice.IO.DirectoryInfo.DirectoryType.Collection)
        End If
    End Sub

    <TearDown> Public Sub CleanupTestData()
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        Dim OpenedCleanupDir As CenterDevice.IO.DirectoryInfo
        BaseTestPath = Me.IOClient.RootDirectory
        If BaseTestPath.DirectoryExists(RemoteTestDirPath) Then
            OpenedCleanupDir = BaseTestPath.OpenDirectoryPath(RemoteTestDirPath)
            If (OpenedCleanupDir.FileExists(RemoteFileName)) Then
                'Clean-up
                CleanupRemoteFile(OpenedCleanupDir, RemoteFileName, False)
            End If
            OpenedCleanupDir.Delete()
        End If
    End Sub

    Protected Function TestFileForUploadTests() As String
        Dim LocalFilePath As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Me.TestAssembly.Location), "TestFile.txt")
        If System.IO.File.Exists(LocalFilePath) = False Then Throw New System.IO.FileNotFoundException("File not found: " & LocalFilePath)
        Return LocalFilePath
    End Function

    <Test> Public Sub UploadDownload()
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        Dim OpenTestPath As String
        Dim OpenedTestDir As CenterDevice.IO.DirectoryInfo
        Dim OpenedTestFile As CenterDevice.IO.FileInfo
        Dim TransferTestFileName As String
        BaseTestPath = Me.IOClient.RootDirectory
        OpenTestPath = RemoteTestDirPath

        TransferTestFileName = RemoteFileName
        BaseTestPath = IOClient.RootDirectory
        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        Dim LocalSourceFile As String = Me.TestFileForUploadTests
        If (OpenedTestDir.FileExists(TransferTestFileName)) Then
            Assert.Fail()
        End If

        'Upload
        System.Console.Write("Upload . . .")
        OpenedTestDir.Upload(LocalSourceFile, TransferTestFileName, CenterDevice.IO.DirectoryInfo.UploadMode.CreateNewVersionOrNewFile)
        System.Console.WriteLine(" DONE!")
        If (OpenedTestDir.FileExists(TransferTestFileName)) Then
            System.Console.WriteLine(Indent("File exists: " + OpenedTestDir.GetFile(TransferTestFileName).ToStringDetails()))
        Else
            System.Console.WriteLine(Indent("File doesn't exist: " + TransferTestFileName))
            Assert.Fail()
        End If
        If OpenedTestDir.GetFile(TransferTestFileName).Size = (New System.IO.FileInfo(LocalSourceFile)).Length Then
            System.Console.WriteLine("File length comparison: equal => SUCCESS!")
        Else
            System.Console.WriteLine("File length comparison: not equal (" + (New System.IO.FileInfo(LocalSourceFile).Length).ToString() + " vs. " + OpenedTestDir.GetFile(TransferTestFileName).Size.ToString() + ") => FAILURE!")
            Assert.Fail()
        End If

        'Re-Download and compare
        System.Console.Write("Re-Download . . .")
        OpenedTestFile = OpenedTestDir.OpenFilePath(TransferTestFileName)
        OpenedTestDir.ResetFilesCache()
        OpenedTestFile = OpenedTestDir.GetFile(TransferTestFileName)
        Dim tmpFile As String = System.IO.Path.GetTempFileName()
        OpenedTestFile.Download(tmpFile)
        System.Console.WriteLine(" => """ + tmpFile + """ DONE!")
        If OpenedTestDir.GetFile(TransferTestFileName).Size = (New System.IO.FileInfo(tmpFile)).Length Then
            System.Console.WriteLine("File length comparison: equal => SUCCESS!")
        Else
            System.Console.WriteLine("File length comparison: not equal (" + (New System.IO.FileInfo(tmpFile).Length).ToString() + " vs. " + OpenedTestDir.GetFile(TransferTestFileName).Size.ToString() + ") => FAILURE!")
            Assert.Fail()
        End If
        If (System.Linq.Enumerable.SequenceEqual(System.IO.File.ReadAllBytes(LocalSourceFile), System.IO.File.ReadAllBytes(tmpFile))) Then
            System.Console.WriteLine("File comparison: equal => SUCCESS!")
        Else
            System.Console.WriteLine("File comparison: not equal => FAILURE!")
            'System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(tmpFile))
            Assert.Fail()
        End If

        'Delete uploaded file again
        System.Console.WriteLine("Deleting . . .")
        Assert.True(CleanupRemoteFile(OpenedTestDir, TransferTestFileName, True))
        System.Console.WriteLine(Indent("DONE!"))
    End Sub

    ''' <summary>
    ''' Delete a remote file
    ''' </summary>
    ''' <param name="openedDirectory"></param>
    ''' <param name="remoteFileName"></param>
    ''' <returns>Success status</returns>
    Private Function CleanupRemoteFile(openedDirectory As CenterDevice.IO.DirectoryInfo, remoteFileName As String, failureOnMissingFile As Boolean) As Boolean
        If failureOnMissingFile = False AndAlso openedDirectory.FileExists(remoteFileName) = False Then
            'nothing to delete -> shortcut exit if file not existing and not required to exist
            Return True
        ElseIf failureOnMissingFile = True AndAlso openedDirectory.FileExists(remoteFileName) = False Then
            Throw New CenterDevice.Model.Exceptions.FileNotFoundException(remoteFileName)
        Else
            openedDirectory.ResetFilesCache()
            If openedDirectory.FileExists(remoteFileName) = True Then
                'file exists and must be deleted
                Dim RemoteFile As CenterDevice.IO.FileInfo
                RemoteFile = openedDirectory.GetFile(remoteFileName)
                If RemoteFile.HasCollidingDuplicateFile Then Assert.Fail("Duplicate files found for " & RemoteFile.FullName)
                RemoteFile.Delete()
                If openedDirectory.FileExists(remoteFileName) Then
                    System.Console.WriteLine(Indent("ERROR: File still exists: " + openedDirectory.GetFile(remoteFileName).ToStringDetails()))
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End If

    End Function

End Class
