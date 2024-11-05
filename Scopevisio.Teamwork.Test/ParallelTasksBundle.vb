Imports System.Threading.Tasks
Imports System.Collections.Generic

Public Class ParallelTasksBundle
    Private ReadOnly tasks As New List(Of Task)()

    ' Fügt einen Task zur Liste hinzu
    Public Sub Add(task As Task)
        tasks.Add(task)
    End Sub

    Public Function Add(Of T)(task As Task(Of T)) As Task(Of T)
        tasks.Add(task)
        Return task
    End Function

    ' Wartet auf die Fertigstellung aller Tasks in der Liste
    Public Sub WaitAll()
        Task.WaitAll(tasks.ToArray())
    End Sub
End Class
