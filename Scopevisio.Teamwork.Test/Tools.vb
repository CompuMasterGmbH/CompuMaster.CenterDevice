Public NotInheritable Class Tools

    Public Shared Function NotNullOrEmptyStringValue(value As String) As String
        If value <> Nothing Then
            Return value
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function DateTimeUtcToLocalTime(value As Date?) As Date?
        If value.HasValue AndAlso Not value.Value = Nothing Then
            Return value.Value.ToLocalTime
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function DateTimeLocalToUtcTime(value As Date?) As Date?
        If value.HasValue AndAlso Not value.Value = Nothing Then
            Return value.Value.ToUniversalTime
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function ConvertNarrowingToNullableInt32(value As Long?) As Integer?
        If value.HasValue = False Then
            Return Nothing
        ElseIf value.Value > Integer.MaxValue Then
            Return Integer.MaxValue
        ElseIf value.Value < Integer.MinValue Then
            Return Integer.MinValue
        Else
            Return CType(value.Value, Integer)
        End If
    End Function

End Class
