Imports System
Imports System.IO
Imports System.Text

Namespace Westwind.Tools
    ''' <summary>
    ''' Summary description for Class1.
    ''' </summary>
    Public Class wwUtils
        Public cErrormsg As String = ""
        Public bError As Boolean = False

        ''' <summary>
        ''' Replaces and  and Quote characters to HTML safe equivalents.
        ''' </summary>
        ''' <paramname="lcHTML">HTML to convert</param>
        ''' <returns>Returns an HTML string of the converted text</returns>
        Public Function FixHTMLForDisplay(ByVal lcHTML As String) As String
            lcHTML = lcHTML.Replace("<", "&lt;")
            lcHTML = lcHTML.Replace(">", "&gt;")
            lcHTML = lcHTML.Replace("""", "&quote;")
            Return lcHTML
        End Function

        Public Function StringToFile(ByVal lcString As String, ByVal lcFile As String, ByVal loEncoding As Encoding) As Boolean
            SetError(Nothing)

            Try
                Dim loStream As StreamWriter = New StreamWriter(lcFile, False)
                loStream.Write(lcString)
                loStream.Close()
            Catch ex As Exception
                SetError(ex.Message)
                Return False
            End Try

            Return True
        End Function

        ''' <summary>
        ''' Writes a string to a file.
        ''' </summary>
        ''' <paramname="lcString"></param>
        ''' <paramname="lcFile"></param>
        ''' <returns></returns>
        Public Function StringToFile(ByVal lcString As String, ByVal lcFile As String) As Boolean
            Return StringToFile(lcString, lcFile, Encoding.Default)
        End Function

        Public Function FileToString(ByVal lcFile As String) As String
            Dim lcResult As String
            SetError(Nothing)

            Try
                Dim loStream As StreamReader = New StreamReader(lcFile)
                lcResult = loStream.ReadToEnd()
                loStream.Close()
            Catch ex As Exception
                SetError(ex.Message)
                Return ""
            End Try

            Return lcResult
        End Function


        ''' <summary>
        ''' Searches one string into another string and replaces all occurences with
        ''' a blank character.
        ''' <pre>
        ''' Example:
        ''' VFPToolkit.strings.StrTran("Joe Doe", "o");		//returns "J e D e" :)
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchIn"> </param>
        ''' <paramname="cSearchFor"> </param>
        Public Shared Function StrTran(ByVal cSearchIn As String, ByVal cSearchFor As String) As String
            'Create the StringBuilder
            Dim sb As StringBuilder = New StringBuilder(cSearchIn)

            'Call the Replace() method of the StringBuilder
            Return sb.Replace(cSearchFor, " ").ToString()
        End Function

        ''' <summary>
        ''' Searches one string into another string and replaces all occurences with
        ''' a third string.
        ''' <pre>
        ''' Example:
        ''' VFPToolkit.strings.StrTran("Joe Doe", "o", "ak");		//returns "Jake Dake" 
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchIn"> </param>
        ''' <paramname="cSearchFor"> </param>
        ''' <paramname="cReplaceWith"> </param>
        Public Shared Function StrTran(ByVal cSearchIn As String, ByVal cSearchFor As String, ByVal cReplaceWith As String) As String
            'Create the StringBuilder
            Dim sb As StringBuilder = New StringBuilder(cSearchIn)

            'There is a bug in the replace method of the StringBuilder
            sb.Replace(cSearchFor, cReplaceWith)

            'Call the Replace() method of the StringBuilder and specify the string to replace with
            Return sb.Replace(cSearchFor, cReplaceWith).ToString()
        End Function

        ''' Searches one string into another string and replaces each occurences with
        ''' a third string. The fourth parameter specifies the starting occurence and the 
        ''' number of times it should be replaced
        ''' <pre>
        ''' Example:
        ''' VFPToolkit.strings.StrTran("Joe Doe", "o", "ak", 2, 1);		//returns "Joe Dake" 
        ''' </pre>
        Public Shared Function StrTran(ByVal cSearchIn As String, ByVal cSearchFor As String, ByVal cReplaceWith As String, ByVal nStartoccurence As Integer, ByVal nCount As Integer) As String
            'Create the StringBuilder
            Dim sb As StringBuilder = New StringBuilder(cSearchIn)

            'There is a bug in the replace method of the StringBuilder
            sb.Replace(cSearchFor, cReplaceWith)

            'Call the Replace() method of the StringBuilder specifying the replace with string, occurence and count
            Return sb.Replace(cSearchFor, cReplaceWith, nStartoccurence, nCount).ToString()
        End Function

        ''' <summary>
        ''' Receives a string along with starting and ending delimiters and returns the 
        ''' part of the string between the delimiters. Receives a beginning occurence
        ''' to begin the extraction from and also receives a flag (0/1) where 1 indicates
        ''' that the search should be case insensitive.
        ''' <pre>
        ''' Example:
        ''' string cExpression = "JoeDoeJoeDoe";
        ''' VFPToolkit.strings.StrExtract(cExpression, "o", "eJ", 1, 0);		//returns "eDo"
        ''' </pre>
        ''' </summary>
        Public Shared Function StrExtract(ByVal cSearchExpression As String, ByVal cBeginDelim As String, ByVal cEndDelim As String, ByVal nBeginOccurence As Integer, ByVal nFlags As Integer) As String
            Dim cstring = cSearchExpression
            Dim cb = cBeginDelim
            Dim ce = cEndDelim
            'string lcRetVal = "";

            If nFlags = 1 Then
                cb = cb.ToLower()
                ce = ce.ToLower()
                cstring = cstring.ToLower()
            End If

            Dim lnAt = cSearchExpression.IndexOf(cb, 0)
            If lnAt < 0 Then Return ""
            Dim lnAtCut = lnAt + cb.Length
            Dim lnAt2 = cSearchExpression.IndexOf(ce, lnAtCut)
            If lnAt2 < 0 Then Return ""
            Return cSearchExpression.Substring(lnAtCut, lnAt2 - lnAtCut)
        End Function

        ''' <summary>
        ''' Receives a string and a delimiter as parameters and returns a string starting 
        ''' from the position after the delimiter
        ''' <pre>
        ''' Example:
        ''' string cExpression = "JoeDoeJoeDoe";
        ''' VFPToolkit.strings.StrExtract(cExpression, "o");		//returns "eDoeJoeDoe"
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchExpression"> </param>
        ''' <paramname="cBeginDelim"> </param>
        Public Shared Function StrExtract(ByVal cSearchExpression As String, ByVal cBeginDelim As String) As String
            Dim nbpos = At(cBeginDelim, cSearchExpression)
            Return cSearchExpression.Substring(nbpos + cBeginDelim.Length - 1)
        End Function

        ''' <summary>
        ''' Receives a string along with starting and ending delimiters and returns the 
        ''' part of the string between the delimiters
        ''' <pre>
        ''' Example:
        ''' string cExpression = "JoeDoeJoeDoe";
        ''' VFPToolkit.strings.StrExtract(cExpression, "o", "eJ");		//returns "eDo"
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchExpression"> </param>
        ''' <paramname="cBeginDelim"> </param>
        ''' <paramname="cEndDelim"> </param>
        Public Shared Function StrExtract(ByVal cSearchExpression As String, ByVal cBeginDelim As String, ByVal cEndDelim As String) As String
            Return StrExtract(cSearchExpression, cBeginDelim, cEndDelim, 1, 0)
        End Function

        ''' <summary>
        ''' Receives a string along with starting and ending delimiters and returns the 
        ''' part of the string between the delimiters. It also receives a beginning occurence
        ''' to begin the extraction from.
        ''' <pre>
        ''' Example:
        ''' string cExpression = "JoeDoeJoeDoe";
        ''' VFPToolkit.strings.StrExtract(cExpression, "o", "eJ", 2);		//returns ""
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchExpression"> </param>
        ''' <paramname="cBeginDelim"> </param>
        ''' <paramname="cEndDelim"> </param>
        ''' <paramname="nBeginOccurence"> </param>
        Public Shared Function StrExtract(ByVal cSearchExpression As String, ByVal cBeginDelim As String, ByVal cEndDelim As String, ByVal nBeginOccurence As Integer) As String
            Return StrExtract(cSearchExpression, cBeginDelim, cEndDelim, nBeginOccurence, 0)
        End Function


        ''' Private Implementation: This is the actual implementation of the At() and RAt() functions. 
        ''' Receives two strings, the expression in which search is performed and the expression to search for. 
        ''' Also receives an occurence position and the mode (1 or 0) that specifies whether it is a search
        ''' from Left to Right (for At() function)  or from Right to Left (for RAt() function)
        Private Shared Function __at(ByVal cSearchFor As String, ByVal cSearchIn As String, ByVal nOccurence As Integer, ByVal nMode As Integer) As Integer
            'In this case we actually have to locate the occurence
            Dim i = 0
            Dim nOccured = 0
            Dim nPos = 0

            If nMode = 1 Then
                nPos = 0
            Else
                nPos = cSearchIn.Length
            End If

            'Loop through the string and get the position of the requiref occurence
            For i = 1 To nOccurence

                If nMode = 1 Then
                    nPos = cSearchIn.IndexOf(cSearchFor, nPos)
                Else
                    nPos = cSearchIn.LastIndexOf(cSearchFor, nPos)
                End If

                If nPos < 0 Then
                    'This means that we did not find the item
                    Exit For
                Else
                    'Increment the occured counter based on the current mode we are in
                    nOccured += 1

                    'Check if this is the occurence we are looking for
                    If nOccured = nOccurence Then
                        Return nPos + 1
                    Else

                        If nMode = 1 Then
                            nPos += 1
                        Else
                            nPos -= 1
                        End If
                    End If
                End If
            Next
            'We never found our guy if we reached here
            Return 0
        End Function
        ''' <summary>
        ''' Receives two strings as parameters and searches for one string within another. 
        ''' If found, returns the beginning numeric position otherwise returns 0
        ''' <pre>
        ''' Example:
        ''' VFPToolkit.strings.At("D", "Joe Doe");	//returns 5
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchFor"> </param>
        ''' <paramname="cSearchIn"> </param>
        Public Shared Function At(ByVal cSearchFor As String, ByVal cSearchIn As String) As Integer
            Return cSearchIn.IndexOf(cSearchFor) + 1
        End Function

        ''' <summary>
        ''' Receives two strings and an occurence position (1st, 2nd etc) as parameters and 
        ''' searches for one string within another for that position. 
        ''' If found, returns the beginning numeric position otherwise returns 0
        ''' <pre>
        ''' Example:
        ''' VFPToolkit.strings.At("o", "Joe Doe", 1);	//returns 2
        ''' VFPToolkit.strings.At("o", "Joe Doe", 2);	//returns 6
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchFor"> </param>
        ''' <paramname="cSearchIn"> </param>
        ''' <paramname="nOccurence"> </param>
        Public Shared Function At(ByVal cSearchFor As String, ByVal cSearchIn As String, ByVal nOccurence As Integer) As Integer
            Return __at(cSearchFor, cSearchIn, nOccurence, 1)
        End Function


        ''' <summary>
        ''' Receives two strings as parameters and searches for one string within another. 
        ''' This function ignores the case and if found, returns the beginning numeric position 
        ''' otherwise returns 0
        ''' <pre>
        ''' Example:
        ''' VFPToolkit.strings.AtC("d", "Joe Doe");	//returns 5
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchFor"> </param>
        ''' <paramname="cSearchIn"> </param>
        Public Shared Function AtC(ByVal cSearchFor As String, ByVal cSearchIn As String) As Integer
            Return cSearchIn.ToLower().IndexOf(cSearchFor.ToLower()) + 1
        End Function

        ''' <summary>
        ''' Receives two strings and an occurence position (1st, 2nd etc) as parameters and 
        ''' searches for one string within another for that position. This function ignores the
        ''' case of both the strings and if found, returns the beginning numeric position 
        ''' otherwise returns 0.
        ''' <pre>
        ''' Example:
        ''' VFPToolkit.strings.AtC("d", "Joe Doe", 1);	//returns 5
        ''' VFPToolkit.strings.AtC("O", "Joe Doe", 2);	//returns 6
        ''' </pre>
        ''' </summary>
        ''' <paramname="cSearchFor"> </param>
        ''' <paramname="cSearchIn"> </param>
        ''' <paramname="nOccurence"> </param>
        Public Shared Function AtC(ByVal cSearchFor As String, ByVal cSearchIn As String, ByVal nOccurence As Integer) As Integer
            Return __at(cSearchFor.ToLower(), cSearchIn.ToLower(), nOccurence, 1)
        End Function

        Protected Sub SetError(ByVal lcError As String)
            If Equals(lcError, Nothing) OrElse lcError.Length = 0 Then
                bError = False
                cErrormsg = ""
            Else
                cErrormsg = lcError
            End If
        End Sub
    End Class
End Namespace
