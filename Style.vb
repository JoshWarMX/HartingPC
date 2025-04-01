Module Style
    Public Sub ApplyButtonStyle(ByRef button As Button)
        ' Cambiar el color de fondo
        button.BackColor = Color.LightBlue

        ' Cambiar el color del texto
        button.ForeColor = Color.DarkBlue

        ' Cambiar el estilo de la fuente
        button.Font = New Font("Arial", 12, FontStyle.Bold Or FontStyle.Italic)

        ' Cambiar el tamaño del botón
        button.Size = New Size(150, 50)

        ' Cambiar el borde del botón y redondear esquinas
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 2
        button.FlatAppearance.BorderColor = Color.DarkBlue
        button.FlatAppearance.BorderSize = 2
        button.FlatAppearance.BorderColor = Color.DarkBlue
        'modify DataGridViewTextBoxColumn bgColor

        ' Cambiar el estilo del cursor al pasar sobre el botón
        button.Cursor = Cursors.Hand

    End Sub
End Module
