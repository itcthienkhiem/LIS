USE [MM]
GO
ALTER TABLE YKienKhachHang
ADD [TenCongTy] [nvarchar](500) NULL,
	[MaKhachHang] [nvarchar](200) NULL,
	[MucDich] [nvarchar](max) NULL
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[YKienKhachHangView]
AS
SELECT     dbo.YKienKhachHang.YKienKhachHangGUID, dbo.YKienKhachHang.PatientGUID, dbo.YKienKhachHang.TenKhachHang, dbo.YKienKhachHang.SoDienThoai, 
                      dbo.YKienKhachHang.DiaChi, dbo.YKienKhachHang.YeuCau, dbo.YKienKhachHang.Nguon, dbo.YKienKhachHang.Note, dbo.YKienKhachHang.ContactDate, 
                      dbo.YKienKhachHang.ContactBy, dbo.YKienKhachHang.UpdatedDate, dbo.YKienKhachHang.UpdatedBy, dbo.YKienKhachHang.DeletedDate, 
                      dbo.YKienKhachHang.DeletedBy, dbo.YKienKhachHang.Status, 
                      CASE dbo.YKienKhachHang.ContactBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiTao, 
                      CASE dbo.YKienKhachHang.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat, 
                      dbo.YKienKhachHang.KetLuan, dbo.YKienKhachHang.NguoiKetLuan, DocStaffView_2.FullName AS TenNguoiKetLuan, dbo.YKienKhachHang.BacSiPhuTrachGUID, 
                      dbo.YKienKhachHang.DaXong, ISNULL(DocStaffView_3.FullName, N'') AS BacSiPhuTrach, 
                      CASE YKienKhachHang.DaXong WHEN 'False' THEN N'Chưa xong' ELSE N'Đã xong' END AS DaXongStr, dbo.YKienKhachHang.IsIN, dbo.YKienKhachHang.SoTongDai, 
                      CASE IsIN WHEN 'False' THEN 'OUT' ELSE 'IN' END AS InOut, dbo.YKienKhachHang.TenCongTy, dbo.YKienKhachHang.MaKhachHang, 
                      dbo.YKienKhachHang.MucDich
FROM         dbo.YKienKhachHang LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_3 ON dbo.YKienKhachHang.BacSiPhuTrachGUID = DocStaffView_3.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_2 ON dbo.YKienKhachHang.NguoiKetLuan = DocStaffView_2.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.YKienKhachHang.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.YKienKhachHang.ContactBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'c66382e8-cb30-40b2-a7e3-e3c60ea50e7c', N'ThongKeChiDinhCuaBacSi', N'Thông kê chỉ định của bác sĩ')
GO
/****** Object:  Table [dbo].[TuVanKhachHang]    Script Date: 07/14/2015 21:28:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TuVanKhachHang](
	[TuVanKhachHangGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NULL,
	[TenCongTy] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaKhachHang] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenKhachHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoDienThoai] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MucDich] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[YeuCau] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Nguon] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactDate] [datetime] NULL,
	[ContactBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_TuVanKhachHang_Status]  DEFAULT ((0)),
	[KetLuan] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NguoiKetLuan] [uniqueidentifier] NULL,
	[BacSiPhuTrachGUID] [uniqueidentifier] NULL,
	[DaXong] [bit] NOT NULL CONSTRAINT [DF__TuVan__DaXon__55C14FF6]  DEFAULT ((0)),
	[IsIN] [bit] NOT NULL CONSTRAINT [DF__TuVan__IsIN__0DF09A80]  DEFAULT ((0)),
	[SoTongDai] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BanThe] [bit] NOT NULL CONSTRAINT [DF_TuVanKhachHang_BanThe]  DEFAULT ((0)),
 CONSTRAINT [PK_TuVanKhachHang] PRIMARY KEY CLUSTERED 
(
	[TuVanKhachHangGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[TuVanKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_TuVanKhachHang_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[TuVanKhachHangView]    Script Date: 07/14/2015 21:28:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TuVanKhachHangView]
AS
SELECT     dbo.TuVanKhachHang.TuVanKhachHangGUID, dbo.TuVanKhachHang.PatientGUID, dbo.TuVanKhachHang.TenKhachHang, dbo.TuVanKhachHang.SoDienThoai, 
                      dbo.TuVanKhachHang.DiaChi, dbo.TuVanKhachHang.YeuCau, dbo.TuVanKhachHang.Nguon, dbo.TuVanKhachHang.Note, dbo.TuVanKhachHang.ContactDate, 
                      dbo.TuVanKhachHang.ContactBy, dbo.TuVanKhachHang.UpdatedDate, dbo.TuVanKhachHang.UpdatedBy, dbo.TuVanKhachHang.DeletedDate, 
                      dbo.TuVanKhachHang.DeletedBy, dbo.TuVanKhachHang.Status, 
                      CASE dbo.TuVanKhachHang.ContactBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiTao, 
                      CASE dbo.TuVanKhachHang.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat, 
                      dbo.TuVanKhachHang.KetLuan, dbo.TuVanKhachHang.NguoiKetLuan, DocStaffView_2.FullName AS TenNguoiKetLuan, dbo.TuVanKhachHang.BacSiPhuTrachGUID, 
                      dbo.TuVanKhachHang.DaXong, ISNULL(DocStaffView_3.FullName, N'') AS BacSiPhuTrach, 
                      CASE TuVanKhachHang.DaXong WHEN 'False' THEN N'Chưa xong' ELSE N'Đã xong' END AS DaXongStr, dbo.TuVanKhachHang.IsIN, dbo.TuVanKhachHang.SoTongDai, 
                      CASE IsIN WHEN 'False' THEN 'OUT' ELSE 'IN' END AS InOut, dbo.TuVanKhachHang.TenCongTy, dbo.TuVanKhachHang.MaKhachHang, 
                      dbo.TuVanKhachHang.MucDich, dbo.TuVanKhachHang.BanThe
FROM         dbo.TuVanKhachHang LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_3 ON dbo.TuVanKhachHang.BacSiPhuTrachGUID = DocStaffView_3.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_2 ON dbo.TuVanKhachHang.NguoiKetLuan = DocStaffView_2.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.TuVanKhachHang.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.TuVanKhachHang.ContactBy = dbo.DocStaffView.DocStaffGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'1e661972-68b9-44e6-8357-b4a2748555da', N'TuVanKhachHang', N'Tư vấn khách hàng')












