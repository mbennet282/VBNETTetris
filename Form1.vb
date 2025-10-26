Public Class Form1

    Private board As Board = New Board()
    Private currentShape As Shape = New Shape()
    Private nextShape As Shape = New Shape()
    Private preview As Preview = New Preview()
    Private gamePaused As Boolean = False
    Private currentLevel As Integer = 1
    Private isGameOver As Boolean = False
    Private interval As Integer = 900

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        board.RenderBoard(e)
        preview.RenderPreviewBoard(e)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'refresh board when any graphics is moved
        Me.Invalidate()


        'mino movement
        Select Case e.KeyCode
            Case Keys.Space
                If isGameOver Then Exit Sub
                If Not gamePaused Then
                    gamePaused = True
                    Me.Timer1.Stop()
                Else
                    gamePaused = False
                    Me.Timer1.Start()
                End If
            Case Keys.Q
                Me.Close()
            Case Keys.Up
                If gamePaused OrElse isGameOver Then Exit Sub
                'rotate the mino
                currentShape.currentShape = currentShape.Rotate()

                If Not board.DrawRotatedMino(currentShape.currentShape) Then
                    'rollback if not fits
                    currentShape.currentShape = currentShape.Rotate()
                    currentShape.currentShape = currentShape.Rotate()
                    currentShape.currentShape = currentShape.Rotate()
                End If

            Case Keys.Down
                If gamePaused OrElse isGameOver Then Exit Sub
                'move the current shape down
                If board.MoveMinoDown() Then
                    board.LockShapes()
                    'scoring
                    While board.MarkRowForRemoval() > 0
                        Me.ScoreLabel.Text += board.MarkRowForRemoval()
                        board.ShiftRowsDown()
                    End While

                    'increase the level counter after every 20 rows removed
                    If board.rowsRemoved >= 20 Then
                        currentLevel += 1
                        Me.LevelLabel.Text = currentLevel
                        board.rowsRemoved = 0
                        Me.Timer1.Interval = interval * (11 - currentLevel) / 10
                    End If

                    currentShape = nextShape
                    nextShape = New Shape()
                    isGameOver = board.DrawShapeToBoard(currentShape.currentShape)
                    If isGameOver Then
                        Me.Timer1.Stop()
                        Exit Sub
                    End If
                    preview.ClearPreviewBoard()
                    preview.DrawShapeToPreviewBoard(nextShape.currentShape)

                End If
            Case Keys.Left
                If gamePaused OrElse isGameOver Then Exit Sub
                'move the current shape left
                board.MoveMinoLeft()

            Case Keys.Right
                If gamePaused OrElse isGameOver Then Exit Sub
                'move the current shape right
                board.MoveMinoRight()
            Case Keys.R
                'restart the game
                Me.ScoreLabel.Text = 0
                Me.LevelLabel.Text = 1
                currentLevel = 1
                board = New Board()
                currentShape = New Shape()
                nextShape = New Shape()
                preview = New Preview()
                gamePaused = False
                isGameOver = False
                board.DrawShapeToBoard(currentShape.currentShape)
                preview.DrawShapeToPreviewBoard(nextShape.currentShape)
                Me.Timer1.Interval = interval
                Me.Timer1.Start()
            Case Keys.A
                'show about page
                MsgBox("VBNET Tetris. Released in 2025")

        End Select

    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        'draw shape to canvas (board)
        board.DrawShapeToBoard(currentShape.currentShape)
        preview.DrawShapeToPreviewBoard(nextShape.currentShape)
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Invalidate()
        If gamePaused OrElse isGameOver Then Exit Sub
        'move the current shape down
        If board.MoveMinoDown() Then
            board.LockShapes()
            'scoring
            While board.MarkRowForRemoval() > 0
                Me.ScoreLabel.Text += board.MarkRowForRemoval()
                board.ShiftRowsDown()
            End While

            'increase the level counter after every 20 rows removed
            If board.rowsRemoved >= 20 Then
                currentLevel += 1
                Me.LevelLabel.Text = currentLevel
                board.rowsRemoved = 0
                Me.Timer1.Interval = interval * (11 - currentLevel) / 10
            End If
            currentShape = nextShape
            nextShape = New Shape()
            isGameOver = board.DrawShapeToBoard(currentShape.currentShape)
            If isGameOver Then
                isGameOver = True
                Exit Sub
            End If
            preview.ClearPreviewBoard()
            preview.DrawShapeToPreviewBoard(nextShape.currentShape)
        End If
    End Sub
End Class
