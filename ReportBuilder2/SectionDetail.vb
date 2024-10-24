'
' Created by SharpDevelop.
' User: Louis
' Date: 1/31/2017
' Time: 2:17 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Class SectionDetail
    Inherits Section

    <System.ComponentModel.Category("Fila alternativa")>
    Public Property Color_alternativo() As Color
        'Public Property AlternateBackColor() As color
        Get
            Return AlternateBackColor
        End Get
        Set(ByVal Value As Color)
            AlternateBackColor = Value
            'refresh()
        End Set

    End Property

    '<System.ComponentModel.Category("Alternate Row")> _
    <System.ComponentModel.Category("Fila alternativa")>
    Public Property Color_de_fondo_alternativo() As Image
        'Public Property AlternateBackgroundImage() As Image
        Get
            Return AlternateBackgroundImage
        End Get
        Set(ByVal Value As Image)
            AlternateBackgroundImage = Value
            'refresh()
        End Set
    End Property
End Class
