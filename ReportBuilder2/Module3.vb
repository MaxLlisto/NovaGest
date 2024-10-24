Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Globalization

Public Module CamposyVariables
    Public Variables() As String = {"clientes.Codigo_cliente", "clientes.razón social", "clientes.nif", "calculado.total_compras"}
End Module

Module FuncionesGrid

    Public Class ComboVariables
        Inherits StringConverter

        Public Sub New()
            MyBase.New()
        End Sub

        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            'True = Se despliega la lista
            'False = No se despliega la lista y el ususario debe escribir un valor
            Return True
        End Function

        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Return New StandardValuesCollection(CamposyVariables.Variables)
        End Function
        Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
            'True = el combo no admite más items que los de la lista
            'False = el combo admite un item que no esté en la lista
            Return True
        End Function

    End Class

    Public Class ColorConverter : Inherits ExpandableObjectConverter ' Int32Converter
        Public Overloads Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
            If sourceType Is GetType(Integer) Then
                Return True
            End If
            Return MyBase.CanConvertFrom(context, sourceType)
        End Function

        Public Overloads Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext,
    ByVal culture As CultureInfo, ByVal value As Object) As Object

            If TypeOf value Is Integer Then
                Return Color.FromArgb(value)
            End If
            Return MyBase.ConvertFrom(context, culture, value)
        End Function

        Public Overloads Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext,
    ByVal destinationType As Type) As Boolean
            If destinationType Is GetType(Color) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destinationType)
        End Function

        Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext,
    ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
            If destinationType Is GetType(Integer) AndAlso TypeOf value Is Color Then

                Return CType(value, Color).ToArgb
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function
    End Class


End Module
