Imports System.Data.SqlClient
Imports HartingPC.HartingDB3DataSetTableAdapters

Module DatabaseOperations

    Public serverConnectionString As String = "Server=COLORINSPECT_L1\SQLEXPRESS;Integrated Security=True;Database={0};"
    Public serverConnectionString2 As String = "Server=COLORINSPECT_L1\SQLEXPRESS;Integrated Security=True;"

    ' Function to generate the database name
    Public Function GenerateDatabaseName() As String
        Dim year As Integer = DateTime.Now.Year
        Dim quarter As Integer = (DateTime.Now.Month - 1) \ 3 + 1
        Return $"Database_{year}_Q{quarter}"
    End Function

    ' Function to configure the TableAdapter with the generated databasesql
    Public Sub ConfigureTableAdapter(adapter As TestFusBoxTableAdapter)
        Try
            Dim databaseName As String = GenerateDatabaseName()
            ' Define the base connection string
            Dim connectionString As String = String.Format(serverConnectionString, databaseName)

            ' Configure the adapter's connection string
            adapter.Connection.ConnectionString = connectionString

        Catch ex As SqlException
            ' Handle SQL-specific errors
            MessageBox.Show("Error al conectar a la base de datos: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As InvalidOperationException
            ' Handle invalid operation errors
            MessageBox.Show("Operación inválida al configurar el TableAdapter: " & ex.Message, "Error de Operación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ' Handle any other type of error
            MessageBox.Show("Ocurrió un error al configurar el TableAdapter: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Function to fill the data adapter with the last 10 records
    Public Sub SQL_Fill(adapter As TestFusBoxTableAdapter, dataset As HartingDB3DataSet, dataGridView As DataGridView)
        ' Before binding data, define the columns
        dataGridView.Columns.Clear()
        Try
            ConfigureTableAdapter(adapter) ' Configure the adapter before using it
            If adapter.Connection IsNot Nothing AndAlso adapter.Connection.State = ConnectionState.Closed Then
                adapter.Connection.Open() ' Open the connection before using it
            End If


            ' Create a command to select the last 10 records
            Dim command As New SqlCommand("SELECT TOP 10 * FROM TestFusBox ORDER BY FechaHora DESC", adapter.Connection)
            Dim adapterSQL As New SqlDataAdapter(command)
            Dim tempDataTable As New DataTable()
            adapterSQL.Fill(tempDataTable)

            ' Bind the DataGridView to the DataTable
            dataGridView.DataSource = tempDataTable

            ' Adjust the column width to fit the content
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        Catch ex As ApplicationException
            ' Capture specific errors of the TableAdapter configuration
            MessageBox.Show("Ocurrió un error al configurar el TableAdapter: " & ex.Message & vbCrLf & "Detalles: " & ex.InnerException?.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ' Capture general errors
            MessageBox.Show("Ocurrió un error al intentar cargar los datos de la tabla: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure to close the connection
            If adapter.Connection IsNot Nothing AndAlso adapter.Connection.State = ConnectionState.Open Then
                adapter.Connection.Close()
            End If
        End Try
    End Sub

    ' Function to write a new record to the database
    Public Sub SQL_WriteNewRegister(CodScanner As String, VarImage As Byte(), TestVar As String, LabelText As String, adapter As TestFusBoxTableAdapter)
        Try
            ConfigureTableAdapter(adapter) ' Configure the adapter before using it
            adapter.Connection.Open() ' Open the connection before using it
            ' Attempt to write a new record
            adapter.NEW_Register(CodScanner, VarImage, TestVar, LabelText)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error al intentar escribir el nuevo registro: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If adapter.Connection.State = ConnectionState.Open Then
                adapter.Connection.Close() ' Ensure to close the connection
            End If
        End Try
    End Sub

    ' Function to create databases and tables
    Public Sub CreateDatabase()
        Dim databaseName As String = GenerateDatabaseName()
        Dim createDatabaseQuery As String = $"CREATE DATABASE {databaseName}"
        Dim createTableQuery As String = "
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TestFusBox')
            BEGIN
                CREATE TABLE TestFusBox (
                    CodScanner VARCHAR(50),
                    Imagen IMAGE,
                    Test CHAR(10),
                    FechaHora DATETIME
                );
            END"

        Try
            Using connection As New SqlConnection(serverConnectionString2)
                connection.Open()
                Using command As New SqlCommand(createDatabaseQuery, connection)
                    command.ExecuteNonQuery()
                End Using
                ' Switch to the newly created database
                connection.ChangeDatabase(databaseName)
                Using command As New SqlCommand(createTableQuery, connection)
                    command.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show($"Base de datos '{databaseName}' y tabla 'TestFusBox' creadas exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error al crear la base de datos o la tabla: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function DatabaseExists(databaseName As String) As Boolean
        Dim query As String = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{databaseName}'"
        Dim exists As Boolean = False
        Try
            Using connection As New SqlConnection(serverConnectionString2)
                connection.Open()
                Using command As New SqlCommand(query, connection)
                    exists = Convert.ToInt32(command.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error al verificar la existencia de la base de datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return exists
    End Function

End Module
