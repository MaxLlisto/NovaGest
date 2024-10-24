Imports System
Imports System.IO
Imports System.Text
Imports System.Data
'using System.Collectionsç.Specialized;
'using System.Collections;
Imports Microsoft.CSharp
Imports Microsoft.VisualBasic
Imports System.Reflection
Imports System.CodeDom.Compiler
Imports Westwind.RemoteLoader

Namespace Westwind.wwScripting
    ''' <summary>
    ''' Deletgate para el evento completado
    ''' </summary>
    Public Delegate Sub DelegateCompleted(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Clase que permite la ejecución de código creado dinámicamente en tiempo de ejecución.
    ''' Proporciona funcionalidad para evaluar y ejecutar código compilado.
    ''' </summary>
    Public Class wwScripting
        ''' <summary>
        '''Objeto compilador utilizado para compilar nuestro código.
        ''' </summary>
        Protected oCompiler As ICodeCompiler = Nothing

        ''' <summary>
        ''' Referencia al objeto Parámetro del compilador
        ''' </summary>
        Protected oParameters As CompilerParameters = Nothing

        ''' <summary>
        ''' Referencia al montaje final
        ''' </summary>
        Protected oAssembly As Assembly = Nothing

        ''' <summary>
        ''' El objeto de resultados del compilador que se usa para descubrir errores.
        ''' </summary>
        Protected oCompiled As CompilerResults = Nothing
        Protected cOutputAssembly As String = Nothing
        Protected cNamespaces As String = ""
        Protected lFirstLoad As Boolean = True


        ''' <summary>
        '' La referencia de objeto al objeto compilado disponible después de la primera llamada al método.
        ''' Puede usar este método para llamar a métodos adicionales en el objeto.
        ''' Por ejemplo, puede usar CallMethod y pasar múltiples métodos de código cada uno de
        ''' que se puede ejecutar indirectamente usando CallMethod() en esta referencia de objeto.
        ''' </summary>
        Public oObjRef As Object = Nothing

        ''' <summary>
        ''' Si es verdadero, guarda el código fuente antes de compilar en la propiedad cSourceCode.
        ''' </summary>
        Public lSaveSourceCode As Boolean = False

        ''' <summary>
        ''' Contiene el código fuente del código ensamblador compilado completo.
        ''' Nota: este no es el código pasado, sino el código ensamblador fijo completo.
        ''' Solo se establece si lSaveSourceCode=true.
        ''' </summary>
        Public cSourceCode As String = ""

        ''' <summary>
        ''' Línea donde comienza el código que se ejecuta
        ''' </summary>
        Protected nStartCodeLine As Integer = 0

        ''' <summary>
        ''' Espacio de nombres del ensamblado creado por el procesador de secuencias de comandos. determina
        ''' cómo se referenciará y cargará la clase.
        ''' </summary>
        Public cAssemblyNamespace As String = "WestWindScripting"

        ''' <summary>
        ''' Nombre de la clase creada por el procesador de scripts. El código de secuencia de comandos se convierte en métodos de la clase.
        ''' </summary>
        Public cClassName As String = "WestWindScript"

        ''' <summary>
        ''' Determina si se agregan ensamblajes predeterminados. system, system.IO, system.Reflection
        ''' </summary>
        Public lDefaultAssemblies As Boolean = True
        Protected oAppDomain As AppDomain = Nothing
        Public cErrorMsg As String = ""
        Public bError As Boolean = False

        ''' <summary>
        ''' Ruta para los ensamblajes de soporte wwScripting y RemoteLoader.
        ''' De forma predeterminada, esto puede estar en blanco, pero si está utilizando esta funcionalidad
        ''' en ASP.Net especifique la ruta bin explícitamente. Debe incluir el seguimiento
        ''' estrellarse.
        ''' ''' </summary>
        '[Description("Ruta para los ensamblajes de soporte wwScripting y RemoteLoader. En blanco por defecto. Incluir guión final.")]
        Public cSupportAssemblyPath As String = ""

        ''' <summary>
        ''' The scripting language used. CSharp, VB, JScript
        ''' </summary>
        Public cScriptingLanguage As String = "VB"

        ''' <summary>
        ''' El idioma que utilizará esta clase de secuencias de comandos. Actualmente solo se admite C#
        ''' con sintaxis VB disponible pero no probada.
        ''' </summary>
        ''' <paramname="lcLanguage">CSharp or VB</param>
        ''' 
        Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

        Public Sub New(ByVal lcLanguage As String)
            SetLanguage(lcLanguage)
        End Sub

        Public Sub New()
            SetLanguage("VB")
        End Sub

        ''' <summary>
        ''' Specifies the language that is used. Supported languages include
        ''' CSHARP C# VB
        ''' </summary>
        ''' <paramname="lcLanguage"></param>
        Public Sub SetLanguage(ByVal lcLanguage As String)
            cScriptingLanguage = lcLanguage

            If Equals(cScriptingLanguage, "CSharp") OrElse Equals(cScriptingLanguage, "C#") Then
                oCompiler = New CSharpCodeProvider().CreateCompiler()
                cScriptingLanguage = "CSharp"
            ElseIf Equals(cScriptingLanguage, "VB") Then
                oCompiler = New VBCodeProvider().CreateCompiler()
            End If
            ' else throw(Exception ex);

            oParameters = New CompilerParameters()
        End Sub

        Public Property og As ObjetoGlobal.ObjetoGlobal
            Get
                Return ObjetoGlobal
            End Get
            Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
                ObjetoGlobal = value
            End Set
        End Property

        ''' <summary>
        ''' Agrega un ensamblado al código compilado.
        ''' </summary>
        ''' <paramname="lcAssemblyDll">Nombre de archivo de ensamblado de DLL</param>
        ''' <paramname="lcNamespace">Espacio de nombres para agregar, si corresponde. Pase nulo si no se va a agregar ningún espacio de nombres</param>
        Public Sub AddAssembly(ByVal lcAssemblyDll As String, ByVal lcNamespace As String)
            If Equals(lcAssemblyDll, Nothing) AndAlso Equals(lcNamespace, Nothing) Then
                ' *** clear out assemblies and namespaces
                oParameters.ReferencedAssemblies.Clear()
                cNamespaces = ""
                Return
            End If

            If Not Equals(lcAssemblyDll, Nothing) Then oParameters.ReferencedAssemblies.Add(lcAssemblyDll)

            If Not Equals(lcNamespace, Nothing) Then
                If Equals(cScriptingLanguage, "CSharp") Then
                    cNamespaces = cNamespaces & "using " & lcNamespace & ";" & Microsoft.VisualBasic.Constants.vbCrLf
                Else
                    cNamespaces = cNamespaces & "imports " & lcNamespace & Microsoft.VisualBasic.Constants.vbCrLf
                End If
            End If
        End Sub

        ''' <summary>
        ''' Adds an assembly to the compiled code.
        ''' </summary>
        ''' <paramname="lcAssemblyDll">DLL assembly file name</param>
        Public Sub AddAssembly(ByVal lcAssemblyDll As String)
            AddAssembly(lcAssemblyDll, Nothing)
        End Sub

        Public Sub AddNamespace(ByVal lcNamespace As String)
            AddAssembly(Nothing, lcNamespace)
        End Sub

        Public Sub AddDefaultAssemblies()
            AddAssembly("System.dll", "System")
            AddNamespace("System.Reflection")
            AddNamespace("System.IO")
        End Sub


        ''' <summary>
        ''' Ejecuta un método completo envolviéndolo en una clase.
        ''' </summary>
        ''' <paramname="lcCode">Uno o más métodos completos.</param>
        ''' <paramname="lcMethodName">Nombre del método a llamar.</param>
        ''' <paramname="loParameters">cualquier número de parámetros variables</param>
        ''' <returns></returns>
        Public Function ExecuteMethod(ByVal lcCode As String, ByVal lcMethodName As String, ParamArray loParameters As Object()) As Object
            If oObjRef Is Nothing Then
                If lFirstLoad Then
                    If lDefaultAssemblies Then
                        AddDefaultAssemblies()
                    End If

                    AddAssembly(cSupportAssemblyPath & "RemoteLoader.dll", "Westwind.RemoteLoader")
                    AddAssembly(cSupportAssemblyPath & "wwScripting.dll", "Westwind.wwScripting")
                    'AddAssembly("System.Data.Common.dll", "System.Data")
                    lFirstLoad = False
                End If

                Dim sb As StringBuilder = New StringBuilder("")

                '*** Introducción al programa y encabezado de clase
                sb.Append(cNamespaces)
                sb.Append(Microsoft.VisualBasic.Constants.vbCrLf)

                If Equals(cScriptingLanguage, "CSharp") Then
                    ' *** Encabezados de espacio de nombres y definición de clase
                    sb.Append("namespace " & cAssemblyNamespace & "{" & Microsoft.VisualBasic.Constants.vbCrLf & "public class " & cClassName & ":MarshalByRefObject,IRemoteInterface {" & Microsoft.VisualBasic.Constants.vbCrLf)

                    ' *** Método de invocación genérico requerido para la interfaz de llamada remota
                    sb.Append("public object Invoke(string lcMethod,object[] parms) {" & Microsoft.VisualBasic.Constants.vbCrLf & "return this.GetType().InvokeMember(lcMethod,BindingFlags.InvokeMethod,null,this,parms );" & Microsoft.VisualBasic.Constants.vbCrLf & "}" & Microsoft.VisualBasic.Constants.vbCrLf & Microsoft.VisualBasic.Constants.vbCrLf)

                    '*** El código real que se ejecutará en forma de una definición de método completa.
                    sb.Append(lcCode)
                    sb.Append(Microsoft.VisualBasic.Constants.vbCrLf & "} }")  'Clase y espacio de nombres cerrados
                ElseIf Equals(cScriptingLanguage, "VB") Then
                    ' *** Encabezados de espacio de nombres y definición de clase
                    sb.Append("Namespace " & cAssemblyNamespace & Microsoft.VisualBasic.Constants.vbCrLf & "public class " & cClassName & Microsoft.VisualBasic.Constants.vbCrLf)
                    sb.Append("Inherits MarshalByRefObject" & Microsoft.VisualBasic.Constants.vbCrLf & "Implements IRemoteInterface" & Microsoft.VisualBasic.Constants.vbCrLf & Microsoft.VisualBasic.Constants.vbCrLf)

                    ' *** Método de invocación genérico requerido para la interfaz de llamada remota
                    sb.Append("Public Overridable Overloads Function Invoke(ByVal lcMethod As String, ByVal Parameters() As Object) As Object _" & Microsoft.VisualBasic.Constants.vbCrLf & "Implements IRemoteInterface.Invoke" & Microsoft.VisualBasic.Constants.vbCrLf & "return me.GetType().InvokeMember(lcMethod,BindingFlags.InvokeMethod,nothing,me,Parameters)" & Microsoft.VisualBasic.Constants.vbCrLf & "End Function" & Microsoft.VisualBasic.Constants.vbCrLf & Microsoft.VisualBasic.Constants.vbCrLf)

                    '*** El código real que se ejecutará en forma de una definición de método completa.
                    sb.Append(lcCode)
                    sb.Append(Microsoft.VisualBasic.Constants.vbCrLf & Microsoft.VisualBasic.Constants.vbCrLf & "End Class" & Microsoft.VisualBasic.Constants.vbCrLf & "End Namespace" & Microsoft.VisualBasic.Constants.vbCrLf)  ' Class and namespace closed
                End If

                If lSaveSourceCode Then
                    cSourceCode = sb.ToString()
                    'MessageBox.Show(this.cSourceCode);
                End If

                If Not CompileAssembly(sb.ToString()) Then Return Nothing
                Dim loTemp As Object = CreateInstance()
                If loTemp Is Nothing Then Return Nothing
            End If

            Return CallMethod(oObjRef, lcMethodName, loParameters)
        End Function

        ''' <summary>
        ''' Ejecuta un fragmento de código. Pasar un número variable de parámetros
        ''' (accesible a través de la matriz loParameters[0..n]) y devuelve un parámetro de objeto.
        ''' El código debe incluir: return (objeto) SomeValue como la última línea o return null
        ''' </summary>
        ''' <paramname="lcCode">El código a ejecutar</param>
        ''' <paramname="loParameters">Los parámetros para pasar el código.</param>
        ''' <returns></returns>
        'Public Function ExecuteCode(ByVal lcCode As String, ParamArray loParameters As Object()) As Object
        Public Function ExecuteCode(ByVal lcCode As String, ParamArray p As Object()) As Object
            If Equals(cScriptingLanguage, "CSharp") Then
                'Return ExecuteMethod("public object ExecuteCode(params object[] p) {" & lcCode & "}", "ExecuteCode", loParameters)
            ElseIf Equals(cScriptingLanguage, "VB") Then
                Return Me.ExecuteMethod("public function ExecuteCode(ParamArray p As Object()) as object" & Microsoft.VisualBasic.Constants.vbCrLf & lcCode & Microsoft.VisualBasic.Constants.vbCrLf & "end function" & Microsoft.VisualBasic.Constants.vbCrLf, "ExecuteCode", p) 'loParameters)
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Compila y ejecuta el código fuente para un ensamblaje completo.
        ''' </summary>
        ''' <paramname="lcSource"></param>
        ''' <returns></returns>
        Public Function CompileAssembly(ByVal lcSource As String) As Boolean
            'this.oParameters.GenerateExecutable = false;

            If oAppDomain Is Nothing AndAlso Equals(cOutputAssembly, Nothing) Then
                oParameters.GenerateInMemory = True
            ElseIf oAppDomain IsNot Nothing AndAlso Equals(cOutputAssembly, Nothing) Then
                ' *** Generar un ensamblado con el mismo nombre que el dominio
                cOutputAssembly = "wws_" & Guid.NewGuid().ToString() & ".dll"
                oParameters.OutputAssembly = cOutputAssembly
            Else
                oParameters.OutputAssembly = cOutputAssembly
            End If

            oCompiled = oCompiler.CompileAssemblyFromSource(oParameters, lcSource)

            If oCompiled.Errors.HasErrors Then
                bError = True

                ' *** Create Error String
                cErrorMsg = oCompiled.Errors.Count.ToString() & " Errors:"

                For x = 0 To oCompiled.Errors.Count - 1
                    cErrorMsg = cErrorMsg & Microsoft.VisualBasic.Constants.vbCrLf & "Line: " & oCompiled.Errors(x).Line.ToString() & " - " & oCompiled.Errors(x).ErrorText
                Next

                Return False
            End If

            If oAppDomain Is Nothing Then oAssembly = oCompiled.CompiledAssembly
            Return True
        End Function

        Public Function CreateInstance() As Object
            If oObjRef IsNot Nothing Then
                Return oObjRef
            End If

            ' *** Crear una instancia del nuevo objeto.
            Try

                If oAppDomain Is Nothing Then
                    Try
                        oObjRef = oAssembly.CreateInstance(cAssemblyNamespace & "." & cClassName)
                        Return oObjRef
                    Catch ex As Exception
                        bError = True
                        cErrorMsg = ex.Message
                        Return Nothing
                    End Try
                Else
                    ' crea la clase de fábrica en el dominio de la aplicación secundaria
                    Dim factory As RemoteLoaderFactory = CType(oAppDomain.CreateInstance("RemoteLoader", "Westwind.RemoteLoader.RemoteLoaderFactory").Unwrap(), RemoteLoaderFactory)

                    ' con la ayuda de esta fábrica, ahora podemos crear un verdadero 'LiveClass' instance
                    oObjRef = factory.Create(cOutputAssembly, cAssemblyNamespace & "." & cClassName, Nothing)
                    Return oObjRef
                End If

            Catch ex As Exception
                bError = True
                cErrorMsg = ex.Message
                Return Nothing
            End Try
        End Function

        Public Function CallMethod(ByVal loObject As Object, ByVal lcMethod As String, ParamArray loParameters As Object()) As Object
            ' *** Intenta ejecutarlo
            Try

                If oAppDomain Is Nothing Then
                    ' *** solo invoque el método directamente a través de Reflection
                    Return loObject.GetType().InvokeMember(lcMethod, BindingFlags.InvokeMethod, Nothing, loObject, loParameters)
                Else
                    ' *** Invocar el método a través de la interfaz Remota y el método Invocar
                    Dim loResult As Object

                    Try
                        ' *** Envíe el objeto a la interfaz remota para evitar cargar información de tipo
                        Dim loRemote = CType(loObject, IRemoteInterface)

                        ' *** Indirectly call the remote interface
                        loResult = loRemote.Invoke(lcMethod, loParameters)
                    Catch ex As Exception
                        bError = True
                        cErrorMsg = ex.Message
                        Return Nothing
                    End Try

                    Return loResult
                End If

            Catch ex As Exception
                bError = True
                cErrorMsg = ex.Message
            End Try

            Return Nothing
        End Function

        Public Function CreateAppDomain(ByVal lcAppDomain As String) As Boolean
            If Equals(lcAppDomain, Nothing) Then lcAppDomain = "wwscript"
            Dim loSetup As AppDomainSetup = New AppDomainSetup()
            loSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
            oAppDomain = AppDomain.CreateDomain(lcAppDomain, Nothing, loSetup)
            Return True
        End Function

        Public Function UnloadAppDomain() As Boolean
            If oAppDomain IsNot Nothing Then AppDomain.Unload(oAppDomain)
            oAppDomain = Nothing

            If Not Equals(cOutputAssembly, Nothing) Then
                Try
                    File.Delete(cOutputAssembly)
                Catch __unusedException1__ As Exception
                End Try
            End If

            Return True
        End Function

        Public Sub Release()
            oObjRef = Nothing
        End Sub

        Public Sub Dispose()
            Release()
            UnloadAppDomain()
        End Sub

        Protected Overrides Sub Finalize()
            Dispose()
        End Sub
    End Class
End Namespace
