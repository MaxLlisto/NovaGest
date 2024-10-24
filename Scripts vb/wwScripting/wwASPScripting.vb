Imports System.Text
Imports Westwind.Tools

Namespace Westwind.wwScripting
    ''' <summary>
    ''' Summary description for Class1.
    ''' </summary>
    Public Class wwASPScripting
        Public oScript As wwScripting = Nothing
        Public cErrormsg As String = ""
        Public bError As Boolean = False

        Public Sub New()
            '
            ' TODO: Add constructor logic here
            '
            oScript = New wwScripting("CSharp")
        End Sub


        ''' <summary>
        ''' Parses a script into a compilable program
        ''' </summary>
        ''' <paramname="lcCode"></param>
        ''' <returns></returns>
        Public Function ParseScript(ByVal lcCode As String) As String
            If Equals(lcCode, Nothing) Then Return ""
            Dim oSb As wwScriptResponse = New wwScriptResponse()
            Dim lnLast = 0
            Dim lnAt2 = 0
            Dim lnAt = lcCode.IndexOf("<%", 0)
            If lnAt = -1 Then Return lcCode

            ' *** Create the Response object which is used to write output into the code generator
            oSb.Append("wwScriptResponse Response = new wwScriptResponse();" & Microsoft.VisualBasic.Constants.vbCrLf)

            While lnAt > -1
                ' *** Catch the plain text write out to the Response Stream as is - fix up for quotes
                If lnAt > -1 Then oSb.Append("Response.oSb.Append(@""" & lcCode.Substring(CInt(lnLast), CInt(lnAt - lnLast)).Replace("""", """""") & """ );" & Microsoft.VisualBasic.Constants.vbCrLf & Microsoft.VisualBasic.Constants.vbCrLf)

                '*** Find end tag
                lnAt2 = lcCode.IndexOf("%>", lnAt)
                If lnAt2 < 0 Then Exit While
                Dim lcSnippet = lcCode.Substring(lnAt, lnAt2 - lnAt + 2)

                If Equals(lcSnippet.Substring(2, 1), "=") Then
                    ' *** Write out an expression. 'Eval' inside of a Response.Write call
                    oSb.Append("Response.oSb.Append(" & lcSnippet.Substring(CInt(3), CInt(lcSnippet.Length - 5)).Trim() & ".ToString());" & Microsoft.VisualBasic.Constants.vbCrLf)
                ElseIf Equals(lcSnippet.Substring(2, 1), "@") Then
                    Dim lcAttribute = ""

                    ' *** Handle Directives
                    lcAttribute = wwUtils.StrExtract(lcSnippet, "Assembly", "=")

                    If lcAttribute.Length > 0 Then
                        lcAttribute = wwUtils.StrExtract(lcSnippet, """", """")
                        If lcAttribute.Length > 0 Then oScript.AddAssembly(lcAttribute)
                    Else
                        lcAttribute = wwUtils.StrExtract(lcSnippet, "Import", "=")

                        If lcAttribute.Length > 0 Then
                            lcAttribute = wwUtils.StrExtract(lcSnippet, """", """")
                            If lcAttribute.Length > 0 Then oScript.AddNamespace(lcAttribute)
                        End If
                    End If
                Else
                    ' *** Write out a line of code as is.
                    oSb.Append(lcSnippet.Substring(2, lcSnippet.Length - 4) & Microsoft.VisualBasic.Constants.vbCrLf)
                End If

                lnLast = lnAt2 + 2
                lnAt = lcCode.IndexOf("<%", lnLast)
                ' *** Write out the final block of non-code text
                If lnAt < 0 Then oSb.Append("Response.oSb.Append(@""" & lcCode.Substring(CInt(lnLast), CInt(lcCode.Length - lnLast)).Replace("""", """""") & """ );" & Microsoft.VisualBasic.Constants.vbCrLf & Microsoft.VisualBasic.Constants.vbCrLf)
            End While

            oSb.Append("return Response.oSb.ToString();")
            Return oSb.ToString()
        End Function

        Public Sub Release()
            oScript.Release()
        End Sub
    End Class

    Public Class wwScriptResponse
        Public oSb As StringBuilder

        Public Sub New()
            oSb = New StringBuilder()
        End Sub

        Public Sub Write(ByVal lcString As String)
            oSb.Append(lcString)
        End Sub

        Public Sub Append(ByVal lcString As String)
            oSb.Append(lcString)
        End Sub

        Public Overrides Function ToString() As String
            Return oSb.ToString()
        End Function
    End Class
End Namespace
