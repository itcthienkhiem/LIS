USE MM
GO
ALTER TABLE ReceiptDetail
ADD [SoLuong] [int] NOT NULL DEFAULT ((1))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ReceiptDetailView]
AS
SELECT     dbo.Services.Status AS ServiceStatus, dbo.ReceiptDetail.ReceiptDetailGUID, dbo.ReceiptDetail.ReceiptGUID, dbo.ReceiptDetail.ServiceHistoryGUID, 
                      dbo.ReceiptDetail.CreatedDate, dbo.ReceiptDetail.CreatedBy, dbo.ReceiptDetail.UpdatedDate, dbo.ReceiptDetail.UpdatedBy, dbo.ReceiptDetail.DeletedDate, 
                      dbo.ReceiptDetail.DeletedBy, dbo.ServiceHistory.Price, dbo.ServiceHistory.Discount, dbo.ServiceHistory.Note, dbo.Services.ServiceGUID, dbo.Services.Code, 
                      dbo.Services.Name, dbo.ReceiptDetail.Status AS ReceiptDetailStatus, dbo.ServiceHistory.Status AS ServiceHistoryStatus, dbo.ServiceHistory.GiaVon, 
                      dbo.ReceiptDetail.SoLuong
FROM         dbo.ReceiptDetail INNER JOIN
                      dbo.ServiceHistory ON dbo.ReceiptDetail.ServiceHistoryGUID = dbo.ServiceHistory.ServiceHistoryGUID INNER JOIN
                      dbo.Services ON dbo.ServiceHistory.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO




