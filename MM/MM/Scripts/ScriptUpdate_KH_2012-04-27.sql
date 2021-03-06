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
/****** Object:  View [dbo].[KetQuaXetNghiem_ManualView]    Script Date: 05/01/2012 12:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[KetQuaXetNghiem_ManualView]
AS
SELECT     dbo.KetQuaXetNghiem_Manual.KetQuaXetNghiemManualGUID, dbo.KetQuaXetNghiem_Manual.NgayXN, dbo.KetQuaXetNghiem_Manual.PatientGUID, 
                      dbo.KetQuaXetNghiem_Manual.CreatedDate, dbo.KetQuaXetNghiem_Manual.CreatedBy, dbo.KetQuaXetNghiem_Manual.UpdatedDate, 
                      dbo.KetQuaXetNghiem_Manual.UpdatedBy, dbo.KetQuaXetNghiem_Manual.DeletedDate, dbo.KetQuaXetNghiem_Manual.DeletedBy, 
                      dbo.KetQuaXetNghiem_Manual.Status, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.Archived, 
                      dbo.PatientView.FileNum
FROM         dbo.KetQuaXetNghiem_Manual LEFT OUTER JOIN
                      dbo.PatientView ON dbo.KetQuaXetNghiem_Manual.PatientGUID = dbo.PatientView.PatientGUID

GO
/****** Object:  View [dbo].[ChiTietKetQuaXetNghiem_ManualView]    Script Date: 05/01/2012 11:12:50 ******/
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
                      dbo.ChiTietKetQuaXetNghiem_Manual.Status, dbo.XetNghiem_Manual.TenXetNghiem, dbo.XetNghiem_Manual.Fullname, dbo.XetNghiem_Manual.Type, 
                      dbo.XetNghiem_Manual.Status AS XetNghiemStatus
FROM         dbo.ChiTietKetQuaXetNghiem_Manual INNER JOIN
                      dbo.XetNghiem_Manual ON dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID = dbo.XetNghiem_Manual.XetNghiem_ManualGUID

GO
ALTER TABLE XetNghiem_CellDyn3200 
ADD [Order] [int] NULL,
	[GroupID] [int] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_CellDyn3200View]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_CellDyn3200.ChiTietKQXN_CellDyn3200GUID, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TenXetNghiem, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TestResult, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TestPercent, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TinhTrang, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.CreatedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.CreatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.UpdatedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.UpdatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DeletedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DeletedBy, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.Status, 
                      dbo.XetNghiem_CellDyn3200.FullName, dbo.XetNghiem_CellDyn3200.Type, dbo.XetNghiem_CellDyn3200.FromValue, dbo.XetNghiem_CellDyn3200.ToValue, 
                      dbo.XetNghiem_CellDyn3200.ToPercent, dbo.XetNghiem_CellDyn3200.DonVi, dbo.XetNghiem_CellDyn3200.FromPercent, dbo.XetNghiem_CellDyn3200.[Order], 
                      dbo.XetNghiem_CellDyn3200.GroupID
FROM         dbo.ChiTietKetQuaXetNghiem_CellDyn3200 LEFT OUTER JOIN
                      dbo.XetNghiem_CellDyn3200 ON dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TenXetNghiem = dbo.XetNghiem_CellDyn3200.TenXetNghiem
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  Table [dbo].[XetNghiem_CellDyn3200]    Script Date: 04/26/2012 09:39:54 ******/
DELETE FROM [dbo].[XetNghiem_CellDyn3200]
GO
/****** Object:  Table [dbo].[XetNghiem_CellDyn3200]    Script Date: 04/23/2012 15:24:37 ******/
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'cce92e37-a209-467e-a1e3-124c25e0b99b', N'LYM', N'LYM', N'Haematology', 0.6, 5.5, 10, 50, N'%L', 1, 3)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'66816f56-00e2-4575-8729-2620a8a6a5b0', N'MCHC', N'MCHC', N'Haematology', 29, 38.5, NULL, NULL, N'g/dL', 2, 12)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'76de0acc-d79d-47eb-a7fa-27a36d1677a1', N'BASO', N'BASO', N'Haematology', 0, 0.2, 0, 2.5, N'%B', 1, 6)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'd169a536-364e-41b3-a1fa-2a99bcc357a5', N'PCT', N'PCT', N'Haematology', 0, 9.99, NULL, NULL, N'%', 3, 16)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'f9055cb1-538e-4ab9-8e51-2cc9fe15f851', N'MONO', N'MONO', N'Haematology', 0, 0.9, 0, 12, N'%M', 1, 4)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'24a66cf6-dd1d-4574-a2de-3a5dabcc4637', N'PLT', N'PLT', N'Haematology', 140, 440, NULL, NULL, N'M/µL', 3, 14)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'f9d6fecb-50c6-436b-bbeb-612da6418162', N'HGB', N'HGB', N'Haematology', 12, 18, NULL, NULL, N'g/dL', 2, 8)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'6326b670-6526-412b-b848-684c460ba420', N'MPV', N'MPV', N'Haematology', 6, 12, NULL, NULL, N'fL', 3, 15)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'c4023eed-22aa-48c4-99d3-853249949170', N'MCV', N'MCV', N'Haematology', 80, 97, NULL, NULL, N'fL', 2, 10)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'59da534c-9480-49da-8d9f-89371e374453', N'RDW', N'RDW', N'Haematology', 9, 15, NULL, NULL, NULL, 2, 13)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'8771268f-b601-4b98-996b-8c2c87be0970', N'RBC', N'RBC', N'Haematology', 3.8, 6, NULL, NULL, N'M/µL', 2, 7)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'4e558051-4d9b-4f6e-8f38-919f2e6056af', N'EOS', N'EOS', N'Haematology', 0, 0.7, 0, 7, N'%E', 1, 5)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'23a4994e-bdf2-4f3c-97a2-9331183c66a7', N'HCT', N'HCT', N'Haematology', 36, 50, NULL, NULL, N'%', 2, 9)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'4f15d385-1c83-4cf0-a36a-d01c833aa4bd', N'WBC', N'WBC', N'Haematology', 4, 10, NULL, NULL, N'K/µL', 1, 1)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'ef1097e0-88a7-48ba-98dd-dd779a276695', N'NEU', N'NEU', N'Haematology', 2, 6.9, 37, 80, N'%N', 1, 2)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'666f272a-5c20-4f7a-9d44-e8390e99bec4', N'PDW', N'PDW', N'Haematology', 0, 99.9, NULL, NULL, NULL, 3, 17)
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [GroupID], [Order]) VALUES (N'08bea725-29a8-4e40-9df9-f876d4001784', N'MCH', N'MCH', N'Haematology', 26, 35, NULL, NULL, N'pg', 2, 11)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_CellDyn3200View]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_CellDyn3200.ChiTietKQXN_CellDyn3200GUID, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TenXetNghiem, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TestResult, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TestPercent, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TinhTrang, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.CreatedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.CreatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.UpdatedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.UpdatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DeletedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DeletedBy, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.Status, 
                      dbo.XetNghiem_CellDyn3200.FullName, dbo.XetNghiem_CellDyn3200.Type, dbo.XetNghiem_CellDyn3200.FromValue, dbo.XetNghiem_CellDyn3200.ToValue, 
                      dbo.XetNghiem_CellDyn3200.ToPercent, dbo.XetNghiem_CellDyn3200.DonVi, dbo.XetNghiem_CellDyn3200.FromPercent, dbo.XetNghiem_CellDyn3200.[Order], 
                      dbo.XetNghiem_CellDyn3200.GroupID, dbo.KetQuaXetNghiem_CellDyn3200.NgayXN, dbo.KetQuaXetNghiem_CellDyn3200.PatientGUID, 
                      dbo.KetQuaXetNghiem_CellDyn3200.Status AS KQXNStatus
