USE MM
GO
ALTER TABLE Invoice
ADD [HinhThucNhanHoaDon] [nvarchar](250) NULL
GO
ALTER TABLE HoaDonHopDong
ADD [HinhThucNhanHoaDon] [nvarchar](250) NULL
GO
ALTER TABLE HoaDonThuoc
ADD [HinhThucNhanHoaDon] [nvarchar](250) NULL
GO
ALTER TABLE HoaDonXuatTruoc
ADD [HinhThucNhanHoaDon] [nvarchar](250) NULL
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[InvoiceView]
AS
SELECT     dbo.Invoice.InvoiceGUID, dbo.Invoice.InvoiceCode, dbo.Invoice.InvoiceDate, dbo.Invoice.TenDonVi, dbo.Invoice.SoTaiKhoan, dbo.Invoice.HinhThucThanhToan, 
                      dbo.Invoice.CreatedDate, dbo.Invoice.CreatedBy, dbo.Invoice.UpdatedDate, dbo.Invoice.UpdatedBy, dbo.Invoice.DeletedDate, dbo.Invoice.DeletedBy, 
                      dbo.Invoice.Status, dbo.Invoice.VAT, CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, 
                      dbo.Invoice.MaSoThue, dbo.Invoice.ReceiptGUIDList, dbo.Invoice.TenNguoiMuaHang, dbo.Invoice.DiaChi, dbo.Invoice.Notes, dbo.Invoice.ChuaThuTien, 
                      dbo.Invoice.MauSo, dbo.Invoice.KiHieu, dbo.Invoice.HinhThucNhanHoaDon, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao
FROM         dbo.Invoice LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Invoice.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[HoaDonHopDongView]
AS
SELECT     dbo.HoaDonHopDong.HoaDonHopDongGUID, dbo.HoaDonHopDong.PhieuThuHopDongGUIDList, dbo.HoaDonHopDong.SoHoaDon, 
                      dbo.HoaDonHopDong.NgayXuatHoaDon, dbo.HoaDonHopDong.TenNguoiMuaHang, dbo.HoaDonHopDong.DiaChi, dbo.HoaDonHopDong.TenDonVi, 
                      dbo.HoaDonHopDong.MaSoThue, dbo.HoaDonHopDong.SoTaiKhoan, dbo.HoaDonHopDong.HinhThucThanhToan, dbo.HoaDonHopDong.VAT, 
                      dbo.HoaDonHopDong.Notes, dbo.HoaDonHopDong.Status, dbo.HoaDonHopDong.CreatedDate, dbo.HoaDonHopDong.CreatedBy, dbo.HoaDonHopDong.UpdatedDate, 
                      dbo.HoaDonHopDong.UpdatedBy, dbo.HoaDonHopDong.DeletedDate, dbo.HoaDonHopDong.DeletedBy, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, dbo.HoaDonHopDong.ChuaThuTien, 
                      dbo.HoaDonHopDong.MauSo, dbo.HoaDonHopDong.KiHieu, dbo.HoaDonHopDong.HinhThucNhanHoaDon, ISNULL(dbo.DocStaffView.FullName, 'Admin') 
                      AS NguoiTao
FROM         dbo.HoaDonHopDong LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.HoaDonHopDong.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[HoaDonThuocView]
AS
SELECT     dbo.HoaDonThuoc.HoaDonThuocGUID, dbo.HoaDonThuoc.SoHoaDon, dbo.HoaDonThuoc.NgayXuatHoaDon, dbo.HoaDonThuoc.TenNguoiMuaHang, 
                      dbo.HoaDonThuoc.DiaChi, dbo.HoaDonThuoc.TenDonVi, dbo.HoaDonThuoc.MaSoThue, dbo.HoaDonThuoc.SoTaiKhoan, dbo.HoaDonThuoc.HinhThucThanhToan, 
                      dbo.HoaDonThuoc.VAT, dbo.HoaDonThuoc.CreatedDate, dbo.HoaDonThuoc.CreatedBy, dbo.HoaDonThuoc.UpdatedDate, dbo.HoaDonThuoc.UpdatedBy, 
                      dbo.HoaDonThuoc.DeletedDate, dbo.HoaDonThuoc.DeletedBy, dbo.HoaDonThuoc.Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, 
                      dbo.HoaDonThuoc.PhieuThuThuocGUIDList, dbo.HoaDonThuoc.Notes, dbo.HoaDonThuoc.ChuaThuTien, dbo.HoaDonThuoc.MauSo, dbo.HoaDonThuoc.KiHieu, 
                      dbo.HoaDonThuoc.HinhThucNhanHoaDon, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao
