Imports System.Xml
Imports System.Xml.Serialization
<Serializable>
Public Class ConfigFile
  Implements ICloneable
  Implements IDisposable

  Public Class Company
    Public Property ERPCompany As String = ""
    Public Property IsActive As Boolean = True
    Public Property URLToken As String = ""
    Public Property UserName As String = ""
    Public Property Password As String = ""
    Public Property ClientID As String = ""
    Public Property ClientSecret As String = ""
    Public Property GrantType As String = ""
    Public Property URLSubmitInvoice As String = ""
    Public Property URLCancelInvoice As String = ""
    Public Property URLReFetchInvoice As String = ""
  End Class
  Public Function Clone() As Object Implements ICloneable.Clone
    Return MyBase.MemberwiseClone()
  End Function
  Public Property Companies As New List(Of Company)
  Public Property StartupPath As String = ""
  Public Property SerializedAt As String = ""
  Public Property BaaNLive As Boolean = False
  Public Property JoomlaLive As Boolean = False
  Public Property Testing As Boolean = True
  Public Shared Function Serialize(ByVal jpConfig As ConfigFile, ByVal SerializeAt As String) As ConfigFile
    jpConfig.SerializedAt = SerializeAt
    Dim oSrz As XmlSerializer = New XmlSerializer(jpConfig.GetType)
    Dim oSW As IO.StreamWriter = New IO.StreamWriter(SerializeAt)
    oSrz.Serialize(oSW, jpConfig)
    oSW.Close()
    Return jpConfig
  End Function
  Public Shared Function DeSerialize(ByVal SerializedAt As String) As ConfigFile
    Dim jpConfig As ConfigFile = Nothing
    If IO.File.Exists(SerializedAt) Then
      Dim oFS As IO.FileStream = New IO.FileStream(SerializedAt, IO.FileMode.Open)
      Dim oSrz As XmlSerializer = New XmlSerializer(GetType(ConfigFile))
      jpConfig = CType(oSrz.Deserialize(oFS), ConfigFile)
      oFS.Close()
    End If
    Return jpConfig
  End Function

#Region "IDisposable Support"
  Private disposedValue As Boolean ' To detect redundant calls

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: dispose managed state (managed objects).
        Companies = Nothing
      End If

      ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
      ' TODO: set large fields to null.
    End If
    disposedValue = True
  End Sub

  ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
  'Protected Overrides Sub Finalize()
  '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
  '    Dispose(False)
  '    MyBase.Finalize()
  'End Sub

  ' This code added by Visual Basic to correctly implement the disposable pattern.
  Public Sub Dispose() Implements IDisposable.Dispose
    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    Dispose(True)
    ' TODO: uncomment the following line if Finalize() is overridden above.
    ' GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class
