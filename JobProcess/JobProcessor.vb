Imports System.Reflection
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Web.Script.Serialization
Imports System.Net
Imports System.Drawing
Imports System.IO
Imports ejiVault
Public Class JobProcessor
  Inherits TimerSupport
  Implements IDisposable

  Private jpConfig As ConfigFile = Nothing
  Private MayContinue As Boolean = True
  Private LibraryConnected As Boolean = False

  Public Event JobStarted()
  Public Event JobStopped()
  Public Event Log(ByVal Message As String)
  Public Event Err(ByVal Message As String)
  Public Event Show(ByVal Message As String)

  Public Property InvoiceNo As String = ""

  Public Sub Shw(ByVal str As String)
    RaiseEvent Show(str)
  End Sub
  Public Sub Msg(ByVal str As String)
    RaiseEvent Log(str)
  End Sub
  Public Sub MsgErr(ByVal str As String)
    RaiseEvent Err(str)
  End Sub
  Public Overrides Sub Process()
    'If file is not attached, to attach file
    'Enter Document Type+Document Number in TextBox
    'Click Start
    If InvoiceNo <> "" Then
      jpConfig.Testing = True
    Else
      jpConfig.Testing = False
    End If
    Try
      If IsStopping Then
        Msg("Cancelling.")
        Exit Sub
      End If
      For Each cmp As ConfigFile.Company In jpConfig.Companies
        If Not cmp.IsActive Then Continue For
        Shw("ERP Company: " & cmp.ERPCompany)
        Dim ci800s As List(Of SIS.CIISG.ciisg800) = SIS.CIISG.ciisg800.GetPending(cmp.ERPCompany, jpConfig.Testing, InvoiceNo)
        Shw("To be processed: " & ci800s.Count)
        Dim cnt As Integer = 0
        If ci800s.Count > 0 Then
          Msg("ERP Company: " & cmp.ERPCompany)
          Msg("To be processed: " & ci800s.Count)
          For Each ci800 As SIS.CIISG.ciisg800 In ci800s
            cnt += 1
            Shw("Processing: " & cnt)
            If jpConfig.Testing Then
              SubmitInvoice(ci800, cmp)
            Else
              If ci800.t_rers = enumFetchInvoice.YES AndAlso ci800.t_rpst = enumProcessStatus.Success Then
                'Re-Fetch Invoice
              ElseIf ci800.t_invs = enumProcessFor.CancelInvoice AndAlso ci800.t_rpst = enumProcessStatus.Success Then
                CancelInvoice(ci800, cmp)
              ElseIf ci800.t_rpst = enumProcessStatus.Free Or ci800.t_rpst = enumProcessStatus.Retry Then
                SubmitInvoice(ci800, cmp)
              End If
            End If
            If jpConfig.Testing Then
              StopJob()
              Msg("Auto cancelling Invoice Process.")
            End If
            If IsStopping Then
              Exit For
            End If
          Next
        End If
        If jpConfig.Testing Then
          StopJob()
          Msg("Auto cancelling for Company.")
        End If
        If IsStopping Then
          Msg("Cancelled")
          Exit For
        End If
      Next
    Catch ex As Exception
      MsgErr(ex.Message)
    End Try
  End Sub
  Private Sub CancelInvoice(ci800 As SIS.CIISG.ciisg800, cmp As ConfigFile.Company)
    Dim xResponse As SIS.CIISG.miCancelResponce = Nothing
    Dim Token As SIS.CIISG.miTokenResponce = GetToken(cmp)
    If Token IsNot Nothing Then
      Dim miInvoice As SIS.CIISG.miCancelInvoice = SIS.CIISG.ciisg800.ConvertToMiCancelInvoice(Token, ci800, cmp.ERPCompany)
      If miInvoice IsNot Nothing Then
        MsgErr("Cancelling=>Comp: " & ci800.t_comp & ", Tran: " & ci800.t_tran & ", DocN: " & ci800.t_docn & "  /||\")
        xResponse = GetCancelResponse(miInvoice, cmp.URLCancelInvoice)
        If xResponse.isError Then
          SIS.CIISG.ciisg800.UpdateCancelInvoice(ci800, cmp, enumProcessStatus.Failed, xResponse)
          MsgErr(xResponse.results.errorMessage)
          MsgErr("==============================")
        Else
          SIS.CIISG.ciisg800.UpdateCancelInvoice(ci800, cmp, enumProcessStatus.Cancelled, xResponse)
        End If
      End If
    End If
  End Sub
  Private Function GetCancelResponse(miInvoice As SIS.CIISG.miCancelInvoice, url As String) As SIS.CIISG.miCancelResponce
    Dim xErr As New SIS.CIISG.miError
    Dim xResponse As SIS.CIISG.miCancelResponce = Nothing
    Dim xWebRequest As HttpWebRequest = GetWebRequest(url)
    Dim jsonStr As String = New JavaScriptSerializer().Serialize(miInvoice)
    If jpConfig.JSONLog Then
      MsgErr("Cancel Submited JSON=>" & jsonStr)
    End If
    Dim jsonByte() As Byte = System.Text.Encoding.ASCII.GetBytes(jsonStr)
    xWebRequest.ContentLength = jsonByte.Count
    xWebRequest.GetRequestStream().Write(jsonByte, 0, jsonByte.Count)
    Try
      Dim rs As WebResponse = xWebRequest.GetResponse()
      Dim st As IO.Stream = rs.GetResponseStream
      Dim sr As IO.StreamReader = New IO.StreamReader(st)
      Dim strResponse As String = sr.ReadToEnd
      sr.Close()
      strResponse = strResponse.Replace("""message"":""""", """message"":{}")
      If jpConfig.JSONLog Then
        MsgErr("Cancel Response JSON=>" & strResponse)
      End If
      xResponse = New JavaScriptSerializer().Deserialize(strResponse, GetType(SIS.CIISG.miCancelResponce))
    Catch ex As Exception
      MsgErr(ex.Message)
    End Try
    Return xResponse
  End Function

  Private Sub SubmitInvoice(ci800 As SIS.CIISG.ciisg800, cmp As ConfigFile.Company)
    Dim xResponse As SIS.CIISG.miSubmitResponce = Nothing
    Dim Token As SIS.CIISG.miTokenResponce = GetToken(cmp)
    If Token IsNot Nothing Then
      Dim miInvoice As SIS.CIISG.miSubmitInvoice = SIS.CIISG.ciisg800.ConvertToMiSubmitInvoice(Token, ci800, cmp.ERPCompany)
      If miInvoice IsNot Nothing Then
        MsgErr("Submitting=>Comp: " & ci800.t_comp & ", Tran: " & ci800.t_tran & ", DocN: " & ci800.t_docn & "  /||\")
        xResponse = GetSubmitResponse(miInvoice, cmp.URLSubmitInvoice)
        If xResponse.isError Then
          SIS.CIISG.ciisg800.UpdateInvoice(ci800, cmp, enumProcessStatus.Failed, xResponse)
          MsgErr(xResponse.results.errorMessage)
          MsgErr("==============================")
        Else
          SIS.CIISG.ciisg800.UpdateInvoice(ci800, cmp, enumProcessStatus.Success, xResponse)
          '===========================================
          DownloadFiles(cmp, xResponse, ci800.IndxKey)
          '===========================================
        End If
      End If
    End If
  End Sub
  Private Sub DownloadFiles(cmp As ConfigFile.Company, xResponse As SIS.CIISG.miSubmitResponce, IndxKey As String)
    Dim URL As String = ""
    Dim FileName As String = ""
    Dim FilePath As String = ""
    '1. Download QRCode PNG
    URL = xResponse.results.xMessage.QRCodeUrl.Replace("\/", "/")
    FileName = URL.Substring(URL.LastIndexOf("/") + 1)
    FilePath = jpConfig.TempPath & "\" & FileName & ".PNG"
    If My.Computer.FileSystem.FileExists(FilePath) Then
      My.Computer.FileSystem.DeleteFile(FilePath)
    End If
    My.Computer.Network.DownloadFile(URL, FilePath)
    Try
      EJI.ediAFile.UploadFile(cmp.QRCodeHandle, IndxKey, FilePath, "0340")
    Catch ex As Exception
      MsgErr(ex.Message)
    End Try
    '2. Download Invoice PDF
    URL = xResponse.results.xMessage.EinvoicePdf.Replace("\/", "/")
    FileName = URL.Substring(URL.LastIndexOf("/") + 1)
    FilePath = jpConfig.TempPath & "\" & FileName & ".PDF"
    If My.Computer.FileSystem.FileExists(FilePath) Then
      My.Computer.FileSystem.DeleteFile(FilePath)
    End If
    My.Computer.Network.DownloadFile(URL, FilePath)
    Try
      EJI.ediAFile.UploadFile(cmp.InvoiceHandle, IndxKey, FilePath, "0340")
    Catch ex As Exception
      MsgErr(ex.Message)
    End Try
  End Sub
  Private Function GetSubmitResponse(miInvoice As SIS.CIISG.miSubmitInvoice, url As String) As SIS.CIISG.miSubmitResponce
    Dim xErr As New SIS.CIISG.miError
    Dim xResponse As SIS.CIISG.miSubmitResponce = Nothing
    Dim xWebRequest As HttpWebRequest = GetWebRequest(url)
    Dim jsonStr As String = New JavaScriptSerializer().Serialize(miInvoice)
    If jpConfig.JSONLog Then
      MsgErr("Processing: " & miInvoice.document_details.document_type & " - " & miInvoice.document_details.document_number)
      MsgErr("Submitted JSON=>" & jsonStr)
    End If
    Dim jsonByte() As Byte = System.Text.Encoding.ASCII.GetBytes(jsonStr)
    xWebRequest.ContentLength = jsonByte.Count
    xWebRequest.GetRequestStream().Write(jsonByte, 0, jsonByte.Count)
    Try
      Dim rs As WebResponse = xWebRequest.GetResponse()
      Dim st As IO.Stream = rs.GetResponseStream
      Dim sr As IO.StreamReader = New IO.StreamReader(st)
      Dim strResponse As String = sr.ReadToEnd
      sr.Close()
      strResponse = strResponse.Replace("""message"":""""", """message"":{}")
      If jpConfig.JSONLog Then
        MsgErr("Response JSON=>" & strResponse)
      End If
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
      If jpConfig.UseProxy Then
        .Proxy = New WebProxy(jpConfig.Proxy)
      End If
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

      ejiVault.EJI.DBCommon.BaaNLive = jpConfig.BaaNLive
      ejiVault.EJI.DBCommon.ERPCompany = jpConfig.VaultLibraryCompany
      ejiVault.EJI.DBCommon.IsLocalISGECVault = jpConfig.IsLocalISGECVault
      ejiVault.EJI.DBCommon.ISGECVaultIP = jpConfig.ISGECVaultIP
      If ejiVault.EJI.DBCommon.ConnectLibrary() Then
        LibraryConnected = True
      End If
      If jpConfig.Testing Then
        If InvoiceNo = "" Then
          MsgErr("When Testing, Invoice No. is required to enter.")
          StopJob()
        End If
      End If
    Catch ex As Exception
      StopJob()
      MsgErr(ex.Message)
    End Try
  End Sub

  Public Overrides Sub Stopped()
    If LibraryConnected Then
      If ejiVault.EJI.DBCommon.DisconnectLibrary() Then
        LibraryConnected = False
      End If
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
