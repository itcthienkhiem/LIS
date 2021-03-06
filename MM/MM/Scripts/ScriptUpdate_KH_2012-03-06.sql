USE MM
GO
/****** Object:  Table [dbo].[HoaDonXuatTruoc]    Script Date: 03/04/2012 09:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonXuatTruoc](
	[HoaDonXuatTruocGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_HoaDonXuatTruoc_HoaDonXuatTruoc]  DEFAULT (newid()),
	[SoHoaDon] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayXuatHoaDon] [datetime] NOT NULL,
	[TenNguoiMuaHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenDonVi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaSoThue] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoTaiKhoan] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HinhThucThanhToan] [tinyint] NULL,
	[VAT] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_HoaDonXuatTruoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_HoaDonXuatTruoc] PRIMARY KEY CLUSTERED 
(
	[HoaDonXuatTruocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDonXuatTruoc]    Script Date: 03/04/2012 09:16:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonXuatTruoc](
	[ChiTietHoaDonXuatTruocGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ChiTietHoaDonXuatTruoc_ChiTietHoaDonXuatTruocGUID]  DEFAULT (newid()),
	[HoaDonXuatTruocGUID] [uniqueidentifier] NOT NULL,
	[TenMatHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonViTinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuong] [int] NOT NULL CONSTRAINT [DF_ChiTietHoaDonXuatTruoc_SoLuong]  DEFAULT ((0)),
	[DonGia] [float] NOT NULL CONSTRAINT [DF_ChiTietHoaDonXuatTruoc_DonGia]  DEFAULT ((0)),
	[ThanhTien] [float] NOT NULL CONSTRAINT [DF_ChiTietHoaDonXuatTruoc_ThanhTien]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_ChiTietHoaDonXuatTruoc] PRIMARY KEY CLUSTERED 
(
	[ChiTietHoaDonXuatTruocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietHoaDonXuatTruoc]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDonXuatTruoc_HoaDonXuatTruoc1] FOREIGN KEY([HoaDonXuatTruocGUID])
REFERENCES [dbo].[HoaDonXuatTruoc] ([HoaDonXuatTruocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[HoaDonXuatTruocView]
AS
SELECT     SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, HinhThucThanhToan, VAT, CreatedDate, CreatedBy, UpdatedDate, 
                      UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, HoaDonXuatTruocGUID
FROM         dbo.HoaDonXuatTruoc
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'dab2005c-7eb0-4228-882c-4d9e7042e095', N'HoaDonXuatTruoc', N'Hóa đơn xuất trước')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'1f114d44-a8b4-478d-86ab-b8a07c62ed08', N'DangKyHoaDonXuatTruoc', N'Đăng ký hóa đơn xuất trước')
GO


