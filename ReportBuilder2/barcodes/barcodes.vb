Option Explicit On
Option Strict On
Imports MessagingToolkit.QRCode.Codec

Namespace Barcodes
    Public Class Barcode39
        Private Const WIDEBAR_WIDTH As Short = 2
        Private Const NARROWBAR_WIDTH As Short = 1
        Private Const NUM_CHARACTERS As Integer = 43

        Private mEncoding As Hashtable = New Hashtable
        Dim mCodeValue(NUM_CHARACTERS - 1) As Char

        'Additional properties 
        Public ShowString As Boolean
        Public IncludeCheckSumDigit As Boolean
        Public TextFont As New Font("Courier New", 7)
        Public TextColor As Color = Color.Black

        Public Sub New()
            '        Character, symbol
            mEncoding.Add("*", "bWbwBwBwb")
            mEncoding.Add("-", "bWbwbwBwB")
            mEncoding.Add("$", "bWbWbWbwb")
            mEncoding.Add("%", "bwbWbWbWb")
            mEncoding.Add(" ", "bWBwbwBwb")
            mEncoding.Add(".", "BWbwbwBwb")
            mEncoding.Add("/", "bWbWbwbWb")
            mEncoding.Add("+", "bWbwbWbWb")
            mEncoding.Add("0", "bwbWBwBwb")
            mEncoding.Add("1", "BwbWbwbwB")
            mEncoding.Add("2", "bwBWbwbwB")
            mEncoding.Add("3", "BwBWbwbwb")
            mEncoding.Add("4", "bwbWBwbwB")
            mEncoding.Add("5", "BwbWBwbwb")
            mEncoding.Add("6", "bwBWBwbwb")
            mEncoding.Add("7", "bwbWbwBwB")
            mEncoding.Add("8", "BwbWbwBwb")
            mEncoding.Add("9", "bwBWbwBwb")
            mEncoding.Add("A", "BwbwbWbwB")
            mEncoding.Add("B", "bwBwbWbwB")
            mEncoding.Add("C", "BwBwbWbwb")
            mEncoding.Add("D", "bwbwBWbwB")
            mEncoding.Add("E", "BwbwBWbwb")
            mEncoding.Add("F", "bwBwBWbwb")
            mEncoding.Add("G", "bwbwbWBwB")
            mEncoding.Add("H", "BwbwbWBwb")
            mEncoding.Add("I", "bwBwbWBwb")
            mEncoding.Add("J", "bwbwBWBwb")
            mEncoding.Add("K", "BwbwbwbWB")
            mEncoding.Add("L", "bwBwbwbWB")
            mEncoding.Add("M", "BwBwbwbWb")
            mEncoding.Add("N", "bwbwBwbWB")
            mEncoding.Add("O", "BwbwBwbWb")
            mEncoding.Add("P", "bwBwBwbWb")
            mEncoding.Add("Q", "bwbwbwBWB")
            mEncoding.Add("R", "BwbwbwBWb")
            mEncoding.Add("S", "bwBwbwBWb")
            mEncoding.Add("T", "bwbwBwBWb")
            mEncoding.Add("U", "BWbwbwbwB")
            mEncoding.Add("V", "bWBwbwbwB")
            mEncoding.Add("W", "BWBwbwbwb")
            mEncoding.Add("X", "bWbwBwbwB")
            mEncoding.Add("Y", "BWbwBwbwb")
            mEncoding.Add("Z", "bWBwBwbwb")

            mCodeValue(0) = "0"c
            mCodeValue(1) = "1"c
            mCodeValue(2) = "2"c
            mCodeValue(3) = "3"c
            mCodeValue(4) = "4"c
            mCodeValue(5) = "5"c
            mCodeValue(6) = "6"c
            mCodeValue(7) = "7"c
            mCodeValue(8) = "8"c
            mCodeValue(9) = "9"c
            mCodeValue(10) = "A"c
            mCodeValue(11) = "B"c
            mCodeValue(12) = "C"c
            mCodeValue(13) = "D"c
            mCodeValue(14) = "E"c
            mCodeValue(15) = "F"c
            mCodeValue(16) = "G"c
            mCodeValue(17) = "H"c
            mCodeValue(18) = "I"c
            mCodeValue(19) = "J"c
            mCodeValue(20) = "K"c
            mCodeValue(21) = "L"c
            mCodeValue(22) = "M"c
            mCodeValue(23) = "N"c
            mCodeValue(24) = "O"c
            mCodeValue(25) = "P"c
            mCodeValue(26) = "Q"c
            mCodeValue(27) = "R"c
            mCodeValue(28) = "S"c
            mCodeValue(29) = "T"c
            mCodeValue(30) = "U"c
            mCodeValue(31) = "V"c
            mCodeValue(32) = "W"c
            mCodeValue(33) = "X"c
            mCodeValue(34) = "Y"c
            mCodeValue(35) = "Z"c
            mCodeValue(36) = "-"c
            mCodeValue(37) = "."c
            mCodeValue(38) = " "c
            mCodeValue(39) = "$"c
            mCodeValue(40) = "/"c
            mCodeValue(41) = "+"c
            mCodeValue(42) = "%"c
        End Sub

        Public Function GenerateBarcodeImage(ByVal ImageWidth As Integer,
                                             ByVal ImageHeight As Integer,
                                             ByVal OriginalString As String) As Image

            '-- create a image where to paint the bars
            Dim pb As PictureBox
            pb = New PictureBox
            With pb
                .Width = ImageWidth
                .Height = ImageHeight
                pb.Image = New Bitmap(.Width, .Height)
            End With
            '---------------------

            'clear the image and set it to white background
            Dim g As Graphics = Graphics.FromImage(pb.Image)
            g.Clear(Color.White)


            'get the extended string
            Dim ExtString As String
            ExtString = ExtendedString(OriginalString)


            '-- This part format the sring that will be encoded
            '-- The string needs to be surrounded by asterisks 
            '-- to make it a valid Code39 barcode
            Dim EncodedString As String
            Dim ChkSum As Integer
            If IncludeCheckSumDigit = False Then
                EncodedString = String.Format("{0}{1}{0}", "*", ExtString)
            Else
                ChkSum = CheckSum(ExtString)

                EncodedString = String.Format("{0}{1}{2}{0}",
                                              "*", ExtString, mCodeValue(ChkSum))
            End If
            '----------------------

            '-- write the original string at the bottom if ShowString = True
            Dim textBrush As New SolidBrush(TextColor)
            If ShowString Then
                If Not IsNothing(TextFont) Then
                    'calculates the height of the string
                    Dim H As Single = g.MeasureString(OriginalString, TextFont).Height
                    g.DrawString(OriginalString, TextFont, textBrush, 0, ImageHeight - H)
                    ImageHeight = ImageHeight - CShort(H)
                End If
            End If
            '----------------------------------------

            'THIS IS WHERE THE BARCODE DRAWING HAPPENS
            DrawBarcode(g, EncodedString, ImageHeight)

            'IMAGE OBJECT IS RETURNED
            Return pb.Image


        End Function

        Private Sub DrawBarcode(ByVal g As Graphics,
                                ByVal EncodedString As String,
                                ByVal Height As Integer)

            'Start drawing at 0, 0
            Dim XPosition As Short = 0
            Dim YPosition As Short = 0

            'Dim invalidCharacter As Boolean = False
            Dim CurrentSymbol As String = String.Empty
            Dim EncodedSymbol As String
            '-- draw the bars
            For j As Short = 0 To CShort(EncodedString.Length - 1)
                CurrentSymbol = EncodedString.Chars(j)
                EncodedSymbol = mEncoding(CurrentSymbol).ToString

                For i As Short = 0 To CShort(EncodedSymbol.Length - 1)
                    'Dim CurrentCode As String = EncodedSymbol.Substring(i, 1)
                    Dim CurrentCode As Char = EncodedSymbol.Chars(i)

                    g.FillRectangle(getBCSymbolColor(CurrentCode), XPosition, YPosition, getBCSymbolWidth(CurrentCode), Height)

                    XPosition = XPosition + getBCSymbolWidth(CurrentCode)
                Next

                'After each written full symbol we need a whitespace (narrow width)
                g.FillRectangle(getBCSymbolColor("w"c), XPosition, YPosition, getBCSymbolWidth("w"c), Height)
                XPosition = XPosition + getBCSymbolWidth("w"c)

            Next
            '--------------------------


        End Sub


        Private Function getBCSymbolColor(ByVal symbol As Char) As System.Drawing.Brush
            If symbol = "W"c Or symbol = "w"c Then
                Return Brushes.White
            Else
                Return Brushes.Black
            End If
        End Function

        Private Function getBCSymbolWidth(ByVal symbol As Char) As Short
            If symbol = "B"c Or symbol = "W"c Then
                Return WIDEBAR_WIDTH
            Else
                Return NARROWBAR_WIDTH
            End If
        End Function


        Private Function CheckSum(ByVal sCode As String) As Integer
            Dim CurrentSymbol As Char
            Dim Chk As Integer
            For j As Integer = 0 To sCode.Length - 1
                CurrentSymbol = sCode.Chars(j)
                Chk += GetSymbolValue(CurrentSymbol)
            Next
            Return Chk Mod (NUM_CHARACTERS)
        End Function

        Private Function GetSymbolValue(ByVal s As Char) As Integer
            Dim k As Integer

            For k = 0 To NUM_CHARACTERS - 1
                If mCodeValue(k) = s Then
                    Return k
                End If
            Next
            Return Nothing
        End Function


        Private Function ExtendedString(ByVal s As String) As String
            Dim Ch As Char
            Dim KeyChar As Integer
            Dim retVal As String = ""

            For Each Ch In s
                KeyChar = Asc(Ch)
                Select Case KeyChar
                    Case 0
                        retVal &= "%U"
                    Case 1 To 26
                        retVal &= "$" & Chr(64 + KeyChar)
                    Case 27 To 31
                        retVal &= "%" & Chr(65 - 27 + KeyChar)
                    Case 33 To 44
                        retVal &= "/" & Chr(65 - 33 + KeyChar)
                    Case 47
                        retVal &= "/O"
                    Case 58
                        retVal &= "/Z"
                    Case 59 To 63
                        retVal &= "%" & Chr(70 - 59 + KeyChar)
                    Case 64
                        retVal &= "%V"
                    Case 91 To 95
                        retVal &= "%" & Chr(75 - 91 + KeyChar)
                    Case 96
                        retVal &= "%W"
                    Case 97 To 122
                        retVal &= "+" & Chr(65 - 97 + KeyChar)
                    Case 123 To 127
                        retVal &= "%" & Chr(80 - 123 + KeyChar)
                    Case Else
                        retVal &= Ch
                End Select

            Next
            Return retVal

        End Function
    End Class

    Public Class Barcode128
        Dim BINARY As String
        Dim CheckSumVal As Integer
        Dim BINVAL As String
        Dim BINARYLength As Int32
        Dim bmpBarcode As Bitmap
        Dim bSize As Integer = 0
        Dim bLen As Integer = 9
        Dim bColor As Color = Color.White
        Dim FontSize As Integer = 0
        Dim LengthOfBarcode As Integer = 50

        Public ShowString As Boolean
        Public IncludeCheckSumDigit As Boolean
        Public TextFont As New Font("Courier New", 7)
        Public TextColor As Color = Color.Black

        Private Const WIDEBAR_WIDTH As Short = 2
        Private Const NARROWBAR_WIDTH As Short = 1
        Private Const NUM_CHARACTERS As Integer = 43

        Private mEncoding As Hashtable = New Hashtable

        Public Sub New()

            bColor = Color.White
            FontSize = 1
            LengthOfBarcode = 50
            bSize = 0
            bLen = 9

        End Sub

        'Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

        '    GenerateBINARY(txtInput.Text)
        '    GenerateBarcode(BINARY)

        'End Sub

        Private Function GenerateBINARY(Input As String) As String
            Dim CharVal As Integer = 0
            Dim CharCount As Integer = 0
            Dim GrandCharCount As Integer = 0
            Dim CheckSum As Integer = 0
            Dim BINARY As String = ""
            Dim cItem As String

            For Each cItem In Input

                Select Case cItem
                    Case " "
                        BINARY = BINARY & "11011001100"
                        CharVal = 0
                    Case "!"
                        BINARY = BINARY & "11001101100"
                        CharVal = 1
                    Case """"
                        BINARY = BINARY & "11001100110"
                        CharVal = 2
                    Case "#"
                        BINARY = BINARY & "10010011000"
                        CharVal = 3
                    Case "$"
                        BINARY = BINARY & "10010001100"
                        CharVal = 4
                    Case "%"
                        BINARY = BINARY & "10001001100"
                        CharVal = 5
                    Case "&"
                        BINARY = BINARY & "10011001000"
                        CharVal = 6
                    Case "'"
                        BINARY = BINARY & "10011000100"
                        CharVal = 7
                    Case "("
                        BINARY = BINARY & "10001100100"
                        CharVal = 8
                    Case ")"
                        BINARY = BINARY & "11001001000"
                        CharVal = 9
                    Case "*"
                        BINARY = BINARY & "11001000100"
                        CharVal = 10
                    Case "+"
                        BINARY = BINARY & "11000100100"
                        CharVal = 11
                    Case ","
                        BINARY = BINARY & "10110011100"
                        CharVal = 12
                    Case "-"
                        BINARY = BINARY & "10011011100"
                        CharVal = 13
                    Case "."
                        BINARY = BINARY & "10011001110"
                        CharVal = 14
                    Case "/"
                        BINARY = BINARY & "10111001100"
                        CharVal = 15
                    Case "0"
                        BINARY = BINARY & "10011101100"
                        CharVal = 16
                    Case "1"
                        BINARY = BINARY & "10011100110"
                        CharVal = 17
                    Case "2"
                        BINARY = BINARY & "11001110010"
                        CharVal = 18
                    Case "3"
                        BINARY = BINARY & "11001011100"
                        CharVal = 19
                    Case "4"
                        BINARY = BINARY & "11001001110"
                        CharVal = 20
                    Case "5"
                        BINARY = BINARY & "11011100100"
                        CharVal = 21
                    Case "6"
                        BINARY = BINARY & "11001110100"
                        CharVal = 22
                    Case "7"
                        BINARY = BINARY & "11101101110"
                        CharVal = 23
                    Case "8"
                        BINARY = BINARY & "11101001100"
                        CharVal = 24
                    Case "9"
                        BINARY = BINARY & "11100101100"
                        CharVal = 25
                    Case ":"
                        BINARY = BINARY & "11100100110"
                        CharVal = 26
                    Case " ;"
                        BINARY = BINARY & "11101100100"
                        CharVal = 27
                    Case "<"
                        BINARY = BINARY & "11100110100"
                        CharVal = 28
                    Case "="
                        BINARY = BINARY & "11100110010"
                        CharVal = 29
                    Case ">"
                        BINARY = BINARY & "11011011000"
                        CharVal = 30
                    Case " ?"
                        BINARY = BINARY & "11011000110"
                        CharVal = 31
                    Case "@"
                        BINARY = BINARY & "11000110110"
                        CharVal = 32
                    Case "A"
                        BINARY = BINARY & "10100011000"
                        CharVal = 33
                    Case "B"
                        BINARY = BINARY & "10001011000"
                        CharVal = 34
                    Case "C"
                        BINARY = BINARY & "10001000110"
                        CharVal = 35
                    Case "D"
                        BINARY = BINARY & "10110001000"
                        CharVal = 36
                    Case "E"
                        BINARY = BINARY & "10001101000"
                        CharVal = 37
                    Case "F"
                        BINARY = BINARY & "10001100010"
                        CharVal = 38
                    Case "G"
                        BINARY = BINARY & "11010001000"
                        CharVal = 39
                    Case "H"
                        BINARY = BINARY & "11000101000"
                        CharVal = 40
                    Case "I"
                        BINARY = BINARY & "11000100010"
                        CharVal = 41
                    Case "J"
                        BINARY = BINARY & "10110111000"
                        CharVal = 42
                    Case "K"
                        BINARY = BINARY & "10110001110"
                        CharVal = 43
                    Case "L"
                        BINARY = BINARY & "10001101110"
                        CharVal = 44
                    Case "M"
                        BINARY = BINARY & "10111011000"
                        CharVal = 45
                    Case "N"
                        BINARY = BINARY & "10111000110"
                        CharVal = 46
                    Case "O"
                        BINARY = BINARY & "10001110110"
                        CharVal = 47
                    Case "P"
                        BINARY = BINARY & "11101110110"
                        CharVal = 48
                    Case "Q"
                        BINARY = BINARY & "11010001110"
                        CharVal = 49
                    Case "R"
                        BINARY = BINARY & "11000101110"
                        CharVal = 50
                    Case "S"
                        BINARY = BINARY & "11011101000"
                        CharVal = 51
                    Case "T"
                        BINARY = BINARY & "11011100010"
                        CharVal = 52
                    Case "U"
                        BINARY = BINARY & "11011101110"
                        CharVal = 53
                    Case "V"
                        BINARY = BINARY & "11101011000"
                        CharVal = 54
                    Case "W"
                        BINARY = BINARY & "11101000110"
                        CharVal = 55
                    Case "X"
                        BINARY = BINARY & "11100010110"
                        CharVal = 56
                    Case "Y"
                        BINARY = BINARY & "11101101000"
                        CharVal = 57
                    Case "Z"
                        BINARY = BINARY & "11101100010"
                        CharVal = 58
                    Case "["
                        BINARY = BINARY & "11100011010"
                        CharVal = 59
                    Case "\"
                        BINARY = BINARY & "11101111010"
                        CharVal = 60
                    Case "]"
                        BINARY = BINARY & "11001000010"
                        CharVal = 61
                    Case "^"
                        BINARY = BINARY & "11110001010"
                        CharVal = 62
                    Case "_"
                        BINARY = BINARY & "10100110000"
                        CharVal = 63
                    Case "`"
                        BINARY = BINARY & "10100001100"
                        CharVal = 64
                    Case "a"
                        BINARY = BINARY & "10010110000"
                        CharVal = 65
                    Case "b"
                        BINARY = BINARY & "10010000110"
                        CharVal = 66
                    Case "c"
                        BINARY = BINARY & "10000101100"
                        CharVal = 67
                    Case "d"
                        BINARY = BINARY & "10000100110"
                        CharVal = 68
                    Case "e"
                        BINARY = BINARY & "10110010000"
                        CharVal = 69
                    Case "f"
                        BINARY = BINARY & "10110000100"
                        CharVal = 70
                    Case "g"
                        BINARY = BINARY & "10011010000"
                        CharVal = 71
                    Case "h"
                        BINARY = BINARY & "10011000010"
                        CharVal = 72
                    Case "i"
                        BINARY = BINARY & "10000110100"
                        CharVal = 73
                    Case "j"
                        BINARY = BINARY & "10000110010"
                        CharVal = 74
                    Case "k"
                        BINARY = BINARY & "11000010010"
                        CharVal = 75
                    Case "l"
                        BINARY = BINARY & "11001010000"
                        CharVal = 76
                    Case "m"
                        BINARY = BINARY & "11110111010"
                        CharVal = 77
                    Case "n"
                        BINARY = BINARY & "11000010100"
                        CharVal = 78
                    Case "o"
                        BINARY = BINARY & "10001111010"
                        CharVal = 79
                    Case "p"
                        BINARY = BINARY & "10100111100"
                        CharVal = 80
                    Case "q"
                        BINARY = BINARY & "10010111100"
                        CharVal = 81
                    Case "r"
                        BINARY = BINARY & "10010011110"
                        CharVal = 82
                    Case "s"
                        BINARY = BINARY & "10111100100"
                        CharVal = 83
                    Case "t"
                        BINARY = BINARY & "10011110100"
                        CharVal = 84
                    Case "u"
                        BINARY = BINARY & "10011110010"
                        CharVal = 85
                    Case "v"
                        BINARY = BINARY & "11110100100"
                        CharVal = 86
                    Case "w"
                        BINARY = BINARY & "11110010100"
                        CharVal = 87
                    Case "x"
                        BINARY = BINARY & "11110010010"
                        CharVal = 88
                    Case "y"
                        BINARY = BINARY & "11011011110"
                        CharVal = 89
                    Case "z"
                        BINARY = BINARY & "11011110110"
                        CharVal = 90
                    Case "{"
                        BINARY = BINARY & "11110110110"
                        CharVal = 91
                    Case "|"
                        BINARY = BINARY & "10101111000"
                        CharVal = 92
                    Case "}"
                        BINARY = BINARY & "10100011110"
                        CharVal = 93
                    Case "~"
                        BINARY = BINARY & "10001011110"
                        CharVal = 94
                End Select
                CharCount = CharCount + 1
                GrandCharCount = CharVal * CharCount
                CheckSum = CheckSum + GrandCharCount

            Next

            'Include start code B 
            CheckSum = CheckSum + 104
            'Get the remander
            CheckSumVal = CheckSum Mod 103

            'Set it to zero before we generate it
            BINVAL = "0"
            GenerateCheckSum(CheckSumVal)

            'Start
            BINARY = "00011010010000" & BINARY
            'End
            BINARY = BINARY & BINVAL & "1100011101011000"

            BINARYLength = 0
            For Each dig As String In BINARY
                CharCount = CharCount + 1
            Next

            Return BINARY

        End Function

        Private Sub GenerateCheckSum(Dig As Integer)


            Select Case CStr(Dig)
                Case "0"
                    BINVAL = "11011001100"
                Case "1"
                    BINVAL = "11001101100"
                Case "2"
                    BINVAL = "11001100110"
                Case "3"
                    BINVAL = "10010011000"
                Case "4"
                    BINVAL = "10010001100"
                Case "5"
                    BINVAL = "10001001100"
                Case "6"
                    BINVAL = "10011001000"
                Case "7"
                    BINVAL = "10011000100"
                Case "8"
                    BINVAL = "10001100100"
                Case "9"
                    BINVAL = "11001001000"
                Case "10"
                    BINVAL = "11001000100"
                Case "11"
                    BINVAL = "11000100100"
                Case "12"
                    BINVAL = "10110011100"
                Case "13"
                    BINVAL = "10011011100"
                Case "14"
                    BINVAL = "10011001110"
                Case "15"
                    BINVAL = "10111001100"
                Case "16"
                    BINVAL = "10011101100"
                Case "17"
                    BINVAL = "10011100110"
                Case "18"
                    BINVAL = "11001110010"
                Case "19"
                    BINVAL = "11001011100"
                Case "20"
                    BINVAL = "11001001110"
                Case "21"
                    BINVAL = "11011100100"
                Case "22"
                    BINVAL = "11001110100"
                Case "23"
                    BINVAL = "11101101110"
                Case "24"
                    BINVAL = "11101001100"
                Case "25"
                    BINVAL = "11100101100"
                Case "26"
                    BINVAL = "11100100110"
                Case "27"
                    BINVAL = "11101100100"
                Case "28"
                    BINVAL = "11100110100"
                Case "29"
                    BINVAL = "11100110010"
                Case "30"
                    BINVAL = "11011011000"
                Case "31"
                    BINVAL = "11011000110"
                Case "32"
                    BINVAL = "11000110110"
                Case "33"
                    BINVAL = "10100011000"
                Case "34"
                    BINVAL = "10001011000"
                Case "35"
                    BINVAL = "10001000110"
                Case "36"
                    BINVAL = "10110001000"
                Case "37"
                    BINVAL = "10001101000"
                Case "38"
                    BINVAL = "10001100010"
                Case "39"
                    BINVAL = "11010001000"
                Case "40"
                    BINVAL = "11000101000"
                Case "41"
                    BINVAL = "11000100010"
                Case "42"
                    BINVAL = "10110111000"
                Case "43"
                    BINVAL = "10110001110"
                Case "44"
                    BINVAL = "10001101110"
                Case "45"
                    BINVAL = "10111011000"
                Case "46"
                    BINVAL = "10111000110"
                Case "47"
                    BINVAL = "10001110110"
                Case "48"
                    BINVAL = "11101110110"
                Case "49"
                    BINVAL = "11010001110"
                Case "50"
                    BINVAL = "11000101110"
                Case "51"
                    BINVAL = "11011101000"
                Case "52"
                    BINVAL = "11011100010"
                Case "53"
                    BINVAL = "11011101110"
                Case "54"
                    BINVAL = "11101011000"
                Case "55"
                    BINVAL = "11101000110"
                Case "56"
                    BINVAL = "11100010110"
                Case "57"
                    BINVAL = "11101101000"
                Case "58"
                    BINVAL = "11101100010"
                Case "59"
                    BINVAL = "11100011010"
                Case "60"
                    BINVAL = "11101111010"
                Case "61"
                    BINVAL = "11001000010"
                Case "62"
                    BINVAL = "11110001010"
                Case "63"
                    BINVAL = "10100110000"
                Case "64"
                    BINVAL = "10100001100"
                Case "65"
                    BINVAL = "10010110000"
                Case "66"
                    BINVAL = "10010000110"
                Case "67"
                    BINVAL = "10000101100"
                Case "68"
                    BINVAL = "10000100110"
                Case "69"
                    BINVAL = "10110010000"
                Case "70"
                    BINVAL = "10110000100"
                Case "71"
                    BINVAL = "10011010000"
                Case "72"
                    BINVAL = "10011000010"
                Case "73"
                    BINVAL = "10000110100"
                Case "74"
                    BINVAL = "10000110010"
                Case "75"
                    BINVAL = "11000010010"
                Case "76"
                    BINVAL = "11001010000"
                Case "77"
                    BINVAL = "11110111010"
                Case "78"
                    BINVAL = "11000010100"
                Case "79"
                    BINVAL = "10001111010"
                Case "80"
                    BINVAL = "10100111100"
                Case "81"
                    BINVAL = "10010111100"
                Case "82"
                    BINVAL = "10010011110"
                Case "83"
                    BINVAL = "10111100100"
                Case "84"
                    BINVAL = "10011110100"
                Case "85"
                    BINVAL = "10011110010"
                Case "86"
                    BINVAL = "11110100100"
                Case "87"
                    BINVAL = "11110010100"
                Case "88"
                    BINVAL = "11110010010"
                Case "89"
                    BINVAL = "11011011110"
                Case "90"
                    BINVAL = "11011110110"
                Case "91"
                    BINVAL = "11110110110"
                Case "92"
                    BINVAL = "10101111000"
                Case "93"
                    BINVAL = "10100011110"
                Case "94"
                    BINVAL = "10001011110"

            End Select



        End Sub


        'Private Sub GenerateBarcode(Input As String)
        '    Dim num As Integer = 0
        '    For Each one As String In Input
        '        num = num + bSize
        '    Next

        '    Dim rec As New Rectangle(1, 1, num, bLen)
        '    Dim img As New Bitmap(num, Convert.ToInt32(bLen))
        '    Dim count As Integer = 0
        '    Dim length As Integer = 0
        '    Dim aBlackPen As New Pen(Color.Black)
        '    Dim aWhitePen As New Pen(Color.White)

        '    aBlackPen.Width = bSize
        '    aWhitePen.Width = bSize

        '    length = length + bLen

        '    For Each item As String In Input
        '        count = count + bSize


        '        If CInt(item) = 1 Then
        '            picBarcode.CreateGraphics.DrawLine(aBlackPen, count, 1, count, length)

        '        Else
        '            picBarcode.CreateGraphics.DrawLine(aWhitePen, count, 1, count, length)

        '        End If

        '    Next

        '    picBarcode.DrawToBitmap(img, rec)
        '    bmpBarcode = img

        'End Sub

        Public Function GenerateBarcodeImage(ByVal ImageWidth As Integer,
                                             ByVal ImageHeight As Integer,
                                             ByVal OriginalString As String) As Image

            '-- create a image where to paint the bars
            Dim cItem As String
            Dim pb As PictureBox
            Dim BINARY As String
            pb = New PictureBox

            With pb
                .Width = ImageWidth
                .Height = ImageHeight
                pb.Image = New Bitmap(.Width, .Height)
            End With
            '---------------------
            BINARY = GenerateBINARY(Trim(OriginalString))

            'clear the image and set it to white background
            Dim g As Graphics = Graphics.FromImage(pb.Image)
            g.Clear(Color.White)

            Dim num As Integer = 0
            For Each one As Char In BINARY
                num = num + getBCSymbolWidth(one)
            Next

            Dim rec As New Rectangle(1, 1, num, ImageHeight) 'bLen)
            Dim img As New Bitmap(num, ImageHeight) 'bLen) 'Convert.ToInt32(bLen))
            Dim count As Integer = 0
            Dim length As Integer = 0
            Dim aBlackPen As New Pen(Color.Black)
            Dim aWhitePen As New Pen(Color.White)

            '-- write the original string at the bottom if ShowString = True
            Dim textBrush As New SolidBrush(TextColor)
            If ShowString Then
                If Not IsNothing(TextFont) Then
                    'calculates the height of the string
                    Dim H As Single = g.MeasureString(OriginalString, TextFont).Height
                    g.DrawString(OriginalString, TextFont, textBrush, 0, ImageHeight - H)
                    ImageHeight = ImageHeight - CShort(H)
                End If
            End If

            DrawBarcode(g, BINARY, ImageHeight)
            'MsgBox(BINARY)
            'aBlackPen.Width = bSize
            'aWhitePen.Width = bSize

            ''length = length + bLen
            'length = 10

            'For Each cItemb As Char In BINARY
            '    count = count + getBCSymbolWidth(cItemb)
            '    If cItemb = "1" Then
            '        pb.CreateGraphics.DrawLine(aBlackPen, count, 1, count, length)
            '    Else
            '        pb.CreateGraphics.DrawLine(aWhitePen, count, 2, count, length)
            '    End If
            'Next

            Return pb.Image

        End Function

        Private Function getBCSymbolWidth(ByVal symbol As Char) As Short
            If symbol = "0"c Then
                Return NARROWBAR_WIDTH 'WIDEBAR_WIDTH
            Else
                Return NARROWBAR_WIDTH
            End If
        End Function

        Private Sub DrawBarcode(ByVal g As Graphics,
                                ByVal EncodedString As String,
                                ByVal Height As Integer)

            'Start drawing at 0, 0
            Dim XPosition As Short = 0
            Dim YPosition As Short = 0

            'Dim invalidCharacter As Boolean = False
            Dim CurrentSymbol As String = String.Empty
            '-- draw the bars
            'For j As Short = 0 To CShort(EncodedString.Length - 1)
            For Each cItemb As Char In EncodedString
                g.FillRectangle(getBCSymbolColor(cItemb), XPosition, YPosition, getBCSymbolWidth(cItemb), Height)
                XPosition = XPosition + getBCSymbolWidth(cItemb)
            Next
            '--------------------------
        End Sub

        Private Function getBCSymbolColor(ByVal symbol As Char) As System.Drawing.Brush
            If symbol = "0"c Then
                Return Brushes.White
            Else
                Return Brushes.Black
            End If
        End Function


    End Class

    Public Class Barcodeean13

        Private Structure Ean13Tables
            Public TableA As String
            Public TableB As String
            Public TableC As String
        End Structure
        Private Tables(0 To 9) As Ean13Tables
        Private BarcodeValue As System.Text.StringBuilder
        Dim ResizeRedraw As Boolean
        Dim M_BarWidth As Double
        Private m_BarcodeText As String
        Private m_CheckSum As Byte
        Public ShowString As Boolean
        Public IncludeCheckSumDigit As Boolean
        Public TextFont As New Font("Courier New", 7)
        Public TextColor As Color = Color.Black


        Const StartMark As String = "101"
        Const SplittingMark As String = "01010"
        Const EndMark As String = "101"





        Public Sub New()

            ResizeRedraw = True
            InitBarcode()
            InitEAN13Tables()
            M_BarWidth = 0.33 ' mm
            TextFont = New Font("Arial", 18)

        End Sub


        Private Sub InitBarcode()
            m_BarcodeText = "000000000000"
        End Sub

        Public Sub InitEAN13Tables()
            '          Zero
            Tables(0).TableA = "0001101"
            Tables(0).TableB = "0100111"
            Tables(0).TableC = "1110010"
            '          One
            Tables(1).TableA = "0011001"
            Tables(1).TableB = "0110011"
            Tables(1).TableC = "1100110"
            '          Two
            Tables(2).TableA = "0010011"
            Tables(2).TableB = "0011011"
            Tables(2).TableC = "1101100"
            '          Three
            Tables(3).TableA = "0111101"
            Tables(3).TableB = "0100001"
            Tables(3).TableC = "1000010"
            '          Four
            Tables(4).TableA = "0100011"
            Tables(4).TableB = "0011101"
            Tables(4).TableC = "1011100"
            '          Five
            Tables(5).TableA = "0110001"
            Tables(5).TableB = "0111001"
            Tables(5).TableC = "1001110"
            '          Six
            Tables(6).TableA = "0101111"
            Tables(6).TableB = "0000101"
            Tables(6).TableC = "1010000"
            '          Seven
            Tables(7).TableA = "0111011"
            Tables(7).TableB = "0010001"
            Tables(7).TableC = "1000100"
            '          Eight
            Tables(8).TableA = "0110111"
            Tables(8).TableB = "0001001"
            Tables(8).TableC = "1001000"
            '          Nine
            Tables(9).TableA = "0001011"
            Tables(9).TableB = "0010111"
            Tables(9).TableC = "1110100"

        End Sub

        Private Function CalculateCheckSum() As Boolean
            Dim X As Integer = 0
            Dim Y As Integer = 0
            Dim j As Integer = 11

            Try
                For i As Integer = 1 To 12
                    If i Mod 2 = 0 Then
                        X += Val(m_BarcodeText(j))
                    Else
                        Y += Val(m_BarcodeText(j))
                    End If
                    j -= 1
                Next

                Dim Z As Integer = X + (3 * Y)

                'first way
                m_CheckSum = CByte((10 - (Z Mod 10)) Mod 10)

                Return True
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End Function

        Private Function CalculateValue() As Boolean
            ' Clear any previous Value
            BarcodeValue = New System.Text.StringBuilder(95)
            Try
                ' Add The Start Mark
                BarcodeValue.Append(StartMark)
                Select Case CStr(m_BarcodeText(0))
                    Case "0"
                        For i As Integer = 1 To 6
                            BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                        Next
                    Case "1"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 2) Or (i = 4) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                    Case "2"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 2) Or (i = 5) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                    Case "3"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 2) Or (i = 6) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                    Case "4"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 3) Or (i = 4) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                    Case "5"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 4) Or (i = 5) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                    Case "6"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 5) Or (i = 6) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                    Case "7"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 3) Or (i = 5) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                    Case "8"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 3) Or (i = 6) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                    Case "9"
                        For i As Integer = 1 To 6
                            If (i = 1) Or (i = 4) Or (i = 6) Then
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableA)
                            Else
                                BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableB)
                            End If
                        Next
                End Select
                ' Add The Splitting Mark
                BarcodeValue.Append(SplittingMark)

                For i As Integer = 7 To (m_BarcodeText.Length - 1)
                    BarcodeValue.Append(Tables(Val(m_BarcodeText(i))).TableC)
                Next
                ' Add Checksum
                BarcodeValue.Append(Tables(CheckSum).TableC)
                ' Add The End Mark
                BarcodeValue.Append(EndMark)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function


        Private Function DrawBarcodeText(ByVal e As Graphics, ByVal ImageHeight As Integer) As Integer
            ' Create font and brush. 
            'Dim drawFont As New Font("Arial", 18)
            'Dim drawFont As Font = TextFont
            Dim drawBrush As New SolidBrush(TextColor)

            '' Create rectangle for drawing. 
            'Dim x As Single = 3.61
            'Dim y As Single = (30 + (5 * 0.33F)) '31.4F

            '' Create string to draw. 
            'Dim drawString As [String] = m_BarcodeText
            'If IncludeCheckSumDigit = True Then
            '    drawString += CStr(CheckSum)
            '    x -= 1.2F
            'End If

            '' Measure string. 
            'Dim stringSize As New SizeF
            'stringSize = e.MeasureString(drawString, drawFont)

            'Dim drawRect As New RectangleF(x, y, stringSize.Width, stringSize.Height)

            '' Set format of string. 
            'Dim drawFormat As New StringFormat
            'drawFormat.Alignment = StringAlignment.Center

            '' Draw string to screen.
            'e.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat)

            If ShowString Then
                If Not IsNothing(TextFont) Then
                    'calculates the height of the string
                    Dim H As Single = e.MeasureString(m_BarcodeText, TextFont).Height
                    e.DrawString(m_BarcodeText, TextFont, drawBrush, 0, ImageHeight - H)
                    ImageHeight = ImageHeight - CShort(H)
                End If
            End If

            Return ImageHeight

        End Function

        Private Sub EAN13Barcode_Paint(ByVal e As Graphics, ByVal txt As String, ByVal ImageHeight As Integer)
            CalculateValue()
            ' Change the page scale.  
            e.PageUnit = GraphicsUnit.Millimeter
            ' Dim units As GraphicsUnit = GraphicsUnit.Millimeter
            Dim s As Single = 0
            Dim h As Integer

            h = ImageHeight - 60

            If ShowString = True Then
                h = DrawBarcodeText(e, h)
            End If

            Try
                For i As Integer = 0 To 94
                    If BarcodeValue(i) = "1" Then
                        Select Case i
                    ' Case 94
                    '    e.Graphics.FillRectangle(New SolidBrush(Me.ForeColor), s + 0.2F, 10, 0.33F, (15 + (5 * 0.33F)))
                            Case 0, 1, 2, 45, 46, 47, 48, 49, 92, 93, 94
                                e.FillRectangle(New SolidBrush(TextColor), s + 0.11F, 0, 0.5F, (CSng(h) + (5 * 0.33F)))
                            Case Else
                                e.FillRectangle(New SolidBrush(TextColor), s + 0.11F, 0, 0.5F, CSng(h))
                        End Select
                    ElseIf BarcodeValue(i) = "0" Then
                        Select Case i
                            Case 0, 1, 2, 45, 46, 47, 48, 49, 92, 93, 94
                                e.FillRectangle(Brushes.White, s + 0.11F, 0, 0.5F, (CSng(h) + (5 * 0.33F)))
                            Case Else
                                e.FillRectangle(Brushes.White, s + 0.11F, 0, 0.5F, CSng(h))
                        End Select
                    End If
                    s += 0.5F
                Next

            Catch ex As Exception

            End Try


        End Sub

        Public Function GenerateBarcodeImage(ByVal ImageWidth As Integer,
                                             ByVal ImageHeight As Integer,
                                             ByVal OriginalString As String) As Image

            '-- create a image where to paint the bars
            Dim pb As PictureBox
            pb = New PictureBox

            With pb
                .Width = ImageWidth
                .Height = ImageHeight
                pb.Image = New Bitmap(.Width, .Height)
            End With

            m_BarcodeText = OriginalString

            'MsgBox(m_BarcodeText)
            '---------------------

            'clear the image and set it to white background
            Dim g As Graphics = Graphics.FromImage(pb.Image)
            g.Clear(Color.White)

            EAN13Barcode_Paint(g, OriginalString, ImageHeight)

            Return pb.Image

        End Function

        Public ReadOnly Property CheckSum() As Byte
            Get
                CalculateCheckSum()
                Return m_CheckSum
            End Get
        End Property

    End Class

    Class BarcodeQR
        'Additional properties 
        Public ShowString As Boolean
        Public IncludeCheckSumDigit As Boolean
        Public TextFont As New Font("Courier New", 7)
        Public TextColor As Color = Color.Black

        Public Function GenerateBarcodeImage(ByVal ImageWidth As Integer, ByVal ImageHeight As Integer, ByVal OriginalString As String) As Image
            Dim pb As PictureBox
            pb = New PictureBox

            With pb
                .Width = ImageWidth
                .Height = ImageHeight
                pb.Image = New Bitmap(.Width, .Height)
            End With

            'clear the image and set it to white background
            Dim g As Graphics = Graphics.FromImage(pb.Image)
            g.Clear(Color.White)

            Try
                Dim qrCode As New QRCodeEncoder
                qrCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
                qrCode.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
                pb.Image = qrCode.Encode(OriginalString, System.Text.Encoding.UTF8)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

            Return pb.Image

        End Function


    End Class
End Namespace
