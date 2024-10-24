Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.IO

Public Class RawPrinterHelper
    ' Structure and API declarations:
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Class DOCINFOA
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDocName As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDataType As String
    End Class
    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA",
    SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True,
    CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function OpenPrinter(<MarshalAs(UnmanagedType.LPStr)>
    szPrinter As String, ByRef hPrinter As IntPtr, pd As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter",
    SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function ClosePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA",
    SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True,
    CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function StartDocPrinter(hPrinter As IntPtr, level As Int32,
    <[In](), MarshalAs(UnmanagedType.LPStruct)> di As DOCINFOA) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter",
    SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function EndDocPrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter",
    SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function StartPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter",
    SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function EndPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="WritePrinter",
    SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WritePrinter(hPrinter As IntPtr, pBytes As IntPtr,
    dwCount As Int32, ByRef dwWritten As Int32) As Boolean
    End Function

    Private hPrinter As New IntPtr(0)
    Private di As New DOCINFOA()
    Private PrinterOpen As Boolean = False

    Public ReadOnly Property PrinterIsOpen As Boolean
        Get
            PrinterIsOpen = PrinterOpen
        End Get
    End Property

    Public Function OpenPrint(szPrinterName As String) As Boolean
        If PrinterOpen = False Then
            di.pDocName = ".NET RAW Document"
            di.pDataType = "RAW"

            If OpenPrinter(szPrinterName.Normalize(), hPrinter, IntPtr.Zero) Then
                ' Start a document.
                If StartDocPrinter(hPrinter, 1, di) Then
                    If StartPagePrinter(hPrinter) Then
                        PrinterOpen = True
                    End If
                End If
            End If
        End If

        OpenPrint = PrinterOpen
    End Function

    Public Sub ClosePrint()
        If PrinterOpen Then
            EndPagePrinter(hPrinter)
            EndDocPrinter(hPrinter)
            ClosePrinter(hPrinter)
            PrinterOpen = False
        End If
    End Sub

    Public Function SendStringToPrinter(szPrinterName As String, szString As String) As Boolean
        If PrinterOpen Then
            Dim pBytes As IntPtr
            Dim dwCount As Int32
            Dim dwWritten As Int32 = 0

            dwCount = szString.Length

            pBytes = Marshal.StringToCoTaskMemAnsi(szString)

            SendStringToPrinter = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)

            Marshal.FreeCoTaskMem(pBytes)
        Else
            SendStringToPrinter = False
        End If
    End Function

End Class
'Imports System
'Imports System.IO
'    Imports System.Net
'    Imports System.Net.Sockets
'    Imports System.Threading
'    Imports System.Collections
'    Imports System.Collections.Generic

'    Public Class BinaryPrint
'        Private Const cPort As Integer = 515
'    Private Const cLineFeed As String = vbLf
'    Private Const cDefaultByteSize As Integer = 4
'        Public Shared ErrorMessage As String = String.Empty
'        Private Shared mHost As String
'        Private Shared mQueue As String
'        Private Shared mUser As String
'        Private Shared ReadOnly mPrintQueue As Queue = New Queue()
'        Private Shared ReadOnly mLastPrintId As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)()

'        Public Shared Function PrintBinaryFile(ByVal filePath As String, ByVal printerName As String, ByVal queueName As String, ByVal userName As String) As Boolean
'            Try
'                mHost = printerName
'                mQueue = queueName
'                mUser = userName
'                BeginPrint(filePath)
'            Catch ex As Exception
'                ErrorMessage += ex.Message & cLineFeed & ex.StackTrace
'            End Try

'            Return ErrorMessage.Length <= 0
'        End Function

'        Private Shared Sub BeginPrint(ByVal filePath As String)
'            mPrintQueue.Enqueue(filePath)
'            Dim myThreadDelegate As ThreadStart = AddressOf SendFileToPrinter
'            Dim myThread = New Thread(myThreadDelegate)
'            myThread.Start()
'        End Sub

'        Private Shared Sub SendFileToPrinter()
'            ErrorMessage = String.Empty
'            Dim fileFromQueue = CStr(mPrintQueue.Dequeue())
'            Dim tcpClient = New TcpClient()
'            tcpClient.Connect(mHost, cPort)
'        Const space As String = " "

'        Using networkStream = tcpClient.GetStream()

'                If Not networkStream.CanWrite Then
'                    ErrorMessage = "NetworkStream.CanWrite failed"
'                    networkStream.Close()
'                    tcpClient.Close()
'                    Return
'                End If

