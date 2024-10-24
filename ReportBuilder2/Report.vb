Imports System.Data
'
' Created by SharpDevelop.
' User: Toshiba
' Date: 13-Jun-16
' Time: 10:52 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Class Report
    Public nm As String = "Informe sin nombre"
    Public w As Integer = 0
    Public leftm As Integer = 0
    Public Rightm As Integer = 0
    Public topm As Integer = 0
    Public bottomm As Integer = 0
    Public Sections As List(of section)
    Public Orn As Boolean = True ' False
    Public mcol As Boolean = False
	Public bw As Integer = 0
    Public linetype As BorderLineType = 0
    Public name As String
    Public proceso As String = ""
    Public documento As String
    Public formato As String
    Public nuevodocumento As Boolean
    Public Updating As Boolean = False
    Public impresora_defecto As String
    Public copias As Integer
    Public nombre_informe As String
    Public dtVariables As libcomunes.ControlVariables = New libcomunes.ControlVariables

    Public Sub New()
        w = 1200
        Sections = new List(of section)
	End Sub

    Public Sub New(ByVal ancho As Integer)
        w = ancho
        Sections = New List(Of Section)
    End Sub

    '<System.ComponentModel.Category("Design")> _
    <System.ComponentModel.Category("Diseño")>
    Public Property Nombre() As String
        Get
            Return name
        End Get
        Set(ByVal Value As String)
            nm = Value
            name = nm
            _DF.ShowTree()
            _DF.ShowProperty(RP)
        End Set
    End Property

    '<System.ComponentModel.Category("Orientation")> _
    <System.ComponentModel.Category("Orientación")>
    Public ReadOnly Property Horizontal() As Boolean
        Get
            Return Orn
        End Get
    End Property
    ' <System.ComponentModel.Category("Size")>
    <System.ComponentModel.Category("Tamaño")>
    Public Property Ancho() As Decimal
        Get
            Return w / unit
        End Get
        Set(ByVal Value As Decimal)
            w = Value * unit
            _DF.Reshape()
            _DF.Done()
        End Set
    End Property


    '<System.ComponentModel.Category("Margins")>
    <System.ComponentModel.Category("Margenes")>
    Public Property Margen_superior() As Decimal
        Get
            Return topm / unit
        End Get
        Set(ByVal Value As Decimal)
            topm = Value * unit
            '_DF.Reshape()
            'refresh()
            _DF.Done()
        End Set
    End Property

    '<System.ComponentModel.Category("Margins")> _
    <System.ComponentModel.Category("Margenes")>
    Public Property Margen_izquierdo() As Decimal
        Get
            Return leftm / unit
        End Get
        Set(ByVal Value As Decimal)
            leftm = Value * unit
            _DF.Reshape()
            'refresh()
            _DF.Done()
        End Set
    End Property

    '<System.ComponentModel.Category("Margins")> _
    <System.ComponentModel.Category("Margenes")>
    Public Property Margen_derecho() As Decimal
        Get
            Return Rightm / unit
        End Get
        Set(ByVal Value As Decimal)
            Rightm = Value * unit
            _DF.Reshape()
            'refresh()
            _DF.Done()
        End Set
    End Property

    '<System.ComponentModel.Category("Margins")> _
    <System.ComponentModel.Category("Margenes")>
    Public Property Margen_inferior() As Decimal
        Get
            Return bottomm / unit
        End Get
        Set(ByVal Value As Decimal)
            bottomm = Value * unit
            'refresh()
            '_DF.Reshape()
            _DF.Done()
        End Set
    End Property

    '<System.ComponentModel.Category("Border")> _
    <System.ComponentModel.Category("Borde")>
    Public Property Ancho_borde() As Decimal
        Get
            Return bw / unit
        End Get
        Set(ByVal Value As Decimal)
            bw = Value * unit
            'refresh()
            '_DF.Reshape()
            _DF.Done()
        End Set
    End Property

    '<System.ComponentModel.Category("Border")> _
    <System.ComponentModel.Category("Borde")>
    Public Property Estilo_linea() As BorderLineType
        Get
            Return linetype
        End Get
        Set(ByVal Value As BorderLineType)
            linetype = Value
            'refresh()
            '_DF.Reshape()
            _DF.Done()
        End Set
    End Property

    '<System.ComponentModel.Category("Behaviour")> _
    <System.ComponentModel.Category("Comportamiento")>
    Public Property Multicolumna As Boolean
        Get
            Return mcol
        End Get
        Set(ByVal Value As Boolean)
            mcol = Value
        End Set
    End Property


    Public Function Clone(optional byval src as Report = nothing) As Report
        If src is nothing Then
        	src = me
        End If
        Dim temp As New Report(src.w)
        temp.bottomm = src.bottomm
        temp.leftm = src.leftm
        temp.nm = src.nm
        temp.Orn = src.Orn
        temp.topm = src.topm
        temp.w = src.w
        For i As Integer = 0 To src.Sections.Count - 1
        	Dim tsecn As New Section
        	Dim secn As Section
        	secn = src.Sections(i)
        	tsecn._Constr = secn._Constr
            tsecn._sql = secn._sql
            tsecn.bcolor = secn.bcolor
            tsecn.BorderRad = secn.BorderRad
        	tsecn.bw = secn.bw
        	tsecn.DT = secn.DT.Clone()
        	tsecn.GroupID = secn.GroupID
        	tsecn.GrpList = secn.GrpList
        	tsecn.h = secn.h
        	tsecn.headerLabel = secn.headerLabel
        	tsecn.img = secn.img
        	tsecn.linecolor = secn.linecolor
        	tsecn.nm = secn.nm
        	tsecn.RecSides = secn.RecSides
        	tsecn.selected = secn.selected
        	tsecn.state = secn.state
            tsecn.Visble = secn.Visble
            tsecn.indice = secn.indice
            For j As Integer = 0 To secn.AR.Count - 1
        		Dim itm As Item
        		itm = secn.AR(j)
        		tsecn.AR.Add(itm.clone())
        	Next        	
        	temp.Sections.Add(tsecn)
        Next
        return temp
    End Function
    
    Public Sub PrintPreview()
    	Using p As New PrintDesign(me)			
			p.PrintReport(true)
		End Using
    End Sub
    
    Public Sub Print()
    	Using p As New PrintDesign(me)			
			p.PrintReport(false)
		End Using
    End Sub
    
    Public Function RptToString() As String
        'return ReportToString(me)
    End Function
    
    Public Function RptFromString(byval str as String) As Report
        'return ReportFromString(str)
    End Function
End Class
