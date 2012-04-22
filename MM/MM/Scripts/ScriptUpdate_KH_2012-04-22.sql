USE MM
GO
ALTER TABLE KetQuaXetNghiem_Hitachi917 
ADD [Sex] [int] NULL,
	[Age] [int] NULL,
	[AgeUnit] [int] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaXetNghiem_Hitachi917View]
AS
SELECT     dbo.KetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID, dbo.KetQuaXetNghiem_Hitachi917.IDNum, dbo.KetQuaXetNghiem_Hitachi917.PatientGUID, 
                      dbo.KetQuaXetNghiem_Hitachi917.NgayXN, dbo.KetQuaXetNghiem_Hitachi917.OperationID, dbo.KetQuaXetNghiem_Hitachi917.CreatedDate, 
                      dbo.KetQuaXetNghiem_Hitachi917.CreatedBy, dbo.KetQuaXetNghiem_Hitachi917.UpdatedDate, dbo.KetQuaXetNghiem_Hitachi917.UpdatedBy, 
                      dbo.KetQuaXetNghiem_Hitachi917.DeletedDate, dbo.KetQuaXetNghiem_Hitachi917.DeletedBy, dbo.KetQuaXetNghiem_Hitachi917.Status, dbo.PatientView.FullName, 
                      dbo.PatientView.DobStr, dbo.PatientView.FileNum, dbo.PatientView.GenderAsStr, dbo.PatientView.Archived, dbo.KetQuaXetNghiem_Hitachi917.Sex, 
                      dbo.KetQuaXetNghiem_Hitachi917.Age, dbo.KetQuaXetNghiem_Hitachi917.AgeUnit
FROM         dbo.KetQuaXetNghiem_Hitachi917 LEFT OUTER JOIN
                      dbo.PatientView ON dbo.KetQuaXetNghiem_Hitachi917.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  Table [dbo].[XetNghiem_CellDyn3200]    Script Date: 04/22/2012 10:09:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XetNghiem_CellDyn3200](
	[XetNghiemGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_XetNghiem_CellDyn3200_XetNghiemGUID]  DEFAULT (newid()),
	[TenXetNghiem] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FullName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Type] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FromValue] [float] NULL,
	[ToValue] [float] NULL,
	[FromPercent] [float] NULL,
	[ToPercent] [float] NULL,
	[DonVi] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_XetNghiem_CellDyn3200] PRIMARY KEY CLUSTERED 
(
	[XetNghiemGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KetQuaXetNghiem_CellDyn3200]    Script Date: 04/22/2012 11:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaXetNghiem_CellDyn3200](
	[KQXN_CellDyn3200GUID] [uniqueidentifier] NOT NULL,
	[MessageType] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InstrumentType] [nvarchar](7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SerialNum] [nvarchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SequenceNum] [int] NULL,
	[SpareField] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SpecimenType] [int] NULL,
	[SpecimenID] [nvarchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SpecimenName] [nvarchar](28) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PatientID] [nvarchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SpecimenSex] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SpecimenDOB] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DrName] [nvarchar](22) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperatorID] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayXN] [datetime] NULL,
	[CollectionDate] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CollectionTime] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comment] [nvarchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PatientGUID] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KetQuaXetNghiem_CellDyn3200_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KetQuaXetNghiem_CellDyn3200] PRIMARY KEY CLUSTERED 
(
	[KQXN_CellDyn3200GUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietKetQuaXetNghiem_CellDyn3200]    Script Date: 04/22/2012 11:23:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietKetQuaXetNghiem_CellDyn3200](
	[ChiTietKQXN_CellDyn3200GUID] [uniqueidentifier] NOT NULL,
	[KQXN_CellDyn3200GUID] [uniqueidentifier] NOT NULL,
	[TenXetNghiem] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TestResult] [float] NULL,
	[TestPercent] [float] NULL,
	[TinhTrang] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietKetQuaXetNghiem_CellDyn3200_TinhTrang]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietKetQuaXetNghiem_CellDyn3200_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietKetQuaXetNghiem_CellDyn3200] PRIMARY KEY CLUSTERED 
