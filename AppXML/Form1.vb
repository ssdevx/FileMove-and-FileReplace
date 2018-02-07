Imports System.IO
Public Class Form1
    Dim sucursal As String
    Dim anio As Integer
    Dim mes As Integer
    Dim dia As Integer
    Public cadena As String

    Private Sub btnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargar.Click
        cargarXML()
    End Sub

    Public Sub cargarXML()
        ListView1.Clear()
        sucursal = Trim(cboSucursal.Text)
        anio = calendario.Value.Year
        mes = calendario.Value.Month
        dia = calendario.Value.Day


        'Dim carpeta As New DirectoryInfo("C:\Users\Usuario\Desktop\Comprobantes\SIA\" & sucursal & "\" & anio & "\" & mes & "\" & dia)
        'Dim carpeta As New DirectoryInfo("C:\Users\Usuario\Desktop\Comprobantes\SIA\Merida\2017\12\30")
        Dim carpeta As New DirectoryInfo("X:\Silver\Comprobantes\SIA\Merida\2017\12\30")
        cadena = carpeta.ToString & "\"

        ListView1.View = View.Details
        ListView1.FullRowSelect = True
        ListView1.Columns.Add("Nombre", 140)
        ListView1.Columns.Add("Fecha de modificación", 150)
        Try
            For Each archivo As FileInfo In carpeta.GetFiles()
                Dim subElemento As ListViewItem = ListView1.Items.Add(archivo.Name)
                subElemento.SubItems.Add(archivo.LastWriteTime)
            Next
            lblTotal.Text = ListView1.Items.Count.ToString
        Catch ex As Exception
            MsgBox("Error: " & Chr(13) & ex.Message & Chr(13) & "VERIFIQUE QUE EXISTA EL DIRECTORIO", MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim archivo As String

        Dim directorio As String = "C:\Users\Usuario\Desktop\Comprobantes1"


        'File.Copy()
        'MsgBox(cadena & archivo)
        Try

            'File.Move(cadena & archivo, directorio & "\" & archivo)
            'For Each elementoSelecionado In ListView1.SelectedItems
            '    archivo = ListView1.SelectedItems(0).Text
            '    MsgBox(archivo)
            'Next
            Dim item As ListViewItem

            For Each item In ListView1.Items
                If item.Selected Then
                    archivo = item.Text
                    MsgBox(archivo)
                End If

            Next

            'MsgBox("Archivo movido correctamente.", MsgBoxStyle.Critical)
            'cargarXML()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ' MsgBox(archivo)
    End Sub
End Class
