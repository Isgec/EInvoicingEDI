Imports System.Xml
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Text

Public Class vaultXML
  Public Class Part
    Public Property p_no As String = ""
    Public Property p_desc As String = ""
    Public Property spec As String = ""
    Public Property size As String = ""
    Public Property p_qty As String = ""
    Public Property p_wt As String = ""
    Public Property remark As String = ""
  End Class
  Public Class Item
    Public Property remark As String = ""
    Public Property it_qty As String = ""
    Public Property it_wt As String = ""
    Public Property item_g As String = ""
    Public Property t As String = ""
    Public Property item_code As String = ""
    Public Property item_descn As String = ""

    Public Property Parts As New List(Of Part)
  End Class
  Public Class RefDoc
    Public Property drgno As String = ""
    Public Property drg_descn As String = ""
    Public Property rev As String = ""
    Public Property PDF_filename As String = ""
    Public Property filename As String = ""
  End Class
  Public Property PDF_filename As String = ""
  Public Property filename As String = ""
  Public Property number As String = ""
  Public Property title As String = ""
  Public Property rev As String = ""
  Public Property el_id As String = ""
  Public Property sheetsize As String = ""
  Public Property scale As String = ""
  Public Property weight As String = ""
  Public Property drawn As String = ""
  Public Property chqd As String = ""
  Public Property appd As String = ""
  Public Property dDate As String = ""
  Public Property resp_dept As String = ""
  Public Property apr As String = ""
  Public Property inf As String = ""
  Public Property pro As String = ""
  Public Property ere As String = ""
  Public Property drgid As String = ""
  Public Property VaultDBName As String = ""
  Public Property VaultUserName As String = ""
  Public Property VaultClientMachine As String = ""
  Public Property VaultSubmittedDate As String = ""
  Public Property ISGEC_Datasource As String = ""
  Public Property name_software As String = ""
  Public Property slzFileName As String = ""
  Public Property ORGFilePathName As String = ""

  Public Property contract As String = ""
  Public Property service1 As String = ""
  Public Property service2 As String = ""
  Public Property iwt As String = ""
  Public Property year As String = ""
  Public Property consultant As String = ""
  Public Property client As String = ""
  Public Property group As String = ""

  Public Property Items As New List(Of Item)
  Public Property RefDocs As New List(Of RefDoc)

  Public Property PDFFilePathName As String = ""
  Public Property LibraryID As String = ""
  Public Property LibraryPath As String = ""

  Public Property Errors As New ArrayList
  Public Property SendMailRequired As Boolean = False

  Public ReadOnly Property AttachmentIndex As String
    Get
      Return drgid & "_" & rev
    End Get
  End Property
  Private _ErpCompany As String = ""
  Public Property ERPCompany As String
    Get
      If _ErpCompany = "" Then
        If VaultDBName = "ISGEC REDECAM" Then
          Return "700"
        ElseIf VaultDBName = "ISGEC COVEMA" Then
          Return "651"
        Else
          Return "200"
        End If
      Else
        Return _ErpCompany
      End If
    End Get
    Set(value As String)
      _ErpCompany = value
    End Set
  End Property
  Public ReadOnly Property ItemSuffix As String
    Get
      Return drgid.Substring(6)
    End Get
  End Property


  Public Shared Function GetVaultXML(ByVal FilePath As String) As vaultXML
    Dim tmp As vaultXML = Nothing
    Dim oXml As New XmlDocument
    Try
      oXml.Load(FilePath)
    Catch ex As Exception
      Return tmp
    End Try
    tmp = New vaultXML

    Dim nDoc As XmlNode = oXml.ChildNodes(1)
    Dim nPrj As XmlNode = oXml.ChildNodes(1).ChildNodes(0)
    Dim nItms As XmlNode = oXml.ChildNodes(1).ChildNodes(1)
    Dim nRefs As XmlNode = oXml.ChildNodes(1).ChildNodes(2)

    tmp.PDF_filename = nDoc.Attributes("PDF_filename").Value
    tmp.filename = nDoc.Attributes("filename").Value
    tmp.number = nDoc.Attributes("number").Value
    tmp.title = nDoc.Attributes("title").Value
    tmp.rev = nDoc.Attributes("rev").Value
    tmp.el_id = nDoc.Attributes("el.id.").Value
    tmp.sheetsize = nDoc.Attributes("sheetsize").Value
    tmp.scale = nDoc.Attributes("scale").Value
    tmp.weight = nDoc.Attributes("weight").Value
    tmp.drawn = nDoc.Attributes("drawn").Value
    tmp.chqd = nDoc.Attributes("chqd").Value
    tmp.appd = nDoc.Attributes("appd").Value
    tmp.dDate = nDoc.Attributes("date").Value
    tmp.resp_dept = nDoc.Attributes("resp_dept").Value
    tmp.apr = nDoc.Attributes("apr").Value
    tmp.inf = nDoc.Attributes("inf").Value
    tmp.pro = nDoc.Attributes("pro").Value
    tmp.ere = nDoc.Attributes("ere").Value
    tmp.drgid = nDoc.Attributes("drgid").Value
    tmp.VaultDBName = nDoc.Attributes("VaultDBName").Value
    tmp.VaultUserName = nDoc.Attributes("VaultUserName").Value
    tmp.VaultClientMachine = nDoc.Attributes("VaultClientMachine").Value
    tmp.VaultSubmittedDate = nDoc.Attributes("VaultSubmittedDate").Value
    tmp.ISGEC_Datasource = nDoc.Attributes("ISGEC_Datasource").Value
    tmp.name_software = nDoc.Attributes("name_software").Value
    Try
      tmp.slzFileName = nDoc.Attributes("slzFileName").Value
      tmp.ORGFilePathName = nDoc.Attributes("ORGFilePathName").Value
    Catch ex As Exception
    End Try

    tmp.contract = nPrj.Attributes("contract").Value
    tmp.service1 = nPrj.Attributes("service1").Value
    tmp.service2 = nPrj.Attributes("service2").Value
    tmp.iwt = nPrj.Attributes("iwt").Value
    tmp.year = nPrj.Attributes("year").Value
    tmp.consultant = nPrj.Attributes("consultant").Value
    tmp.client = nPrj.Attributes("client").Value
    tmp.group = nPrj.Attributes("group").Value

    For Each nItm As XmlNode In nItms.ChildNodes
      Dim tmpItm As New Item
      tmpItm.remark = nItm.Attributes("remark").Value
      tmpItm.it_qty = nItm.Attributes("it.qty").Value
      tmpItm.it_wt = nItm.Attributes("it.wt").Value
      tmpItm.item_g = nItm.Attributes("item_g").Value
      tmpItm.t = nItm.Attributes("t").Value
      tmpItm.item_code = nItm.Attributes("item_code").Value
      tmpItm.item_descn = nItm.Attributes("item_descn").Value
      tmp.Items.Add(tmpItm)
      Dim pItms As XmlNode = nItm.ChildNodes(0)
      For Each pItm As XmlNode In pItms.ChildNodes
        Dim tmpPItm As New Part
        tmpPItm.p_no = pItm.Attributes("p.no").Value
        tmpPItm.p_desc = pItm.Attributes("p_desc").Value
        tmpPItm.spec = pItm.Attributes("spec").Value
        tmpPItm.size = pItm.Attributes("size").Value
        tmpPItm.p_qty = pItm.Attributes("p_qty").Value
        tmpPItm.p_wt = pItm.Attributes("p_wt").Value
        tmpPItm.remark = pItm.Attributes("remark").Value
        tmpItm.Parts.Add(tmpPItm)
      Next
    Next
    For Each rDoc As XmlNode In nRefs.ChildNodes
      Dim tmpDoc As New RefDoc
      tmpDoc.drgno = rDoc.Attributes("drgno").Value
      tmpDoc.drg_descn = rDoc.Attributes("drg_descn").Value
      tmpDoc.rev = rDoc.Attributes("rev").Value
      tmpDoc.PDF_filename = rDoc.Attributes("PDF_filename").Value
      tmpDoc.filename = rDoc.Attributes("filename").Value
      tmp.RefDocs.Add(tmpDoc)
    Next

    If tmp.rev.Trim.Length < 2 Then tmp.rev.PadLeft(2, "0")

    Return tmp
  End Function
  Public Shared Function GetHTMLError(ByVal fl As vaultXML) As String
    Dim oTbl As New Table
    oTbl.GridLines = GridLines.Both
    oTbl.Width = 700
    oTbl.Style.Add("text-align", "left")
    oTbl.Style.Add("font", "Tahoma")
    Dim oCol As TableCell = Nothing
    Dim oRow As TableRow = Nothing
    '1.
    oRow = New TableRow
    oCol = New TableCell
    oCol.Text = "Transfer to BaaN-PLM Failed"
    oCol.Style.Add("text-align", "center")
    oCol.Style.Add("border-bottom", "none")
    oCol.Font.Size = "14"
    oRow.Cells.Add(oCol)
    oTbl.Rows.Add(oRow)
    '2.
    oRow = New TableRow
    oCol = New TableCell
    oCol.Text = "<u>Error(s) found while Transfering Document to BaaN-PLM</u>"
    oCol.Style.Add("text-align", "center")
    oCol.Style.Add("border-bottom", "none")
    oCol.Font.Size = "10"
    oRow.Cells.Add(oCol)
    oTbl.Rows.Add(oRow)
    '2.
    oRow = New TableRow
    oCol = New TableCell
    oCol.Text = "Transfer Stopped. Please correct them and transfer again."
    oCol.Style.Add("text-align", "center")
    oCol.Style.Add("border-bottom", "none")
    oCol.Font.Size = "10"
    oRow.Cells.Add(oCol)
    oTbl.Rows.Add(oRow)
    For Each dd As String In fl.Errors
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = dd
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = dd
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
    Next

    Dim sb As StringBuilder = New StringBuilder()
    Dim sw As IO.StringWriter = New IO.StringWriter(sb)
    Dim writer As HtmlTextWriter = New HtmlTextWriter(sw)
    Try
      oTbl.RenderControl(writer)
    Catch ex As Exception
    End Try
    Dim Header As String = ""
    Header = Header & "<html xmlns=""http://www.w3.org/1999/xhtml"">"
    Header = Header & "<head>"
    Header = Header & "<title></title>"
    Header = Header & "<style>"
    Header = Header & "table{"

    Header = Header & "border: solid 1pt black;"
    Header = Header & "border-collapse:collapse;"
    Header = Header & "font-family: Tahoma;}"

    Header = Header & "td{"
    Header = Header & "border: solid 1pt black;"
    Header = Header & "font-family: Tahoma;"
    Header = Header & "font-size: 9px;"
    Header = Header & "vertical-align:top;}"

    Header = Header & "</style>"
    Header = Header & "</head>"
    Header = Header & "<body>"
    Header = Header & sb.ToString
    Header = Header & "</body></html>"

    Return sb.ToString
  End Function

  Public Shared Sub SendMail(ByVal fl As vaultXML)
    Dim emailIDError As Boolean = False
    Dim ToEMailID As String = ""
    Try
      ToEMailID = GetMailID(fl.VaultUserName)
    Catch ex As Exception
      ToEMailID = "baansupport@isgec.co.in"
      emailIDError = True
    End Try
    Try
      Dim oClient As SmtpClient = New SmtpClient("192.9.200.214", 25)
      oClient.Credentials = New Net.NetworkCredential("adskvaultadmin", "isgec@123")
      Dim oMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
      oMsg.From = New System.Net.Mail.MailAddress("Error IN Transfer Vault To ERPLN<adskvaultadmin@isgec.co.in>")
      With oMsg
        If ToEMailID <> String.Empty Then
          .To.Add(ToEMailID)
        End If
        .To.Add("adskvaultadmin@isgec.co.in")
        '.CC.Add("harishkumar@isgec.co.in")
        .IsBodyHtml = True
        .Subject = fl.number & " Not Transfered in BaaN"
        If emailIDError Then
          .Body = "E-Mail Address Not found for Vault User : " & fl.VaultUserName
        End If
        .Body = .Body & vaultXML.GetHTMLError(fl)
      End With
      If SIS.SYS.SQLDatabase.DBCommon.BaaNLive Then
        oClient.Send(oMsg)
      End If
    Catch ex As Exception
    End Try
  End Sub

  Public Shared Function GetMailID(ByVal LoginID As String) As String
    Dim _Result As String = ""
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString)
      Using Cmd As SqlCommand = Con.CreateCommand()
        Dim mSql As String = "SELECT TOP 1 EMailID FROM HRM_Employees WHERE CardNo = '" & LoginID & "'"
        Cmd.CommandType = System.Data.CommandType.Text
        Cmd.CommandText = mSql
        Con.Open()
        _Result = Cmd.ExecuteScalar()
        If _Result IsNot Nothing Then
          If Convert.IsDBNull(_Result) Then
            _Result = ""
          End If
        Else
          _Result = ""
        End If
      End Using
    End Using
    Return _Result
  End Function

End Class
