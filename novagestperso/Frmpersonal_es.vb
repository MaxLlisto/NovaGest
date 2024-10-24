Imports System.ComponentModel

Public Class Frmpersonal_es
    Inherits LibreriaModeloMantenimiento.ModeloMantenimiento

    'Public Overrides Function HOOK_Antes_de_crear(CT As CnTabla.CnTabla, Optional GE As DataGridView = Nothing, Optional HH As Hashtable = Nothing) As Boolean
    '    'Se invoca exactamente antes de ceder el control al usuario para introducir los datos de creaci�n.
    '    'Hay dos puntos de llamada a esta funci�n, seg�n la edici�n de la tabla sea en formato ficha o grid.
    '    'Si la tabla se edita en formato grid, la llamada est� en el formulario GridEdicion
    '    'Si la tabla se edita en formato ficha, la llamada est� en el propio mantenimiento
    '    'El primer par�metro corresponde al objeto CnTabla de la tabla en edici�n. 
    '    'GE y HH provienen del GridEdicion y son el Objeto DataGridView y la Hashtable que permite localizar una columna en ese objeto. Si la llamda proviene del propio mantenimiento, son nulos.
    '    'Si la rutina debe realizar tareas diferente en ambos casos:
    '    '           If CT.HayGrid = True then
    '    '               ...
    '    '           Else
    '    '               ...
    '    '           End if
    '    'Este parece el momento para asignar valores inciales variables a los campos de la tabla en edici�n. 
    '    'Si los valores no son variables, se pueden incluir en dise�o en los controles CnEdicion
    '    'Sin embargo, podr�an depender del usuario o de la fecha o de otros valores variables en cada ejecuci�n.
    '    'Por ejemplo, supongamos que queremos que un dato almacene el usuario que cre� el registro. 
    '    'Ser�a posible hacer (mala soluci�n):    CT.RsCreacion("nombrecampo") = Parametros(5)  
    '    'Pero no es una buena soluci�n (�qu� pasa con txtDatos? �Est� el campo en grid? �El mtmto est� en grid y ese campo fuera?)
    '    'La soluci�n correcta SI SON VALORES POR DEFECTO (es decir, modificables por el usuario) es utilizar el procedimiento HOOK_AsignarValores. Por ejemplo:
    '    '   Dim Campos() As String, Valores() As String
    '    '   ReDim Preserve Campos(1), Valores(1)
    '    '   Campos(0) = "nombrecampo" : Valores(0) = Trim(Parametros(5))
    '    '   Campos(1) = "otrocampo" : Valores(1) = otrovalor
    '    '   HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    '    'La soluci�n correcta SI SON VALORES FIJOS CREACI�N es utilizar el procedimiento HOOK de antes de PasandoACrear haciendo:
    '    '               Cadena = Trim("test_serio2.contador")
    '    '               CT.XCnEdicion(Cadena).HayValorFijoCreacion = True
    '    '               CT.XCnEdicion(Cadena).ValorFijoCreacion = xxxxxx
    '    '               (la llamada que se ha construido para invocar al CnEdicion (CT.XCnEdicion(Cadena)) se puede cambiar simplemente a CnEdicionxxx


    '    HOOK_Antes_de_crear = False

    '    Dim Campos(5)
    '    Dim Valores(5)
    '    Dim CamposTabla(5)
    '    Campos(0) = "contador"
    '    Campos(1) = "redondeo_entrada"
    '    Campos(2) = "redondeo_salida"
    '    Campos(3) = "parada_1_sn"
    '    Campos(4) = "parada_2_sn"
    '    Valores(1) = "S"
    '    Valores(2) = "S"
    '    Valores(3) = "S"
    '    Valores(4) = "N"

    '    Dim Rs2 As cnRecordset.CnRecordset

    '    Rs2.Open("Select MAX(contador) FROM personal_es WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "'", ObjetoGlobal.cn)
    '    Valores(0) = CStr(IIf(Rs2(0) Is Nothing, 1, Rs2(0) + 1))
    '    Rs2.Close
    '    HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    '    If CT.XCnEdicion("contador") Is Nothing Then
    '        TxtDatos002

    '    End If

    'End Function


End Class
