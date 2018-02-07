Imports System.IO
Public Class QuitarXML
    Dim sucursal As String
    Dim anio As Integer
    Dim mes As Integer
    Dim dia As Integer
    Public cadena As String
    Dim datos As New infoPC

    ' Metodo para cargar los archivos XML en la lista.
    ' Extrae Nombre de sucuarsal
    ' Extrae Año del calendario
    ' Extrae Mes del calendario
    ' Extrae Dia del calendario
    ' Arma el directorio con los datos y carga todos los documentos (xml) que se encuentran en el directorio.
    Public Sub cargarXML()
        lista.Items.Clear()
        sucursal = Trim(cboSucursal.Text)
        anio = calendario.Value.Year
        mes = calendario.Value.Month
        dia = calendario.Value.Day

        'Dim carpeta As New DirectoryInfo("C:\Users\Usuario\Desktop\Comprobantes\SIA\" & sucursal & "\" & anio & "\" & mes & "\" & dia)
        'Dim carpeta As New DirectoryInfo("Y:\Comprobantes\SIA\" & sucursal & "\" & anio & "\" & mes & "\" & dia)


        Dim carpeta As New DirectoryInfo("X:\Silver\Comprobantes\SIA\" & sucursal & "\" & anio & "\" & mes & "\" & dia)
        '0 Dim carpeta As New DirectoryInfo("C:\Users\Usuario\Desktop\Comprobantes\SIA\Merida\2017\12\30")
        cadena = carpeta.ToString & "\"
        ' Para cada archivo en la carpeta(directorio), almacenar nombre en la lista
        Try
            For Each archivo As FileInfo In carpeta.GetFiles()
                lista.Items.Add(archivo.Name)
            Next
            ' Imprimir Total de archivos encontrados.
            lblTotal.Text = lista.Items.Count.ToString
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


    Private Sub btnMover_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMover.Click
        If lista.CheckedItems.Count > 0 Then
            If MessageBox.Show("¿Realmente desea quitar estos archivos del directorio?", "Quitar Archivos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                Dim archivo As String
                Dim archivoextraer As String
                Dim base As String
                Dim base2 As String
                Dim cbb As String
                Dim cbb2 As String
                Dim pdf As String
                Dim cad As String()
                Dim cad2 As String
                Dim con As Integer

                con = 0

                'Dim archivoLog As String = "C:\Users\Usuario\Desktop\log.txt"
                'Dim directorio As String = "C:\Users\Usuario\Desktop\Comprobantes1"
                Dim directorio As String = cadena & "cdcgroup"
                Dim dir_bas As String = cadena & "cdcgroup\base"
                Dim dir_cbb As String = cadena & "cdcgroup\cbb"
                Dim dir_pdf As String = cadena & "cdcgroup\pdf"

                'Dim backupnull As String =

                Dim i As Integer
                For i = 0 To (lista.Items.Count - 1)
                    If lista.GetItemChecked(i) = True Then
                        archivo = lista.Items(i).ToString
                        archivoextraer = Path.GetFileNameWithoutExtension(lista.Items(i).ToString)
                        cad = archivoextraer.Split("_")
                        cad2 = cad(0).ToString & "_" & cad(1).ToString

                        ' Armar los sub-directorios
                        base = cadena & "base\" & cad2 & ".xml"
                        base2 = cadena & "base\" & cad2 & "_timbrado.xml"
                        cbb = cadena & "cbb\" & cad2 & "_cbb.png"
                        cbb2 = cadena & "cbb\" & cad2 & "_cbb.gif"
                        pdf = cadena & "pdf\" & archivoextraer & ".pdf"

                        'MsgBox(cadena & archivo & Chr(13) & base & Chr(13) & base2 & Chr(13) & cbb & Chr(13) & cbb2 & Chr(13) & pdf & Chr(13))

                        ' Comparar si existen los directorios de salida, SINO lo crea.

                        If Directory.Exists(directorio) = False Then
                            Directory.CreateDirectory(directorio)
                        End If

                        ' Dir base
                        If Directory.Exists(dir_bas) = False Then
                            Directory.CreateDirectory(dir_bas)
                        End If

                        ' Dir cbb
                        If Directory.Exists(dir_cbb) = False Then
                            Directory.CreateDirectory(dir_cbb)
                        End If

                        ' Dir pdf
                        If Directory.Exists(dir_pdf) = False Then
                            Directory.CreateDirectory(dir_pdf)
                        End If


                        Try
                            ' Hacer la operacion:
                            ' Si el archivo no existe, se mueve, si ya existe entonces se reemplaza.

                            If File.Exists(directorio & "\" & archivo) = False Then
                                File.Move(cadena & archivo, directorio & "\" & archivo)
                            Else
                                File.Replace(cadena & archivo, directorio & "\" & archivo, Nothing)
                            End If


                            If File.Exists(dir_bas & "\" & cad2 & ".xml") = False Then
                                File.Move(base, dir_bas & "\" & cad2 & ".xml")
                            Else
                                File.Replace(base, dir_bas & "\" & cad2 & ".xml", Nothing)
                            End If


                            If File.Exists(dir_bas & "\" & cad2 & "_timbrado.xml") = False Then
                                File.Move(base2, dir_bas & "\" & cad2 & "_timbrado.xml")
                            Else
                                File.Replace(base2, dir_bas & "\" & cad2 & "_timbrado.xml", Nothing)
                            End If


                            If File.Exists(dir_cbb & "\" & cad2 & "_cbb.png") = False Then
                                File.Move(cbb, dir_cbb & "\" & cad2 & "_cbb.png")
                            Else
                                File.Replace(cbb, dir_cbb & "\" & cad2 & "_cbb.png", Nothing)
                            End If

                            If File.Exists(dir_cbb & "\" & cad2 & "_cbb.gif") = False Then
                                File.Move(cbb2, dir_cbb & "\" & cad2 & "_cbb.gif")
                            Else
                                File.Replace(cbb2, dir_cbb & "\" & cad2 & "_cbb.gif", Nothing)
                            End If

                            If File.Exists(dir_pdf & "\" & archivoextraer & ".pdf") = False Then
                                File.Move(pdf, dir_pdf & "\" & archivoextraer & ".pdf")
                            Else
                                File.Replace(pdf, dir_pdf & "\" & archivoextraer & ".pdf", Nothing)
                            End If

                            'Subcarpetas

Linea2:
                            ' Registrar transferencia de archivo.                    
                            Dim archivoLog As String = cadena & "\cdcgroup\" & anio & "_" & mes & "_" & dia & "_" & "log.txt"
                            'Dim path As String = "C:\Users\Usuario\Desktop\Comprobantes1\log.txt"

                            Dim esc As New System.IO.StreamWriter(archivoLog, True)

                            ' Si directorio y por logica el archivo existe entonces escribe la transaccion, Si no escribe "No se encontró archivos ... para mover"
                            If File.Exists(directorio & "\" & archivo) Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & archivo & " a " & directorio & "\" & archivo)
                                con = con + 1
                            Else
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " No se encontró archivo XML para mover.")
                            End If


                            If File.Exists(dir_bas & "\" & cad2 & ".xml") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "base\" & cad2 & ".xml" & " a " & dir_bas & "\" & cad2 & ".xml")
                                con = con + 1
                            Else
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " No se encontró archivo XML para mover.")
                            End If

                            If File.Exists(dir_bas & "\" & cad2 & "_timbrado.xml") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "base\" & cad2 & "_timbrado.xml" & " a " & dir_bas & "\" & cad2 & "_timbrado.xml")
                                con = con + 1
                            Else
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " No se encontró archivo _TIMBRADO.XML para mover.")
                            End If

                            If File.Exists(dir_cbb & "\" & cad2 & "_cbb.png") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "cbb\" & cad2 & "_cbb.png" & " a " & dir_cbb & "\" & cad2 & "_cbb.png")
                                con = con + 1
                            Else
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " No se encontró archivo PNG para mover.")
                            End If

                            If File.Exists(dir_cbb & "\" & cad2 & "_cbb.gif") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "cbb\" & cad2 & "_cbb.gif" & " a " & dir_cbb & "\" & cad2 & "_cbb.gif")
                                con = con + 1
                            Else
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " No se encontró archivo GIF para mover.")
                            End If

                            If File.Exists(dir_pdf & "\" & archivoextraer & ".pdf") Then
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " Se movió archivo " & "pdf\" & archivoextraer & ".pdf" & " a " & dir_pdf & "\" & archivoextraer & ".pdf")
                                con = con + 1
                            Else
                                esc.WriteLine(datos.obtenerNombre & " | " & Date.Now & " | " & " No se encontró archivo PDF para mover.")
                            End If
                            esc.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------")
                            esc.Close()
                        Catch ex As Exception
                            ' Si entra a Catch, escribe en log txt el error y continua la ejecución.
                            MsgBox(ex.Message)
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
