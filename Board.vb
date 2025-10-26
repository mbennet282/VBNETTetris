Public Class Board
    Inherits List(Of List(Of Integer))
    Private Const LEFT As Integer = 1
    Private Const RIGHT As Integer = 2
    Private Const DOWN As Integer = 3
    Private currX As Integer = 3
    Private currXMax As Integer = 0
    Private currY As Integer = 0
    Private currYMax As Integer = 0
    Private _rowsRemoved As Integer = 0
    Private overlayLeft As Boolean = False
    Private overlayRight As Boolean = False


    Public Property rowsRemoved As Integer
        Get
            Return _rowsRemoved
        End Get
        Set(ByVal value As Integer)
            _rowsRemoved = value
        End Set
    End Property

    'defined mino types
    Private Const minoL As Integer = 1
    Private Const minoJ As Integer = 2
    Private Const minoO As Integer = 3
    Private Const minoT As Integer = 4
    Private Const minoS As Integer = 5
    Private Const minoZ As Integer = 6
    Private Const minoI As Integer = 7

    'defined mino types (placed)
    Private Const minoLPlaced As Integer = 101
    Private Const minoJPlaced As Integer = 102
    Private Const minoOPlaced As Integer = 103
    Private Const minoTPlaced As Integer = 104
    Private Const minoSPlaced As Integer = 105
    Private Const minoZPlaced As Integer = 106
    Private Const minoIPlaced As Integer = 107

    Private rect As List(Of List(Of Rectangle)) = Nothing
    Sub New()
        'create the game board
        For i As Integer = 1 To 20
            Me.Add(New List(Of Integer))
            For j As Integer = 1 To 10
                Me(i - 1).Add(0)
            Next
        Next
    End Sub

    Sub RenderBoard(e As PaintEventArgs)
        'draw the game board
        e.Graphics.FillRectangle(Brushes.Black, 10, 10, 250, 500)

        'render the game board
        rect = New List(Of List(Of Rectangle))
        For i As Integer = 0 To Me.Count - 1

            rect.Add(New List(Of Rectangle))
            For j As Integer = 0 To Me(i).Count - 1
                rect(i).Add(New Rectangle(10 + (25 * j), 10 + (25 * i), 25, 25))
                If Me(i)(j) <> 0 Then e.Graphics.FillRectangle(Me.ChooseColor(Me(i)(j)), rect(i)(j))
            Next
        Next
    End Sub


    Function MoveMinoDown() As Boolean
        If HasTouchedAnotherBlock(DOWN) Then Return True
        For i As Integer = Me.Count - 1 To 0 Step -1
            For j As Integer = Me(i).Count - 1 To 0 Step -1
                'only move the mino if the number is between
                '1 and 7 which are block IDs (i.e., 1 - L, 2 - J, 3 - O etc)
                If Me(i)(j) >= 1 AndAlso Me(i)(j) <= 7 Then
                    If i = Me.Count - 1 Then
                        'return if the shape has touched the bottom
                        If Me(i)(j) >= 1 AndAlso Me(i)(j) <= 7 Then
                            Return True
                        End If
                    Else
                        'move the shape down the board
                        Me(i + 1)(j) = Me(i)(j)
                        Me(i)(j) = 0
                    End If
                End If
            Next
        Next
        currY += 1
        Return False
    End Function

    Sub MoveMinoLeft()
        'If currX = 0 Then Exit Sub
        If HasTouchedAnotherBlock(LEFT) Then Exit Sub
        For i As Integer = Me.Count - 1 To 0 Step -1
            For j As Integer = 0 To Me(i).Count - 1 Step 1
                If Me(i)(j) >= 1 AndAlso Me(i)(j) <= 7 Then
                    'don't do anything if the mino is on the far left
                    If j = 0 Then
                        Exit Sub
                    Else
                        'some shapes have overlay, meaning that
                        'the invisible blocks may prevent the shape
                        'from moving to the far left
                        If overlayLeft = True OrElse currX > 0 Then
                            Me(i)(j - 1) = Me(i)(j)
                            Me(i)(j) = 0
                        Else
                            Exit Sub
                        End If
                    End If
                End If
            Next
        Next
        currX -= 1
    End Sub

    Sub MoveMinoRight()
        If HasTouchedAnotherBlock(RIGHT) Then Exit Sub
        For i As Integer = Me.Count - 1 To 0 Step -1
            For j As Integer = Me(i).Count - 1 To 0 Step -1
                If Me(i)(j) >= 1 AndAlso Me(i)(j) <= 7 Then
                    If j = Me(i).Count - 1 Then
                        Exit Sub
                    Else
                        'some shapes have overlay, meaning that
                        'the invisible blocks may prevent the shape
                        'from moving to the far right
                        If overlayRight = True OrElse currX < 10 - currXMax Then
                            Me(i)(j + 1) = Me(i)(j)
                            Me(i)(j) = 0
                        Else
                            Exit Sub
                        End If

                    End If
                End If
            Next
        Next
        currX += 1
    End Sub

    Function DrawShapeToBoard(shape As List(Of List(Of Integer))) As Boolean
        currXMax = 0
        currYMax = 0
        Dim l As Integer = 0
        Dim r As Integer = 0
        For i As Integer = 0 To shape.Count - 1
            For j As Integer = 0 To shape(i).Count - 1
                If Me(i + currY)(j + currX) > 0 Then Return True
                Me(i + currY)(j + currX) = shape(i)(j)

                'determine overlays

                If j = 0 Then
                    If Me(i + currY)(j + currX) = 0 Then l += 1
                End If

                If j = shape(i).Count - 1 Then
                    If Me(i + currY)(j + currX) = 0 Then r += 1
                End If

                If l = shape.Count Then
                    overlayLeft = True
                Else
                    overlayLeft = False
                End If

                If r = shape.Count Then
                    overlayRight = True
                Else
                    overlayRight = False
                End If


                If shape(i)(j) >= 1 AndAlso shape(i)(j) <= 7 Then
                    If currXMax <> shape.Count Then currXMax += 1
                    If currYMax <> shape.Count Then currYMax += 1
                End If
            Next
        Next
        Return False
    End Function

    'color the shapes
    Protected Function ChooseColor(num As Integer) As Brush
        Dim brush As Brush = Nothing
        Select Case num
            Case minoL
                brush = Brushes.Blue
            Case minoJ
                brush = Brushes.Red
            Case minoO
                brush = Brushes.Green
            Case minoT
                brush = Brushes.White
            Case minoS
                brush = Brushes.Purple
            Case minoZ
                brush = Brushes.Magenta
            Case minoI
                brush = Brushes.Yellow
            Case minoLPlaced
                brush = Brushes.Blue
            Case minoJPlaced
                brush = Brushes.Red
            Case minoOPlaced
                brush = Brushes.Green
            Case minoTPlaced
                brush = Brushes.White
            Case minoSPlaced
                brush = Brushes.Purple
            Case minoZPlaced
                brush = Brushes.Magenta
            Case minoIPlaced
                brush = Brushes.Yellow
            Case Else
                brush = Brushes.Black
        End Select
        Return brush
    End Function

    Function DrawRotatedMino(shape As List(Of List(Of Integer))) As Boolean
        'don't do anything if there is insufficient space for the mino to rotate,
        'the shape should be rotated three another three times for the shape to be
        'rotated back before attempting to rotate when this occurs
        If CheckForCollisionWithOtherMinos(shape) Then Return False
        If currX < 0 OrElse currX > 10 - currXMax OrElse currY > 20 - currYMax Then Return False
        currXMax = 0
        currYMax = 0
        Dim l As Integer = 0
        Dim r As Integer = 0
        For i As Integer = 0 To shape.Count - 1
            For j As Integer = 0 To shape(i).Count - 1
                Me(i + currY)(j + currX) = 0
                Me(i + currY)(j + currX) = shape(i)(j)

                'determine overlays
                If j = 0 Then
                    If Me(i + currY)(j + currX) = 0 Then l += 1
                End If

                If j = shape(i).Count - 1 Then
                    If Me(i + currY)(j + currX) = 0 Then r += 1
                End If

                If shape(i)(j) >= 1 AndAlso shape(i)(j) <= 7 Then
                    If currXMax <> shape.Count Then currXMax += 1
                    If currYMax <> shape.Count Then currYMax += 1
                End If

            Next
        Next
        If l = shape.Count Then
            overlayLeft = True
        Else
            overlayLeft = False
        End If

        If r = shape.Count Then
            overlayRight = True
        Else
            overlayRight = False
        End If

        Return True
    End Function

    'after the mino has touched the bottom, add 100 to all the minos
    'to identity that the mino has landed and proceed with the next mino
    Sub LockShapes()
        For i As Integer = Me.Count - 1 To 0 Step -1
            For j As Integer = Me(i).Count - 1 To 0 Step -1
                If Me(i)(j) >= 1 AndAlso Me(i)(j) <= 7 Then
                    Me(i)(j) += 100
                End If
            Next
        Next
        'reset the x y values
        currX = 3
        currY = 0
    End Sub

    'check for collisions
    Function HasTouchedAnotherBlock(direction As Integer) As Boolean
        Select Case direction
            Case DOWN
                For i As Integer = Me.Count - 1 To 0 Step -1
                    For j As Integer = 0 To Me(i).Count - 1 Step 1
                        If Me(i)(j) >= 1 AndAlso Me(i)(j) <= 7 Then
                            If i < Me.Count - 1 Then
                                If Me(i + 1)(j) > 100 Then Return True
                            End If
                        End If
                    Next
                Next
            Case LEFT
                For i As Integer = Me.Count - 1 To 0 Step -1
                    For j As Integer = 0 To Me(i).Count - 1 Step 1
                        If Me(i)(j) >= 1 AndAlso Me(i)(j) <= 7 Then
                            If j > 0 Then
                                If Me(i)(j - 1) > 100 Then Return True
                            End If
                        End If
                    Next
                Next
            Case RIGHT
                For i As Integer = Me.Count - 1 To 0 Step -1
                    For j As Integer = Me(i).Count - 1 To 0 Step -1
                        If Me(i)(j) >= 1 AndAlso Me(i)(j) <= 7 Then
                            If j < Me(i).Count - 1 Then
                                If Me(i)(j + 1) > 100 Then Return True
                            End If
                        End If
                    Next
                Next
        End Select
        Return False
    End Function

    'determine if the rotation collides with another mino
    Function CheckForCollisionWithOtherMinos(shape As List(Of List(Of Integer))) As Boolean
        If currX < 0 OrElse currX > 10 - currXMax OrElse currY > 20 - currYMax Then Return True
        For i As Integer = 0 To shape.Count - 1
            For j As Integer = 0 To shape(i).Count - 1
                If Me(i + currY)(j + currX) > 100 Then Return True
            Next
        Next
        Return False
    End Function

    'determine if any rows are full
    Function CheckForFullRow(row As Integer) As Boolean
        Dim count As Integer = 0
        For i As Integer = 0 To Me(row).Count - 1
            If Me(row)(i) <> 0 Then count += 1
        Next
        If count = 10 Then Return True
        Return False
    End Function

    'mark the row for removal by setting the full row column values to -1
    'alongside determining the score
    Function MarkRowForRemoval() As Integer
        Dim score As Integer = 0
        For i As Integer = 0 To Me.Count - 1
            If CheckForFullRow(i) Then
                For j As Integer = 0 To Me(i).Count - 1
                    Me(i)(j) = -1
                Next
                System.Media.SystemSounds.Beep.Play()
                score += 100 * (i + 1) / 2
                Me._rowsRemoved += 1
            End If
        Next

        Return score
    End Function

    Sub ShiftRowsDown()
        For i As Integer = Me.Count - 1 To 0 Step -1
            For j As Integer = Me(i).Count - 1 To 0 Step -1
                If Me(i)(j) = -1 Then
                    Me(i)(j) = Me(i - 1)(j)
                    If RemainderBlocks(i - 1) Then Me(i - 1)(j) = -1
                End If
            Next
        Next

    End Sub

    Function RemainderBlocks(row As Integer) As Boolean
        For i As Integer = 0 To Me(row).Count - 1
            If Me(row)(i) <> 0 Then Return True
        Next
        Return False
    End Function

End Class
