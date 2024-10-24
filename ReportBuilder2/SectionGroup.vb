'
' Created by SharpDevelop.
' User: Louis
' Date: 1/3/2017
' Time: 6:04 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.ComponentModel
Imports System.Collections.Generic

Public Class SectionGroup
	Inherits Section
    Private m_TextValue As String = "Valor por defecto"
    '<System.ComponentModel.Category("Data")> _
    '<Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))> _
    'Public Property GroupFieldList As List(Of GroupItem)
    Public Property Lista_campos_del_grupo As List(Of GroupItem)
        Get
            Return GrpList
        End Get
        Set(ByVal Value As List(Of GroupItem))
            Dim lst As String = ""
            MsgBox(lst)
            GrpList.Clear()

            For Each itm As GroupItem In Value
                GrpList.Add(itm)
                If lst <> "" Then
                    lst &= ", "
                End If
                lst &= itm.ItemName
            Next
            'GrpList = Value

            If Trim(lst).Length > 0 Then
                headerLabel = nm & " (" & lst & ")"
            Else
                headerLabel = nm & " <Ilimitado>"
            End If

            _DF.Reshape()
            _DF.Done()
        End Set

    End Property
    'Declare a public event.
    'Public Event TheEvent()
    'The TextValue property.

    <Description("Valor de texto del objeto."),
        Category("Valores de cadena"),
        Browsable(True),
        DefaultValue("Valor por defecto")>
    Public Property Valor_texto() As String
        Get
            Return m_TextValue
        End Get
        Set(ByVal value As String)
            m_TextValue = value
        End Set
    End Property


End Class



