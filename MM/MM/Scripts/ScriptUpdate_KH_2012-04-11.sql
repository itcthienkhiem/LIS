USE MM
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ReceiptView]
AS
SELECT     dbo.PatientView.FullName, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.Receipt.ReceiptGUID, dbo.Receipt.PatientGUID, 
                      dbo.Receipt.ReceiptDate, dbo.Receipt.CreatedDate, dbo.Receipt.CreatedBy, dbo.Receipt.UpdatedDate, dbo.Receipt.UpdatedBy, 
                      dbo.Receipt.DeletedDate, dbo.Receipt.DeletedBy, dbo.Receipt.Status, dbo.Receipt.ReceiptCode, dbo.Receipt.IsExportedInVoice, 
                      dbo.PatientView.CompanyName, dbo.Receipt.Notes, dbo.Receipt.ChuaThuTien, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao
FROM         dbo.Receipt INNER JOIN
                      dbo.PatientView ON dbo.Receipt.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Receipt.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  View [dbo].[PhieuThuThuocView]    Script Date: 04/11/2012 12:52:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PhieuThuThuocView]
AS
SELECT     dbo.PhieuThuThuoc.PhieuThuThuocGUID, dbo.PhieuThuThuoc.ToaThuocGUID, dbo.PhieuThuThuoc.MaPhieuThuThuoc, dbo.PhieuThuThuoc.NgayThu, 
                      dbo.PhieuThuThuoc.MaBenhNhan, dbo.PhieuThuThuoc.TenBenhNhan, dbo.PhieuThuThuoc.TenCongTy, dbo.PhieuThuThuoc.DiaChi, 
                      dbo.PhieuThuThuoc.CreatedBy, dbo.PhieuThuThuoc.CreatedDate, dbo.PhieuThuThuoc.UpdatedBy, dbo.PhieuThuThuoc.UpdatedDate, 
                      dbo.PhieuThuThuoc.DeletedBy, dbo.PhieuThuThuoc.DeletedDate, dbo.PhieuThuThuoc.IsExported, dbo.PhieuThuThuoc.Status, 
                      dbo.PhieuThuThuoc.Notes, dbo.PhieuThuThuoc.ChuaThuTien, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao
FROM         dbo.PhieuThuThuoc LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuThuoc.CreatedBy = dbo.DocStaffView.DocStaffGUID

GO
/****** Object:  View [dbo].[PhieuThuHopDongView]    Script Date: 04/11/2012 12:52:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PhieuThuHopDongView]
AS
SELECT     dbo.PhieuThuHopDong.PhieuThuHopDongGUID, dbo.PhieuThuHopDong.HopDongGUID, dbo.PhieuThuHopDong.MaPhieuThuHopDong, 
                      dbo.PhieuThuHopDong.TenNguoiNop, dbo.PhieuThuHopDong.TenCongTy, dbo.PhieuThuHopDong.DiaChi, dbo.PhieuThuHopDong.NgayThu, 
                      dbo.PhieuThuHopDong.Notes, dbo.PhieuThuHopDong.IsExported, dbo.PhieuThuHopDong.Status, dbo.PhieuThuHopDong.CreatedDate, 
                      dbo.PhieuThuHopDong.CreatedBy, dbo.PhieuThuHopDong.UpdatedDate, dbo.PhieuThuHopDong.UpdatedBy, dbo.PhieuThuHopDong.DeletedDate, 
                      dbo.PhieuThuHopDong.DeletedBy, dbo.PhieuThuHopDong.ChuaThuTien, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao
FROM         dbo.PhieuThuHopDong LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuHopDong.CreatedBy = dbo.DocStaffView.DocStaffGUID

GO