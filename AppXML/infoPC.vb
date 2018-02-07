Imports System.Net
Public Class infoPC
    ' Crear la funcion para obtener el nombre de la PC.

    Public Function obtenerNombre()
        Dim nombreHost As String = Dns.GetHostName
        Dim NombrePC As String = ""
        Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(nombreHost)
        NombrePC = hostInfo.HostName.ToString

        Return NombrePC

        If NombrePC = "" Then
            Return "SN"
        End If
    End Function
End Class
