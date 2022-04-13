Option Explicit On
Option Strict On

Imports NUnit.Framework

<TestFixture, NonParallelizable> Public Class DirectoriesBasicTest
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

End Class
