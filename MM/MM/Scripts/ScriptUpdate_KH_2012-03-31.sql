USE MM
GO
ALTER TABLE [dbo].[NhatKyLienHeCongTy] 
DROP CONSTRAINT [DF_NhatKyLienHeCongTy_SoNguoiKham]
GO
ALTER TABLE NhatKyLienHeCongTy 
ALTER COLUMN SoDienThoaiLienHe NVARCHAR(500)
GO
ALTER TABLE NhatKyLienHeCongTy 
ALTER COLUMN SoNguoiKham NVARCHAR(500)
GO
ALTER TABLE NhatKyLienHeCongTy 
ADD [DiaChi] [nvarchar](500) NULL,
[Email] [nvarchar](500) NULL
GO
ALTER TABLE Invoice 
ADD [ChuaThuTien] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE HoaDonThuoc 
ADD [ChuaThuTien] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE HoaDonXuatTruoc 
ADD [ChuaThuTien] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE HoaDonHopDong 
ADD [ChuaThuTien] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE Permission 
ADD [IsExportAll] [bit] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[NhatKyLienHeCongTyView]
AS
SELECT     dbo.NhatKyLienHeCongTy.NhatKyLienHeCongTyGUID, dbo.NhatKyLienHeCongTy.DocStaffGUID, dbo.NhatKyLienHeCongTy.NgayGioLienHe, 
                      dbo.NhatKyLienHeCongTy.CongTyLienHe, dbo.NhatKyLienHeCongTy.NoiDungLienHe, dbo.NhatKyLienHeCongTy.CreatedDate, dbo.NhatKyLienHeCongTy.CreatedBy, 
                      dbo.NhatKyLienHeCongTy.UpdatedDate, dbo.NhatKyLienHeCongTy.UpdatedBy, dbo.NhatKyLienHeCongTy.DeletedDate, dbo.NhatKyLienHeCongTy.DeletedBy, 
                      dbo.NhatKyLienHeCongTy.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS FullName, dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, 
                      dbo.DocStaffView.Status AS DocStaffStatus, dbo.NhatKyLienHeCongTy.Note, 
                      CASE dbo.NhatKyLienHeCongTy.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat, 
                      dbo.NhatKyLienHeCongTy.TenNguoiLienHe, dbo.NhatKyLienHeCongTy.SoDienThoaiLienHe, dbo.NhatKyLienHeCongTy.SoNguoiKham, 
                      dbo.NhatKyLienHeCongTy.ThangKham, dbo.NhatKyLienHeCongTy.DiaChi, dbo.NhatKyLienHeCongTy.Email
FROM         dbo.NhatKyLienHeCongTy LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.NhatKyLienHeCongTy.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.NhatKyLienHeCongTy.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
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
SELECT     InvoiceGUID, InvoiceCode, InvoiceDate, TenDonVi, SoTaiKhoan, HinhThucThanhToan, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, 
                      Status, VAT, CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, MaSoThue, 
                      ReceiptGUIDList, TenNguoiMuaHang, DiaChi, Notes, ChuaThuTien
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
SELECT     HoaDonThuocGUID, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, HinhThucThanhToan, VAT, CreatedDate, 
                      CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, PhieuThuThuocGUIDList, Notes, 
                      ChuaThuTien
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
SELECT     SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, HinhThucThanhToan, VAT, CreatedDate, CreatedBy, UpdatedDate, 
                      UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, HoaDonXuatTruocGUID, Notes, 
                      ChuaThuTien
FROM         dbo.HoaDonXuatTruoc
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
SELECT     HoaDonHopDongGUID, PhieuThuHopDongGUIDList, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, 
                      HinhThucThanhToan, VAT, Notes, Status, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, ChuaThuTien
FROM         dbo.HoaDonHopDong
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PermissionView]
AS
SELECT     dbo.Permission.PermissionGUID, dbo.Permission.LogonGUID, dbo.[Function].FunctionGUID, dbo.[Function].FunctionCode, dbo.Permission.IsView, 
                      dbo.Permission.IsAdd, dbo.Permission.IsEdit, dbo.Permission.IsDelete, dbo.Permission.IsPrint, dbo.Permission.IsExport, dbo.Permission.CreatedDate, 
                      dbo.Permission.CreatedBy, dbo.Permission.UpdatedDate, dbo.Permission.UpdatedBy, dbo.Permission.DeletedDate, dbo.Permission.DeletedBy, 
                      dbo.[Function].FunctionName, dbo.Permission.IsImport, dbo.Permission.IsConfirm, dbo.Permission.IsLock, dbo.Permission.IsExportAll
FROM         dbo.Permission INNER JOIN
                      dbo.[Function] ON dbo.Permission.FunctionGUID = dbo.[Function].FunctionGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

