Imports S7.Net

Module PlcOperations

    Public Sub ConectarPLC(ByRef Plc As Plc, ByRef Comm_OK As Boolean)
        Try
            Plc.Open()
            Comm_OK = True
        Catch ex As Exception
            Comm_OK = False
            MessageBox.Show("Ocurrió un error al intentar Conectar al PLC: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function ReadPLC(ByRef Plc As Plc) As (Integer, Integer, Integer, Integer, Integer, Integer, Integer, Integer, Integer, Integer, Boolean, Boolean, Boolean, Boolean, Boolean, Boolean, Boolean, Boolean)
        Dim int1 As Integer
        Dim int2 As Integer
        Dim int3 As Integer
        Dim int4 As Integer
        Dim int5 As Integer
        Dim int6 As Integer
        Dim int7 As Integer
        Dim int8 As Integer
        Dim int9 As Integer
        Dim int10 As Integer
        Dim bool1 As Boolean
        Dim bool2 As Boolean
        Dim bool3 As Boolean
        Dim bool4 As Boolean
        Dim bool5 As Boolean
        Dim bool6 As Boolean
        Dim bool7 As Boolean
        Dim bool8 As Boolean

        int1 = Plc.Read("DB10.DBW0.0")
        int2 = Plc.Read("DB10.DBW2.0")
        int3 = Plc.Read("DB10.DBW4.0")
        int4 = Plc.Read("DB10.DBW6.0")
        int5 = Plc.Read("DB10.DBW8.0")
        int6 = Plc.Read("DB10.DBW10.0")
        int7 = Plc.Read("DB10.DBW12.0")
        int8 = Plc.Read("DB10.DBW14.0")
        int9 = Plc.Read("DB10.DBW16.0")
        int10 = Plc.Read("DB10.DBW18.0")
        bool1 = Plc.Read("DB10.DBX20.0")
        bool2 = Plc.Read("DB10.DBX20.1")
        bool3 = Plc.Read("DB10.DBX20.2")
        bool4 = Plc.Read("DB10.DBX20.3")
        bool5 = Plc.Read("DB10.DBX20.4")
        bool6 = Plc.Read("DB10.DBX20.5")
        bool7 = Plc.Read("DB10.DBX20.6")
        bool8 = Plc.Read("DB10.DBX20.7")

        Return (int1, int2, int3, int4, int5, int6, int7, int8, int9, int10, bool1, bool2, bool3, bool4, bool5, bool6, bool7, bool8)
    End Function

    Public Sub WritePLC(Plc As Plc, type As Integer, address As String, value As String)
        If type = 1 Then
            If value = "0" Then
                Plc.Write(address, False)
            End If
            If value = "1" Then
                Plc.Write(address, True)
            End If
        End If

        If type = 2 Then
            If value IsNot "" Then
                Dim numero As Short = Integer.Parse(value)
                Plc.Write(address, numero.ConvertToUshort())
            End If
        End If

        If type = 3 Then
            If value IsNot "" Then
                Dim numero As Single = Single.Parse(value)
                Plc.Write(address, numero)
            End If
        End If
    End Sub

End Module
