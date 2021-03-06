USE MM
GO
ALTER TABLE [NgayBatDauLamMoiSoHoaDon]
ADD [SoHoaDonBatDau] [int] NOT NULL DEFAULT ((1))
GO
/****** Object:  Table [dbo].[NgayBatDauLamMoiSoHoaDonXetNghiemYKhoa]    Script Date: 09/01/2013 20:35:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NgayBatDauLamMoiSoHoaDonXetNghiemYKhoa](
	[MaNgayBatDauGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_NgayBatDauLamMoiSoHoaDonXetNghiemYKhoa_MaNgayBatDauGUID]  DEFAULT (newid()),
	[NgayBatDau] [datetime] NOT NULL,
	[MauSo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KiHieu] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoHoaDonBatDau] [int] NOT NULL CONSTRAINT [DF_NgayBatDauLamMoiSoHoaDonXetNghiemYKhoa_SoHoaDonBatDau]  DEFAULT ((1)),
 CONSTRAINT [PK_NgayBatDauLamMoiSoHoaDonXetNghiemYKhoa] PRIMARY KEY CLUSTERED 
(
	[MaNgayBatDauGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuanLySoHoaDonXetNghiemYKhoa]    Script Date: 09/01/2013 20:38:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuanLySoHoaDonXetNghiemYKhoa](
	[QuanLySoHoaDonGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_QuanLySoHoaDonXetNghiemYKhoa_QuanLySoHoaDonGUID]  DEFAULT (newid()),
	[SoHoaDon] [int] NOT NULL,
	[DaXuat] [bit] NOT NULL CONSTRAINT [DF_QuanLySoHoaDonXetNghiemYKhoa_DaXuat]  DEFAULT ((0)),
	[XuatTruoc] [bit] NOT NULL CONSTRAINT [DF_QuanLySoHoaDonXetNghiemYKhoa_XuatTruoc]  DEFAULT ((0)),
	[NgayBatDau] [datetime] NULL,
 CONSTRAINT [PK_QuanLySoHoaDonXetNghiemYKhoa] PRIMARY KEY CLUSTERED 
(
	[QuanLySoHoaDonGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Statistic [_dta_stat_1310783877_4_5]    Script Date: 09/01/2013 20:38:35 ******/
CREATE STATISTICS [_dta_stat_1310783877_4_5] ON [dbo].[QuanLySoHoaDonXetNghiemYKhoa]([XuatTruoc], [NgayBatDau])
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'71c67741-4d3c-445d-8e2f-575bdf32bee3', N'HoaDonXetNghiem', N'Hóa đơn xét nghiệm')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'79a8ec40-b3da-46b7-ac13-23f43388263f', N'ThayDoiSoHoaDonXetNghiem', N'Thay đổi số hoa đơn xét nghiệm')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'9d7bdbd5-60fc-40b3-b041-b1f36c0eaddb', N'ThayDoiSoHoaDon', N'Thay đổi số hoa đơn')
GO
/****** Object:  Table [dbo].[HoaDonXetNghiem]    Script Date: 09/01/2013 20:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonXetNghiem](
	[HoaDonXetNghiemGUID] [uniqueidentifier] NOT NULL,
	[SoHoaDon] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayXuatHoaDon] [datetime] NULL,
	[TenNguoiMuaHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenDonVi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaSoThue] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoTaiKhoan] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HinhThucThanhToan] [tinyint] NULL,
	[VAT] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_HoaDonXetNghiem_Status]  DEFAULT ((0)),
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ChuaThuTien] [bit] NOT NULL CONSTRAINT [DF_HoaDonXetNghiem_ChuaThuTien]  DEFAULT ((0)),
	[MauSo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KiHieu] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HinhThucNhanHoaDon] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_HoaDonXetNghiem] PRIMARY KEY CLUSTERED 
(
	[HoaDonXetNghiemGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDonXetNghiem]    Script Date: 09/01/2013 20:46:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonXetNghiem](
	[ChiTietHoaDonXetNghiemGUID] [uniqueidentifier] NOT NULL,
	[HoaDonXetNghiemGUID] [uniqueidentifier] NOT NULL,
	[TenHangHoa] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonViTinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuong] [int] NOT NULL CONSTRAINT [DF_ChiTietHoaDonXetNghiem_SoLuong]  DEFAULT ((0)),
	[DonGia] [float] NOT NULL CONSTRAINT [DF_ChiTietHoaDonXetNghiem_DonGia]  DEFAULT ((0)),
	[ThanhTien] [float] NOT NULL CONSTRAINT [DF_ChiTietHoaDonXetNghiem_ThanhTien]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietHoaDonXetNghiem_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietHoaDonXetNghiem] PRIMARY KEY CLUSTERED 
(
	[ChiTietHoaDonXetNghiemGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[ChiTietHoaDonXetNghiem]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDonXetNghiem_HoaDonXetNghiem] FOREIGN KEY([HoaDonXetNghiemGUID])
REFERENCES [dbo].[HoaDonXetNghiem] ([HoaDonXetNghiemGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[HoaDonXetNghiemView]    Script Date: 09/01/2013 22:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[HoaDonXetNghiemView]
AS
SELECT     dbo.HoaDonXetNghiem.HoaDonXetNghiemGUID, dbo.HoaDonXetNghiem.SoHoaDon, dbo.HoaDonXetNghiem.NgayXuatHoaDon, dbo.HoaDonXetNghiem.TenNguoiMuaHang, 
                      dbo.HoaDonXetNghiem.DiaChi, dbo.HoaDonXetNghiem.TenDonVi, dbo.HoaDonXetNghiem.MaSoThue, dbo.HoaDonXetNghiem.SoTaiKhoan, dbo.HoaDonXetNghiem.HinhThucThanhToan, 
                      dbo.HoaDonXetNghiem.VAT, dbo.HoaDonXetNghiem.CreatedDate, dbo.HoaDonXetNghiem.CreatedBy, dbo.HoaDonXetNghiem.UpdatedDate, dbo.HoaDonXetNghiem.UpdatedBy, 
                      dbo.HoaDonXetNghiem.DeletedDate, dbo.HoaDonXetNghiem.DeletedBy, dbo.HoaDonXetNghiem.Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr, 
                      dbo.HoaDonXetNghiem.Notes, dbo.HoaDonXetNghiem.ChuaThuTien, dbo.HoaDonXetNghiem.MauSo, dbo.HoaDonXetNghiem.KiHieu, 
                      dbo.HoaDonXetNghiem.HinhThucNhanHoaDon, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao
FROM         dbo.HoaDonXetNghiem LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.HoaDonXetNghiem.CreatedBy = dbo.DocStaffView.DocStaffGUID





