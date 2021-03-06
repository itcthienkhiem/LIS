USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'c984988e-6561-457a-bbc2-90bf8105e1cf', N'NhomNguoiSuDung', N'Nhóm người sử dụng')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'78340266-c237-4c19-8853-10bcc831728d', N'NguoiSuDung', N'Người sử dụng')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'fa350401-2afd-4986-9b2d-c0f53902266f', N'BaoCaoCapCuuHetHan', N'Báo cáo cấp cứu hết hạn')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'fa350401-2afd-4986-9b2d-c0f53902f66f', N'BaoCaoTonKhoCapCuu', N'Báo cáo tồn kho cấp cứu')
GO
DELETE [Function]
WHERE FunctionCode = N'ThuocTonKhoTheoKhoangThoiGian'
GO
DELETE [Function]
WHERE FunctionCode = N'Permission'
GO
UPDATE [Function]
SET FunctionName = N'Lịch hẹn'
WHERE FunctionCode = N'Booking'
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 11/08/2012 08:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[UserGroupGUID] [uniqueidentifier] NOT NULL,
	[GroupName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_UserGroup_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[UserGroupGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup_Logon]    Script Date: 11/08/2012 08:56:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup_Logon](
	[UserGroup_LogonGUID] [uniqueidentifier] NOT NULL,
	[LogonGUID] [uniqueidentifier] NOT NULL,
	[UserGroupGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserGroup_Logon] PRIMARY KEY CLUSTERED 
(
	[UserGroup_LogonGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserGroup_Logon]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Logon_Logon] FOREIGN KEY([LogonGUID])
REFERENCES [dbo].[Logon] ([LogonGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroup_Logon] CHECK CONSTRAINT [FK_UserGroup_Logon_Logon]
GO
ALTER TABLE [dbo].[UserGroup_Logon]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Logon_UserGroup] FOREIGN KEY([UserGroupGUID])
REFERENCES [dbo].[UserGroup] ([UserGroupGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroup_Logon] CHECK CONSTRAINT [FK_UserGroup_Logon_UserGroup]
GO
/****** Object:  Table [dbo].[UserGroup_Permission]    Script Date: 11/08/2012 08:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup_Permission](
	[UserGroup_PermissionGUID] [uniqueidentifier] NOT NULL,
	[UserGroupGUID] [uniqueidentifier] NOT NULL,
	[FunctionGUID] [uniqueidentifier] NOT NULL,
	[IsView] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsView]  DEFAULT ((0)),
	[IsAdd] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsAdd]  DEFAULT ((0)),
	[IsEdit] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsEdit]  DEFAULT ((0)),
	[IsDelete] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsDelete]  DEFAULT ((0)),
	[IsPrint] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsPrint]  DEFAULT ((0)),
	[IsImport] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsImport]  DEFAULT ((0)),
	[IsExport] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsExport]  DEFAULT ((0)),
	[IsConfirm] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsConfirm]  DEFAULT ((0)),
	[IsLock] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsLock]  DEFAULT ((0)),
	[IsExportAll] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsExportAll]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_UserGroup_Permission] PRIMARY KEY CLUSTERED 
(
	[UserGroup_PermissionGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserGroup_Permission]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Permission_Function] FOREIGN KEY([FunctionGUID])
REFERENCES [dbo].[Function] ([FunctionGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroup_Permission] CHECK CONSTRAINT [FK_UserGroup_Permission_Function]
GO
ALTER TABLE [dbo].[UserGroup_Permission]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Permission_UserGroup] FOREIGN KEY([UserGroupGUID])
REFERENCES [dbo].[UserGroup] ([UserGroupGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroup_Permission] CHECK CONSTRAINT [FK_UserGroup_Permission_UserGroup]
GO
/****** Object:  View [dbo].[UserGroup_PermissionView]    Script Date: 11/08/2012 08:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserGroup_PermissionView]
AS
SELECT     dbo.UserGroup_Permission.*, dbo.[Function].FunctionCode, dbo.[Function].FunctionName
FROM         dbo.UserGroup_Permission INNER JOIN
                      dbo.[Function] ON dbo.UserGroup_Permission.FunctionGUID = dbo.[Function].FunctionGUID

GO
CREATE STATISTICS [_dta_stat_225487932_24_26] ON [dbo].[Contact]([CreatedDate], [UpdatedDate])
GO
CREATE STATISTICS [_dta_stat_225487932_28_24_26_1] ON [dbo].[Contact]([DeletedDate], [CreatedDate], [UpdatedDate], [ContactGUID])
GO
CREATE STATISTICS [_dta_stat_225487932_1_26_28] ON [dbo].[Contact]([ContactGUID], [UpdatedDate], [DeletedDate])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Contact_12_225487932__K26_K28_K24_K1] ON [dbo].[Contact] 
(
	[UpdatedDate] ASC,
	[DeletedDate] ASC,
	[CreatedDate] ASC,
	[ContactGUID] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_225487932_1_24_26] ON [dbo].[Contact]([ContactGUID], [CreatedDate], [UpdatedDate])



















