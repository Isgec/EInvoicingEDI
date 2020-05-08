Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.CIISG
  <DataObject()>
  Public Class ciisg803
    Public Property t_comp As Integer = 0
    Public Property t_tran As String = ""
    Public Property t_docn As Integer = 0
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
    Public Property t_bfph As Integer = 0
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
    Public Property t_sfph As Integer = 0
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
    Public Property t_stph As Integer = 0
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
    Public Shared Function GetItems(t_comp As String, t_tran As String, t_docn As String, comp As String) As List(Of SIS.CIISG.ciisg803)
      Dim Results As New List(Of SIS.CIISG.ciisg803)
      Dim Sql As String = ""
      Sql &= " SELECT * FROM [tciisg803" & comp & "] "
      Sql &= " WHERE "
      Sql &= " (t_comp ='" & t_comp & "') "
      Sql &= " AND "
      Sql &= " (t_tran ='" & t_tran & "') "
      Sql &= " AND "
      Sql &= " (t_docn ='" & t_docn & "') "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While Reader.Read()
            Results.Add(New SIS.CIISG.ciisg803(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function ConvertToMiItem(ci800 As SIS.CIISG.ciisg800, ci803 As SIS.CIISG.ciisg803) As SIS.CIISG.miSubmitInvoice.citem_list
      Dim tmp As New miSubmitInvoice.citem_list
      With tmp
        .item_serial_number = ci803.t_item
        .product_description = ci803.t_dsca
        .is_service = "N" 'Not available in Mapping
        .hsn_code = ci803.t_hsnc
        .bar_code = ci803.t_barc
        .quantity = ci803.t_qnty
        .free_quantity = ci803.t_fqty
        .unit = ci803.t_cuni
        .unit_price = ci803.t_pric
        .total_amount = ci803.t_amnt
        .discount = ci803.t_disc
        '.pre_tax_value = 0 'Not available in Mapping
        .other_charge = ci803.t_othc
        .assessable_value = ci803.t_assv
        .gst_rate = ci803.t_cgst + ci803.t_sgst + ci803.t_igst
        .igst_amount = ci803.t_assv * ci803.t_igst / 100
        .cgst_amount = ci803.t_assv * ci803.t_cgst / 100
        .sgst_amount = ci803.t_assv * ci803.t_sgst / 100
        .cess_nonadvol_amount = ci803.t_cnaa
        .cess_amount = ci800.t_tces
        .state_cess_amount = ci800.t_tscs
        .state_cess_nonadvol_amount = ci800.t_cnaa
        '.order_line_reference = 'Not available in Mapping
        '.country_origin = 'Not available in Mapping
        '.product_serial_number = 'Not available in Mapping
        '.item_attribute_details = 'Not available in Mapping
        '.item_attribute_value = 'Not available in Mapping
        .total_item_value = ci803.t_valu
        With .batch_details
          .name = ci803.t_batn
          .expiry_date = ci803.t_bate
          .warranty_date = ci803.t_ward
        End With

      End With
      Return tmp
    End Function
    'Public Shared Sub dmisg001DeleteAll(ByVal t_docn As String, ByVal t_revn As String, ByVal comp As String)
    '  Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
    '    Con.Open()
    '    Using Cmd As SqlCommand = Con.CreateCommand()
    '      Cmd.CommandType = CommandType.StoredProcedure
    '      Cmd.CommandText = "spdmisg002" & comp & "DeleteText"
    '      SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_docn", SqlDbType.VarChar, 33, t_docn)
    '      SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_revn", SqlDbType.VarChar, 21, t_revn)
    '      Cmd.ExecuteNonQuery()
    '    End Using
    '    Using Cmd As SqlCommand = Con.CreateCommand()
    '      Cmd.CommandType = CommandType.Text
    '      Cmd.CommandText = "DELETE tdmisg002" & comp & " where t_docn='" & t_docn & "' and t_revn='" & t_revn & "'"
    '      Cmd.ExecuteNonQuery()
    '    End Using
    '    'Type R-Item
    '    Using Cmd As SqlCommand = Con.CreateCommand()
    '      Cmd.CommandType = CommandType.StoredProcedure
    '      Cmd.CommandText = "spdmisg021" & comp & "DeleteText"
    '      SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_docn", SqlDbType.VarChar, 33, t_docn)
    '      SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_revn", SqlDbType.VarChar, 21, t_revn)
    '      Cmd.ExecuteNonQuery()
    '    End Using
    '  End Using
    'End Sub
    '05. Used

    Public Sub New(ByVal Reader As SqlDataReader)
      Try
        For Each pi As System.Reflection.PropertyInfo In Me.GetType.GetProperties
          If pi.MemberType = Reflection.MemberTypes.Property Then
            Try
              Dim Found As Boolean = False
              For I As Integer = 0 To Reader.FieldCount - 1
                If Reader.GetName(I).ToLower = pi.Name.ToLower Then
                  Found = True
                  Exit For
                End If
              Next
              If Found Then
                If Convert.IsDBNull(Reader(pi.Name)) Then
                  Select Case Reader.GetDataTypeName(Reader.GetOrdinal(pi.Name))
                    Case "decimal"
                      CallByName(Me, pi.Name, CallType.Let, "0.00")
                    Case "bit"
                      CallByName(Me, pi.Name, CallType.Let, Boolean.FalseString)
                    Case Else
                      CallByName(Me, pi.Name, CallType.Let, String.Empty)
                  End Select
                Else
                  CallByName(Me, pi.Name, CallType.Let, Reader(pi.Name))
                End If
              End If
            Catch ex As Exception
            End Try
          End If
        Next
      Catch ex As Exception
      End Try
    End Sub
    Public Sub New()
    End Sub

  End Class
End Namespace
