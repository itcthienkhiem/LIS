USE MM
GO
UPDATE [Function]
SET FunctionName = N'Kê toa thuốc'
WHERE FunctionCode = 'KeToa'
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'cba4477e-12ad-4c4b-ab39-1bda3ed5c624', N'KeToaCapCuu', N'Kê toa cấp cứu')
GO
/****** Object:  Table [dbo].[ToaCapCuu]    Script Date: 11/11/2012 08:31:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToaCapCuu](
	[ToaCapCuuGUID] [uniqueidentifier] NOT NULL,
	[MaToaCapCuu] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NgayKeToa] [datetime] NOT NULL,
	[BacSiKeToaGUID] [uniqueidentifier] NULL,
	[MaBenhNhan] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenBenhNhan] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DiaChi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenCongTy] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ToaCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ToaCapCuu] PRIMARY KEY CLUSTERED 
(
	[ToaCapCuuGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ToaCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_ToaCapCuu_DocStaff] FOREIGN KEY([BacSiKeToaGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
/****** Object:  Table [dbo].[ChiTietToaCapCuu]    Script Date: 11/11/2012 08:18:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietToaCapCuu](
	[ChiTietToaCapCuuGUID] [uniqueidentifier] NOT NULL,
	[ToaCapCuuGUID] [uniqueidentifier] NOT NULL,
	[KhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[SoLuong] [float] NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietToaCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietToaCapCuu] PRIMARY KEY CLUSTERED 
(
	[ChiTietToaCapCuuGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[ChiTietToaCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietToaCapCuu_KhoCapCuu] FOREIGN KEY([KhoCapCuuGUID])
REFERENCES [dbo].[KhoCapCuu] ([KhoCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietToaCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietToaCapCuu_ToaCapCuu] FOREIGN KEY([ToaCapCuuGUID])
REFERENCES [dbo].[ToaCapCuu] ([ToaCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[ToaCapCuuView]    Script Date: 11/11/2012 08:19:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ToaCapCuuView]
AS
SELECT     dbo.DocStaffView.FullName, dbo.ToaCapCuu.*
FROM         dbo.ToaCapCuu LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.ToaCapCuu.BacSiKeToaGUID = dbo.DocStaffView.DocStaffGUID

GO
/****** Object:  View [dbo].[ChiTietToaCapCuuView]    Script Date: 11/11/2012 08:19:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietToaCapCuuView]
AS
SELECT     dbo.KhoCapCuu.MaCapCuu, dbo.KhoCapCuu.TenCapCuu, dbo.KhoCapCuu.DonViTinh, dbo.KhoCapCuu.Status AS KhoCapCuuStatus, 
                      dbo.ChiTietToaCapCuu.ToaCapCuuGUID, dbo.ChiTietToaCapCuu.ChiTietToaCapCuuGUID, dbo.ChiTietToaCapCuu.KhoCapCuuGUID, dbo.ChiTietToaCapCuu.SoLuong, 
                      dbo.ChiTietToaCapCuu.Note, dbo.ChiTietToaCapCuu.CreatedDate, dbo.ChiTietToaCapCuu.CreatedBy, dbo.ChiTietToaCapCuu.UpdatedBy, 
                      dbo.ChiTietToaCapCuu.UpdatedDate, dbo.ChiTietToaCapCuu.DeletedDate, dbo.ChiTietToaCapCuu.DeletedBy, dbo.ChiTietToaCapCuu.Status
FROM         dbo.ChiTietToaCapCuu INNER JOIN
                      dbo.KhoCapCuu ON dbo.ChiTietToaCapCuu.KhoCapCuuGUID = dbo.KhoCapCuu.KhoCapCuuGUID

GO
