USE MM
GO
ALTER TABLE NgayBatDauLamMoiSoHoaDon 
ADD [MauSo] [nvarchar](50) NULL,
	[KiHieu] [nvarchar](50) NULL
GO
ALTER TABLE HoaDonHopDong 
ADD [MauSo] [nvarchar](50) NULL,
	[KiHieu] [nvarchar](50) NULL
GO
ALTER TABLE HoaDonThuoc 
ADD [MauSo] [nvarchar](50) NULL,
	[KiHieu] [nvarchar](50) NULL
GO
ALTER TABLE HoaDonXuatTruoc 
ADD [MauSo] [nvarchar](50) NULL,
	[KiHieu] [nvarchar](50) NULL

GO
ALTER TABLE Invoice 
ADD [MauSo] [nvarchar](50) NULL,
	[KiHieu] [nvarchar](50) NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[HoaDonHopDongView]
AS
SELECT     HoaDonHopDongGUID, PhieuThuHopDongGUIDList, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, 
                      HinhThucThanhToan, VAT, Notes, Status, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, ChuaThuTien, MauSo, KiHieu
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

ALTER VIEW [dbo].[HoaDonThuocView]
AS
SELECT     HoaDonThuocGUID, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, HinhThucThanhToan, VAT, CreatedDate, 
                      CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, PhieuThuThuocGUIDList, Notes, 
                      ChuaThuTien, MauSo, KiHieu
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
                      ChuaThuTien, MauSo, KiHieu
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

ALTER VIEW [dbo].[InvoiceView]
AS
SELECT     InvoiceGUID, InvoiceCode, InvoiceDate, TenDonVi, SoTaiKhoan, HinhThucThanhToan, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, 
                      Status, VAT, CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, MaSoThue, 
                      ReceiptGUIDList, TenNguoiMuaHang, DiaChi, Notes, ChuaThuTien, MauSo, KiHieu
FROM         dbo.Invoice
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
UPDATE NgayBatDauLamMoiSoHoaDon
SET MauSo = N'01GTKT3/001', KiHieu = N'AA/11T'
WHERE NgayBatDau < '2012-05-25'
GO
UPDATE NgayBatDauLamMoiSoHoaDon
SET MauSo = N'01GTKT3/002', KiHieu = N'AA/12T'
WHERE NgayBatDau >= '2012-05-25'
GO
UPDATE HoaDonHopDong
SET MauSo = N'01GTKT3/001', KiHieu = N'AA/11T'
WHERE NgayXuatHoaDon < '2012-05-25'
GO
UPDATE HoaDonThuoc
SET MauSo = N'01GTKT3/001', KiHieu = N'AA/11T'
WHERE NgayXuatHoaDon < '2012-05-25'
GO
UPDATE HoaDonXuatTruoc
SET MauSo = N'01GTKT3/001', KiHieu = N'AA/11T'
WHERE NgayXuatHoaDon < '2012-05-25'
GO
UPDATE Invoice
SET MauSo = N'01GTKT3/001', KiHieu = N'AA/11T'
WHERE InvoiceDate < '2012-05-25'
GO
UPDATE HoaDonHopDong
SET MauSo = N'01GTKT3/002', KiHieu = N'AA/12T'
WHERE NgayXuatHoaDon >= '2012-05-25'
GO
UPDATE HoaDonThuoc
SET MauSo = N'01GTKT3/002', KiHieu = N'AA/12T'
WHERE NgayXuatHoaDon >= '2012-05-25'
GO
UPDATE HoaDonXuatTruoc
SET MauSo = N'01GTKT3/002', KiHieu = N'AA/12T'
WHERE NgayXuatHoaDon >= '2012-05-25'
GO
UPDATE Invoice
SET MauSo = N'01GTKT3/002', KiHieu = N'AA/12T'
WHERE InvoiceDate >= '2012-05-25'
