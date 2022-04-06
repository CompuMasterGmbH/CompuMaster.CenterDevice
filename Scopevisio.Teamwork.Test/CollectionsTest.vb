Imports NUnit.Framework

<TestFixture> Public Class CollectionsTest
    Inherits TestBase

    <Test> Public Sub GetCollections()
        System.Console.WriteLine(IOClient.TeamworkRestClient.Collections.GetCollections(IOClient.CurrentContextUserId))
        Assert.NotNull(IOClient.TeamworkRestClient.Collections.GetCollections(IOClient.CurrentContextUserId))
        Assert.NotNull(IOClient.TeamworkRestClient.Collections.GetCollections(IOClient.CurrentContextUserId).Collections)
        Assert.NotZero(IOClient.TeamworkRestClient.Collections.GetCollections(IOClient.CurrentContextUserId).Collections.Count)
    End Sub

    <Test> Public Sub GetCollectionIds()
        Dim CollectionIds As IEnumerable(Of String) = IOClient.TeamworkRestClient.Collections.GetCollectionIds(IOClient.CurrentContextUserId, True)
        Assert.IsNotNull(CollectionIds)
        Assert.NotZero(CollectionIds.Count)
    End Sub

End Class
