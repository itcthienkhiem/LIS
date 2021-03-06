USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'34295971-43c4-4a9d-be85-364c24dad998', N'GiaVonDichVu', N'Giá vốn dịch vụ')
GO
/****** Object:  Table [dbo].[GiaVonDichVu]    Script Date: 01/13/2012 11:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaVonDichVu](
	[GiaVonDichVuGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[GiaVon] [float] NOT NULL,
	[NgayApDung] [datetime] NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_GiaVonDichVu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_GiaVonDichVu] PRIMARY KEY CLUSTERED 
(
	[GiaVonDichVuGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GiaVonDichVu]  WITH CHECK ADD  CONSTRAINT [FK_GiaVonDichVu_Services] FOREIGN KEY([ServiceGUID])
REFERENCES [dbo].[Services] ([ServiceGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GiaVonDichVu] CHECK CONSTRAINT [FK_GiaVonDichVu_Services]
GO
/****** Object:  View [dbo].[GiaVonDichVuView]    Script Date: 01/13/2012 11:09:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GiaVonDichVuView]
AS
SELECT     dbo.GiaVonDichVu.GiaVonDichVuGUID, dbo.GiaVonDichVu.ServiceGUID, dbo.GiaVonDichVu.GiaVon, dbo.GiaVonDichVu.NgayApDung, 
                      dbo.GiaVonDichVu.Note, dbo.GiaVonDichVu.CreatedDate, dbo.GiaVonDichVu.CreatedBy, dbo.GiaVonDichVu.UpdatedDate, 
                      dbo.GiaVonDichVu.UpdatedBy, dbo.GiaVonDichVu.DeletedDate, dbo.GiaVonDichVu.DeletedBy, dbo.GiaVonDichVu.Status AS GiaVonDichVuStatus, 
                      dbo.Services.Code, dbo.Services.Name, dbo.Services.EnglishName, dbo.Services.Price, dbo.Services.Type, 
                      dbo.Services.Status AS ServiceStatus
FROM         dbo.GiaVonDichVu INNER JOIN
                      dbo.Services ON dbo.GiaVonDichVu.ServiceGUID = dbo.Services.ServiceGUID

GO
/****** Object:  Table [dbo].[KetLuan]    Script Date: 01/13/2012 14:51:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetLuan](
	[KetLuanGUID] [uniqueidentifier] NOT NULL,
	[NgayKetLuan] [datetime] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[DocStaffGUID] [uniqueidentifier] NOT NULL,
	[HasLamThemXetNghiem] [bit] NOT NULL CONSTRAINT [DF_KetLuan_HasLamThemXetNghiem]  DEFAULT ((0)),
	[CacXetNghiemLamThem] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HasLamDuCanLamSang] [bit] NOT NULL CONSTRAINT [DF_KetLuan_HasLamDuCanLamSang]  DEFAULT ((0)),
	[LyDo_CanLamSang] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HasDuSucKhoe] [bit] NOT NULL CONSTRAINT [DF_KetLuan_HasDuSucKhoe]  DEFAULT ((0)),
	[LyDo_SucKhoe] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KetLuan_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KetLuan] PRIMARY KEY CLUSTERED 
(
	[KetLuanGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[KetLuan]  WITH CHECK ADD  CONSTRAINT [FK_KetLuan_DocStaff] FOREIGN KEY([DocStaffGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
GO
ALTER TABLE [dbo].[KetLuan] CHECK CONSTRAINT [FK_KetLuan_DocStaff]
GO
ALTER TABLE [dbo].[KetLuan]  WITH CHECK ADD  CONSTRAINT [FK_KetLuan_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KetLuan] CHECK CONSTRAINT [FK_KetLuan_Patient]
GO
/****** Object:  View [dbo].[KetLuanView]    Script Date: 01/13/2012 14:51:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[KetLuanView]
AS
SELECT     dbo.KetLuan.*, dbo.DocStaffView.FirstName, dbo.DocStaffView.SurName, dbo.DocStaffView.Archived, dbo.DocStaffView.Address, 
                      dbo.DocStaffView.FullName, dbo.DocStaffView.GenderAsStr, dbo.DocStaffView.DobStr
FROM         dbo.KetLuan INNER JOIN
                      dbo.DocStaffView ON dbo.KetLuan.DocStaffGUID = dbo.DocStaffView.DocStaffGUID

GO