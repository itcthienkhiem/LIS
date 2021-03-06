USE MM
GO
ALTER TABLE Receipt
ADD [ChuaThuTien] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE PhieuThuThuoc
ADD [ChuaThuTien] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE PhieuThuHopDong
ADD [ChuaThuTien] [bit] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ReceiptView]
AS
SELECT     dbo.PatientView.FullName, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.Receipt.ReceiptGUID, dbo.Receipt.PatientGUID, dbo.Receipt.ReceiptDate, 
                      dbo.Receipt.CreatedDate, dbo.Receipt.CreatedBy, dbo.Receipt.UpdatedDate, dbo.Receipt.UpdatedBy, dbo.Receipt.DeletedDate, dbo.Receipt.DeletedBy, 
                      dbo.Receipt.Status, dbo.Receipt.ReceiptCode, dbo.Receipt.IsExportedInVoice, dbo.PatientView.CompanyName, dbo.Receipt.Notes, dbo.Receipt.ChuaThuTien
FROM         dbo.Receipt INNER JOIN
                      dbo.PatientView ON dbo.Receipt.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
GO
/****** Object:  Table [dbo].[YKienKhachHang]    Script Date: 03/23/2012 21:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YKienKhachHang](
	[YKienKhachHangGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NULL,
	[TenKhachHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoDienThoai] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[YeuCau] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Nguon] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactDate] [datetime] NULL,
	[ContactBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_YKienKhachHang_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_YKienKhachHang] PRIMARY KEY CLUSTERED 
(
	[YKienKhachHangGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[YKienKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_YKienKhachHang_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  Table [dbo].[NhatKyLienHeCongTy]    Script Date: 03/24/2012 00:46:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhatKyLienHeCongTy](
	[NhatKyLienHeCongTyGUID] [uniqueidentifier] NOT NULL,
	[DocStaffGUID] [uniqueidentifier] NULL,
	[NgayGioLienHe] [datetime] NOT NULL,
	[CongTyLienHe] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NoiDungLienHe] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_NhatKyLienHeCongTy_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_NhatKyLienHeCongTy] PRIMARY KEY CLUSTERED 
(
	[NhatKyLienHeCongTyGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[NhatKyLienHeCongTy]  WITH CHECK ADD  CONSTRAINT [FK_NhatKyLienHeCongTy_DocStaff] FOREIGN KEY([DocStaffGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[NhatKyLienHeCongTyView]    Script Date: 03/24/2012 00:47:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NhatKyLienHeCongTyView]
AS
SELECT     dbo.NhatKyLienHeCongTy.NhatKyLienHeCongTyGUID, dbo.NhatKyLienHeCongTy.DocStaffGUID, dbo.NhatKyLienHeCongTy.NgayGioLienHe, 
                      dbo.NhatKyLienHeCongTy.CongTyLienHe, dbo.NhatKyLienHeCongTy.NoiDungLienHe, dbo.NhatKyLienHeCongTy.CreatedDate, dbo.NhatKyLienHeCongTy.CreatedBy, 
                      dbo.NhatKyLienHeCongTy.UpdatedDate, dbo.NhatKyLienHeCongTy.UpdatedBy, dbo.NhatKyLienHeCongTy.DeletedDate, dbo.NhatKyLienHeCongTy.DeletedBy, 
                      dbo.NhatKyLienHeCongTy.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS FullName, dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, 
                      dbo.DocStaffView.Status AS DocStaffStatus, dbo.NhatKyLienHeCongTy.Note
FROM         dbo.NhatKyLienHeCongTy LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.NhatKyLienHeCongTy.DocStaffGUID = dbo.DocStaffView.DocStaffGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'b554fa8d-2671-4bd4-b70a-7357c6e6dac2', N'YKienKhachHang', N'Ý kiến khách hàng')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'bb7467b6-f0a2-4db1-a51b-880469ed0cb7', N'NhatKyLienHeCongTy', N'Nhật ký liên hệ công ty')
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
                      CASE dbo.NhatKyLienHeCongTy.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat
FROM         dbo.NhatKyLienHeCongTy LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.NhatKyLienHeCongTy.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.NhatKyLienHeCongTy.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
GO
/****** Object:  View [dbo].[YKienKhachHangView]    Script Date: 03/24/2012 09:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[YKienKhachHangView]
AS
SELECT     dbo.YKienKhachHang.YKienKhachHangGUID, dbo.YKienKhachHang.PatientGUID, dbo.YKienKhachHang.TenKhachHang, dbo.YKienKhachHang.SoDienThoai, 
                      dbo.YKienKhachHang.DiaChi, dbo.YKienKhachHang.YeuCau, dbo.YKienKhachHang.Nguon, dbo.YKienKhachHang.Note, dbo.YKienKhachHang.ContactDate, 
                      dbo.YKienKhachHang.ContactBy, dbo.YKienKhachHang.UpdatedDate, dbo.YKienKhachHang.UpdatedBy, dbo.YKienKhachHang.DeletedDate, 
                      dbo.YKienKhachHang.DeletedBy, dbo.YKienKhachHang.Status, 
                      CASE dbo.YKienKhachHang.ContactBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiTao, 
                      CASE dbo.YKienKhachHang.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat
FROM         dbo.YKienKhachHang LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.YKienKhachHang.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.YKienKhachHang.ContactBy = dbo.DocStaffView.DocStaffGUID

GO



