FROM         dbo.HoaDonThuoc LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.HoaDonThuoc.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[HoaDonXuatTruocView]
AS
SELECT     dbo.HoaDonXuatTruoc.SoHoaDon, dbo.HoaDonXuatTruoc.NgayXuatHoaDon, dbo.HoaDonXuatTruoc.TenNguoiMuaHang, dbo.HoaDonXuatTruoc.DiaChi, 
                      dbo.HoaDonXuatTruoc.TenDonVi, dbo.HoaDonXuatTruoc.MaSoThue, dbo.HoaDonXuatTruoc.SoTaiKhoan, dbo.HoaDonXuatTruoc.HinhThucThanhToan, 
                      dbo.HoaDonXuatTruoc.VAT, dbo.HoaDonXuatTruoc.CreatedDate, dbo.HoaDonXuatTruoc.CreatedBy, dbo.HoaDonXuatTruoc.UpdatedDate, 
                      dbo.HoaDonXuatTruoc.UpdatedBy, dbo.HoaDonXuatTruoc.DeletedDate, dbo.HoaDonXuatTruoc.DeletedBy, dbo.HoaDonXuatTruoc.Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, 
                      dbo.HoaDonXuatTruoc.HoaDonXuatTruocGUID, dbo.HoaDonXuatTruoc.Notes, dbo.HoaDonXuatTruoc.ChuaThuTien, dbo.HoaDonXuatTruoc.MauSo, 
                      dbo.HoaDonXuatTruoc.KiHieu, dbo.HoaDonXuatTruoc.HinhThucNhanHoaDon, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao
FROM         dbo.HoaDonXuatTruoc LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.HoaDonXuatTruoc.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietChiDinhView]
AS
SELECT     dbo.ChiTietChiDinh.ChiTietChiDinhGUID, dbo.ChiTietChiDinh.ChiDinhGUID, dbo.ChiTietChiDinh.ServiceGUID, dbo.ChiTietChiDinh.CreatedDate, 
                      dbo.ChiTietChiDinh.CreatedBy, dbo.ChiTietChiDinh.UpdatedDate, dbo.ChiTietChiDinh.UpdatedBy, dbo.ChiTietChiDinh.DeletedDate, dbo.ChiTietChiDinh.DeletedBy, 
                      dbo.ChiTietChiDinh.Status AS CTCDStatus, dbo.Services.Status AS ServiceStatus, dbo.Services.Code, dbo.Services.Name, dbo.Services.Price, 
                      dbo.Services.EnglishName, dbo.Services.Type, dbo.ChiDinhView.NgayChiDinh, dbo.ChiDinhView.BacSiChiDinhGUID, dbo.ChiDinhView.BenhNhanGUID, 
                      dbo.ChiDinhView.FullName, dbo.ChiDinhView.DobStr, dbo.ChiDinhView.GenderAsStr, dbo.ChiDinhView.Archived, dbo.ChiDinhView.MaChiDinh, 
                      dbo.ChiDinhView.Status AS ChiDinhStatus
FROM         dbo.ChiTietChiDinh INNER JOIN
                      dbo.Services ON dbo.ChiTietChiDinh.ServiceGUID = dbo.Services.ServiceGUID INNER JOIN
                      dbo.ChiDinhView ON dbo.ChiTietChiDinh.ChiDinhGUID = dbo.ChiDinhView.ChiDinhGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[DichVuChiDinhView]
AS
SELECT     dbo.DichVuChiDinh.DichVuChiDinhGUID, dbo.DichVuChiDinh.ChiTietChiDinhGUID, dbo.DichVuChiDinh.ServiceHistoryGUID, 
                      dbo.ServiceHistoryView.Name, dbo.ServiceHistoryView.Price, dbo.ServiceHistoryView.Code, dbo.ServiceHistoryView.ServiceGUID, 
                      dbo.ServiceHistoryView.Status, dbo.ServiceHistoryView.Type, dbo.ServiceHistoryView.EnglishName, dbo.ServiceHistoryView.PatientGUID
FROM         dbo.DichVuChiDinh INNER JOIN
                      dbo.ServiceHistoryView ON dbo.DichVuChiDinh.ServiceHistoryGUID = dbo.ServiceHistoryView.ServiceHistoryGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

