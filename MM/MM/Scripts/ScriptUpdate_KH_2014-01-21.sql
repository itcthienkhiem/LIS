USE MM
GO
ALTER TABLE HoaDonXuatTruoc
ALTER COLUMN MaSoThue NVARCHAR(50)
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
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' WHEN 2 THEN N'Tiền mặt/Chuyển khoản' END AS HinhThucThanhToanStr, 
                      dbo.HoaDonXuatTruoc.HoaDonXuatTruocGUID, dbo.HoaDonXuatTruoc.Notes, dbo.HoaDonXuatTruoc.ChuaThuTien, dbo.HoaDonXuatTruoc.MauSo, 
                      dbo.HoaDonXuatTruoc.KiHieu, dbo.HoaDonXuatTruoc.HinhThucNhanHoaDon, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, 
                      dbo.HoaDonXuatTruoc.MaDonVi
FROM         dbo.HoaDonXuatTruoc LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.HoaDonXuatTruoc.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO









