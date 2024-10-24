Public Class FrmCambioConfeccion
    Inherits libcomunes.FormGenerico
    Public resultado As String
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public correcto As Boolean
    Public pulsado As Boolean
    Public Cajas As String
    Public Confeccion As String
    Public paletizacion As String

    Private Sub Cambiar_Click(sender As Object, e As EventArgs) Handles Cambiar.Click

        If Trim(TxtCodigo_confeccion.Text) = "" Then
            MsgBox("Debe de indicar un código de confección")
            Return
        End If
        If Trim(TxtPaletizacion.Text) = "" Then
            MsgBox("Debe de indicar un código de paletización")
            Return
        End If
        If Trim(TxtCajas.Text) = "" Then
            MsgBox("Debe de indicar un número de cajas")
            Return
        End If

        Cajas = TxtCajas.Text
        Confeccion = TxtCodigo_confeccion.Text
        paletizacion = TxtPaletizacion.Text

        correcto = True
        DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub ComprobacionPaletizacion(Campo As Control)
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String

        If TxtPaletizacion.Text.Trim <> "" Then
            sSql = "SELECT * FROM confecciones WHERE empresa = '" & Trim(ObjetoGlobal.empresaActual) & "' AND codigo_confeccion = '" & Trim(TxtPaletizacion.Text) & "'"
            rs.Open(sSql, ObjetoGlobal.cn)
            If rs.EOF Then
                MsgBox("No existe referencia en confeccion según el valor introducido")
            End If
        End If

    End Sub

    Private Sub FrmCambioConfeccion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String
        correcto = False

        TxtCodigo_confeccion.Text = Confeccion
        TxtPaletizacion.Text = paletizacion
        TxtCajas.Text = Cajas

        If Trim(TxtCodigo_confeccion.Text) <> "" Then
            sSql = "SELECT * FROM confeccion WHERE empresa = '" & Trim(ObjetoGlobal.empresaActual) & "' AND codigo_confeccion = '" & Trim(TxtCodigo_confeccion.Text) & "'"
            rs.Open(sSql, ObjetoGlobal.cn)
            If Not rs.EOF Then
                LblConfeccion.Text = rs!Descripcion
            End If
        End If
        rs.Close()

        If Trim(TxtPaletizacion.Text) <> "" Then
            sSql = "SELECT * FROM confeccion WHERE empresa = '" & Trim(ObjetoGlobal.empresaActual) & "' AND codigo_confeccion = '" & Trim(TxtPaletizacion.Text) & "'"
            rs.Open(sSql, ObjetoGlobal.cn)
            If Not rs.EOF Then
                lblPaletizacion.Text = rs!Descripcion
            End If
        End If
        rs.Close()

    End Sub

    Private Sub TxtCodigo_confeccion_Validated(sender As Object, e As EventArgs) Handles TxtCodigo_confeccion.Validated
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String

        If Trim(TxtCodigo_confeccion.Text) <> "" Then
            sSql = "SELECT * FROM confeccion WHERE empresa = '" & Trim(ObjetoGlobal.empresaActual) & "' AND codigo_confeccion = '" & Trim(TxtCodigo_confeccion.Text) & "'"
            rs.Open(sSql, ObjetoGlobal.cn)
            If rs.EOF Then
                LblConfeccion.Text = ""
                MsgBox("No existe referencia en confeccion según el valor introducido")
            Else
                LblConfeccion.Text = rs!Descripcion
            End If
        End If

    End Sub

    Private Sub TxtPaletizacion_Validated(sender As Object, e As EventArgs) Handles TxtPaletizacion.Validated
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String

        If Trim(TxtPaletizacion.Text) <> "" Then
            sSql = "SELECT * FROM confeccion WHERE empresa = '" & Trim(ObjetoGlobal.empresaActual) & "' AND codigo_confeccion = '" & Trim(TxtPaletizacion.Text) & "'"
            rs.Open(sSql, ObjetoGlobal.cn)
            If rs.EOF Then
                MsgBox("No existe referencia en confeccion según el valor introducido")
                lblPaletizacion.Text = ""
            Else
                lblPaletizacion.Text = rs!Descripcion
            End If
        End If

    End Sub

    Private Sub TxtCajas_Validated(sender As Object, e As EventArgs) Handles TxtCajas.Validated

        If Not IsNumeric(Trim(TxtCajas.Text)) Then
            MsgBox("Cajas debe de tener un contenido únicamente numérico")
            TxtCajas.Text = ""
            Return
        ElseIf Val(TxtCajas.Text) = 0 Then
            MsgBox("Cajas debe de tener algún valor númerico")
        End If

    End Sub

End Class