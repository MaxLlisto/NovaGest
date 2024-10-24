'
' Created by SharpDevelop.
' User: Toshiba
' Date: 11-Jun-16
' Time: 10:35 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.ComponentModel
imports System.Data
Public Class Section
	Public h As Integer
	Public state As Integer = 1
	Public headerLabel As String	
	Public AR As List(of item)
	Public selected As Boolean = False
	Public nm As String = ""
	Public img As Image
	Public aimg As Image
    Public bcolor As Color = Color.White
    Public BackColor As Color = Color.White
    Public abcolor As Color = Color.White
    Public BorderRad As Integer = 0
    Public linecolor As Color = Color.Black
    Public Bordercolor As Color = Color.LightGray
    Public bw As Integer = 1
    Public dt As DataTable
    Public _sql as String = ""
	Public _Constr As String = ""
	Public _Provider As DataProv = dataprov.None
    Public RecSides As AnchorStyles
    Public BorderSides As AnchorStyles
    Public Visble As Boolean = True
	Public GrpList As new List(Of GroupItem)
    Public GroupID As Integer = 5
    Public ItemName As String
    Public AlternateBackColor As Color
    Public AlternateBackgroundImage As Image
    Public BorderWidth As Decimal
    Public indice As Integer

    Public Enum DataProv
		None = 0
		Microsoft_Access_Database = 1
		Microsoft_SQL_Server = 2
		MySQL_Database= 3
		Microsoft_ODBC = 4
	End Enum
	
	
	
	Public Sub New()
		AR = new List(of item)
		headerlabel = ""		
		DT = new DataTable
	End Sub

    '<System.ComponentModel.Category("Design")>
    <System.ComponentModel.Category("Diseño")>
    Public ReadOnly Property Nombre() As String
        Get
            Return nm
        End Get
    End Property

    Public ReadOnly Property name() As String
        Get
            Return nm
        End Get
    End Property




    '<System.ComponentModel.Category("Appearance")>
    <System.ComponentModel.Category("Apariencia")>
    Public Property Borde_caras() As AnchorStyles
        Get
            Return RecSides
        End Get
        Set(ByVal Value As AnchorStyles)
            RecSides = Value
            BorderSides = Value
            _DF.Reshape()
            _DF.Done()
        End Set
    End Property


    '<System.ComponentModel.Category("Size")> _
    <System.ComponentModel.Category("Tamaño")>
    Public Property Alto() As Decimal
        Get
            Return h / unit
        End Get
        Set(ByVal Value As Decimal)
            h = Value * unit
            _DF.Reshape()
            _DF.Done()
        End Set
    End Property

    '<System.ComponentModel.Category("Appearance")> _
    <System.ComponentModel.Category("Apariencia")>
    Public Property Color_fondo() As Color
        Get
            Return bcolor
        End Get
        Set(ByVal Value As Color)
            bcolor = Value
            BackColor = Value
            Refresh()
        End Set
    End Property

    '<System.ComponentModel.Category("Appearance")> _
    <System.ComponentModel.Category("Apariencia")>
    Public Property Imagen_de_fondo() As Image
        Get
            Return img
        End Get
        Set(ByVal Value As Image)
            img = Value
            Refresh()
        End Set
    End Property

    '<System.ComponentModel.Category("Appearance")>
    <System.ComponentModel.Category("Apariencia")>
    Public Property Color_borde() As Color
        Get
            Return linecolor
        End Get
        Set(ByVal Value As Color)
            linecolor = Value
            Bordercolor = Value
            Refresh()
        End Set
    End Property

    '<System.ComponentModel.Category("Behaviour")>
    <System.ComponentModel.Category("Comportamiento")>
    Public Property Visible As Boolean
        Get
            Return Visble
        End Get
        Set(ByVal Value As Boolean)
            Visble = Value
        End Set
    End Property

    '<System.ComponentModel.Category("Appearance"), System.ComponentModel.Description("With of the border of shape")> _
    <System.ComponentModel.Category("Apariencia"), System.ComponentModel.Description("Del borde de la figura")>
    Public Property Ancho_borde() As Decimal
        Get
            Return BorderWidth / unit
        End Get
        Set(ByVal Value As Decimal)
            BorderWidth = Value * unit
            Refresh()
        End Set
    End Property

    '<System.ComponentModel.Category("DataSource")> _
    <System.ComponentModel.Category("Fuente de datos")>
    Public Property Proveedor_datos() As DataProv
        Get
            Return _Provider
        End Get
        Set(ByVal Value As DataProv)
            _Provider = Value
        End Set
    End Property


    '<System.ComponentModel.Category("DataSource")>
    <System.ComponentModel.Category("Fuente de datos")>
    <Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))> _
       Public Property SQL() As String
       	Get
            Return _sql
        End Get
        Set(ByVal Value As string)
            _sql = Value
        End Set
       End Property
    '<System.ComponentModel.Category("DataSource")> _
    <System.ComponentModel.Category("Fuente de datos")>
    <Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))> _
       Public Property ConnectionString() As String
       	Get
            Return _ConStr
        End Get
        Set(ByVal Value As string)
            _Constr = Value
        End Set
       End Property
    
    Public Sub Refresh()
    	_df.Reshape
    	_DF.Done()
    End Sub
    
End Class


<Serializable()> _
Public Class GroupItem
    'The ItemName property.
    Public ItemName As String
    Public Property Nombre_Item() As String
        Get
            Return ItemName
        End Get
        Set(ByVal value As String)
            ItemName = value
        End Set
    End Property
End Class
