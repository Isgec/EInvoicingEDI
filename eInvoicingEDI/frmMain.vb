Imports System.Reflection
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Web.Script.Serialization
Imports System.IO

Public Class frmMain
  Delegate Sub ThreadedSub(InvNo As String)
  Delegate Sub ThreadedShow(ByVal slzFile As String)
  Delegate Sub ThreadedNone()
  Dim WithEvents jp As JobProcess.JobProcessor = Nothing
  Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
    Dim InvNo As String = F_Invoice.Text
    If InvNo <> "" Then
      If MessageBox.Show(Me, "Do you want to Re-download QR Code and Invoice PDF ?" & vbCrLf & "OR" & vbCrLf & "To start EDI, Please remove Invoice No", "", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
        Exit Sub
      End If
    End If
    cmdStart.Enabled = False
    cmdStart.Text = "Loading..."
    ListBox1.Items.Clear()
    ListBox2.Items.Clear()
    Dim tmp As ThreadedSub = AddressOf Start
    tmp.BeginInvoke(InvNo, Nothing, Nothing)
  End Sub
  Private Sub Start(InvNo As String)
    jp = New JobProcess.JobProcessor()
    jp.InvoiceNo = InvNo
    jp.Start()
  End Sub

  Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
    cmdStop.Enabled = False
    cmdStop.Text = "Closing..."
    jp.StopJob()
  End Sub

  Private Sub jp_JobStarted() Handles jp.JobStarted
    If cmdStart.InvokeRequired Then
      cmdStart.Invoke(New ThreadedNone(AddressOf jp_JobStarted))
    Else
      cmdStart.Enabled = False
      cmdStart.Text = "Start"
      cmdStop.Enabled = True
    End If
  End Sub
  Private Sub jp_JobStopped() Handles jp.JobStopped
    If cmdStop.InvokeRequired Then
      cmdStop.Invoke(New ThreadedNone(AddressOf jp_JobStopped))
    Else
      cmdStop.Enabled = False
      cmdStop.Text = "Stop"
      cmdStart.Enabled = True
      F_Invoice.Text = ""
    End If
  End Sub

  Private Sub jp_Show(Message As String) Handles jp.Show
    If ListBox1.InvokeRequired Then
      ListBox1.Invoke(New ThreadedShow(AddressOf jp_Show), Message)
    Else
      Label1.Text = Message
    End If
  End Sub
  Private Sub jp_Log(Message As String) Handles jp.Log
    If ListBox1.InvokeRequired Then
      ListBox1.Invoke(New ThreadedShow(AddressOf jp_Log), Message)
    Else
      ListBox1.Items.Insert(0, Message)
      ' Label1.Text = Message
    End If
  End Sub

  Private Sub jp_Err(Message As String) Handles jp.Err
    If ListBox2.InvokeRequired Then
      ListBox2.Invoke(New ThreadedShow(AddressOf jp_Err), Message)
    Else
      ListBox2.Items.Insert(0, Message)
      ' Label1.Text = Message
    End If
  End Sub

  Private Sub ListBox2_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox2.MouseDoubleClick
    Dim s As ListBox = CType(sender, ListBox)
    Dim f As New frmView
    Dim x As String = s.SelectedItem.ToString
    If x.StartsWith("Submitted JSON=>") Then
      x = x.Replace("Submitted JSON=>", "")
      Dim t As New TreeView
      Dim Inv As JobProcess.SIS.CIISG.miSubmitInvoice = New JavaScriptSerializer().Deserialize(x, GetType(JobProcess.SIS.CIISG.miSubmitInvoice))
      LoadTree(t, Inv)
      f.Controls.Add(t)
    ElseIf x.StartsWith("Response JSON=>") Then
      x = x.Replace("Response JSON=>", "")
      Dim t As New TreeView
      Dim Inv As JobProcess.SIS.CIISG.miSubmitResponce = New JavaScriptSerializer().Deserialize(x, GetType(JobProcess.SIS.CIISG.miSubmitResponce))
      LoadTree(t, Inv)
      f.Controls.Add(t)
    ElseIf x.StartsWith("Cancel Submited JSON=>") Then
      x = x.Replace("Cancel Submited JSON=>", "")
      Dim t As New TreeView
      Dim Inv As JobProcess.SIS.CIISG.miCancelInvoice = New JavaScriptSerializer().Deserialize(x, GetType(JobProcess.SIS.CIISG.miCancelInvoice))
      LoadTree(t, Inv)
      f.Controls.Add(t)
    ElseIf x.StartsWith("Cancel Response JSON=>") Then
      x = x.Replace("Cancel Response JSON=>", "")
      Dim t As New TreeView
      Dim Inv As JobProcess.SIS.CIISG.miCancelResponce = New JavaScriptSerializer().Deserialize(x, GetType(JobProcess.SIS.CIISG.miCancelResponce))
      LoadTree(t, Inv)
      f.Controls.Add(t)
    Else
      Dim l As New TextBox
      With l
        .Multiline = True
        .BorderStyle = BorderStyle.None
        .BackColor = Color.Black
        .Font = New Font("Ariel", 12)
        .ForeColor = Color.White
        .Text = x
        .Dock = DockStyle.Fill
        .Padding = New Padding(4)
      End With
      f.Controls.Add(l)
    End If
    f.Top = e.Location.Y
    f.Left = e.Location.X
    f.Show()
  End Sub
  Private Sub LoadTree(t As TreeView, Inv As Object)
    Dim sw As StringWriter = New StringWriter()
    Dim xs As New XmlSerializer(Inv.GetType())
    xs.Serialize(sw, Inv)
    Dim dom As New XmlDocument
    dom.LoadXml(sw.ToString())
    t.Nodes.Add(New TreeNode(dom.DocumentElement.Name))
    Dim tNode As TreeNode = New TreeNode()
    tNode = t.Nodes(0)
    AddNode(dom.DocumentElement, tNode)
    t.Nodes(0).Expand()
    With t
      .Font = New Font("Ariel", 12)
      .BackColor = Color.Black
      .ForeColor = Color.Lime
      .Dock = DockStyle.Fill
      .Padding = New Padding(4)
    End With
  End Sub

  Private Sub AddNode(inXmlNode As XmlNode, inTreeNode As TreeNode)
    Dim XNode As XmlNode
    Dim tNode As TreeNode
    Dim nodeList As XmlNodeList
    Dim i As Integer
    'If (inXmlNode.HasChildNodes) Then
    nodeList = inXmlNode.ChildNodes
      For i = 0 To nodeList.Count - 1
        XNode = inXmlNode.ChildNodes(i)
        Try
          If XNode.HasChildNodes Then
            If XNode.ChildNodes(0).Name = "#text" Then
              Dim nd As TreeNode = New TreeNode(XNode.Name & IIf(XNode.ChildNodes(0).InnerText <> "", "=" & (XNode.ChildNodes(0).InnerText).Trim(), ""))
              nd.ForeColor = Color.Yellow
              inTreeNode.Nodes.Add(nd)
            Else
              inTreeNode.Nodes.Add(New TreeNode(XNode.Name))
              tNode = inTreeNode.Nodes(i)
              AddNode(XNode, tNode)
            End If
          Else
            inTreeNode.Nodes.Add(New TreeNode(XNode.Name))
            inTreeNode.ForeColor = Color.Aqua
          End If
        Catch ex As Exception
        End Try
      Next
    'Else
    '  inTreeNode.Parent.Text = inTreeNode.Parent.Text & IIf(inXmlNode.InnerText <> "", "=" & (inXmlNode.InnerText).Trim(), "")
    '  inTreeNode.ForeColor = Color.Yellow
    'End If
  End Sub

  Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
    Dim dom As New XmlDocument
    dom.Load(Application.StartupPath & "\Settings.xml")
    For Each nd As XmlNode In dom.ChildNodes(1).ChildNodes
      If nd.Name = "JSONLog" Then
        F_JSONLog.Checked = Convert.ToBoolean(nd.InnerText)
        Exit For
      End If
    Next
  End Sub

  Private Sub F_JSONLog_CheckedChanged(sender As Object, e As EventArgs) Handles F_JSONLog.CheckedChanged
    Dim dom As New XmlDocument
    dom.Load(Application.StartupPath & "\Settings.xml")
    For Each nd As XmlNode In dom.ChildNodes(1).ChildNodes
      If nd.Name = "JSONLog" Then
        nd.InnerText = IIf(F_JSONLog.Checked, "true", "false")
        Exit For
      End If
    Next
    dom.Save(Application.StartupPath & "\Settings.xml")
  End Sub
End Class