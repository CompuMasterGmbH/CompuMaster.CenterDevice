Imports NUnit.Framework

<TestFixture> Public Class TeamworkBasicTest
    Inherits TestBase

    <Test> Public Sub GetTypes()
#Disable Warning BC42024 ' Nicht verwendete lokale Variable
        Dim IOClient As CompuMaster.Scopevisio.Teamwork.TeamworkIOClient
        Dim RestClient As CompuMaster.Scopevisio.CenterDeviceApi.TeamworkRestClient
        Dim OpenScopeClient As CompuMaster.Scopevisio.OpenApi.OpenScopeApiClient
#Enable Warning BC42024 ' Nicht verwendete lokale Variable
        Assert.Pass()
    End Sub

    <Test> Public Sub ServerAccessAndAuthorization()
        Assert.NotNull(IOClient.CurrentAuthenticationContextUserID)
    End Sub

    <Test> Public Sub CurrentContextUserId()
        System.Console.WriteLine(IOClient.CurrentContextUserId)
        Assert.NotNull(IOClient.CurrentContextUserId)
    End Sub

    <Test> Public Sub CurrentAuthenticationContextUserID()
        System.Console.WriteLine(IOClient.CurrentAuthenticationContextUserID)
        Assert.NotNull(IOClient.CurrentAuthenticationContextUserID)
    End Sub

    <Test> Public Sub RootDirAccess()
        System.Console.WriteLine(System.Environment.NewLine & "## Initial directory listing - flat")
        System.Console.WriteLine(IOClient.RootDirectory.ToStringListing(False, True))

        System.Console.WriteLine(System.Environment.NewLine & "## Initial directory listing - recursive - without display of files")
        System.Console.WriteLine(IOClient.RootDirectory.ToStringListing(True, False))

        System.Console.WriteLine(System.Environment.NewLine & "## Full directory listing - after GetDirectories(0)")
        IOClient.RootDirectory.GetDirectories(0, False)
        System.Console.WriteLine(IOClient.RootDirectory.ToStringListing(True, True))
    End Sub

    <Test> Public Sub RootDirectory()
        System.Console.WriteLine(IOClient.RootDirectory.ToStringListing(False, True))
        IOClient.RootDirectory.GetDirectories(0, False)
        Assert.NotZero(IOClient.RootDirectory.GetDirectories(0, False).Length)
    End Sub

End Class
