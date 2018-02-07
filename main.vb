Imports System.IO
Public Class QuitarXML
    Dim sucursal As String
    Dim anio As Integer
    Dim mes As Integer
    Dim dia As Integer
    Public cadena As String
    Dim datos As New infoPC

    ' Metodo para cargar los archivos XML en la lista.
    Public Sub cargarXML()
        lista.Items.Clear()
        sucursal = Trim(cboSucursal.Text)   ' Extrae Nombre de sucursal
        anio = calendario.Value.Year        ' Extrae Año del calendario
        mes = calendario.Value.Month        ' Extrae Mes del calendario
        dia = calendario.Value.Day          ' Extrae Dia del calendario

        ' Arma el directorio con los datos y carga todos los documentos (xml) que se encuentran en ella.
        'Dim carpeta As New DirectoryInfo("W:\Silver\Comprobantes\SIA\" & sucursal & "\" & anio & "\" & mes & "\" & dia)
        'Dim carpeta As New DirectoryInfo("C:\Users\Usuario\Desktop\Comprobantes\SIA\" & sucursal & "\" & anio & "\" & mes & "\" & dia)
        Dim carpeta As New DirectoryInfo("Y:\Comprobantes\SIA\" & sucursal & "\" & anio & "\" & mes & "\" & dia)
        'Dim carpeta As New DirectoryInfo("C:\Users\Usuario\Desktop\Comprobantes\SIA\Merida\2017\12\30")
        cadena = carpeta.ToString & "\"
        'Para cada archivo en la carpeta(directorio), almacenar nombre en la lista
        Try
            For Each archivo As FileInfo In carpeta.GetFiles()
                lista.Items.Add(archivo.Name)
            Next
            lblTotal.Text = lista.Items.Count.ToString  ' Imprimir Total de archivos encontrados.
        Catch ex As Exception
            MsgBox("Error: " & Chr(13) & ex.Message & Chr(13) & "VERIFIQUE QUE EXISTA EL DIRECTORIO", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargar.Click
        If cboSucursal.Text = "Seleccione..." Then
            MsgBox("Seleccione una sucursal", MsgBoxStyle.Critical)
        Else
            cargarXML()
        End If
    End Sub

    ' Clic boton quitar
    Private Sub btnMover_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMover.Click
        If lista.CheckedItems.Count > 0 Then
            If MessageBox.Show("¿Realmente desea quitar estos archivos del directorio?", "Quitar Archivos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                Dim archivo As String                               'Nombre de archivo con extension
                Dim archivoextraer As String                        'Nombre de archivo sin extension
                Dim base As String                                  'Nombre de archivo XXX_00000.xml
                Dim base2 As String                                 'Nombre de archivo XXX_00000_timbrado.xml
                Dim cbb As String                                   'Nombre de archivo XXX_00000_cbb.png
                Dim cbb2 As String                                  'Nombre de archivo XXX_00000_cbb.gif     
                Dim pdf As String                                   'Nombre de archivo XXX_00000_cfdi.pdg
                Dim cad As String()                                 'Array separar nombre de archivos: cad(0)= XXX, cad(1)=00000
                Dim cad2 As String                                  'Union de cad(0) & cad(1) = XXX_00000
                Dim con As Integer                                  'Contador archivos a mover
                con = 0
                                                                    'Crear directorios de salida.
                Dim directorio As String = cadena & "cdcgroup"      'Directorio principal.
                Dim dir_bas As String = cadena & "cdcgroup\base"    'Base
                Dim dir_cbb As String = cadena & "cdcgroup\cbb"     'cbb
                Dim dir_pdf As String = cadena & "cdcgroup\pdf"     'pdf

                Dim i As Integer
                For i = 0 To (lista.Items.Count - 1)
                    If lista.GetItemChecked(i) = True Then
                        archivo = lista.Items(i).ToString
                        archivoextraer = Path.GetFileNameWithoutExtension(lista.Items(i).ToString)
                        cad = archivoextraer.Split("_")
                        cad2 = cad(0).ToString & "_" & cad(1).ToString
                        base = cadena & "base\" & cad2 & ".xml"             '...\base\XXX_00000.xml
                        base2 = cadena & "base\" & cad2 & "_timbrado.xml"   '...\base\XXX_00000_timbrado.xml
                        cbb = cadena & "cbb\" & cad2 & "_cbb.png"           '...\cbb\XXX_00000_cbb.png
                        cbb2 = cadena & "cbb\" & cad2 & "_cbb.gif"          '...\cbb\XXX_00000_cbb_gif 
                        pdf = cadena & "pdf\" & archivoextraer & ".pdf"     '...\pdf\XXX_00000_cfdi.pdf

                        ' Sino existen directorios de salida, crearlos.
                        If Directory.Exists(directorio) = False Then
                            Directory.CreateDirectory(directorio)   'Directorio principal
                        End If
                        If Directory.Exists(dir_bas) = False Then
                            Directory.CreateDirectory(dir_bas)      ' Directorio base
                        End If
                        If Directory.Exists(dir_cbb) = False Then
                            Directory.CreateDirectory(dir_cbb)      ' Directorio cbb
                        End If
                        If Directory.Exists(dir_pdf) = False Then
                            Directory.CreateDirectory(dir_pdf)      ' Directorio pdf
                        End If

                        Try
                            ' Hacer la operacion:   Si el archivo no existe, se mueve, si ya existe entonces se reemplaza.
                            '   Mover archivo principal
                            If File.Exists(directorio & "\" & archivo) = False Then
                                File.Move(cadena & archivo, directorio & "\" & archivo)
                            Else
                                File.Replace(cadena & archivo, directorio & "\" & archivo, Nothing)
                            End If

                            '   Mover archivo base\XXX_0000.xml
                            If File.Exists(dir_bas & "\" & cad2 & ".xml") = False Then
                                File.Move(base, dir_bas & "\" & cad2 & ".xml")
                            Else
                                File.Replace(base, dir_bas & "\" & cad2 & ".xml", Nothing)
                            End If

                            '   Mover archivo base\XXX_0000_timbrado.xml
                            If File.Exists(dir_bas & "\" & cad2 & "_timbrado.xml") = False Then
                                File.Move(base2, dir_bas & "\" & cad2 & "_timbrado.xml")
                            Else
                                File.Replace(base2, dir_bas & "\" & cad2 & "_timbrado.xml", Nothing)
                            End If

                            '   Mover archivo cbb\XXX_0000_cbb.png
                            If File.Exists(dir_cbb & "\" & cad2 & "_cbb.png") = False Then
                                File.Move(cbb, dir_cbb & "\" & cad2 & "_cbb.png")
                            Else
                                File.Replace(cbb, dir_cbb & "\" & cad2 & "_cbb.png", Nothing)
                            End If

                            '   Mover archivo cbb\XXX_0000_cbb.gif  
                            If File.Exists(dir_cbb & "\" & cad2 & "_cbb.gif") = False Then
                                File.Move(cbb2, dir_cbb & "\" & cad2 & "_cbb.gif")
                            Else
                                File.Replace(cbb2, dir_cbb & "\" & cad2 & "_cbb.gif", Nothing)
                            End If

                            '   Mover archivo pdf\XXX_0000_cfdi.pdf
                            If File.Exists(dir_pdf & "\" & archivoextraer & ".pdf") = False Then
                                File.Move(pdf, dir_pdf & "\" & archivoextraer & ".pdf")
                            Else
                                File.Replace(pdf, dir_pdf & "\" & archivoextraer & ".pdf", Nothing)
                            End If

Linea2:
                            ' Registrar transferencia de archivo.                    
                            Dim archivoLog As String = cadena & "\cdcgroup\" & anio & "_" & mes & "_" & dia & "_" & "log.txt"
                            Dim esc As New System.IO.StreamWriter(archivoLog, True)

                            If File.Exists(directorio & "\" & archivo) Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & archivo & " a " & directorio & "\" & archivo)
                                con = con + 1
                            End If

                            If File.Exists(dir_bas & "\" & cad2 & ".xml") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "base\" & cad2 & ".xml" & " a " & dir_bas & "\" & cad2 & ".xml")
                                con = con + 1
                            End If

                            If File.Exists(dir_bas & "\" & cad2 & "_timbrado.xml") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "base\" & cad2 & "_timbrado.xml" & " a " & dir_bas & "\" & cad2 & "_timbrado.xml")
                                con = con + 1
                            End If

                            If File.Exists(dir_cbb & "\" & cad2 & "_cbb.png") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "cbb\" & cad2 & "_cbb.png" & " a " & dir_cbb & "\" & cad2 & "_cbb.png")
                                con = con + 1
                            End If

                            If File.Exists(dir_cbb & "\" & cad2 & "_cbb.gif") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "cbb\" & cad2 & "_cbb.gif" & " a " & dir_cbb & "\" & cad2 & "_cbb.gif")
                                con = con + 1
                            End If

                            If File.Exists(dir_pdf & "\" & archivoextraer & ".pdf") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "pdf\" & archivoextraer & ".pdf" & " a " & dir_pdf & "\" & archivoextraer & ".pdf")
                                con = con + 1
                            End If
                            esc.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------")
                            esc.Close()
                        Catch ex As Exception
                            ' Si entra a Catch, escribe en log txt el error y continua la ejecución.
                            Dim archivoLog As String = cadena & "\cdcgroup\" & anio & "_" & mes & "_" & dia & "_" & "log.txt"
                            Dim esc As New System.IO.StreamWriter(archivoLog, True)
                            esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & ex.Message)
                            esc.Close()
                            GoTo Linea2
                        End Try
                    End If
                Next
                If con = 0 Then
                    MsgBox(" NO se pudo quitar los archivos seleccionados.", MsgBoxStyle.Information)
                Else
                    MsgBox(con & " Archivos quitados correctamente.", MsgBoxStyle.Information)
                End If
                cargarXML()
            End If
        Else
            MsgBox("Seleccione al menos un elemento de la lista para procesar.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub QuitarXML_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub QuitarXML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Application.ProductName.ToString & " - Ver. " & Application.ProductVersion.ToString
    End Sub
End Class