'                Dim thisPc = Dns.GetHostName()
'                Dim printId = GetPrintId()
'                Dim dfA = String.Format("dfA{0}{1}", printId, thisPc)
'                Dim cfA = String.Format("cfA{0}{1}", printId, thisPc)
'                Dim controlFile = String.Format("H{0}" & vbLf & "P{1}" & vbLf & "{5}{2}" & vbLf & "U{3}" & vbLf & "N{4}" & vbLf, thisPc, mUser, dfA, dfA, Path.GetFileName(fileFromQueue), True)
'                Const bufferSize As Integer = (cDefaultByteSize * 1024)
'                Dim buffer = New Byte(4095) {}
'                Dim acknowledgement = New Byte(3) {}
'                Dim position = 0
'                buffer(Math.Min(System.Threading.Interlocked.Increment(position), position - 1)) = 2
'                ProcessBuffer(mQueue, buffer, position, CByte(cLineFeed))
'                If Not IsAcknowledgementValid(buffer, position, acknowledgement, networkStream, tcpClient, "No response from printer") Then Return
'                position = 0
'                buffer(Math.Min(System.Threading.Interlocked.Increment(position), position - 1)) = 2
'                Dim cFileLength = controlFile.Length.ToString()
'                ProcessBuffer(cFileLength, buffer, position, CByte(space))
'                ProcessBuffer(cfA, buffer, position, CByte(cLineFeed))
'                If Not IsAcknowledgementValid(buffer, position, acknowledgement, networkStream, tcpClient, "Error on control file len") Then Return
'                position = 0
'                ProcessBuffer(controlFile, buffer, position, 0)
'                If Not IsAcknowledgementValid(buffer, position, acknowledgement, networkStream, tcpClient, "Error on control file") Then Return
'                position = 0
'                buffer(Math.Min(System.Threading.Interlocked.Increment(position), position - 1)) = 3
'                Dim dataFileInfo = New FileInfo(fileFromQueue)
'                cFileLength = dataFileInfo.Length.ToString()
'                ProcessBuffer(cFileLength, buffer, position, CByte(space))
'                ProcessBuffer(dfA, buffer, position, CByte(cLineFeed))
'                If Not IsAcknowledgementValid(buffer, position, acknowledgement, networkStream, tcpClient, "Error on dfA") Then Return
'                Dim totalbytes As Long = 0

'                Using fileStream = New FileStream(fileFromQueue, FileMode.Open)
'                    Dim bytesRead As Integer

'                    While (CSharpImpl.__Assign(bytesRead, fileStream.Read(buffer, 0, bufferSize))) > 0
'                        totalbytes += bytesRead
'                        networkStream.Write(buffer, 0, bytesRead)
'                        networkStream.Flush()
'                    End While

'                    fileStream.Close()
'                End Using

'                If dataFileInfo.Length <> totalbytes Then ErrorMessage = fileFromQueue & "File length error"
'                position = 0
'                buffer(Math.Min(System.Threading.Interlocked.Increment(position), position - 1)) = 0
'                If Not IsAcknowledgementValid(buffer, position, acknowledgement, networkStream, tcpClient, "Error on file") Then Return
'                networkStream.Close()
'                tcpClient.Close()
'            End Using
'        End Sub

'        Private Shared Function GetPrintId() As Integer
'        Dim count As Integer = 0

'        SyncLock mLastPrintId
'                If mLastPrintId.ContainsKey(mUser) Then count = mLastPrintId(mUser)
'                count += 1
'                count = count Mod 1000

'                If mLastPrintId.ContainsKey(mUser) Then
'                    mLastPrintId(mUser) = count
'                Else
'                    mLastPrintId.Add(mUser, count)
'                End If
'            End SyncLock

'            Return count
'        End Function

'        Private Shared Sub ProcessBuffer(ByVal item As String, ByRef buffer As Byte(), ByRef position As Integer, ByVal nextPosition As Byte)
'        For Each t As String In item
'            buffer(Math.Min(System.Threading.Interlocked.Increment(position), position - 1)) = CByte(t)
'        Next

'        buffer(Math.Min(System.Threading.Interlocked.Increment(position), position - 1)) = nextPosition
'        End Sub

'        Private Shared Function IsAcknowledgementValid(ByVal buffer As Byte(), ByVal position As Integer, ByVal acknowledgement As Byte(), ByVal networkStream As NetworkStream, ByVal tcpClient As TcpClient, ByVal errorMsg As String) As Boolean
'            networkStream.Write(buffer, 0, position)
'            networkStream.Flush()
'            networkStream.Read(acknowledgement, 0, cDefaultByteSize)
'            If acknowledgement(0) = 0 Then Return True
'            ErrorMessage = errorMsg
'            networkStream.Close()
'            tcpClient.Close()
'            Return False
'        End Function

'        Private Class CSharpImpl
'            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
'            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
'                target = value
'                Return value
'            End Function
'        End Class
'    End Class