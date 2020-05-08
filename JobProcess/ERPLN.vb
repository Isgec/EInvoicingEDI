Public Class ERPLN
  Public Delegate Sub showMsg(ByVal str As String)
  'Public Shared Function InsertUpdateInERPLN(ByVal t As vaultXML, Optional ByVal msg As showMsg = Nothing) As Boolean
  '  Dim mRet As Boolean = True
  '  Dim dmDoc As SIS.DMISG.dmisg121 = Nothing
  '  If t.rev = "00" Then
  '    Dim tmpDoc As SIS.DMISG.dmisg001 = SIS.DMISG.dmisg001.dmisg001GetByID(t.drgid, t.rev, t.ERPCompany)
  '    If tmpDoc IsNot Nothing Then
  '      If tmpDoc.t_wfst <> wfst.UnderDesign Then
  '        Throw New Exception("Document is not UNDER DESIGN.")
  '      End If
  '      SIS.DMISG.dmisg001.dmisg001DeleteAll(t.drgid, t.rev, t.ERPCompany)
  '      '===Physical File Delete only when ERP is LIVE
  '      If SIS.SYS.SQLDatabase.DBCommon.BaaNLive Then
  '        Dim tmp As SIS.EDI.ediAFile = SIS.EDI.ediAFile.ediAFileGetByHandleIndex("DOCUMENTMASTERPDF_" & t.ERPCompany, t.AttachmentIndex)
  '        If tmp IsNot Nothing Then
  '          Try
  '            If IO.File.Exists(t.LibraryPath & "\" & tmp.t_dcid) Then
  '              IO.File.Delete(t.LibraryPath & "\" & tmp.t_dcid)
  '            End If
  '          Catch ex As Exception
  '          End Try
  '          SIS.EDI.ediAFile.ediAFileDelete(tmp)
  '        End If
  '        tmp = SIS.EDI.ediAFile.ediAFileGetByHandleIndex("DOCUMENTMASTERORG_" & t.ERPCompany, t.AttachmentIndex)
  '        If tmp IsNot Nothing Then
  '          Try
  '            If IO.File.Exists(t.LibraryPath & "\" & tmp.t_dcid) Then
  '              IO.File.Delete(t.LibraryPath & "\" & tmp.t_dcid)
  '            End If
  '          Catch ex As Exception
  '          End Try
  '          SIS.EDI.ediAFile.ediAFileDelete(tmp)
  '        End If
  '      End If
  '      '===Physical File Handling
  '    End If
  '    Try
  '      '1. Insert in all 5 tables
  '      tmpDoc = SIS.DMISG.dmisg001.Getdmisg001(t)
  '      tmpDoc = SIS.DMISG.dmisg001.InsertData(tmpDoc, t.ERPCompany)
  '      '2. Item & 4 Part Item
  '      Dim tmpCnt As Integer = 0
  '      Dim refCnt As Integer = 0
  '      Dim pCnt As Integer = 0
  '      Dim pItem As String = ""
  '      For Each itm As vaultXML.Item In t.Items
  '        pCnt = 0
  '        Select Case itm.t.ToUpper
  '          Case "R"
  '            refCnt += 1
  '            Dim refItm As SIS.DMISG.dmisg021 = SIS.DMISG.dmisg021.Getdmisg021(itm, t, refCnt)
  '            refItm = SIS.DMISG.dmisg021.InsertData(refItm, t.ERPCompany)
  '            pItem = itm.item_code
  '            For Each pItm As vaultXML.Part In itm.Parts
  '              pCnt += 1
  '              Dim refPitm As SIS.DMISG.dmisg022 = SIS.DMISG.dmisg022.Getdmisg022(pItm, t, refCnt, pCnt, pItem)
  '              refPitm = SIS.DMISG.dmisg022.InsertData(refPitm, t.ERPCompany)
  '            Next
  '          Case Else
  '            tmpCnt += 1
  '            Dim tmpItm As SIS.DMISG.dmisg002 = SIS.DMISG.dmisg002.Getdmisg002(itm, t, tmpCnt)
  '            tmpItm = SIS.DMISG.dmisg002.InsertData(tmpItm, t.ERPCompany)
  '            pItem = itm.item_code
  '            For Each pItm As vaultXML.Part In itm.Parts
  '              pCnt += 1
  '              Dim tmpPitm As SIS.DMISG.dmisg004 = SIS.DMISG.dmisg004.Getdmisg004(pItm, t, tmpCnt, pCnt, pItem)
  '              tmpPitm = SIS.DMISG.dmisg004.InsertData(tmpPitm, t.ERPCompany)
  '            Next
  '        End Select
  '      Next
  '      '3. Ref Dwg
  '      For Each doc As vaultXML.RefDoc In t.RefDocs
  '        Dim refD As SIS.DMISG.dmisg003 = SIS.DMISG.dmisg003.Getdmisg003(doc, t)
  '        Try
  '          refD = SIS.DMISG.dmisg003.InsertData(refD, t.ERPCompany)
  '        Catch ex As Exception
  '          If msg IsNot Nothing Then msg(refD.t_drgt & ": " & ex.Message)
  '        End Try
  '      Next
  '      '5. Master Document List
  '      dmDoc = SIS.DMISG.dmisg121.Getdmisg121(t)
  '      dmDoc = SIS.DMISG.dmisg121.InsertData(dmDoc, t.ERPCompany)
  '    Catch ex As Exception
  '      SIS.DMISG.dmisg001.dmisg001DeleteAll(t.drgid, t.rev, t.ERPCompany)
  '      t.SendMailRequired = True
  '      Throw New Exception(ex.Message)
  '    End Try
  '  Else
  '    Dim tmpDoc As SIS.DMISG.dmisg001 = SIS.DMISG.dmisg001.dmisg001GetByID(t.drgid, t.rev, t.ERPCompany)
  '    If tmpDoc Is Nothing Then
  '      Dim p_rev As String = Convert.ToString(Convert.ToInt32(t.rev) - 1).PadLeft(2, "0")
  '      Dim pDoc As SIS.DMISG.dmisg001 = SIS.DMISG.dmisg001.dmisg001GetByID(t.drgid, p_rev, t.ERPCompany)
  '      If pDoc Is Nothing Then
  '        t.SendMailRequired = True
  '        t.Errors.Add("Preceding Revision of Document Not Found in ERP.: " & p_rev)
  '        Throw New Exception("Preceding Revision of Document Not Found in ERP.: " & p_rev)
  '      End If
  '      If pDoc.t_wfst <> wfst.Superseded Then
  '        If pDoc.t_wfst <> wfst.UnderRevision Then
  '          Throw New Exception("Document is not UNDER REVISION.")
  '        End If
  '        pDoc.t_wfst = wfst.Superseded
  '        pDoc = SIS.DMISG.dmisg001.UpdateData(pDoc, t.ERPCompany)
  '        '121
  '        dmDoc = SIS.DMISG.dmisg121.dmisg121GetByID(t.drgid, p_rev, t.ERPCompany)
  '        If dmDoc IsNot Nothing Then
  '          dmDoc.t_bloc = yesno.YES
  '          dmDoc = SIS.DMISG.dmisg121.UpdateData(dmDoc, t.ERPCompany)
  '        End If
  '      End If
  '    Else
  '      If tmpDoc.t_wfst <> wfst.UnderDesign Then
  '        Throw New Exception("Document is not UNDER DESIGN.")
  '      End If
  '      SIS.DMISG.dmisg001.dmisg001DeleteAll(t.drgid, t.rev, t.ERPCompany)
  '      '==========Physical File Delete When ERP is Live Handling============
  '      If SIS.SYS.SQLDatabase.DBCommon.BaaNLive Then
  '        Dim tmp As SIS.EDI.ediAFile = SIS.EDI.ediAFile.ediAFileGetByHandleIndex("DOCUMENTMASTERPDF_" & t.ERPCompany, t.AttachmentIndex)
  '        If tmp IsNot Nothing Then
  '          If IO.File.Exists(t.LibraryPath & "\" & tmp.t_dcid) Then
  '            IO.File.Delete(t.LibraryPath & "\" & tmp.t_dcid)
  '          End If
  '          SIS.EDI.ediAFile.ediAFileDelete(tmp)
  '        End If
  '        tmp = SIS.EDI.ediAFile.ediAFileGetByHandleIndex("DOCUMENTMASTERORG_" & t.ERPCompany, t.AttachmentIndex)
  '        If tmp IsNot Nothing Then
  '          If IO.File.Exists(t.LibraryPath & "\" & tmp.t_dcid) Then
  '            IO.File.Delete(t.LibraryPath & "\" & tmp.t_dcid)
  '          End If
  '          SIS.EDI.ediAFile.ediAFileDelete(tmp)
  '        End If
  '      End If
  '      '================Physical File Handling==========================
  '    End If
  '    Try
  '      '1. Insert in all 5 tables
  '      tmpDoc = SIS.DMISG.dmisg001.Getdmisg001(t)
  '      tmpDoc = SIS.DMISG.dmisg001.InsertData(tmpDoc, t.ERPCompany)
  '      '2. Item & 4 Part Item
  '      Dim tmpCnt As Integer = 0
  '      Dim refCnt As Integer = 0
  '      Dim pCnt As Integer = 0
  '      Dim pItem As String = ""
  '      For Each itm As vaultXML.Item In t.Items
  '        pCnt = 0
  '        Select Case itm.t.ToUpper
  '          Case "R"
  '            refCnt += 1
  '            Dim refItm As SIS.DMISG.dmisg021 = SIS.DMISG.dmisg021.Getdmisg021(itm, t, refCnt)
  '            refItm = SIS.DMISG.dmisg021.InsertData(refItm, t.ERPCompany)
  '            pItem = itm.item_code
  '            For Each pItm As vaultXML.Part In itm.Parts
  '              pCnt += 1
  '              Dim refPitm As SIS.DMISG.dmisg022 = SIS.DMISG.dmisg022.Getdmisg022(pItm, t, refCnt, pCnt, pItem)
  '              refPitm = SIS.DMISG.dmisg022.InsertData(refPitm, t.ERPCompany)
  '            Next
  '          Case Else
  '            tmpCnt += 1
  '            Dim tmpItm As SIS.DMISG.dmisg002 = SIS.DMISG.dmisg002.Getdmisg002(itm, t, tmpCnt)
  '            tmpItm = SIS.DMISG.dmisg002.InsertData(tmpItm, t.ERPCompany)
  '            pItem = itm.item_code
  '            For Each pItm As vaultXML.Part In itm.Parts
  '              pCnt += 1
  '              Dim tmpPitm As SIS.DMISG.dmisg004 = SIS.DMISG.dmisg004.Getdmisg004(pItm, t, tmpCnt, pCnt, pItem)
  '              tmpPitm = SIS.DMISG.dmisg004.InsertData(tmpPitm, t.ERPCompany)
  '            Next
  '        End Select
  '      Next
  '      '3. Ref Dwg
  '      For Each doc As vaultXML.RefDoc In t.RefDocs
  '        Dim refD As SIS.DMISG.dmisg003 = SIS.DMISG.dmisg003.Getdmisg003(doc, t)
  '        Try
  '          refD = SIS.DMISG.dmisg003.InsertData(refD, t.ERPCompany)
  '        Catch ex As Exception
  '          If msg IsNot Nothing Then msg(refD.t_drgt & ": " & ex.Message)
  '        End Try
  '      Next
  '      '5. Master Document List
  '      dmDoc = SIS.DMISG.dmisg121.Getdmisg121(t)
  '      dmDoc = SIS.DMISG.dmisg121.InsertData(dmDoc, t.ERPCompany)
  '    Catch ex As Exception
  '      SIS.DMISG.dmisg001.dmisg001DeleteAll(t.drgid, t.rev, t.ERPCompany)
  '      t.SendMailRequired = True
  '      Throw New Exception(ex.Message)
  '    End Try
  '  End If
  '  Return mRet
  'End Function
  'Public Shared Function UploadInISGECVault(ByVal t As vaultXML, Optional ByVal msg As showMsg = Nothing) As Boolean
  '  If IO.File.Exists(t.PDFFilePathName) Then
  '    Dim LibFileName As String = ""
  '    Dim Found As Boolean = True
  '    '1. Check PDF Attachment Found
  '    Dim tmp As SIS.EDI.ediAFile = SIS.EDI.ediAFile.ediAFileGetByHandleIndex("DOCUMENTMASTERPDF_" & t.ERPCompany, t.AttachmentIndex)
  '    If tmp Is Nothing Then
  '      Found = False
  '      tmp = New SIS.EDI.ediAFile
  '      LibFileName = SIS.EDI.ediASeries.GetNextFileName
  '    Else
  '      LibFileName = tmp.t_dcid
  '    End If
  '    With tmp
  '      .t_dcid = LibFileName
  '      .t_hndl = "DOCUMENTMASTERPDF_" & t.ERPCompany
  '      .t_indx = t.AttachmentIndex
  '      .t_prcd = "By EDI"
  '      .t_fnam = IO.Path.GetFileName(t.PDFFilePathName)
  '      .t_lbcd = t.LibraryID
  '      .t_atby = t.VaultUserName
  '      .t_aton = Now
  '      .t_Refcntd = 0
  '      .t_Refcntu = 0
  '    End With
  '    If Not Found Then
  '      tmp = SIS.EDI.ediAFile.InsertData(tmp)
  '    Else
  '      tmp = SIS.EDI.ediAFile.UpdateData(tmp)
  '    End If
  '    If msg IsNot Nothing Then
  '      msg.Invoke("PDF Handle: " & tmp.t_fnam)
  '    End If
  '    Try
  '      '2. Move & Overwrite File
  '      If IO.File.Exists(t.LibraryPath & "\" & LibFileName) Then
  '        IO.File.Delete(t.LibraryPath & "\" & LibFileName)
  '      End If
  '      IO.File.Copy(t.PDFFilePathName, t.LibraryPath & "\" & LibFileName)
  '      If msg IsNot Nothing Then
  '        msg.Invoke("PDF Copied: " & tmp.t_fnam)
  '      End If
  '    Catch ex As Exception
  '      If msg IsNot Nothing Then
  '        msg.Invoke("Error Copying PDF: " & tmp.t_fnam)
  '      End If
  '    End Try
  '  End If
  '  '=================================
  '  If IO.File.Exists(t.ORGFilePathName) Then
  '    Dim LibFileName As String = ""
  '    Dim Found As Boolean = True
  '    '1. Check ORG Attachment Found
  '    Dim tmp As SIS.EDI.ediAFile = SIS.EDI.ediAFile.ediAFileGetByHandleIndex("DOCUMENTMASTERORG_" & t.ERPCompany, t.AttachmentIndex)
  '    If tmp Is Nothing Then
  '      Found = False
  '      tmp = New SIS.EDI.ediAFile
  '      LibFileName = SIS.EDI.ediASeries.GetNextFileName
  '    Else
  '      LibFileName = tmp.t_dcid
  '    End If
  '    With tmp
  '      .t_dcid = LibFileName
  '      .t_hndl = "DOCUMENTMASTERORG_" & t.ERPCompany
  '      .t_indx = t.AttachmentIndex
  '      .t_prcd = "By EDI"
  '      .t_fnam = IO.Path.GetFileName(t.ORGFilePathName)
  '      .t_lbcd = t.LibraryID
  '      .t_atby = t.VaultUserName
  '      .t_aton = Now
  '      .t_Refcntd = 0
  '      .t_Refcntu = 0
  '    End With
  '    If Not Found Then
  '      tmp = SIS.EDI.ediAFile.InsertData(tmp)
  '    Else
  '      tmp = SIS.EDI.ediAFile.UpdateData(tmp)
  '    End If
  '    If msg IsNot Nothing Then
  '      msg.Invoke("ORG Handle: " & tmp.t_fnam)
  '    End If
  '    Try
  '      '2. Move & Overwrite File
  '      If IO.File.Exists(t.LibraryPath & "\" & LibFileName) Then
  '        IO.File.Delete(t.LibraryPath & "\" & LibFileName)
  '      End If
  '      IO.File.Move(t.ORGFilePathName, t.LibraryPath & "\" & LibFileName)
  '      If msg IsNot Nothing Then
  '        msg.Invoke("ORG Copied: " & tmp.t_fnam)
  '      End If
  '    Catch ex As Exception
  '      If msg IsNot Nothing Then
  '        msg.Invoke("Error Copying ORG: " & tmp.t_fnam)
  '      End If
  '    End Try
  '  End If
  '  Return True
  'End Function

End Class