(
	[ChiTietKQXN_CellDyn3200GUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[ChiTietKetQuaXetNghiem_CellDyn3200]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietKetQuaXetNghiem_CellDyn3200_KetQuaXetNghiem_CellDyn3200] FOREIGN KEY([KQXN_CellDyn3200GUID])
REFERENCES [dbo].[KetQuaXetNghiem_CellDyn3200] ([KQXN_CellDyn3200GUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[KetQuaXetNghiem_CellDyn3200View]    Script Date: 04/22/2012 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[KetQuaXetNghiem_CellDyn3200View]
AS
SELECT     dbo.KetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID, dbo.KetQuaXetNghiem_CellDyn3200.MessageType, 
                      dbo.KetQuaXetNghiem_CellDyn3200.InstrumentType, dbo.KetQuaXetNghiem_CellDyn3200.SerialNum, dbo.KetQuaXetNghiem_CellDyn3200.SequenceNum, 
                      dbo.KetQuaXetNghiem_CellDyn3200.SpareField, dbo.KetQuaXetNghiem_CellDyn3200.SpecimenType, dbo.KetQuaXetNghiem_CellDyn3200.SpecimenID, 
                      dbo.KetQuaXetNghiem_CellDyn3200.SpecimenName, dbo.KetQuaXetNghiem_CellDyn3200.PatientID, dbo.KetQuaXetNghiem_CellDyn3200.SpecimenSex, 
                      dbo.KetQuaXetNghiem_CellDyn3200.SpecimenDOB, dbo.KetQuaXetNghiem_CellDyn3200.DrName, dbo.KetQuaXetNghiem_CellDyn3200.OperatorID, 
                      dbo.KetQuaXetNghiem_CellDyn3200.NgayXN, dbo.KetQuaXetNghiem_CellDyn3200.CollectionDate, dbo.KetQuaXetNghiem_CellDyn3200.CollectionTime, 
                      dbo.KetQuaXetNghiem_CellDyn3200.Comment, dbo.KetQuaXetNghiem_CellDyn3200.PatientGUID, dbo.KetQuaXetNghiem_CellDyn3200.CreatedDate, 
                      dbo.KetQuaXetNghiem_CellDyn3200.CreatedBy, dbo.KetQuaXetNghiem_CellDyn3200.UpdatedDate, dbo.KetQuaXetNghiem_CellDyn3200.UpdatedBy, 
                      dbo.KetQuaXetNghiem_CellDyn3200.DeletedDate, dbo.KetQuaXetNghiem_CellDyn3200.DeletedBy, dbo.KetQuaXetNghiem_CellDyn3200.Status, 
                      dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.FileNum, dbo.PatientView.Archived
FROM         dbo.KetQuaXetNghiem_CellDyn3200 LEFT OUTER JOIN
                      dbo.PatientView ON dbo.KetQuaXetNghiem_CellDyn3200.PatientGUID = dbo.PatientView.PatientGUID

GO
/****** Object:  View [dbo].[ChiTietKetQuaXetNghiem_CellDynView]    Script Date: 04/22/2012 11:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietKetQuaXetNghiem_CellDyn3200View]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_CellDyn3200.*, dbo.XetNghiem_CellDyn3200.FullName, dbo.XetNghiem_CellDyn3200.Type, dbo.XetNghiem_CellDyn3200.FromValue, 
                      dbo.XetNghiem_CellDyn3200.ToValue, dbo.XetNghiem_CellDyn3200.ToPercent, dbo.XetNghiem_CellDyn3200.DonVi, dbo.XetNghiem_CellDyn3200.FromPercent
FROM         dbo.ChiTietKetQuaXetNghiem_CellDyn3200 LEFT OUTER JOIN
                      dbo.XetNghiem_CellDyn3200 ON dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TenXetNghiem = dbo.XetNghiem_CellDyn3200.TenXetNghiem

GO
