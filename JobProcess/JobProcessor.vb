Imports System.Reflection
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Web.Script.Serialization
Imports System.Net
Imports System.Drawing
Imports System.IO
Public Class JobProcessor
  Inherits TimerSupport
  Implements IDisposable

  Private jpConfig As ConfigFile = Nothing
  Private MayContinue As Boolean = True
  Private LibraryPath As String = ""
  Private LibraryID As String = ""
  Private RemoteLibraryConnected As Boolean = False

  Public Event JobStarted()
  Public Event JobStopped()
  Public Event Log(ByVal Message As String)
  Public Event Err(ByVal Message As String)

  Public Sub Msg(ByVal str As String)
    RaiseEvent Log(str)
  End Sub
  Public Sub MsgErr(ByVal str As String)
    RaiseEvent Err(str)
  End Sub
  Public Overrides Sub Process()
    Try
      If jpConfig.Testing Then
        For Each cmp As ConfigFile.Company In jpConfig.Companies
          If Not cmp.IsActive Then Continue For
          Msg("ERP Company: " & cmp.ERPCompany)
          SubmitInvoice(cmp)
          If IsStopping Then
            Msg("Cancelled")
            Exit For
          End If
        Next
      Else
        For Each cmp As ConfigFile.Company In jpConfig.Companies
          If Not cmp.IsActive Then Continue For
          Msg("ERP Company: " & cmp.ERPCompany)
          Dim ci800s As List(Of SIS.CIISG.ciisg800) = SIS.CIISG.ciisg800.GetPending(cmp.ERPCompany)
          Msg("To be processed: " & ci800s.Count)
          If ci800s.Count <= 0 Then Continue For
          For Each ci800 As SIS.CIISG.ciisg800 In ci800s
            If ci800.t_rers = enumFetchInvoice.YES AndAlso ci800.t_rpst = enumProcessStatus.Success Then
              'Re-Fetch Invoice
            ElseIf ci800.t_invs = enumProcessFor.CancelInvoice AndAlso ci800.t_rpst = enumProcessStatus.Success Then
              CancelInvoice(ci800, cmp)
            ElseIf ci800.t_rpst = enumProcessStatus.Free Or ci800.t_rpst = enumProcessStatus.Retry Then
              SubmitInvoice(ci800, cmp)
            End If
            If IsStopping Then
              Exit For
            End If
          Next
          If IsStopping Then
            Msg("Cancelled")
            Exit For
          End If
        Next
      End If
    Catch ex As Exception
      MsgErr(ex.Message)
    End Try
  End Sub
  Private Sub CancelInvoice(ci800 As SIS.CIISG.ciisg800, cmp As ConfigFile.Company)
    Dim xResponse As SIS.CIISG.miSubmitResponce = Nothing
    Dim Token As SIS.CIISG.miTokenResponce = GetToken(cmp)
    If Token IsNot Nothing Then
      Dim miInvoice As SIS.CIISG.miSubmitInvoice = SIS.CIISG.ciisg800.ConvertToMiSubmitInvoice(Token, ci800, cmp.ERPCompany)
      If miInvoice IsNot Nothing Then
        xResponse = GetSubmitResponse(miInvoice, cmp.URLSubmitInvoice)
        If xResponse.isError Then
          SIS.CIISG.ciisg800.UpdateInvoice(ci800, cmp, enumProcessStatus.Failed, xResponse)
        Else
          SIS.CIISG.ciisg800.UpdateInvoice(ci800, cmp, enumProcessStatus.Success, xResponse)
        End If
      End If
    End If
  End Sub

  Private Sub SubmitInvoice(ci800 As SIS.CIISG.ciisg800, cmp As ConfigFile.Company)
    Dim xResponse As SIS.CIISG.miSubmitResponce = Nothing
    Dim Token As SIS.CIISG.miTokenResponce = GetToken(cmp)
    If Token IsNot Nothing Then
      Dim miInvoice As SIS.CIISG.miSubmitInvoice = SIS.CIISG.ciisg800.ConvertToMiSubmitInvoice(Token, ci800, cmp.ERPCompany)
      If miInvoice IsNot Nothing Then
        xResponse = GetSubmitResponse(miInvoice, cmp.URLSubmitInvoice)
        If xResponse.isError Then
          SIS.CIISG.ciisg800.UpdateInvoice(ci800, cmp, enumProcessStatus.Failed, xResponse)
        Else
          SIS.CIISG.ciisg800.UpdateInvoice(ci800, cmp, enumProcessStatus.Success, xResponse)
        End If
      End If
    End If
  End Sub
  Private Sub SubmitInvoice(cmp As ConfigFile.Company)
    'Testing
    Dim xErr As SIS.CIISG.miError = Nothing
    Dim xResponse As SIS.CIISG.miSubmitResponce = Nothing
    Dim Token As SIS.CIISG.miTokenResponce = GetToken(cmp)
    If Token IsNot Nothing Then
      Dim miInvoice As New SIS.CIISG.miSubmitInvoice
      With miInvoice
        .access_token = Token.access_token
        .document_details.document_date = Now.ToString("dd/MM/yyyy")
      End With
      xResponse = GetSubmitResponse(miInvoice, cmp.URLSubmitInvoice)
      If Not xResponse.isError Then
      Else
        MsgErr(xResponse.results.errorMessage)
      End If
    End If
  End Sub
  Private Function GetSubmitResponse(miInvoice As SIS.CIISG.miSubmitInvoice, url As String) As SIS.CIISG.miSubmitResponce
    Dim xErr As New SIS.CIISG.miError
    Dim xResponse As SIS.CIISG.miSubmitResponce = Nothing
    Dim xWebRequest As HttpWebRequest = GetWebRequest(url)
    Dim jsonStr As String = New JavaScriptSerializer().Serialize(miInvoice)
    Dim jsonByte() As Byte = System.Text.Encoding.ASCII.GetBytes(jsonStr)
    xWebRequest.ContentLength = jsonByte.Count
    xWebRequest.GetRequestStream().Write(jsonByte, 0, jsonByte.Count)
    Try
      Dim rs As WebResponse = xWebRequest.GetResponse()
      Dim st As IO.Stream = rs.GetResponseStream
      Dim sr As IO.StreamReader = New IO.StreamReader(st)
      Dim strResponse As String = sr.ReadToEnd
      sr.Close()
      xResponse = New JavaScriptSerializer().Deserialize(strResponse, GetType(SIS.CIISG.miSubmitResponce))
    Catch ex As Exception
      MsgErr(ex.Message)
    End Try
    Return xResponse
  End Function
  Private Function GetToken(cmp As ConfigFile.Company) As SIS.CIISG.miTokenResponce
    Dim Token As SIS.CIISG.miTokenResponce = Nothing
    Dim xWebRequest As HttpWebRequest = GetWebRequest(cmp.URLToken)
    Dim TokenRequest As New SIS.CIISG.miTokenRequest
    With TokenRequest
      .client_id = cmp.ClientID
      .client_secret = cmp.ClientSecret
      .grant_type = cmp.GrantType
      .password = cmp.Password
      .username = cmp.UserName
    End With
    Dim jsonStr As String = New JavaScriptSerializer().Serialize(TokenRequest)
    Dim jsonByte() As Byte = System.Text.Encoding.ASCII.GetBytes(jsonStr)
    xWebRequest.ContentLength = jsonByte.Count
    xWebRequest.GetRequestStream().Write(jsonByte, 0, jsonByte.Count)
    Try
      Dim rs As WebResponse = xWebRequest.GetResponse()
      Dim st As IO.Stream = rs.GetResponseStream
      Dim sr As IO.StreamReader = New IO.StreamReader(st)
      Dim strResponse As String = sr.ReadToEnd
      sr.Close()
      Token = New JavaScriptSerializer().Deserialize(strResponse, GetType(SIS.CIISG.miTokenResponce))
    Catch ex As Exception
      MsgErr(ex.Message)
    End Try
    Return Token
  End Function
  Private Function GetWebRequest(url As String) As HttpWebRequest
    Dim Uri As Uri = New Uri(url)
    Dim xWebRequest As HttpWebRequest = WebRequest.Create(Uri)
    With xWebRequest
      .Method = "POST"
      .ContentType = "application/json"
      .Accept = "*/*"
      .CachePolicy = New Cache.RequestCachePolicy(Net.Cache.RequestCacheLevel.NoCacheNoStore)
      .Headers.Add("mode", "no-cors")
      '.Host = "clientbasic.mastersindia.co"
      .KeepAlive = True
    End With
    Return xWebRequest
  End Function
  Public Overrides Sub Started()
    Try
      RaiseEvent JobStarted()
      Msg("Reading Settings")
      Dim ConfigPath As String = IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) & "\Settings.xml"
      jpConfig = ConfigFile.DeSerialize(ConfigPath)
      SIS.SYS.SQLDatabase.DBCommon.BaaNLive = jpConfig.BaaNLive
      Dim tmpL As SIS.EDI.ediALib = SIS.EDI.ediALib.GetActiveLibrary
      LibraryPath = "\\192.9.200.146\" & tmpL.t_path
      LibraryID = tmpL.t_lbcd
      If ConnectToNetworkFunctions.connectToNetwork(LibraryPath, "X:", "administrator", "Indian@12345") Then
        RemoteLibraryConnected = True
      End If
    Catch ex As Exception
      StopJob()
      MsgErr(ex.Message)
    End Try
  End Sub

  Public Overrides Sub Stopped()
    If RemoteLibraryConnected Then
      ConnectToNetworkFunctions.disconnectFromNetwork("X:")
      RemoteLibraryConnected = False
    End If
    jpConfig = Nothing
    RaiseEvent JobStopped()
    Msg("Stopped")
  End Sub


  Sub New()
    'dummy
  End Sub

#Region "IDisposable Support"
  Private disposedValue As Boolean ' To detect redundant calls

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: dispose managed state (managed objects).
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
