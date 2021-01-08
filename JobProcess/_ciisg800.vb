Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.CIISG
  <DataObject()>
  Public Class ciisg800
    Public Property t_comp As Integer = 0
    Public Property t_tran As String = ""
    Public Property t_docn As String = ""
    Public Property t_ogst As String = ""
    Public Property t_taxs As String = ""
    Public Property t_vers As Integer = 0
    Public Property t_irnn As String = ""
    Public Property t_catg As String = ""
    Public Property t_regr As String = ""
    Public Property t_ttyp As String = ""
    Public Property t_etrn As Integer = 0
    Public Property t_egst As String = ""
    Public Property t_dtyp As String = ""
    Public Property t_ninv As String = ""
    Public Property t_odoc As String = ""
    Public Property t_odat As DateTime
    Public Property t_bfgn As String = ""
    Public Property t_bfnm As String = ""
    Public Property t_bfbn As String = ""
    Public Property t_bfbm As String = ""
    Public Property t_bffn As String = ""
    Public Property t_bflc As String = ""
    Public Property t_bfdt As String = ""
    Public Property t_bfpn As String = ""
    Public Property t_bfsc As String = ""
    Public Property t_bfph As String = ""
    Public Property t_bfem As String = ""
    Public Property t_btgn As String = ""
    Public Property t_btnm As String = ""
    Public Property t_btbn As String = ""
    Public Property t_btbm As String = ""
    Public Property t_btfn As String = ""
    Public Property t_btlc As String = ""
    Public Property t_btdt As String = ""
    Public Property t_btpn As String = ""
    Public Property t_btsc As String = ""
    Public Property t_btph As String = ""
    Public Property t_btem As String = ""
    Public Property t_sfgn As String = ""
    Public Property t_sfnm As String = ""
    Public Property t_sfbn As String = ""
    Public Property t_sfbm As String = ""
    Public Property t_sffn As String = ""
    Public Property t_sflc As String = ""
    Public Property t_sfdt As String = ""
    Public Property t_sfpn As String = ""
    Public Property t_sfsc As String = ""
    Public Property t_sfph As String = ""
    Public Property t_sfem As String = ""
    Public Property t_stgn As String = ""
    Public Property t_stnm As String = ""
    Public Property t_stbn As String = ""
    Public Property t_stbm As String = ""
    Public Property t_stfn As String = ""
    Public Property t_stlc As String = ""
    Public Property t_stdt As String = ""
    Public Property t_stpn As String = ""
    Public Property t_stsc As String = ""
    Public Property t_stph As String = ""
    Public Property t_stem As String = ""
    Public Property t_item As String = ""
    Public Property t_dsca As String = ""
    Public Property t_hsnc As String = ""
    Public Property t_barc As String = ""
    Public Property t_qnty As Decimal = 0.00
    Public Property t_fqty As Decimal = 0.00
    Public Property t_cuni As String = ""
    Public Property t_pric As Decimal = 0.00
    Public Property t_amnt As Decimal = 0.00
    Public Property t_disc As Decimal = 0.00
    Public Property t_othc As Decimal = 0.00
    Public Property t_assv As Decimal = 0.00
    Public Property t_cgst As Decimal = 0.00
    Public Property t_sgst As Decimal = 0.00
    Public Property t_igst As Decimal = 0.00
    Public Property t_cess As Decimal = 0.00
    Public Property t_cnaa As Decimal = 0.00
    Public Property t_scsr As Decimal = 0.00
    Public Property t_valu As Decimal = 0.00
    Public Property t_batn As String = ""
    Public Property t_bate As DateTime
    Public Property t_ward As DateTime
    Public Property t_tval As Decimal = 0.00
    Public Property t_tcgs As Decimal = 0.00
    Public Property t_tsgs As Decimal = 0.00
    Public Property t_tigs As Decimal = 0.00
    Public Property t_tces As Decimal = 0.00
    Public Property t_tscs As Decimal = 0.00
    Public Property t_tsna As Decimal = 0.00
    Public Property t_tdis As Decimal = 0.00
    Public Property t_othv As Decimal = 0.00
    Public Property t_fivl As Decimal = 0.00
    Public Property t_payn As String = ""
    Public Property t_modp As String = ""
    Public Property t_ifsc As String = ""
    Public Property t_tpay As String = ""
    Public Property t_payi As String = ""
    Public Property t_cret As String = ""
    Public Property t_dird As String = ""
    Public Property t_cred As Integer = 0
    Public Property t_blmt As Decimal = 0.00
    Public Property t_ddop As DateTime
    Public Property t_accd As String = ""
    Public Property t_invr As String = ""
    Public Property t_ipst As DateTime
    Public Property t_iped As DateTime
    Public Property t_prin As String = ""
    Public Property t_prid As DateTime
    Public Property t_rean As String = ""
    Public Property t_lbrn As String = ""
    Public Property t_cren As String = ""
    Public Property t_aore As String = ""
    Public Property t_pren As String = ""
    Public Property t_vprn As String = ""
    Public Property t_expc As String = ""
    Public Property t_expp As String = ""
    Public Property t_shbn As String = ""
    Public Property t_shbd As DateTime
    Public Property t_port As String = ""
    Public Property t_tivf As Decimal = 0.00
    Public Property t_fcur As String = ""
    Public Property t_ccty As String = ""
    Public Property t_Refcntd As Integer = 0
    Public Property t_Refcntu As Integer = 0
    Public Property t_line As Integer = 0
    Public Property t_invs As Integer = 0
    Public Property t_rers As Integer = 0
    Public Property t_rpst As Integer = 0
    Public Property t_flag As Integer = 0
    Public Property t_crdt As String = ""
    Public ReadOnly Property IndxKey As String
      Get
        Return t_comp & "_" & t_tran & "_" & t_docn
      End Get
    End Property
    Public Shared Function GetPending(comp As String, Testing As Boolean, InvoiceNo As String) As List(Of SIS.CIISG.ciisg800)
      Dim Results As New List(Of SIS.CIISG.ciisg800)
      Dim Sql As String = ""
      Sql &= " SELECT *  FROM [tciisg800" & comp & "] "
      Sql &= " WHERE "
      If Testing Then
        Sql &= " t_ninv='" & InvoiceNo & "' "
      Else
        Sql &= " "
        Sql &= " ( "
        Sql &= " (t_rpst IN (" & enumProcessStatus.Free & "," & enumProcessStatus.Retry & ")) "
        Sql &= " OR "
        Sql &= " (t_rpst = " & enumProcessStatus.Success & " AND t_invs = " & enumProcessFor.CancelInvoice & ") "
        Sql &= " OR "
        Sql &= " (t_rpst = " & enumProcessStatus.Success & " AND t_rers = " & enumFetchInvoice.YES & ") "
        Sql &= " ) "
      End If
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While Reader.Read()
            Results.Add(New SIS.CIISG.ciisg800(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Sub UpdateInvoice(ci800 As SIS.CIISG.ciisg800, cmp As ConfigFile.Company, sts As enumProcessStatus, Optional data As SIS.CIISG.miSubmitResponce = Nothing)
      'Below line will be commented latter
      ci800.t_crdt = IIf(ci800.t_crdt = "", "0340", ci800.t_crdt)
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()
        Select Case sts
          Case enumProcessStatus.Failed
            'Error In User Data 'To Reprocess ERP has to set status to Free  After Data Correction, 
            'Or Not To Be Processed can be set from ERP
            Dim IResponseFound As Boolean = True
            Dim IResponse As SIS.CIISG.ciisg801 = SIS.CIISG.ciisg801.GetCiIsg801(ci800.t_comp, ci800.t_tran, ci800.t_docn, cmp.ERPCompany)
            If IResponse Is Nothing Then
              IResponseFound = False
              IResponse = New SIS.CIISG.ciisg801
            End If
            With IResponse
              .t_ackd = Now
              .t_ackn = ""
              .t_comp = ci800.t_comp
              .t_docn = ci800.t_docn
              .t_dtyp = ci800.t_dtyp
              .t_eflg = 1 'ERP Enum YES=1, NO=2
              .t_errc = data.results.code
              .t_errm = data.results.errorMessage.Replace("'", "`")
              .t_irnn = "" 'Govt IRN
              .t_irns = IIf(data.results.status = "Success", 1, 2)
              .t_ninv = ci800.t_ninv
              .t_odat = ci800.t_odat
              .t_ogst = ci800.t_ogst
              .t_qrst = "0" 'Govt Signed QR Code
              .t_Refcntd = 0
              .t_Refcntu = 0
              .t_rqid = data.results.requestId
              .t_sinv = "0" 'Govt Signed Invoice
              .t_stat = IIf(data.results.status = "Success", 1, 2)
              .t_tran = ci800.t_tran
              .t_ispi = ""
              .t_isgi = ""
              .t_isqr = ""
              .t_qrcd = ""
            End With
            If IResponseFound Then
              SIS.CIISG.ciisg801.UpdateCiIsg801(IResponse, cmp.ERPCompany)
            Else
              SIS.CIISG.ciisg801.InsertCiIsg801(IResponse, cmp.ERPCompany)
            End If
            Dim Sql As String = ""
            Using Cmd As SqlCommand = Con.CreateCommand()
              Cmd.CommandType = CommandType.Text
              Cmd.CommandText = "UPDATE tciisg800" & cmp.ERPCompany & " SET t_rpst=" & sts & ", t_flag = 1 WHERE t_comp ='" & ci800.t_comp & "' AND t_tran ='" & ci800.t_tran & "' AND t_docn ='" & ci800.t_docn & "' "
              Cmd.ExecuteNonQuery()
            End Using
          Case enumProcessStatus.Success
            Dim IResponseFound As Boolean = True
            Dim IResponse As SIS.CIISG.ciisg801 = SIS.CIISG.ciisg801.GetCiIsg801(ci800.t_comp, ci800.t_tran, ci800.t_docn, cmp.ERPCompany)
            If IResponse Is Nothing Then
              IResponseFound = False
              IResponse = New SIS.CIISG.ciisg801
            End If
            With IResponse
              .t_ackd = data.results.xMessage.AckDt
              .t_ackn = data.results.xMessage.AckNo
              .t_comp = ci800.t_comp
              .t_docn = ci800.t_docn
              .t_dtyp = ci800.t_dtyp
              .t_eflg = 2 'ERP Enum YES=1, NO=2
              .t_errc = data.results.code
              .t_errm = data.results.errorMessage.Replace("'", "`")
              .t_irnn = data.results.xMessage.Irn
              .t_irns = IIf(data.results.status = "Success", 1, 2)
              .t_ninv = ci800.t_ninv
              .t_odat = ci800.t_odat
              .t_ogst = ci800.t_ogst
              .t_qrst = SIS.CIISG.ciisg801.CreateText("ciisg801" & cmp.ERPCompany & ".qrst", ci800.t_crdt, data.results.xMessage.SignedQRCode, cmp.ERPCompany)
              .t_Refcntd = 0
              .t_Refcntu = 0
              .t_rqid = data.results.RequestID
              .t_sinv = SIS.CIISG.ciisg801.CreateText("ciisg801" & cmp.ERPCompany & ".sinv", ci800.t_crdt, data.results.xMessage.SignedInvoice, cmp.ERPCompany)
              .t_stat = IIf(data.results.status = "Success", 1, 2)
              .t_tran = ci800.t_tran
              .t_ispi = data.results.xMessage.EinvoicePdf
              .t_isgi = ""
              .t_isqr = ""
              .t_qrcd = data.results.xMessage.QRCodeUrl
            End With
            If IResponseFound Then
              SIS.CIISG.ciisg801.UpdateCiIsg801(IResponse, cmp.ERPCompany)
            Else
              SIS.CIISG.ciisg801.InsertCiIsg801(IResponse, cmp.ERPCompany)
            End If
            Dim Sql As String = ""
            Using Cmd As SqlCommand = Con.CreateCommand()
              Cmd.CommandType = CommandType.Text
              Cmd.CommandText = "UPDATE tciisg800" & cmp.ERPCompany & " SET t_rpst=" & sts & ", t_flag = 2 WHERE t_comp ='" & ci800.t_comp & "' AND t_tran ='" & ci800.t_tran & "' AND t_docn ='" & ci800.t_docn & "' "
              Cmd.ExecuteNonQuery()
            End Using
          Case enumProcessStatus.Retry
            Dim Sql As String = ""
            Using Cmd As SqlCommand = Con.CreateCommand()
              Cmd.CommandType = CommandType.Text
              Cmd.CommandText = "UPDATE tciisg800" & cmp.ERPCompany & " SET t_rpst=" & sts & ", t_flag = 2 WHERE t_comp ='" & ci800.t_comp & "' AND t_tran ='" & ci800.t_tran & "' AND t_docn ='" & ci800.t_docn & "' "
              Cmd.ExecuteNonQuery()
            End Using
        End Select

      End Using

    End Sub
    Public Shared Function ConvertToMiSubmitInvoice(Token As SIS.CIISG.miTokenResponce, ci800 As SIS.CIISG.ciisg800, comp As String) As SIS.CIISG.miSubmitInvoice
      Dim tmp As New miSubmitInvoice
      With tmp
        .access_token = Token.access_token
        .user_gstin = ci800.t_bfgn ' "09AAAPG7885R002" 'ci800.t_ogst
        .data_source = "ERP"
        With .transaction_details
          .supply_type = ci800.t_catg
          .charge_type = "N"  ' ci800.t_regr
          .ecommerce_gstin = IIf(ci800.t_etrn = "2", "", "09AAAPG7885R002")
          .igst_on_intra = "N"
        End With
        With .document_details
          .document_number = ci800.t_ninv
          .document_date = ci800.t_odat.AddMinutes(330)
          .document_type = ci800.t_dtyp
        End With
        With .seller_details
          .gstin = ci800.t_bfgn
          .legal_name = ci800.t_bfnm
          .trade_name = ci800.t_bfnm
          .address1 = ci800.t_bfbn
          .address2 = IIf((ci800.t_bfbm & ci800.t_bffn).Trim.Length < 3, "", ci800.t_bfbm & ci800.t_bffn)
          .location = ci800.t_bflc
          .pincode = ci800.t_bfpn

          'Fix-01
          '.state_code = ci800.t_bfsc 'It was blank in ERP
          Try
            .state_code = ci800.t_bfgn.Substring(0, 2)
          Catch ex As Exception
          End Try

          '.phone_number = ci800.t_bfph
          '.email = ci800.t_bfem
        End With
        With .buyer_details
          .legal_name = ci800.t_btnm
          .trade_name = ci800.t_btnm

          'Fix-02
          Select Case ci800.t_catg
            Case "EXPWP", "EXPWOP"
              .place_of_supply = "96"
              .state_code = "96"
              .gstin = "URP"
            Case Else
              .place_of_supply = ci800.t_btsc
              .state_code = ci800.t_btsc
              .gstin = ci800.t_btgn
          End Select

          .address1 = ci800.t_btbn
          .address2 = IIf((ci800.t_btbm & ci800.t_btfn).Trim.Length < 3, "", ci800.t_btbm & ci800.t_btfn)
          .location = ci800.t_btlc
          .pincode = ci800.t_btpn
          '.phone_number = ci800.t_btph
          '.email = ci800.t_btem
        End With

        If ci800.t_bfgn <> ci800.t_sfgn Then
          Dim odispatch_details As New miSubmitInvoice.cdispatch_details
          With odispatch_details
            .company_name = ci800.t_sfnm
            .address1 = ci800.t_sfbn
            .address2 = IIf((ci800.t_sfbm & ci800.t_sffn).Trim.Length < 3, "", ci800.t_sfbm & ci800.t_sffn)
            .location = ci800.t_sflc
            .pincode = ci800.t_sfpn
            'Fix-
            '.state_code = ci800.t_sfsc 'Blank in ERP
            Try
              .state_code = ci800.t_sfgn.Substring(0, 2)
            Catch ex As Exception
            End Try
          End With
          .dispatch_details = odispatch_details
        End If

        'Fix-
        If ci800.t_btgn <> ci800.t_stgn Then
          Dim oship_details As New miSubmitInvoice.cship_details
          With oship_details
            .gstin = ci800.t_stgn
            .legal_name = ci800.t_stnm
            .trade_name = ci800.t_stnm
            .address1 = ci800.t_stbn
            .address2 = IIf((ci800.t_stbm & ci800.t_stfn).Trim.Length < 3, "", ci800.t_stbm & ci800.t_stfn)
            .location = ci800.t_stlc
            .pincode = ci800.t_stpn
            '.state_code = ci800.t_stsc
            Try
              .state_code = ci800.t_stgn.Substring(0, 2)
            Catch ex As Exception
            End Try
          End With
          .ship_details = oship_details
        End If

        Dim oexport_details As New miSubmitInvoice.cexport_details
        With oexport_details
          .ship_bill_number = ci800.t_shbn
          .ship_bill_date = ci800.t_shbd.AddMinutes(330)
          .port_code = ci800.t_port
          .foreign_currency = ci800.t_fcur
          .country_code = ci800.t_ccty
          .refund_claim = "N"
          .export_duty = "0.00"
        End With
        .export_details = oexport_details

        'NOT to be transfer
        'Dim opayment_details As New miSubmitInvoice.cpayment_details
        'With opayment_details
        '  .bank_account_number = ci800.t_accd
        '  .paid_balance_amount = ci800.t_blmt
        '  .credit_days = ci800.t_cred
        '  .credit_transfer = ci800.t_cret
        '  .direct_debit = ci800.t_dird
        '  .branch_or_ifsc = ci800.t_ifsc
        '  .payment_mode = ci800.t_modp
        '  .payee_name = ci800.t_payn
        '  .outstanding_amount = "0.00"
        '  .payment_instruction = ci800.t_payi
        '  .payment_term = ci800.t_tpay
        'End With
        '.payment_details = opayment_details

        With .reference_details
          .invoice_remarks = ci800.t_invr
          Select Case ci800.t_dtyp
            Case "INV"
            Case Else
              .preceding_document_details = New List(Of miSubmitInvoice.cpreceding_document_details)
              Dim o_preceding_document_details As New SIS.CIISG.miSubmitInvoice.cpreceding_document_details
              With o_preceding_document_details
                .reference_of_original_invoice = ci800.t_prin
                .preceding_invoice_date = IIf(ci800.t_prid = "01/01/1970", "", ci800.t_prid.AddMinutes(330))
                .other_reference = ci800.t_aore
              End With
              .preceding_document_details.Add(o_preceding_document_details)
          End Select
          'NOT to be transfer
          'Dim o_contract_details As New SIS.CIISG.miSubmitInvoice.ccontract_details
          'With o_contract_details
          '  .receipt_advice_number = ci800.t_rean
          '  .receipt_advice_date = ""    'Not available In Mapping
          '  .batch_reference_number = ci800.t_lbrn
          '  .contract_reference_number = ci800.t_cren
          '  .other_reference = ci800.t_aore
          '  .project_reference_number = ci800.t_pren
          '  .project_reference_number = ci800.t_pren
          '  Try
          '    .vendor_po_reference_number = ci800.t_vprn.Substring(0, 16)
          '  Catch ex As Exception
          '  End Try
          '  .vendor_po_reference_date = ""   'Not available in Mapping
          'End With
          '.contract_details.Add(o_contract_details)
        End With
        'NOT to be transfer
        'Additional Document Details : Not available in Mapping
        With .value_details
          .total_assessable_value = ci800.t_tval
          .total_cgst_value = ci800.t_tcgs
          .total_sgst_value = ci800.t_tsgs
          .total_igst_value = ci800.t_tigs
          .total_cess_value = ci800.t_tces
          .total_cess_value_of_state = "0"
          .total_discount = "0.00"
          .total_other_charges = "0.00"
          .round_off_amount = "0.00"  'Not available in Mapping
          .total_invoice_value = ci800.t_fivl
          .total_cess_value_of_state = ci800.t_tscs
          .total_invoice_value_additional_currency = ci800.t_fivl
        End With
        'Item_List
        Dim iList As List(Of SIS.CIISG.ciisg803) = SIS.CIISG.ciisg803.GetItems(ci800.t_comp, ci800.t_tran, ci800.t_docn, comp)
        For Each ci803 As SIS.CIISG.ciisg803 In iList
          .item_list.Add(SIS.CIISG.ciisg803.ConvertToMiItem(ci800, ci803))
        Next
      End With
      Return tmp
    End Function
    Public Shared Function ConvertToMiCancelInvoice(Token As SIS.CIISG.miTokenResponce, ci800 As SIS.CIISG.ciisg800, comp As String) As SIS.CIISG.miCancelInvoice
      Dim tmp As New miCancelInvoice
      Dim ci801 As SIS.CIISG.ciisg801 = SIS.CIISG.ciisg801.GetCiIsg801(ci800.t_comp, ci800.t_tran, ci800.t_docn, comp)
      With tmp
        .access_token = Token.access_token
        .user_gstin = ci800.t_bfgn
        .irn = ci801.t_irnn
        .cancel_reason = "1"
        .cancel_remarks = "Wrong Entry"
      End With
      Return tmp
    End Function
    Public Shared Sub UpdateCancelInvoice(ci800 As SIS.CIISG.ciisg800, cmp As ConfigFile.Company, sts As enumProcessStatus, Optional data As SIS.CIISG.miCancelResponce = Nothing)
      ci800.t_crdt = IIf(ci800.t_crdt = "", "0340", ci800.t_crdt)
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()
        Select Case sts
          Case enumProcessStatus.Failed
            Dim IResponse As SIS.CIISG.ciisg801 = SIS.CIISG.ciisg801.GetCiIsg801(ci800.t_comp, ci800.t_tran, ci800.t_docn, cmp.ERPCompany)
            With IResponse
              .t_eflg = 1 'ERP Enum YES=1, NO=2
              .t_errc = data.results.code
              .t_errm = data.results.errorMessage.Replace("'", "`")
              .t_irns = IIf(data.results.status = "Success", 1, 2)
              .t_stat = IIf(data.results.status = "Success", 1, 2)
              If data.results.message.CancelDate <> "" Then
                .t_cdte = data.results.message.CancelDate
              End If
            End With
            SIS.CIISG.ciisg801.UpdateCiIsg801(IResponse, cmp.ERPCompany)
            Dim Sql As String = ""
            Using Cmd As SqlCommand = Con.CreateCommand()
              Cmd.CommandType = CommandType.Text
              Cmd.CommandText = "UPDATE tciisg800" & cmp.ERPCompany & " SET t_rpst=" & sts & ", t_flag = 1 WHERE t_comp ='" & ci800.t_comp & "' AND t_tran ='" & ci800.t_tran & "' AND t_docn ='" & ci800.t_docn & "' "
              Cmd.ExecuteNonQuery()
            End Using
          Case enumProcessStatus.Cancelled
            Dim IResponse As SIS.CIISG.ciisg801 = SIS.CIISG.ciisg801.GetCiIsg801(ci800.t_comp, ci800.t_tran, ci800.t_docn, cmp.ERPCompany)
            With IResponse
              .t_eflg = 2 'ERP Enum YES=1, NO=2
              .t_errc = data.results.code
              .t_errm = data.results.errorMessage
              .t_irns = IIf(data.results.status = "Success", 1, 2)
              .t_stat = IIf(data.results.status = "Success", 1, 2)
              If data.results.message.CancelDate <> "" Then
                .t_cdte = data.results.message.CancelDate
              End If
            End With
            SIS.CIISG.ciisg801.UpdateCiIsg801(IResponse, cmp.ERPCompany)
            Dim Sql As String = ""
            Using Cmd As SqlCommand = Con.CreateCommand()
              Cmd.CommandType = CommandType.Text
              Cmd.CommandText = "UPDATE tciisg800" & cmp.ERPCompany & " SET t_rpst=" & sts & ", t_flag = 2 WHERE t_comp ='" & ci800.t_comp & "' AND t_tran ='" & ci800.t_tran & "' AND t_docn ='" & ci800.t_docn & "' "
              Cmd.ExecuteNonQuery()
            End Using
          Case enumProcessStatus.Retry
            Dim Sql As String = ""
            Using Cmd As SqlCommand = Con.CreateCommand()
              Cmd.CommandType = CommandType.Text
              Cmd.CommandText = "UPDATE tciisg800" & cmp.ERPCompany & " SET t_rpst=" & sts & ", t_flag = 2 WHERE t_comp ='" & ci800.t_comp & "' AND t_tran ='" & ci800.t_tran & "' AND t_docn ='" & ci800.t_docn & "' "
              Cmd.ExecuteNonQuery()
            End Using
        End Select

      End Using

    End Sub
    Public Sub New(ByVal Reader As SqlDataReader)
      SIS.SYS.SQLDatabase.DBCommon.NewObj(Me, Reader)
    End Sub
    Public Sub New()
    End Sub

  End Class
End Namespace
