USE MM
GO
GO
/****** Object:  Table [dbo].[XetNghiem_Manual]    Script Date: 04/26/2012 10:46:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XetNghiem_Manual](
	[XetNghiem_ManualGUID] [uniqueidentifier] NOT NULL,
	[TenXetNghiem] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fullname] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Type] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_XetNghiem_Manual_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_XetNghiem_Manual] PRIMARY KEY CLUSTERED 
(
	[XetNghiem_ManualGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietXetNghiem_Manual]    Script Date: 04/26/2012 10:46:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietXetNghiem_Manual](
	[ChiTietXetNghiem_ManualGUID] [uniqueidentifier] NOT NULL,
	[XetNghiem_ManualGUID] [uniqueidentifier] NOT NULL,
	[FromValue] [float] NULL,
	[ToValue] [float] NULL,
	[DoiTuong] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietXetNghiem_Manual_DoiTuong]  DEFAULT ((0)),
	[DonVi] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietXetNghiem_Manual_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietXetNghiem_Manual] PRIMARY KEY CLUSTERED 
(
	[ChiTietXetNghiem_ManualGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietXetNghiem_Manual]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietXetNghiem_Manual_XetNghiem_Manual] FOREIGN KEY([XetNghiem_ManualGUID])
REFERENCES [dbo].[XetNghiem_Manual] ([XetNghiem_ManualGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  Table [dbo].[KetQuaXetNghiem_Manual]    Script Date: 04/26/2012 10:46:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaXetNghiem_Manual](
	[KetQuaXetNghiemManualGUID] [uniqueidentifier] NOT NULL,
	[NgayXN] [datetime] NOT NULL,
	[PatientGUID] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KetQuaXetNghiem_Manual_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KetQuaXetNghiem_Manual] PRIMARY KEY CLUSTERED 
(
	[KetQuaXetNghiemManualGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietKetQuaXetNghiem_Manual]    Script Date: 04/26/2012 10:47:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietKetQuaXetNghiem_Manual](
	[ChiTietKetQuaXetNghiem_ManualGUID] [uniqueidentifier] NOT NULL,
	[KetQuaXetNghiem_ManualGUID] [uniqueidentifier] NOT NULL,
	[XetNghiem_ManualGUID] [uniqueidentifier] NOT NULL,
	[TestResult] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TinhTrang] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietKetQuaXetNghiem_Manual_TinhTrang]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietKetQuaXetNghiem_Manual_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietKetQuaXetNghiem_Manual] PRIMARY KEY CLUSTERED 
(
	[ChiTietKetQuaXetNghiem_ManualGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietKetQuaXetNghiem_Manual]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietKetQuaXetNghiem_Manual_KetQuaXetNghiem_Manual] FOREIGN KEY([KetQuaXetNghiem_ManualGUID])
REFERENCES [dbo].[KetQuaXetNghiem_Manual] ([KetQuaXetNghiemManualGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietKetQuaXetNghiem_Manual]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietKetQuaXetNghiem_Manual_XetNghiem_Manual] FOREIGN KEY([XetNghiem_ManualGUID])
REFERENCES [dbo].[XetNghiem_Manual] ([XetNghiem_ManualGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[KetQuaXetNghiem_ManualView]    Script Date: 04/26/2012 10:47:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[KetQuaXetNghiem_ManualView]
AS
SELECT     dbo.KetQuaXetNghiem_Manual.KetQuaXetNghiemManualGUID, dbo.KetQuaXetNghiem_Manual.NgayXN, dbo.KetQuaXetNghiem_Manual.PatientGUID, 
                      dbo.KetQuaXetNghiem_Manual.CreatedDate, dbo.KetQuaXetNghiem_Manual.CreatedBy, dbo.KetQuaXetNghiem_Manual.UpdatedDate, 
                      dbo.KetQuaXetNghiem_Manual.UpdatedBy, dbo.KetQuaXetNghiem_Manual.DeletedDate, dbo.KetQuaXetNghiem_Manual.DeletedBy, 
                      dbo.KetQuaXetNghiem_Manual.Status, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.Archived
FROM         dbo.KetQuaXetNghiem_Manual LEFT OUTER JOIN
                      dbo.PatientView ON dbo.KetQuaXetNghiem_Manual.PatientGUID = dbo.PatientView.PatientGUID

GO
/****** Object:  View [dbo].[ChiTietKetQuaXetNghiem_ManualView]    Script Date: 04/26/2012 10:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietKetQuaXetNghiem_ManualView]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_Manual.ChiTietKetQuaXetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.KetQuaXetNghiem_ManualGUID, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.TestResult, dbo.ChiTietKetQuaXetNghiem_Manual.TinhTrang, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.CreatedDate, dbo.ChiTietKetQuaXetNghiem_Manual.CreatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.DeletedDate, dbo.ChiTietKetQuaXetNghiem_Manual.DeletedBy, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.Status, dbo.XetNghiem_Manual.XetNghiem_ManualGUID AS Expr1, dbo.XetNghiem_Manual.TenXetNghiem, 
                      dbo.XetNghiem_Manual.Fullname, dbo.XetNghiem_Manual.Type, dbo.XetNghiem_Manual.Status AS XetNghiemStatus
FROM         dbo.ChiTietKetQuaXetNghiem_Manual INNER JOIN
                      dbo.XetNghiem_Manual ON dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID = dbo.XetNghiem_Manual.XetNghiem_ManualGUID

GO