FROM         dbo.ChiTietKetQuaXetNghiem_CellDyn3200 INNER JOIN
                      dbo.KetQuaXetNghiem_CellDyn3200 ON 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID = dbo.KetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID LEFT OUTER JOIN
                      dbo.XetNghiem_CellDyn3200 ON dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TenXetNghiem = dbo.XetNghiem_CellDyn3200.TenXetNghiem
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_Hitachi917View]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_Hitachi917.ChiTietKQXN_Hitachi917GUID, dbo.ChiTietKetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.TestNum, dbo.ChiTietKetQuaXetNghiem_Hitachi917.TestResult, dbo.ChiTietKetQuaXetNghiem_Hitachi917.AlarmCode, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.TinhTrang, dbo.ChiTietKetQuaXetNghiem_Hitachi917.CreatedDate, dbo.ChiTietKetQuaXetNghiem_Hitachi917.CreatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.UpdatedDate, dbo.ChiTietKetQuaXetNghiem_Hitachi917.UpdatedBy, dbo.ChiTietKetQuaXetNghiem_Hitachi917.DeletedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.DeletedBy, dbo.ChiTietKetQuaXetNghiem_Hitachi917.Status, dbo.XetNghiem_Hitachi917.TenXetNghiem, 
                      dbo.XetNghiem_Hitachi917.Fullname, dbo.XetNghiem_Hitachi917.Type, dbo.XetNghiem_Hitachi917.XetNghiemGUID, dbo.KetQuaXetNghiem_Hitachi917.PatientGUID, 
                      dbo.KetQuaXetNghiem_Hitachi917.NgayXN, dbo.KetQuaXetNghiem_Hitachi917.Status AS KQXNStatus
FROM         dbo.ChiTietKetQuaXetNghiem_Hitachi917 INNER JOIN
                      dbo.KetQuaXetNghiem_Hitachi917 ON 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID = dbo.KetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID LEFT OUTER JOIN
                      dbo.XetNghiem_Hitachi917 ON dbo.ChiTietKetQuaXetNghiem_Hitachi917.TestNum = dbo.XetNghiem_Hitachi917.TestNum
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_ManualView]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_Manual.ChiTietKetQuaXetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.KetQuaXetNghiem_ManualGUID, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.TestResult, dbo.ChiTietKetQuaXetNghiem_Manual.TinhTrang, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.CreatedDate, dbo.ChiTietKetQuaXetNghiem_Manual.CreatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.DeletedDate, dbo.ChiTietKetQuaXetNghiem_Manual.DeletedBy, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.Status, dbo.XetNghiem_Manual.TenXetNghiem, dbo.XetNghiem_Manual.Fullname, dbo.XetNghiem_Manual.Type, 
                      dbo.XetNghiem_Manual.Status AS XetNghiemStatus, dbo.KetQuaXetNghiem_Manual.NgayXN, dbo.KetQuaXetNghiem_Manual.PatientGUID, 
                      dbo.KetQuaXetNghiem_Manual.Status AS KQXNStatus
FROM         dbo.ChiTietKetQuaXetNghiem_Manual INNER JOIN
                      dbo.XetNghiem_Manual ON dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID = dbo.XetNghiem_Manual.XetNghiem_ManualGUID INNER JOIN
                      dbo.KetQuaXetNghiem_Manual ON 
                      dbo.ChiTietKetQuaXetNghiem_Manual.KetQuaXetNghiem_ManualGUID = dbo.KetQuaXetNghiem_Manual.KetQuaXetNghiemManualGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
