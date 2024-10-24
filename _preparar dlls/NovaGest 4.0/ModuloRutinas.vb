Module ModuloRutinas

    'Selección
    'Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    Public Function AntesDeSeleccionar1(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeSeleccionar1 = True
    End Function
    Public Function AntesDeSeleccionar2(pform As Form, CT As CnTabla.CnTabla) As Boolean
        'Aquí se puede añadir selección adicional, que será visible y modificable por el usuario
        ' Por ejemplo:      CT.AsignarValor("texto_requerido", "*A*")
        AntesDeSeleccionar2 = True
    End Function


    Public Function AntesDeAceptarSeleccion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeAceptarSeleccion = True
    End Function
    Public Function RegistrosSeleccionados(pform As Form, CT As CnTabla.CnTabla) As Boolean
        RegistrosSeleccionados = True
    End Function
    Public Function NoHayRegistros(pform As Form, CT As CnTabla.CnTabla) As Boolean
        NoHayRegistros = True
    End Function
    Public Function AntesDeCancelarSeleccion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeCancelarSeleccion = True
    End Function

    Public Function DespuesDeCancelarSeleccion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeCancelarSeleccion = True
    End Function

    'Creación

    Public Function AntesDeCrear1(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeCrear1 = True
    End Function
    Public Function AntesDeCrear2(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeCrear2 = True
    End Function
    Public Function AntesDeAceptarCreacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeAceptarCreacion = True
    End Function
    Public Function DespuesDeFracasarAceptarCreacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeFracasarAceptarCreacion = True
    End Function
    Public Function AntesDeGrabarCreacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeGrabarCreacion = True
    End Function
    Public Function DespuesDeFracasarGrabarCreacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeFracasarGrabarCreacion = True
    End Function
    Public Function DespuesDeGrabarCreacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeGrabarCreacion = True
    End Function
    Public Function AntesDeCancelarCreacion1(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeCancelarCreacion1 = True
    End Function
    Public Function AntesDeCancelarCreacion2(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeCancelarCreacion2 = True
    End Function
    Public Function DespuesDeCancelarCreacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeCancelarCreacion = True
    End Function

    'Modificación

    Public Function AntesDeModificar1(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeModificar1 = True
    End Function
    Public Function AntesDeModificar2(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeModificar2 = True
    End Function

    Public Function AntesDeAceptarModificacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeAceptarModificacion = True
    End Function
    Public Function DespuesDeFracasarAceptarModificacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeFracasarAceptarModificacion = True
    End Function


    Public Function AntesDeGrabarModificacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeGrabarModificacion = True
    End Function

    Public Function DespuesDeGrabarModificacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeGrabarModificacion = True
    End Function

    Public Function AntesDeCancelarModificacion1(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeCancelarModificacion1 = True
    End Function
    Public Function AntesDeCancelarModificacion2(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeCancelarModificacion2 = True
    End Function
    Public Function DespuesDeCancelarModificacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeCancelarModificacion = True
    End Function

    'Borrado

    Public Function AntesDeEliminar1(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeEliminar1 = True
    End Function
    Public Function AntesDeEliminar2(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeEliminar2 = True
    End Function

    Public Function AntesDeAceptarEliminacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeAceptarEliminacion = True
    End Function
    Public Function DespuesDeEliminar(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeEliminar = True
    End Function

    Public Function AntesDeCancelarEliminacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        AntesDeCancelarEliminacion = True
    End Function
    Public Function DespuesDeCancelarEliminacion(pform As Form, CT As CnTabla.CnTabla) As Boolean
        DespuesDeCancelarEliminacion = True
    End Function



    Public Sub GrabarTraza(Proceso As String, P1 As String, P2 As String, P3 As String)
        Dim SQL As String, Rs As New cnRecordset.CnRecordset

        SQL = "SELECT * FROM ZPA_TRAZA WHERE 1=0"
        If Rs.Open(SQL, ObjetoGlobal.cn, True, False, "contador") = False Then
            MsgBox("No se puede abrir zpa_traza")
            Exit Sub
        End If

        Rs.AddNew()
        Rs!fechahora = Now()
        Rs!proceso = Trim(Proceso)
        Rs!parametro1 = Trim(P1)
        Rs!parametro2 = Trim(P2)
        Rs!parametro3 = Trim(P3)
        Rs.Update()
        Rs.Close()

    End Sub

End Module
