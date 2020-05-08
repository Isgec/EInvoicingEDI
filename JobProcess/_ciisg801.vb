Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.CIISG
  <DataObject()>
  Public Class ciisg801
    Public Property t_comp As Integer = 0
    Public Property t_tran As String = ""
    Public Property t_docn As Integer = 0
    Public Property t_ogst As String = ""
    Public Property t_irnn As String = ""
    Public Property t_dtyp As String = ""
    Public Property t_ninv As String = ""
    Public Property t_odat As DateTime
    Public Property t_errm As String = ""
    Public Property t_stat As Integer = 0
    Public Property t_errc As Integer = 0
    Public Property t_ackn As String = ""
    Public Property t_ackd As DateTime
    Public Property t_qrst As Integer = 0
    Public Property t_irns As String = ""
    Public Property t_sinv As Integer = 0
    Public Property t_rqid As String = ""
    Public Property t_eflg As Integer = 0
    Public Property t_Refcntd As Integer = 0
    Public Property t_Refcntu As Integer = 0
    Public Property t_qrcd As String = "" 'ISP URL QrCode
    Public Property t_ispi As String = "" 'ISP URL Invoice
    Public Property t_isqr As String = "" 'ISGEC URL QrCode
    Public Property t_isgi As String = "" 'ISGEC URL Invoice

    Public Shared Function GetCiIsg801(t_comp As String, t_tran As String, t_docn As String, comp As String) As SIS.CIISG.ciisg801
      Dim Results As SIS.CIISG.ciisg801 = Nothing
      Dim Sql As String = ""
      Sql &= " SELECT * FROM [tciisg801" & comp & "] "
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
            Results = New SIS.CIISG.ciisg801(Reader)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Sub InsertCiIsg801(ci801 As SIS.CIISG.ciisg801, comp As String)
      Dim Sql As String = ""
      Sql &= " INSERT [tciisg801" & comp & "] "
      Sql &= " ( "
      Sql &= " t_comp, "
      Sql &= " t_tran, "
      Sql &= " t_docn, "
      Sql &= " t_ogst, "
      Sql &= " t_irnn, "
      Sql &= " t_dtyp, "
      Sql &= " t_ninv, "
      Sql &= " t_odat, "
      Sql &= " t_errm, "
      Sql &= " t_stat, "
      Sql &= " t_errc, "
      Sql &= " t_ackn, "
      Sql &= " t_ackd, "
      Sql &= " t_qrst, "
      Sql &= " t_irns, "
      Sql &= " t_sinv, "
      Sql &= " t_rqid, "
      Sql &= " t_eflg, "
      Sql &= " t_Refcntd, "
      Sql &= " t_Refcntu, "
      Sql &= " t_isgi, "
      Sql &= " t_ispi, "
      Sql &= " t_isqr, "
      Sql &= " t_qrcd  "
      Sql &= " ) "
      Sql &= " VALUES  "
      Sql &= " ( "
      Sql &= "  " & ci801.t_comp & ","
      Sql &= " '" & ci801.t_tran & "',"
      Sql &= "  " & ci801.t_docn & ","
      Sql &= " '" & ci801.t_ogst & "',"
      Sql &= " '" & ci801.t_irnn & "',"
      Sql &= " '" & ci801.t_dtyp & "',"
      Sql &= " '" & ci801.t_ninv & "',"
      Sql &= "  convert(datetime,'" & ci801.t_odat.ToString("dd/MM/yyyy") & "',103),"
      Sql &= " '" & ci801.t_errm & "',"
      Sql &= "  " & ci801.t_stat & ","
      Sql &= "  " & ci801.t_errc & ","
      Sql &= " '" & ci801.t_ackn & "',"
      Sql &= "  convert(datetime,'" & ci801.t_ackd.ToString("dd/MM/yyyy") & "',103),"
      Sql &= "  " & ci801.t_qrst & ","
      Sql &= " '" & ci801.t_irns & "',"
      Sql &= "  " & ci801.t_sinv & ","
      Sql &= " '" & ci801.t_rqid & "',"
      Sql &= "  " & ci801.t_eflg & ","
      Sql &= "  " & ci801.t_Refcntd & ","
      Sql &= "  " & ci801.t_Refcntu & ","
      Sql &= " '" & ci801.t_isgi & "',"
      Sql &= " '" & ci801.t_ispi & "',"
      Sql &= " '" & ci801.t_isqr & "',"
      Sql &= " '" & ci801.t_qrcd & "'"
      Sql &= " ) "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Cmd.ExecuteNonQuery()
        End Using
      End Using
    End Sub
    Public Shared Sub UpdateCiIsg801(ci801 As SIS.CIISG.ciisg801, comp As String)
      Dim Sql As String = ""
      Sql &= " UPDATE [tciisg801" & comp & "] "
      Sql &= " SET "
      Sql &= " t_ogst = '" & ci801.t_ogst & "',"
      Sql &= " t_irnn = '" & ci801.t_irnn & "',"
      Sql &= " t_dtyp = '" & ci801.t_dtyp & "',"
      Sql &= " t_ninv = '" & ci801.t_ninv & "',"
      Sql &= " t_odat = convert(datetime,'" & ci801.t_odat.ToString("dd/MM/yyyy") & "',103),"
      Sql &= " t_errm = '" & ci801.t_errm & "',"
      Sql &= " t_stat = " & ci801.t_stat & ","
      Sql &= " t_errc = " & ci801.t_errc & ","
      Sql &= " t_ackn = '" & ci801.t_ackn & "',"
      Sql &= " t_ackd = convert(datetime,'" & ci801.t_ackd.ToString("dd/MM/yyyy") & "',103),"
      Sql &= " t_qrst = " & ci801.t_qrst & ","
      Sql &= " t_irns = '" & ci801.t_irns & "',"
      Sql &= " t_sinv = " & ci801.t_sinv & ","
      Sql &= " t_rqid = '" & ci801.t_rqid & "',"
      Sql &= " t_eflg = " & ci801.t_eflg & ","
      Sql &= " t_Refcntd = " & ci801.t_Refcntd & ","
      Sql &= " t_Refcntu = " & ci801.t_Refcntu & ","
      Sql &= " t_isgi = '" & ci801.t_isgi & "',"
      Sql &= " t_ispi = '" & ci801.t_ispi & "',"
      Sql &= " t_isqr = '" & ci801.t_isqr & "',"
      Sql &= " t_qrcd = '" & ci801.t_qrcd & "'"
      Sql &= " WHERE "
      Sql &= " t_comp = " & ci801.t_comp & " "
      Sql &= " AND t_tran = '" & ci801.t_tran & "' "
      Sql &= " AND t_docn = " & ci801.t_docn & " "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Cmd.ExecuteNonQuery()
        End Using
      End Using
    End Sub

    Public Shared Function CreateText(ForField As String, CreatedBy As String, data As String, comp As String) As Integer
      If data.Length = 0 Then Return 0
      Dim txtNo As Long = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()
        Dim Sql As String = ""
        Sql &= " declare @Return_t_srno Int "
        Sql &= " Select @Return_t_srno=max(t_ctxt)+1 from ttttxt001" & comp
        Sql &= " INSERT [ttttxt001" & comp & "]([t_ctxt], [t_opwd], [t_txtg], [t_desc], [t_Refcntd], [t_Refcntu]) VALUES (@Return_t_srno,'text','text','',0,0 ) "
        Sql &= " Select @Return_t_srno "
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          txtNo = Cmd.ExecuteScalar()
        End Using
        Sql = ""
        Sql &= "INSERT [ttttxt002" & comp & "] ([t_ctxt],[t_clan],[t_kwd1],[t_kwd2],[t_kwd3],[t_kwd4],[t_ludt],[t_user],[t_nlin],[t_Refcntd],[t_Refcntu]) VALUES (" & txtNo & ",2,'" & ForField & "','','','',GetDate(),'" & CreatedBy & "',1,0,0)"
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Cmd.ExecuteNonQuery()
        End Using
        Dim I As Integer = data.Length
        Dim J As Integer = 0
        Dim cutOff As Integer = 240
        Dim SrNo As Integer = 0
        Do While J < I
          cutOff = IIf(J + cutOff > I, I - J, cutOff)
          Dim s As String = data.Substring(J, cutOff)
          SrNo += 1
          Sql = ""
          Sql &= "INSERT [ttttxt010" & comp & "] ([t_ctxt],[t_clan],[t_seqe],[t_text],[t_Refcntd],[t_Refcntu]) VALUES (" & txtNo & ",2," & SrNo & ",convert(binary,'" & s & "'),0,0)"
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = Sql
            Try
              Cmd.ExecuteNonQuery()
            Catch ex As Exception
              Dim x As String = ex.Message
            End Try
          End Using
          J += cutOff
        Loop
      End Using
      Return txtNo
    End Function

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
