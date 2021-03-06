USE MM
GO
/****** Object:  Table [dbo].[Bookmark]    Script Date: 02/25/2012 18:35:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookmark](
	[BookmarkGUID] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Type] [int] NOT NULL CONSTRAINT [DF_Bookmark_Type]  DEFAULT ((0)),
 CONSTRAINT [PK_Bookmark] PRIMARY KEY CLUSTERED 
(
	[BookmarkGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE Invoice
ADD TenNguoiMuaHang nvarchar(255) NULL,
	DiaChi nvarchar(255) NULL
GO
ALTER TABLE ServiceHistory
ADD [GiaVon] [float] NOT NULL DEFAULT ((0))
GO
ALTER TABLE ChiTietPhieuThuThuoc
ADD [DonGiaNhap] [float] NOT NULL DEFAULT ((0))
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
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' END AS HinhThucThanhToanStr, dbo.Invoice.MaSoThue, 
                      ISNULL(dbo.Invoice.TenNguoiMuaHang, dbo.ReceiptView.FullName) AS TenNguoiMuaHang, ISNULL(dbo.Invoice.DiaChi, dbo.ReceiptView.Address) AS DiaChi
FROM         dbo.Invoice INNER JOIN
                      dbo.ReceiptView ON dbo.Invoice.ReceiptGUID = dbo.ReceiptView.ReceiptGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ServiceHistoryView]
AS
SELECT     dbo.ServiceHistory.PatientGUID, dbo.DocStaff.DocStaffGUID, dbo.ServiceHistory.Price AS FixedPrice, dbo.ServiceHistory.Note, dbo.Services.ServiceGUID, 
                      dbo.Services.Code, dbo.Services.Name, dbo.Services.Price, dbo.ServiceHistory.CreatedDate, dbo.ServiceHistory.CreatedBy, dbo.ServiceHistory.UpdatedDate, 
                      dbo.ServiceHistory.UpdatedBy, dbo.ServiceHistory.DeletedDate, dbo.ServiceHistory.DeletedBy, dbo.DocStaff.AvailableToWork, 
                      dbo.ServiceHistory.ServiceHistoryGUID, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS CreatedName, dbo.ServiceHistory.Status, dbo.ServiceHistory.ActivedDate, 
                      dbo.Contact.FullName, dbo.ServiceHistory.IsExported, dbo.ServiceHistory.Discount, dbo.ServiceHistory.IsNormalOrNegative, dbo.ServiceHistory.Normal, 
                      dbo.ServiceHistory.Abnormal, dbo.ServiceHistory.Negative, dbo.ServiceHistory.Positive, dbo.Services.EnglishName, dbo.Services.Type, 
                      dbo.ServiceHistory.GiaVon
FROM         dbo.Contact INNER JOIN
                      dbo.DocStaff ON dbo.Contact.ContactGUID = dbo.DocStaff.ContactGUID INNER JOIN
                      dbo.ServiceHistory ON dbo.DocStaff.DocStaffGUID = dbo.ServiceHistory.DocStaffGUID INNER JOIN
                      dbo.Services ON dbo.ServiceHistory.ServiceGUID = dbo.Services.ServiceGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.ServiceHistory.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietPhieuThuThuocView]
AS
SELECT     dbo.ChiTietPhieuThuThuoc.ChiTietPhieuThuThuocGUID, dbo.ChiTietPhieuThuThuoc.PhieuThuThuocGUID, dbo.ChiTietPhieuThuThuoc.ThuocGUID, 
                      dbo.ChiTietPhieuThuThuoc.DonGia, dbo.ChiTietPhieuThuThuoc.SoLuong, dbo.ChiTietPhieuThuThuoc.Giam, dbo.ChiTietPhieuThuThuoc.ThanhTien, 
                      dbo.ChiTietPhieuThuThuoc.CreatedDate, dbo.ChiTietPhieuThuThuoc.CreatedBy, dbo.ChiTietPhieuThuThuoc.UpdatedDate, dbo.ChiTietPhieuThuThuoc.UpdatedBy, 
                      dbo.ChiTietPhieuThuThuoc.DeletedDate, dbo.ChiTietPhieuThuThuoc.DeletedBy, dbo.ChiTietPhieuThuThuoc.Status AS CTPTTStatus, dbo.Thuoc.MaThuoc, 
                      dbo.Thuoc.TenThuoc, dbo.Thuoc.DonViTinh, dbo.Thuoc.Status AS ThuocStatus, dbo.ChiTietPhieuThuThuoc.DonGiaNhap
FROM         dbo.ChiTietPhieuThuThuoc INNER JOIN
                      dbo.Thuoc ON dbo.ChiTietPhieuThuThuoc.ThuocGUID = dbo.Thuoc.ThuocGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
                      dbo.Services.Name, dbo.ReceiptDetail.Status AS ReceiptDetailStatus, dbo.ServiceHistory.Status AS ServiceHistoryStatus, dbo.ServiceHistory.GiaVon
FROM         dbo.ReceiptDetail INNER JOIN
                      dbo.ServiceHistory ON dbo.ReceiptDetail.ServiceHistoryGUID = dbo.ServiceHistory.ServiceHistoryGUID INNER JOIN
                      dbo.Services ON dbo.ServiceHistory.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
