Module FileOperations
    'Public Function File_DataRead(documentPath As String, delimiter As String) As (String, String, String, Integer, Integer, Integer)
    '    Dim data1 As String = String.Empty
    '    Dim data2 As String = String.Empty
    '    Dim data3 As String = String.Empty
    '    Dim int1 As Integer = 0
    '    Dim int2 As Integer = 0
    '    Dim int3 As Integer = 0
    '    Try
    '        ' Verifica si el archivo existe antes de intentar acceder a él
    '        If System.IO.File.Exists(documentPath) Then
    '            Using AnalizadorSintactico As New Microsoft.VisualBasic.FileIO.TextFieldParser(documentPath)
    '                AnalizadorSintactico.SetDelimiters(delimiter)
    '                While Not AnalizadorSintactico.EndOfData
    '                    Dim CAMPOS As String() = AnalizadorSintactico.ReadFields()
    '                    If CAMPOS.Length > 0 Then data1 = CAMPOS(0)
    '                    If CAMPOS.Length > 1 Then data2 = CAMPOS(1)
    '                    If CAMPOS.Length > 2 Then data3 = CAMPOS(2)
    '                End While
    '            End Using

    '            ' Convertir los datos a enteros si es posible
    '            If Integer.TryParse(data1, int1) = False Then int1 = 0
    '            If Integer.TryParse(data2, int2) = False Then int2 = 0
    '            If Integer.TryParse(data3, int3) = False Then int3 = 0

    '            'caracter = CChar(Label10.Text)
    '            'ContProdOK = Convert.ToInt16(caracter)
    '            'caracter2 = CChar(Label12.Text)
    '            'ContProdNG = Convert.ToInt16(caracter2)
    '            'CharModel = CChar(ModelItem.Text)
    '            'ModNum = Convert.ToInt16(CharModel)
    '            'ComboBox2.SelectedIndex = ModNum

    '            Return (data1, data2, data3, int1, int2, int3)
    '        Else
    '            MessageBox.Show("El archivo especificado no existe: " & documentPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Return (String.Empty, String.Empty, String.Empty, 0, 0, 0)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Ocurrió un error al leer el archivo: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return (String.Empty, String.Empty, String.Empty, 0, 0, 0)
    '    End Try
    'End Function
End Module

