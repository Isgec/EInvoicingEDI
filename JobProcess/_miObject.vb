Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.CIISG
  Public Class miSubmitInvoice
    Public Property access_token As String = ""
    Public Property user_gstin As String = ""
    Public Property data_source As String = ""
    Public Property transaction_details As New ctransaction_details
    Public Property document_details As New cdocument_details
    Public Property seller_details As New cseller_details
    Public Property buyer_details As New cbuyer_details
    Public Property dispatch_details As New cdispatch_details
    Public Property ship_details As New cship_details
    Public Property export_details As New cexport_details
    Public Property payment_details As New cpayment_details
    Public Property reference_details As New creference_details
    Public Property additional_document_details As New List(Of cadditional_document_details)
    Public Property value_details As New cvalue_details
    Public Property ewaybill_details As New cewaybill_details
    Public Property item_list As New List(Of citem_list)
    Public Class ctransaction_details
      Public Property supply_type As String = ""
      Public Property charge_type As String = ""
      Public Property ecommerce_gstin As String = ""
    End Class
    Public Class cdocument_details
      Public Property document_type As String = ""
      Public Property document_number As String = ""
      Public Property document_date As String = ""
    End Class
    Public Class cseller_details
      Public Property gstin As String = ""
      Public Property legal_name As String = ""
      Public Property trade_name As String = ""
      Public Property address1 As String = ""
      Public Property address2 As String = ""
      Public Property location As String = ""
      Public Property pincode As String = ""
      Public Property state_code As String = ""
      Public Property phone_number As String = ""
      Public Property email As String = ""
    End Class
    Public Class cbuyer_details
      Public Property gstin As String = ""
      Public Property legal_name As String = ""
      Public Property trade_name As String = ""
      Public Property address1 As String = ""
      Public Property address2 As String = ""
      Public Property location As String = ""
      Public Property pincode As String = ""
      Public Property place_of_supply As String = ""
      Public Property state_code As String = ""
      Public Property phone_number As String = ""
      Public Property email As String = ""
    End Class
    Public Class cdispatch_details
      Public Property company_name As String = ""
      Public Property address1 As String = ""
      Public Property address2 As String = ""
      Public Property location As String = ""
      Public Property pincode As String = ""
      Public Property state_code As String = ""
    End Class
    Public Class cship_details
      Public Property gstin As String = ""
      Public Property legal_name As String = ""
      Public Property trade_name As String = ""
      Public Property address1 As String = ""
      Public Property address2 As String = ""
      Public Property location As String = ""
      Public Property pincode As String = ""
      Public Property state_code As String = ""
    End Class
    Public Class cexport_details
      Public Property ship_bill_number As String = ""
      Public Property ship_bill_date As String = ""
      Public Property country_code As String = ""
      Public Property foreign_currency As String = ""
      Public Property refund_claim As String = ""
      Public Property port_code As String = ""
    End Class
    Public Class cpayment_details
      Public Property bank_account_number As String = ""
      Public Property paid_balance_amount As String = ""
      Public Property credit_days As String = ""
      Public Property credit_transfer As String = ""
      Public Property direct_debit As String = ""
      Public Property branch_or_ifsc As String = ""
      Public Property payment_mode As String = ""
      Public Property payee_name As String = ""
      Public Property payment_due_date As String = ""
      Public Property payment_instruction As String = ""
      Public Property payment_term As String = ""
    End Class
    Public Class creference_details
      Public Property invoice_remarks As String = ""
      Public Property invoice_period_start_date As String = ""
      Public Property invoice_period_end_date As String = ""
      Public Property preceding_document_details As New List(Of cpreceding_document_details)
      Public Property contract_details As New List(Of ccontract_details)
    End Class
    Public Class cpreceding_document_details
      Public Property reference_of_original_invoice As String = ""
      Public Property preceding_invoice_date As String = ""
      Public Property other_reference As String = ""
    End Class
    Public Class ccontract_details
      Public Property receipt_advice_number As String = ""
      Public Property receipt_advice_date As String = ""
      Public Property batch_reference_number As String = ""
      Public Property contract_reference_number As String = ""
      Public Property other_reference As String = ""
      Public Property project_reference_number As String = ""
      Public Property vendor_po_reference_number As String = ""
      Public Property vendor_po_reference_date As String = ""
    End Class
    Public Class cadditional_document_details
      Public Property supporting_document_url As String = ""
      Public Property supporting_document As String = ""
      Public Property additional_information As String = ""
    End Class
    Public Class cvalue_details
      Public Property total_assessable_value As String = ""
      Public Property total_cgst_value As String = ""
      Public Property total_sgst_value As String = ""
      Public Property total_igst_value As String = ""
      Public Property total_cess_value As String = ""
      Public Property total_cess_nonadvol_value As String = ""
      Public Property total_invoice_value As String = ""
      Public Property total_cess_value_of_state As String = ""
      Public Property round_off_amount As String = ""
      Public Property total_invoice_value_additional_currency As String = ""
    End Class
    Public Class cewaybill_details
      Public Property transporter_id As String = ""
      Public Property transporter_name As String = ""
      Public Property transportation_mode As String = ""
      Public Property transportation_distance As String = ""
      Public Property transporter_document_number As String = ""
      Public Property transporter_document_date As String = ""
      Public Property vehicle_number As String = ""
      Public Property vehicle_type As String = ""
    End Class
    Public Class citem_list
      Public Property item_serial_number As String = ""
      Public Property product_description As String = ""
      Public Property is_service As String = ""
      Public Property hsn_code As String = ""
      Public Property bar_code As String = ""
      Public Property quantity As String = ""
      Public Property free_quantity As String = ""
      Public Property unit As String = ""
      Public Property unit_price As String = ""
      Public Property total_amount As String = ""
      Public Property pre_tax_value As String = ""
      Public Property discount As String = ""
      Public Property other_charge As String = ""
      Public Property assessable_value As String = ""
      Public Property gst_rate As String = ""
      Public Property igst_amount As String = ""
      Public Property cgst_amount As String = ""
      Public Property sgst_amount As String = ""
      Public Property cess_rate As String = ""
      Public Property cess_amount As String = ""
      Public Property cess_nonadvol_amount As String = ""
      Public Property state_cess_rate As String = ""
      Public Property state_cess_amount As String = ""
      Public Property state_cess_nonadvol_amount As String = ""
      Public Property total_item_value As String = ""
      Public Property country_origin As String = ""
      Public Property order_line_reference As String = ""
      Public Property product_serial_number As String = ""
      Public Property batch_details As New cbatch_details
      Public Property attribute_details As New List(Of cattribute_details)
    End Class
    Public Class cbatch_details
      Public Property name As String = ""
      Public Property expiry_date As String = ""
      Public Property warranty_date As String = ""
    End Class
    Public Class cattribute_details
      Public Property item_attribute_details As String = ""
      Public Property item_attribute_value As String = ""
    End Class
  End Class

  Public Class miTestSubmitInvoice
    Public Property access_token As String = "a78e74508f285f5cd120716b81d8e91f2af96326"
    Public Property user_gstin As String = "09AAAPG7885R002"
    Public Property data_source As String = "erp"
    Public Property transaction_details As New ctransaction_details
    Public Property document_details As New cdocument_details
    Public Property seller_details As New cseller_details
    Public Property buyer_details As New cbuyer_details
    Public Property dispatch_details As New cdispatch_details
    Public Property ship_details As New cship_details
    Public Property export_details As New cexport_details
    Public Property payment_details As New cpayment_details
    Public Property reference_details As New creference_details
    Public Property additional_document_details As New List(Of cadditional_document_details)
    Public Property value_details As New cvalue_details
    Public Property ewaybill_details As New cewaybill_details
    Public Property item_list As New List(Of citem_list)
    Sub New()
      additional_document_details.Add(New cadditional_document_details)
      item_list.Add(New citem_list)
    End Sub
    Public Class ctransaction_details
      Public Property supply_type As String = "B2B"
      Public Property charge_type As String = "N"
      Public Property ecommerce_gstin As String = ""
    End Class
    Public Class cdocument_details
      Public Property document_type As String = "INV"
      Public Property document_number As String = "ISGEC001"
      Public Property document_date As String = "09/03/2020"
    End Class
    Public Class cseller_details
      Public Property gstin As String = "09AAAPG7885R002"
      Public Property legal_name As String = "MastersIndia UP"
      Public Property trade_name As String = "MastersIndia UP"
      Public Property address1 As String = "Vila"
      Public Property address2 As String = "Vila"
      Public Property location As String = "Noida"
      Public Property pincode As String = 201301
      Public Property state_code As String = "UTTAR PRADESH"
      Public Property phone_number As String = 9876543231
      Public Property email As String = ""
    End Class
    Public Class cbuyer_details
      Public Property gstin As String = "05AAAPG7885R002"
      Public Property legal_name As String = "MastersIndia UT"
      Public Property trade_name As String = "MastersIndia UT"
      Public Property address1 As String = "Kila"
      Public Property address2 As String = "Kila"
      Public Property location As String = "Nainital"
      Public Property pincode As String = 110010
      Public Property place_of_supply As String = "9"
      Public Property state_code As String = "UTTARAKHAND"
      Public Property phone_number As String = 9876543231
      Public Property email As String = ""
    End Class
    Public Class cdispatch_details
      Public Property company_name As String = "MastersIndia UP"
      Public Property address1 As String = "Vila"
      Public Property address2 As String = "Vila"
      Public Property location As String = "Noida"
      Public Property pincode As String = 201301
      Public Property state_code As String = "UTTAR PRADESH"
    End Class
    Public Class cship_details
      Public Property gstin As String = "05AAAPG7885R002"
      Public Property legal_name As String = "MastersIndia UT"
      Public Property trade_name As String = "MastersIndia UT"
      Public Property address1 As String = "Kila"
      Public Property address2 As String = "Kila"
      Public Property location As String = "Nainital"
      Public Property pincode As String = 110010
      Public Property state_code As String = "UTTARAKHAND"
    End Class
    Public Class cexport_details
      Public Property ship_bill_number As String = "qwe1233"
      Public Property ship_bill_date As String = "08/02/2020"
      Public Property country_code As String = "IN"
      Public Property foreign_currency As String = "INR"
      Public Property refund_claim As String = "N"
      Public Property port_code As String = "232434"
    End Class
    Public Class cpayment_details
      Public Property bank_account_number As String = "Account Details"
      Public Property paid_balance_amount As String = 100
      Public Property credit_days As String = 2
      Public Property credit_transfer As String = "Credit Transfer"
      Public Property direct_debit As String = "Direct Debit"
      Public Property branch_or_ifsc As String = "KKK000180"
      Public Property payment_mode As String = "CASH"
      Public Property payee_name As String = "Payee Name"
      Public Property payment_due_date As String = "08/02/2020"
      Public Property payment_instruction As String = "Payment Instruction"
      Public Property payment_term As String = "Terms of Payment"
    End Class
    Public Class creference_details
      Public Property invoice_remarks As String = "Invoice Remarks"
      Public Property invoice_period_start_date As String = "2020-01-01"
      Public Property invoice_period_end_date As String = "2020-01-30"
      Public Property preceding_document_details As New List(Of cpreceding_document_details)
      Public Property contract_details As New List(Of ccontract_details)
      Sub New()
        preceding_document_details.Add(New cpreceding_document_details)
        contract_details.Add(New ccontract_details)
      End Sub
    End Class
    Public Class cpreceding_document_details
      Public Property reference_of_original_invoice As String = "CFRT/0006"
      Public Property preceding_invoice_date As String = "08/02/2020"
      Public Property other_reference As String = "2334"
    End Class
    Public Class ccontract_details
      Public Property receipt_advice_number As String = "aaa"
      Public Property receipt_advice_date As String = "10/02/2020"
      Public Property batch_reference_number As String = "2334"
      Public Property contract_reference_number As String = "2334"
      Public Property other_reference As String = "2334"
      Public Property project_reference_number As String = "2334"
      Public Property vendor_po_reference_number As String = "233433454545"
      Public Property vendor_po_reference_date As String = "10/02/2020"
    End Class
    Public Class cadditional_document_details
      Public Property supporting_document_url As String = ""
      Public Property supporting_document As String = "india"
      Public Property additional_information As String = "india"
    End Class
    Public Class cvalue_details
      Public Property total_assessable_value As String = 1
      Public Property total_cgst_value As String = 0
      Public Property total_sgst_value As String = 0
      Public Property total_igst_value As String = 0.01
      Public Property total_cess_value As String = 0
      Public Property total_cess_nonadvol_value As String = 0
      Public Property total_invoice_value As String = 1.01
      Public Property total_cess_value_of_state As String = 0
      Public Property round_off_amount As String = 0
      Public Property total_invoice_value_additional_currency As String = 0
    End Class
    Public Class cewaybill_details
      Public Property transporter_id As String = "05AAABB0639G1Z8"
      Public Property transporter_name As String = "Jay Trans"
      Public Property transportation_mode As String = "1"
      Public Property transportation_distance As String = "120"
      Public Property transporter_document_number As String = "1230"
      Public Property transporter_document_date As String = "08/02/2020"
      Public Property vehicle_number As String = "PQR1234"
      Public Property vehicle_type As String = "R"
    End Class
    Public Class citem_list
      Public Property item_serial_number As String = "8965"
      Public Property product_description As String = "Wheat desc"
      Public Property is_service As String = "N"
      Public Property hsn_code As String = "1001"
      Public Property bar_code As String = "1212"
      Public Property quantity As String = 1
      Public Property free_quantity As String = 0
      Public Property unit As String = "KGS"
      Public Property unit_price As String = 1
      Public Property total_amount As String = 1
      Public Property pre_tax_value As String = 0
      Public Property discount As String = 0
      Public Property other_charge As String = 0
      Public Property assessable_value As String = 1
      Public Property gst_rate As String = 0
      Public Property igst_amount As String = 0
      Public Property cgst_amount As String = 1
      Public Property sgst_amount As String = 0
      Public Property cess_rate As String = 0
      Public Property cess_amount As String = 0
      Public Property cess_nonadvol_amount As String = 0
      Public Property state_cess_rate As String = 0
      Public Property state_cess_amount As String = 0
      Public Property state_cess_nonadvol_amount As String = 0
      Public Property total_item_value As String = 1
      Public Property country_origin As String = "52"
      Public Property order_line_reference As String = "5236"
      Public Property product_serial_number As String = "14785"
      Public Property batch_details As New cbatch_details
      Public Property attribute_details As New List(Of cattribute_details)
      Sub New()
        attribute_details.Add(New cattribute_details)
      End Sub
    End Class
    Public Class cbatch_details
      Public Property name As String = "aaa"
      Public Property expiry_date As String = "10/02/2020"
      Public Property warranty_date As String = "20/02/2020"
    End Class
    Public Class cattribute_details
      Public Property item_attribute_details As String = "aaa"
      Public Property item_attribute_value As String = "147852"
    End Class
  End Class
  Public Class miTokenRequest
    Public Property username As String = "testeway@mastersindia.co"
    Public Property password As String = "Test@1234"
    Public Property client_id As String = "fIXefFyxGNfDWOcCWn"
    Public Property client_secret As String = "QFd6dZvCGqckabKxTapfZgJc"
    Public Property grant_type As String = "password"
  End Class
  Public Class miTokenResponce
    Public Property access_token As String = ""
    Public Property expires_in As Integer = 0
    Public Property token_type As String = ""
    Public Property [error] As String = ""
    Public Property error_description As String = ""
  End Class
  Public Class miSubmitResponce
    Public Property AckNo As String = ""
    Public Property AckDt As String = ""
    Public Property Irn As String = ""
    Public Property SignedInvoice As String = ""
    Public Property SignedQRCode As String = ""
    Public Property Status As String = ""
    Public Property [error] As Boolean = False
    Public Property message As String = ""
    Public Property errorMessage As String = ""
    Public Property code As String = ""
    Public Property EwbNo As String = ""
    Public Property EwbDt As String = ""
    Public Property EwbValidTill As String = ""
    Public Property QRCodeUrl As String = ""
    Public Property EinvoicePdf As String = ""
    Public Property alert As String = ""
    Public Property RequestID As String = ""
    Public Property results As New miError
    Public ReadOnly Property isError As Boolean
      Get
        Return IIf(SignedInvoice = "", True, False)
      End Get
    End Property
  End Class
  Public Class miCancelInvoice
    Public Property access_token As String = ""
    Public Property user_gstin As String = ""
    Public Property irn As String = ""
    Public Property cancel_reason As String = ""
    Public Property cancel_remarks As String = ""
  End Class
  Public Class miCancelResponce
    Public Property Irn As String = ""
    Public Property CancelDate As String = ""
    Public Property Status As String = ""
    Public Property errorMessage As String = ""
    Public Property code As String = ""
  End Class
  Public Class miError
    Public Property message As String = ""
    Public Property Status As String = ""
    Public Property errorMessage As String = ""
    Public Property code As String = ""
    Public Property requestId As String = ""
  End Class
  '======================================
  'Get Invoice GET QueryString Parameters
  'access_token, 
  'gstin, 
  'irn
  '======================================
  Public Class FetchedInvoice
    Inherits miSubmitInvoice
    'Not Clear check the values returned Online
  End Class
End Namespace
