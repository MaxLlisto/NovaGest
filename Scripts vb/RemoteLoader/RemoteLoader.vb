Imports System
Imports System.Reflection

Namespace Westwind.RemoteLoader
    ''' <summary>
    ''' Interface that can be run over the remote AppDomain boundary.
    ''' </summary>
    Public Interface IRemoteInterface
        Function Invoke(ByVal lcMethod As String, ByVal Parameters As Object()) As Object
    End Interface



    ''' <summary>
    ''' Factory class to create objects exposing IRemoteInterface
    ''' </summary>
    Public Class RemoteLoaderFactory
        Inherits MarshalByRefObject

        Private Const bfi As BindingFlags = BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.CreateInstance

        Public Sub New()
        End Sub

        ''' <summary> Factory method to create an instance of the type whose name is specified,
        ''' using the named assembly file and the constructor that best matches the specified parameters. </summary>
        ''' <paramname="assemblyFile"> The name of a file that contains an assembly where the type named typeName is sought. </param>
        ''' <paramname="typeName"> The name of the preferred type. </param>
        ''' <paramname="constructArgs"> An array of arguments that match in number, order, and type the parameters of the constructor to invoke, or null for default constructor. </param>
        ''' <returns> The return value is the created object represented as ILiveInterface. </returns>
        Public Function Create(ByVal assemblyFile As String, ByVal typeName As String, ByVal constructArgs As Object()) As IRemoteInterface
            Return CType(Activator.CreateInstanceFrom(assemblyFile, typeName, False, bfi, Nothing, constructArgs, Nothing, Nothing, Nothing).Unwrap(), IRemoteInterface)
        End Function
    End Class
End Namespace
