Imports Microsoft.VisualBasic
Public Enum enumProcessStatus
  Free = 1
  Success = 2
  Retry = 3
  Failed = 4
  NotToBeProcessed = 5
  Cancelled = 6
End Enum
Public Enum enumProcessFor
  GenerateInvoice = 1
  CancelInvoice = 2
End Enum
Public Enum enumFetchInvoice
  YES = 1
  NO = 2
End Enum