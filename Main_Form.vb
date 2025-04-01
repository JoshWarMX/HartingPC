Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports HartingPC.HartingDB3DataSetTableAdapters
Imports Microsoft.VisualBasic.FileIO
Imports S7.Net

Public Class Main_Form
    ReadOnly DATKEY As String = "c:\KeyenceFTP\imagen.txt"
    Public imagenArch As String
    Public n, i, segcomp, temp, NGtor, ModNum As Integer
    Public NumChar As Integer
    Public Varimagen As Byte()
    'Public auxif As Boolean
    Public boton, o As Boolean
    Public Scanner As String
    Public PtosCom As String()
    Public ScanCodeOK As Boolean
    Public NewRegisterDone As Boolean
    Public ScaneedCodeChange As Boolean

    ' Dim Leer_Datos As String = DATOS
    Dim rev1, rev2 As Integer
    Dim contadorOK As String
    Dim contNG As String
    Dim ModNumber As String

    'Dim Datos1 As String = DATOS
    Dim CAMPOS As String()
    Dim caracter As String
    Dim caracter2 As String
    Dim CharModel As String
    Dim DELIMITADOR As String = "#"

    Public pickeyence, picCopia, picAutomat As String

    Public plc As Plc
    Public PlcComm_OK As Boolean = False ' Indica si se ha perdido la comunicación

    'PLC Communication
    '////////////////////////////////////
    ' Read From PLC

    Dim ModelSelected As Integer
    Dim Secuence As Integer
    Dim ContProdNG As Integer
    Dim ContProdOK As Integer
    Dim JudgmentOK As Boolean ' 
    Dim JudgmentNG As Boolean ' 

    Dim Int1 As Integer
    Dim Int2 As Integer
    Dim Int3 As Integer

    'Write to PLC

    Dim ModelSelect As Integer

    '////////////////////////////////////
    Public Sub New()
        InitializeComponent()
        plc = New Plc(CpuType.S71200, "10.10.1.50", 0, 1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form3.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim nombreBaseDatos As String = DatabaseOperations.GenerateDatabaseName()
        If Not DatabaseExists(nombreBaseDatos) Then
            DatabaseOperations.CreateDatabase()
        Else
            MessageBox.Show($"La base de datos '{nombreBaseDatos}' ya existe.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub CodEscanner_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CodEscanner.KeyPress
        If e.KeyChar = Chr(13) Then
            ScaneedCodeChange = True
        End If
    End Sub


    Public Testvar As String
    Public segundos As Double
    Public seg As Date


    Private Sub ResetFuntion()
        Me.fotografia.Image = Image.FromFile(picAutomat)
        'Me.fotografia.Image.Save(ms, Me.fotografia.Image.RawFormat)
        ContProdNG = ContProdNG + 1
        NGtor = NGtor + 1
        Label12.Text = ContProdNG
        Testvar = ""
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Label10.Text = ContProdOK
        Label12.Text = ContProdNG

        If Secuence = 0 Then
            ComboBox2.Enabled = True
            fotografia.Image = Image.FromFile(picAutomat)
            NewRegisterDone = False
        Else
            ComboBox2.Enabled = False
        End If

        If (NGtor >= 2) Then
            Button3.Visible = True
        Else
            Button3.Visible = False

        End If

        If (Secuence = 0) Then
            LabelSecuencia.Text = "Coloque una caja"
            CodEscanner.Text = ""
            Testvar = ""
            ScanCodeOK = False
        End If
        If (Secuence = 1) Then
            LabelSecuencia.Text = "Presiona ambos pulsadores"
        End If
        If (Secuence = 0) Then
            If ComboBox2.SelectedIndex = 0 Then
                ModelSelect = 0
            End If
            If ComboBox2.SelectedIndex = 1 Then
                ModelSelect = 1
            End If
            If ComboBox2.SelectedIndex = 2 Then
                ModelSelect = 2
            End If
            If ComboBox2.SelectedIndex = 3 Then
                ModelSelect = 3
            End If
            If ComboBox2.SelectedIndex = 4 Then
                ModelSelect = 4
            End If
            If ComboBox2.SelectedIndex = 5 Then
                ModelSelect = 5
            End If
            If ComboBox2.SelectedIndex = 6 Then
                ModelSelect = 6
            End If
            If ComboBox2.SelectedIndex = 7 Then
                ModelSelect = 7
            End If
            If ComboBox2.SelectedIndex = 8 Then
                ModelSelect = 8
            End If
            If ComboBox2.SelectedIndex = 9 Then
                ModelSelect = 9
            End If
            If ComboBox2.SelectedIndex = 10 Then
                ModelSelect = 10
            End If
            If ComboBox2.SelectedIndex = 11 Then
                ModelSelect = 11
            End If
            If ComboBox2.SelectedIndex = 12 Then
                ModelSelect = 12
            End If
            If ComboBox2.SelectedIndex = 13 Then
                ModelSelect = 13
            End If
            If ComboBox2.SelectedIndex = 14 Then
                ModelSelect = 14
            End If
            If ComboBox2.SelectedIndex = 15 Then
                ModelSelect = 15
            End If
            ModelItem.Text = ComboBox2.SelectedIndex
            PlcOperations.WritePLC(plc, 2, "DB10.DBW0", ModelSelect)
        End If
        If (Secuence = 2) Then
            LabelSecuencia.Text = "Torquea los 6 tornillos"
        End If
        If Secuence = 2 Then
            If My.Computer.FileSystem.FileExists(pickeyence) Then
                My.Computer.FileSystem.DeleteFile(pickeyence)
            End If
            If My.Computer.FileSystem.FileExists(picCopia) Then
                My.Computer.FileSystem.DeleteFile(picCopia)
            End If
        End If
        If (Secuence = 3) Then
            LabelSecuencia.Text = "Falta torquear 5 tornillos"
        End If
        If (Secuence = 4) Then
            LabelSecuencia.Text = "Falta torquear 4 tornillos"
        End If
        If (Secuence = 5) Then
            LabelSecuencia.Text = "Falta torquear 3 tornillos"
        End If
        If (Secuence = 6) Then
            LabelSecuencia.Text = "Falta torquear 2 tornillos"
        End If
        If (Secuence = 7) Then
            LabelSecuencia.Text = "Falta torquear 1 tornillo"
        End If
        If (Secuence = 7) Then
            CodEscanner.Focus()
            CodEscanner.SelectAll()
        End If
        If (Secuence = 8) Then
            LabelSecuencia.Text = "Escanea el codigo"

        End If

        'If CodEscanner.Text.SequenceEqual("") And Secuence = 8 Then
        'System.Threading.Thread.Sleep(1300)
        'auxif = 1
        'End If

        'pul = "ok"

        Label7.Text = DateTime.Now
        seg = My.Computer.Clock.LocalTime.AddSeconds(segundos)
        Label8.Text = seg.Second

        'CodEscanner.Text.SequenceEqual("")


        If Secuence = 8 And Not CodEscanner.Text.SequenceEqual("") And ScaneedCodeChange = True Then
            NumChar = Len(CodEscanner.Text)
            ScaneedCodeChange = False
            NewRegisterDone = False
            If NumChar >= 5 Then
                ' Define the regex patterns for the two formats
                Dim pattern1 As String = "^M\d{7}-\d{3}P\d{6}\d{5}$"
                Dim pattern2 As String = "^M\d{7}-\d{3} P\d{7}\d{5}$"
                Dim pattern3 As String = "^M\d{7}-\d{3} P\d{6}\d{5}$"

                ' Check if the scanned text matches any of the patterns
                If Regex.IsMatch(CodEscanner.Text, pattern1) Or Regex.IsMatch(CodEscanner.Text, pattern2) Or Regex.IsMatch(CodEscanner.Text, pattern3) Then
                    i = 0
                    'auxif = 0
                    ScanCodeOK = True

                Else
                    CodEscanner.Text = ""
                    NumChar = 0
                    MessageBox.Show("The scanned format is incorrect.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                CodEscanner.Text = ""
                NumChar = 0
            End If
        End If

        'While Not data.Equals("ok") Or Not data.Equals("ng")
        If Secuence = 9 Then
            ScanCodeOK = False
            NewRegisterDone = False
            LabelSecuencia.Text = "Inspeccionando Foto"
            If JudgmentOK Then
                Testvar = "OK"
            ElseIf JudgmentNG Then
                Testvar = "NG"
            End If
        End If


        If Secuence = 11 Then
            ScanCodeOK = False
            If JudgmentOK Then
                Testvar = "OK"
            ElseIf JudgmentNG Then
                Testvar = "NG"
            End If

        End If

        ChDir("C:\KeyenceFTP")



        If Secuence = 11 And Not Testvar = "" Then
            Dim fs As System.IO.FileStream
            Dim adapter As New TestFusBoxTableAdapter()
            Dim dataset As New HartingDB3DataSet()
            Dim fechaActual As DateTime = DateTime.Now
            Dim ms As New MemoryStream
            ms.SetLength(0)
            ms.Position = 0

            System.Threading.Thread.Sleep(200)

            ' Specify a valid picture file path on your computer.
            ' Duplicate Picture
            My.Computer.FileSystem.CopyFile(pickeyence, picCopia, True)

            fs = New System.IO.FileStream(picCopia, IO.FileMode.Open, IO.FileAccess.Read)
            fotografia.Image = System.Drawing.Image.FromStream(fs)
            fotografia.Image.Save(ms, fotografia.Image.RawFormat)
            fs.Close()
            Varimagen = ms.GetBuffer

            ' SQL New Register
            If Not NewRegisterDone Then
                DatabaseOperations.SQL_WriteNewRegister(CodEscanner.Text, Varimagen, Testvar, fechaActual, adapter)
                DatabaseOperations.SQL_Fill(adapter, dataset, TestFusBoxDataGridView1)
                NewRegisterDone = True
            End If
        End If
        If Secuence = 12 Then
            NewRegisterDone = False
        End If

        If Secuence > 11 Then
            CodEscanner.Text = ""
        End If

        If Secuence = 13 Then
            LabelSecuencia.Text = "NO PASA gire la llave de reset para liberar la pieza."

        End If
        If Secuence = 13 Then
            NewRegisterDone = False
            Testvar = ""
            Scanner = ""
            CodEscanner.Text = ""
            System.Threading.Thread.Sleep(1000)
        End If
        If Secuence = 14 Then
            LabelSecuencia.Text = "Pieza OK retire la pieza"
            NewRegisterDone = False
            NGtor = 0
            Label10.Text = ContProdOK
        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If PlcComm_OK Then
            Dim results2 = PlcOperations.ReadPLC(plc)
            Secuence = results2.Item2
            ContProdOK = results2.Item3
            ContProdNG = results2.Item4
            JudgmentOK = results2.Item13
            JudgmentNG = results2.Item14

            Label4.Text = Secuence & ScanCodeOK & Testvar

            If ScanCodeOK Then
                PlcOperations.WritePLC(plc, 1, "DB10.DBX20.0", 1)
            End If

            If NewRegisterDone Then
                PlcOperations.WritePLC(plc, 1, "DB10.DBX20.4", 1)
            End If
        End If
    End Sub




    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim s As String = "C:\Users\HartingPC1\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\ConsultasHarting\ConsultasHarting.appref-ms"
        Dim p As New Process()
        p.StartInfo.FileName = s
        p.Start()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim adapter As New TestFusBoxTableAdapter()
        Dim dataset As New HartingDB3DataSet()

        'Style.ApplyButtonStyle(Button1)

        'TODO: esta línea de código carga datos en la tabla 'HartingDB3DataSet.TestFusBox' Puede moverla o quitarla según sea necesario.
        Scanner = ""
        n = 0
        NGtor = 0

        'DatabaseOperations.ConfigurarTableAdapter(adapter)
        DatabaseOperations.SQL_Fill(adapter, dataset, TestFusBoxDataGridView1)

        PlcOperations.ConectarPLC(plc, PlcComm_OK)

        picAutomat = "C:\KeyenceFTP\logoautomat.bmp"
        pickeyence = "C:\KeyenceFTP\imagen.bmp"
        picCopia = "C:\KeyenceFTP\imagenCopia.bmp"

        Timer1.Enabled = True
        Timer2.Enabled = True

        'Inizalization model Variables

        ModelSelect = plc.Read("DB10.DBW0.0")
        ComboBox2.SelectedIndex = ModelSelect
        ModelItem.Text = ComboBox2.SelectedIndex



    End Sub



End Class
