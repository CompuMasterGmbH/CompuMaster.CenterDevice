Option Explicit On
Option Strict On

Imports NUnit.Framework

<TestFixture, NonParallelizable> Public Class DirectoriesTest
    Inherits TestBase

    <Test> Public Sub RootDirProperties()
        Assert.AreEqual(True, Me.IOClient.RootDirectory.IsRootDirectory)
        Assert.AreEqual(1, Me.IOClient.RootDirectory.Path.Count)
        Assert.AreEqual("/", Me.IOClient.RootDirectory.Path(0))
        Assert.AreEqual("/", Me.IOClient.RootDirectory.FullName)
        Assert.AreEqual(Nothing, Me.IOClient.RootDirectory.Name)
        Assert.AreEqual(Nothing, Me.IOClient.RootDirectory.FolderID)
        Assert.AreEqual(Nothing, Me.IOClient.RootDirectory.CollectionID)
        Assert.AreEqual(Nothing, Me.IOClient.RootDirectory.AssociatedCollection)
        Assert.AreEqual(Nothing, Me.IOClient.RootDirectory.ParentDirectory)
        Assert.AreEqual(False, Me.IOClient.RootDirectory.Public)
        Assert.AreEqual(CenterDevice.IO.DirectoryInfo.DirectoryType.Collection, Me.IOClient.RootDirectory.Type)
        Assert.NotZero(IOClient.RootDirectory.GetDirectories(0, False).Length)
    End Sub

    <Test> Public Sub RootDirBrowsing()

        System.Console.WriteLine(ControlChars.CrLf & "## Initial directory listing - flat")
        System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(False, True))
        Assert.AreEqual("/", Me.IOClient.RootDirectory.ToStringListing(False, True))

        System.Console.WriteLine(ControlChars.CrLf & "## Initial directory listing - recursive - without display of files")
        System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, False))
        Assert.AreEqual("/" & System.Environment.NewLine & "    [Dirs:?]/", Me.IOClient.RootDirectory.ToStringListing(True, False))

        System.Console.WriteLine(ControlChars.CrLf & "## Full directory listing - after GetDirectories(0)")
        Me.IOClient.RootDirectory.GetDirectories(0, False)
        Assert.NotZero(Me.IOClient.RootDirectory.GetDirectories(0, False).Length)

        If False Then 'Currenlty DEACTIVATED for performance reasons
            System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, True))
            System.Console.WriteLine(ControlChars.CrLf & "## Full directory listing - after GetDirectories(1)")

            Me.IOClient.RootDirectory.GetDirectories(1, True)
            System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, True))

            System.Console.WriteLine(ControlChars.CrLf & "## Full directory listing - after GetDirectories(2)")
            Me.IOClient.RootDirectory.GetDirectories(2, True)

            System.Console.WriteLine(Me.IOClient.RootDirectory.ToStringListing(True, True))
            System.Console.WriteLine(ControlChars.CrLf & "## Full directory listing - after GetDirectories(10)")

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
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - " & OpenTestPath & " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = ""
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - """ + OpenTestPath + """" + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = "."
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = "Test/Summen- und Saldenliste\2020/"
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = "Test/Summen- und Saldenliste\2020/test.txt.txt"
        System.Console.WriteLine(ControlChars.CrLf & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)

        BaseTestPath = OpenedTestDir
        OpenTestPath = "test.txt.txt"
        System.Console.WriteLine(ControlChars.CrLf & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)

        OpenTestPath = "/"
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = ""
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - """ + OpenTestPath + """" + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = "."
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = ".."
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestDir.FullName)

        OpenTestPath = "test.txt.txt"
        System.Console.WriteLine(ControlChars.CrLf & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)

        OpenTestPath = "../test.txt.txt"
        System.Console.WriteLine(ControlChars.CrLf & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)

        OpenTestPath = "/Test/test.txt.txt"
        System.Console.WriteLine(ControlChars.CrLf & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)

        OpenTestPath = "/test.txt.txt"
        System.Console.WriteLine(ControlChars.CrLf & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")

        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine("FullName=" + OpenedTestFile.FullName)
    End Sub

    <Test> Public Sub FileDetails()
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        Dim OpenTestPath As String
        Dim OpenedTestDir As CenterDevice.IO.DirectoryInfo
        Dim OpenedTestFile As CenterDevice.IO.FileInfo
        BaseTestPath = Me.IOClient.RootDirectory

        OpenTestPath = "Test/Summen- und Saldenliste\2020"
        System.Console.WriteLine(ControlChars.CrLf & "## Directory listing for path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestDir = BaseTestPath.OpenDirectoryPath(OpenTestPath)
        OpenedTestDir.GetDirectories(10, True)
        System.Console.WriteLine(OpenedTestDir.ToStringListing(True, True))
        System.Console.WriteLine(ControlChars.CrLf & "## Open directory path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        System.Console.WriteLine(OpenedTestDir.ToStringDetails())

        BaseTestPath = IOClient.RootDirectory
        OpenTestPath = "Test/Summen- und Saldenliste\2020/test.txt.txt"
        System.Console.WriteLine(ControlChars.CrLf & "## Open file path - " + OpenTestPath + " [Start: " & BaseTestPath.ToString & "]")
        OpenedTestFile = BaseTestPath.OpenFilePath(OpenTestPath)
        System.Console.WriteLine(OpenedTestFile.ToStringDetails())
    End Sub

    <Test> Public Sub OpenDirectory()
        Const ExistingDir As String = "Test/Summen- und Saldenliste\2020/"
        Dim BaseTestPath As CenterDevice.IO.DirectoryInfo
        Dim OpenedTestDir As CenterDevice.IO.DirectoryInfo
        BaseTestPath = Me.IOClient.RootDirectory

        OpenedTestDir = BaseTestPath.OpenDirectoryPath(ExistingDir)
        Assert.AreEqual(ExistingDir, OpenedTestDir.FullName)
        Assert.Catch(Of Exception)(Sub()
                                       OpenedTestDir = BaseTestPath.OpenDirectoryPath(ExistingDir & "DIR-NOT-EXISTING")
                                   End Sub)
    End Sub

End Class
