'Module ImprimirPdf
'Setting the display mode: 
'HKEY_CURRENT_USER\Software\CutePDF Writer\BypassSaveAs 

'(0 = show Save as dialog box after spooling, 1 = do not show Save as dialog box.) 
'(This value is of type REG_SZ, not REG_DWORD)

Imports System
Imports Microsoft.Win32
Imports System.Security
Imports System.Security.Permissions
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports pdf7.PdfWriter


'Module Module1
Public Class ImprimirPDF
    Public NombreImpresora As String
    Public NombreArchivo As String = "archivopdf.pdf"

    Public Sub New()
        '  ExisteKey("CutePDF Writer")
    End Sub

    Public Function Dialogoarchivo()
        Dim SaveFileDialog1 As SaveFileDialog = New SaveFileDialog

        SaveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            NombreArchivo = SaveFileDialog1.FileName
            NameFilePdf(NombreArchivo, True)
            Return True
        End If

        Return False

    End Function

    Public Function NameFilePdf(ByVal nombre As String, ByVal lSaveAs As Boolean) As Boolean
        Dim printerName As String = PdfUtil.DefaultPrinterName
        Dim settings As PdfSettings = New PdfSettings()

        NombreImpresora = printerName
        settings.PrinterName = printerName
        settings.SetValue("Output", nombre)
        settings.SetValue("ShowSettings", "no")
        settings.SetValue("ShowSaveAS", "no")
        settings.SetValue("ShowProgress", "no")
        settings.SetValue("ShowProgressFinished", "no")
        settings.SetValue("ShowPDF", "no")
        settings.SetValue("ConfirmOverwrite", "no")
        'settings.SetValue("StatusFile", statusFileName)
        settings.WriteSettings(PdfSettingsFileType.RunOnce)


    End Function

    'Public Function ConDialogo(ByVal op As Boolean) As Boolean
    '    Dim pRegKey As RegistryKey = Registry.CurrentUser
    '    Dim user As String = Environment.UserDomainName & "\" & Environment.UserName
    '    Dim rs As New RegistrySecurity()

    '    Dim rule As New RegistryAccessRule(user,
    '        RegistryRights.ReadKey Or RegistryRights.WriteKey _
    '            Or RegistryRights.Delete,
    '        InheritanceFlags.ContainerInherit,
    '        PropagationFlags.None,
    '        AccessControlType.Allow)
    '    rs.AddAccessRule(rule)

    '    ' Add a rule that allows the current user the right
    '    ' right to set the name/value pairs in a key. 
    '    ' This rule is inherited by contained subkeys, but
    '    ' propagation flags limit it to immediate child 
    '    ' subkeys.
    '    rule = New RegistryAccessRule(user,
    '        RegistryRights.ChangePermissions,
    '        InheritanceFlags.ContainerInherit,
    '        PropagationFlags.InheritOnly Or PropagationFlags.NoPropagateInherit,
    '        AccessControlType.Allow)
    '    rs.AddAccessRule(rule)

    '    Dim pKey As RegistryKey = pRegKey.OpenSubKey("Software\\CutePDF Writer\\BypassSaveAs", True)
    '    pKey.SetAccessControl(rs)

    '    Try
    '        Dialogoarchivo()
    '        pKey.SetValue("BypassSaveAs", "0")
    '    Catch ex As Exception
    '        Return False
    '    End Try
    '    Return False

    'End Function

    'Public Function NombreDocumento(ByVal nombre As String) As Boolean
    '    Dim pRegKey As RegistryKey = Registry.CurrentUser

    '    Dim user As String = Environment.UserDomainName & "\" & Environment.UserName
    '    Dim rs As New RegistrySecurity()

    '    Dim rule As New RegistryAccessRule(user,
    '        RegistryRights.ReadKey Or RegistryRights.WriteKey _
    '            Or RegistryRights.Delete,
    '        InheritanceFlags.ContainerInherit,
    '        PropagationFlags.None,
    '        AccessControlType.Allow)
    '    rs.AddAccessRule(rule)

    '    ' Add a rule that allows the current user the right
    '    ' right to set the name/value pairs in a key. 
    '    ' This rule is inherited by contained subkeys, but
    '    ' propagation flags limit it to immediate child 
    '    ' subkeys.
    '    rule = New RegistryAccessRule(user,
    '        RegistryRights.ChangePermissions,
    '        InheritanceFlags.ContainerInherit,
    '        PropagationFlags.InheritOnly Or PropagationFlags.NoPropagateInherit,
    '        AccessControlType.Allow)
    '    rs.AddAccessRule(rule)

    '    Dim pKey As RegistryKey = pRegKey.OpenSubKey("Software\\CutePDF Writer\\OutputFile", True)
    '    pKey.SetAccessControl(rs)

    '    Try
    '        pKey.SetValue("OutputFile", nombre)
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    '    Return False

    'End Function

    'Private Function ExisteKey(clave) As Boolean
    '    Dim pRegKey As RegistryKey = Registry.CurrentUser
    '    Dim arrkey() As String = pRegKey.OpenSubKey("SOFTWARE").GetSubKeyNames()
    '    Dim i As Integer


    '    Dim user As String = Environment.UserDomainName & "\" & Environment.UserName
    '    Dim rs As New RegistrySecurity()

    '    Dim rule As New RegistryAccessRule(user,
    '        RegistryRights.ReadKey Or RegistryRights.WriteKey _
    '            Or RegistryRights.Delete,
    '        InheritanceFlags.ContainerInherit,
    '        PropagationFlags.None,
    '        AccessControlType.Allow)
    '    rs.AddAccessRule(rule)

    '    ' Add a rule that allows the current user the right
    '    ' right to set the name/value pairs in a key. 
    '    ' This rule is inherited by contained subkeys, but
    '    ' propagation flags limit it to immediate child 
    '    ' subkeys.
    '    rule = New RegistryAccessRule(user,
    '        RegistryRights.ChangePermissions,
    '        InheritanceFlags.ContainerInherit,
    '        PropagationFlags.InheritOnly Or PropagationFlags.NoPropagateInherit,
    '        AccessControlType.Allow)
    '    rs.AddAccessRule(rule)



    '    '        rs.AddAccessRule(New RegistryAccessRule(user, RegistryRights.FullControl, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow))
    '    '       rs.AddAccessRule(New RegistryAccessRule(user, RegistryRights.ChangePermissions, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly Or PropagationFlags.NoPropagateInherit, AccessControlType.Allow))

    '    For i = 0 To UBound(arrkey)
    '        If UCase(arrkey(i).Trim) = UCase(clave.trim) Then
    '            Return True
    '        End If
    '    Next
    '    ' No existe
    '    Try
    '        'pRegKey.SetAccessControl(rs)
    '        Dim newkey As RegistryKey = pRegKey.OpenSubKey("SOFTWARE", True).CreateSubKey(nombreimpresora, RegistryKeyPermissionCheck.ReadWriteSubTree, rs)
    '        Dim newkey1 As RegistryKey = newkey.CreateSubKey("BypassSaveAs", RegistryKeyPermissionCheck.ReadWriteSubTree)
    '        Dim newkey2 As RegistryKey = newkey.CreateSubKey("OutputFile", RegistryKeyPermissionCheck.ReadWriteSubTree)
    '        ' Atirbui o valor para a sub chave
    '        newkey1.SetValue("BypassSaveAs", "0")
    '        newkey2.SetValue("OutputFile", "C:\Users\<username>\Documents\documento impresora.pdf")
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try

    '    Return False

    'End Function

    'Private Function ExisteSubKey(clave) As Boolean
    '    Dim pRegKey As RegistryKey = Registry.CurrentUser
    '    Dim arrkey() As String = pRegKey.OpenSubKey("Software\\CutePDF Writer").GetSubKeyNames()
    '    Dim i As Integer

    '    For i = 0 To UBound(arrkey)
    '        If UCase(arrkey(i).Trim) = UCase(clave.trim) Then
    '            Return True
    '        End If
    '    Next

    '    Return False

    'End Function

End Class

'Note: CutePDF Writer will set this value back to 0 after each PDF creation. The Custom PDF Writer won't.

'Setting the filename:
'HKEY_CURRENT_USER\Software\CutePDF Writer\OutputFile 
'(Use the key above to set the output file for the PDF. A full pathname (e.g. d:\your folder\your file.pdf) is required.)


'End Module
