Public Class Preview
    Inherits Board
    Private rect As List(Of List(Of Rectangle)) = Nothing
    Sub New()
        'create the preview board
        For i As Integer = 1 To 4
            Me.Add(New List(Of Integer))
            For j As Integer = 1 To 4
                Me(i - 1).Add(0)
            Next
        Next
    End Sub

    Sub RenderPreviewBoard(e As PaintEventArgs)
        'draw the preview board
        e.Graphics.FillRectangle(Brushes.Black, 270, 30, 100, 100)

        'render the game board
        rect = New List(Of List(Of Rectangle))
        For i As Integer = 0 To Me.Count - 1

            rect.Add(New List(Of Rectangle))
            For j As Integer = 0 To Me(i).Count - 1
                rect(i).Add(New Rectangle(270 + (25 * j), 30 + (25 * i), 25, 25))
                If Me(i)(j) <> 0 Then e.Graphics.FillRectangle(Me.ChooseColor(Me(i)(j)), rect(i)(j))

            Next
        Next
    End Sub

    Sub ClearPreviewBoard()
        For i As Integer = 0 To Me.Count - 1
            For j As Integer = 0 To Me(i).Count - 1
                Me(i)(j) = 0
            Next
        Next
    End Sub

    Sub DrawShapeToPreviewBoard(shape As List(Of List(Of Integer)))
        For i As Integer = 0 To shape.Count - 1
            For j As Integer = 0 To shape(i).Count - 1
                Me(i)(j) = 0
                Me(i)(j) = shape(i)(j)
            Next
        Next
    End Sub
End Class
