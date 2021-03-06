USE MM
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[HoaDonThuocView]
AS
SELECT     HoaDonThuocGUID, PhieuThuThuocGUID, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, HinhThucThanhToan, VAT, 
                      CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' END AS HinhThucThanhToanStr
FROM         dbo.HoaDonThuoc
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[InvoiceView]
AS
SELECT     dbo.Invoice.InvoiceGUID, dbo.Invoice.ReceiptGUID, dbo.Invoice.InvoiceCode, dbo.Invoice.InvoiceDate, dbo.Invoice.TenDonVi, dbo.Invoice.SoTaiKhoan, 
                      dbo.Invoice.HinhThucThanhToan, dbo.Invoice.CreatedDate, dbo.Invoice.CreatedBy, dbo.Invoice.UpdatedDate, dbo.Invoice.UpdatedBy, dbo.Invoice.DeletedDate, 
                      dbo.Invoice.DeletedBy, dbo.Invoice.Status, dbo.ReceiptView.Address, dbo.ReceiptView.FileNum, dbo.ReceiptView.FullName, dbo.Invoice.VAT, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' END AS HinhThucThanhToanStr, dbo.Invoice.MaSoThue, 
                      ISNULL(dbo.Invoice.TenNguoiMuaHang, dbo.ReceiptView.FullName) AS TenNguoiMuaHang, ISNULL(dbo.Invoice.DiaChi, dbo.ReceiptView.Address) AS DiaChi
FROM         dbo.Invoice INNER JOIN
                      dbo.ReceiptView ON dbo.Invoice.ReceiptGUID = dbo.ReceiptView.ReceiptGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
