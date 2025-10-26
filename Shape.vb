Public Class Shape
    Private _currentShape As List(Of List(Of Integer))
    Private _nextShape As List(Of List(Of Integer))
    Public Property currentShape As List(Of List(Of Integer))
        Get
            Return _currentShape
        End Get
        Set(ByVal value As List(Of List(Of Integer)))
            _currentShape = value
        End Set
    End Property

    Public Property nextShape As List(Of List(Of Integer))
        Get
            Return _nextShape
        End Get
        Set(ByVal value As List(Of List(Of Integer)))
            _nextShape = value
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

    Protected Friend Shared minoType As Integer = 0


    Sub New()
        'determine the current shape
        Me._currentShape = New List(Of List(Of Integer))
        Randomize()
        minoType = Int((7 - 1 + 1) * Rnd() + 1)
        Select Case minoType
            Case minoL
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))

                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(1)

                Me._currentShape(1).Add(1)
                Me._currentShape(1).Add(1)
                Me._currentShape(1).Add(1)

                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(0) 'L
            Case minoJ
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))

                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(0)

                Me._currentShape(1).Add(2)
                Me._currentShape(1).Add(2)
                Me._currentShape(1).Add(2)

                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(2) 'J
            Case minoO
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))

                Me._currentShape(0).Add(3)
                Me._currentShape(0).Add(3)

                Me._currentShape(1).Add(3)
                Me._currentShape(1).Add(3) 'O

            Case minoT

                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))

                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(4)
                Me._currentShape(0).Add(0)

                Me._currentShape(1).Add(4)
                Me._currentShape(1).Add(4)
                Me._currentShape(1).Add(4)

                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(0) 'T
            Case minoS

                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))

                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(0)

                Me._currentShape(1).Add(0)
                Me._currentShape(1).Add(5)
                Me._currentShape(1).Add(5)

                Me._currentShape(2).Add(5)
                Me._currentShape(2).Add(5)
                Me._currentShape(2).Add(0) 'S
            Case minoZ

                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))

                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(0)

                Me._currentShape(1).Add(6)
                Me._currentShape(1).Add(6)
                Me._currentShape(1).Add(0)

                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(6)
                Me._currentShape(2).Add(6) 'Z
            Case minoI

                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))
                Me._currentShape.Add(New List(Of Integer))

                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(7)
                Me._currentShape(0).Add(0)
                Me._currentShape(0).Add(0)

                Me._currentShape(1).Add(0)
                Me._currentShape(1).Add(7)
                Me._currentShape(1).Add(0)
                Me._currentShape(1).Add(0)

                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(7)
                Me._currentShape(2).Add(0)
                Me._currentShape(2).Add(0)

                Me._currentShape(3).Add(0)
                Me._currentShape(3).Add(7)
                Me._currentShape(3).Add(0)
                Me._currentShape(3).Add(0)
        End Select

        'determine the next shape
        'Me._nextShape = New List(Of List(Of Integer))
        'Randomize()
        ''minoType = Int((6 - 1 + 1) * Rnd() + 1)
        'minoType = 1
        'Select Case minoType
        '    Case minoL
        '        Me._nextShape.Add(New List(Of Integer))
        '        Me._nextShape.Add(New List(Of Integer))
        '        Me._nextShape.Add(New List(Of Integer))

        '        Me._nextShape(0).Add(0)
        '        Me._nextShape(0).Add(0)
        '        Me._nextShape(0).Add(1)

        '        Me._nextShape(1).Add(1)
        '        Me._nextShape(1).Add(1)
        '        Me._nextShape(1).Add(1)

        '        Me._nextShape(2).Add(0)
        '        Me._nextShape(2).Add(0)
        '        Me._nextShape(2).Add(0) 'L
        '    Case minoJ
        '    Case minoO
        '    Case minoT
        '    Case minoS
        '    Case minoZ
        '    Case minoI
        'End Select
    End Sub


    Function Rotate() As List(Of List(Of Integer))
        Dim temp As List(Of List(Of Integer)) = New List(Of List(Of Integer))
        For i As Integer = 0 To Me._currentShape.Count - 1
            temp.Add(New List(Of Integer))
            For j As Integer = 0 To Me._currentShape(i).Count - 1
                temp(i).Add(Me._currentShape(Me._currentShape(i).Count - 1 - j)(i))
            Next
        Next
        Return temp
    End Function


End Class
