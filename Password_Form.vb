Option Explicit On
Imports Microsoft.VisualBasic.FileIO
Imports S7.Net


Public Class Form3
    Dim DATOS As String = "c:\KeyenceFTP\DATOS.txt"
    Dim Leer_Datos As String = DATOS
    Dim CAMPOS As String()
    Dim DELIMITADOR As String = "#"
    Dim rev1, rev2 As Integer
    Dim contadorOK As String
    Dim contNG As String
    Dim PtoArduino As String
    Dim PtoScanner As String
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Using AnalizadorSintactico As New TextFieldParser(Leer_Datos)
        '    AnalizadorSintactico.SetDelimiters(DELIMITADOR)
        '    While Not AnalizadorSintactico.EndOfData

        '        CAMPOS = AnalizadorSintactico.ReadFields()

        '        PtoArduino = CAMPOS(0)
        '        PtoScanner = CAMPOS(1)
        '        contadorOK = CAMPOS(2)
        '        contNG = CAMPOS(3)
        '    End While
        'End Using
        TextBox1.Text = ""

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "HartingPass" Then
            contadorOK = "0"
            contNG = "0"
            My.Computer.FileSystem.WriteAllText(DATOS, PtoArduino & "#" & PtoScanner & "#" & contadorOK & "#" & contNG & "#", False)
            Main_Form.Label10.Text = "0"
            Main_Form.Label12.Text = "0"
            Main_Form.Show()
            Me.Close()
            PlcOperations.WritePLC(Main_Form.plc, 1, "DB10.DBX20.5", 1)
        Else
            MsgBox("La contraseña no es valida")
            TextBox1.Text = ""
        End If



    End Sub
End Class