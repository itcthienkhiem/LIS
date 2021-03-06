USE MM
GO
ALTER TABLE Receipt
ADD [TrongGoiKham] [bit] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ReceiptView]
AS
SELECT     dbo.PatientView.FullName, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.Receipt.ReceiptGUID, dbo.Receipt.PatientGUID, dbo.Receipt.ReceiptDate, 
                      dbo.Receipt.CreatedDate, dbo.Receipt.CreatedBy, dbo.Receipt.UpdatedDate, dbo.Receipt.UpdatedBy, dbo.Receipt.DeletedDate, dbo.Receipt.DeletedBy, 
                      dbo.Receipt.Status, dbo.Receipt.ReceiptCode, dbo.Receipt.IsExportedInVoice, dbo.PatientView.CompanyName, dbo.Receipt.Notes, dbo.Receipt.ChuaThuTien, 
                      ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, dbo.Receipt.LyDoGiam, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' WHEN 2 THEN N'Tiền mặt/Chuyển khoản' WHEN 3 THEN N'Bảo hiểm' WHEN 4 THEN
                       N'Cà thẻ' END AS HinhThucThanhToanStr, dbo.Receipt.HinhThucThanhToan, dbo.Receipt.TrongGoiKham
FROM         dbo.Receipt INNER JOIN
                      dbo.PatientView ON dbo.Receipt.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Receipt.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO







