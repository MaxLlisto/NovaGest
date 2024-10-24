'Module VariablesImpresion
'    Public INProceso As String
'    Public INDocumento As String
'    Public INFormato As String
'    Public INRsDatos(2) As System.Data.DataTable
'    Public INRsFormato As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
'    Public INDocumentoAnterior As String
'    Public INFormatoAnterior As String
'    Public INDiccionario As Dictionary(Of String, Long())
'    Public INPrevisualizacion As Boolean
'    Dim instance As New Printing.PrinterSettings
'    Public INImpresoraSistema As String = instance.PrinterName
'    Public INImpresoraFormato As String
'    Public INModoSeleccionImpresora As Integer
'    ' 0 = Impresora en zz formatosdocumento + ImpresoraPredeterminada sin preguntar
'    ' 1 = Siempre pregunta
'    ' 2 = ImpresoraPorDefecto (solamente)
'    Public INApaisado As Boolean
'    Public INCopiasImpresion As Integer 'Nº de copias globales que se imprimen
'    Public INIntercalar As Boolean
'    Public INAltoPaginaReal As Long
'    Public INAnchoPaginaReal As Long
'    Public INTotalPaginas As Long
'    Public INAlturaCab0 As Long
'    Public INAlturaCab1 As Long
'    Public INAlturaCuerpo(10) As Long
'    Public INAlturaPie0 As Long
'    Public INAlturaPie1 As Long
'    Public INAlturaMaximoPie As Long
'    Public INAlturaActual As Long
'    Public INSeleccionImpresoraCancelada As Boolean
'    Public INModoFormulariObjetoImpresionesion As Boolean
'    Public INImpresionPorPaginas As Boolean
'    Public INIntercalarPaginas As Boolean
'    Public INDesdePaginaAImprimir As Long
'    Public INHastaPaginaAImprimir As Long
'    Public INEmpresa As String

'    'Páginas y documentos
'    'NUEVO Public INDiccionarioPaginas As Dictionary
'    Public INRsPaginas As cnRecordset.CnRecordset 'NUEVO
'    Public INDiccionarioDocumentos As Dictionary(Of String, String)
'    Public INDocumentos() As Byte
'    Public INUltimoDocumento As Long

'    'Códigos de barras 3 de 9
'    Public Formato39(90) As String
'    Public ConversionH As Double, ConversionV As Double

'    'Para impresión PDF
'    Public CodigoImpresion As Long

'    Public HayImpresionPDF As Boolean 'PDF
'    Public NombreImpresoraPDF As String 'PDF
'    Public CarpetaParaPDF As String 'PDF
'    'Public OBJPrinter As Printer 'PDF
'    Public ImpresoraAnterior As String 'PDF

'    Dim OBJPrinter As New PrintDesign
'    ' asignamos el método de evento para cada página a imprimir
'    'AddHandler() OBJPrinter.PrintPage, AddressOf print_PrintPage

'    Dim prtSettings As PrinterSettings '= New PrinterSettings

'    Public FuerzaCop As Integer

'End Module
