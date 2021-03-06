USE MM
GO
ALTER TABLE Receipt
ADD [Notes] [nvarchar](max)  NULL
GO
ALTER TABLE PhieuThuThuoc
ADD [Notes] [nvarchar](max)  NULL
GO
ALTER TABLE Invoice
ADD [Notes] [nvarchar](max)  NULL
GO
ALTER TABLE HoaDonThuoc
ADD [Notes] [nvarchar](max)  NULL
GO
ALTER TABLE HoaDonXuatTruoc
ADD [Notes] [nvarchar](max)  NULL
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
                      dbo.PatientView.CompanyName, dbo.Receipt.Notes
FROM         dbo.Receipt INNER JOIN
                      dbo.PatientView ON dbo.Receipt.PatientGUID = dbo.PatientView.PatientGUID
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
SELECT     InvoiceGUID, InvoiceCode, InvoiceDate, TenDonVi, SoTaiKhoan, HinhThucThanhToan, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, 
                      DeletedBy, Status, VAT, CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr,
                       MaSoThue, ReceiptGUIDList, TenNguoiMuaHang, DiaChi, Notes
FROM         dbo.Invoice
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
SELECT     HoaDonThuocGUID, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, HinhThucThanhToan, VAT, 
                      CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, 
                      PhieuThuThuocGUIDList, Notes
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

ALTER VIEW [dbo].[HoaDonXuatTruocView]
AS
SELECT     SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, HinhThucThanhToan, VAT, CreatedDate, CreatedBy, 
                      UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, 
                      HoaDonXuatTruocGUID, Notes
FROM         dbo.HoaDonXuatTruoc
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO