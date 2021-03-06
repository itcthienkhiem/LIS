USE MM
GO
ALTER TABLE Receipt
ADD [LyDoGiam] [nvarchar](max) NULL
GO
ALTER TABLE PhieuThuThuoc
ADD [LyDoGiam] [nvarchar](max) NULL
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
                      ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, dbo.Receipt.LyDoGiam
FROM         dbo.Receipt INNER JOIN
                      dbo.PatientView ON dbo.Receipt.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Receipt.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PhieuThuThuocView]
AS
SELECT     dbo.PhieuThuThuoc.PhieuThuThuocGUID, dbo.PhieuThuThuoc.ToaThuocGUID, dbo.PhieuThuThuoc.MaPhieuThuThuoc, dbo.PhieuThuThuoc.NgayThu, 
                      dbo.PhieuThuThuoc.MaBenhNhan, dbo.PhieuThuThuoc.TenBenhNhan, dbo.PhieuThuThuoc.TenCongTy, dbo.PhieuThuThuoc.DiaChi, dbo.PhieuThuThuoc.CreatedBy, 
                      dbo.PhieuThuThuoc.CreatedDate, dbo.PhieuThuThuoc.UpdatedBy, dbo.PhieuThuThuoc.UpdatedDate, dbo.PhieuThuThuoc.DeletedBy, dbo.PhieuThuThuoc.DeletedDate, 
                      dbo.PhieuThuThuoc.IsExported, dbo.PhieuThuThuoc.Status, dbo.PhieuThuThuoc.Notes, dbo.PhieuThuThuoc.ChuaThuTien, ISNULL(dbo.DocStaffView.FullName, 
                      'Admin') AS NguoiTao, dbo.PhieuThuThuoc.LyDoGiam
FROM         dbo.PhieuThuThuoc LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuThuoc.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





