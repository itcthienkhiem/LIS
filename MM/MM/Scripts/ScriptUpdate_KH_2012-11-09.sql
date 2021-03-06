USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'36ca7275-758d-4298-ba33-56c38a6c4a4d', N'KetQuaCanLamSang', N'Kết quả cận lâm sàng')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'efc08dc0-ad33-423c-a0cc-f68198534798', N'TaoHoSo', N'Tạo hồ sơ')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'da628935-c191-42db-98a2-3ed451c30887', N'UploadHoSo', N'Upload hồ sơ')
GO
ALTER TABLE UserGroup_Permission
ADD [IsCreateReport] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsCreateReport]  DEFAULT ((0)),
	[IsUpload] [bit] NOT NULL CONSTRAINT [DF_UserGroup_Permission_IsUpload]  DEFAULT ((0))
Go
UPDATE [Function]
SET FunctionName = N'Nhân viên'
WHERE FunctionCode = 'DocStaff'
GO
GO
/****** Object:  Table [dbo].[KetQuaCanLamSang]    Script Date: 11/09/2012 08:57:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaCanLamSang](
	[KetQuaCanLamSangGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[BacSiThucHienGUID] [uniqueidentifier] NULL,
	[NgayKham] [datetime] NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsNormalOrNegative] [bit] NOT NULL,
	[Normal] [bit] NOT NULL,
	[Abnormal] [bit] NOT NULL,
	[Negative] [bit] NOT NULL,
	[Positive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KetQuaCanLamSang_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KetQuaCanLamSang] PRIMARY KEY CLUSTERED 
(
	[KetQuaCanLamSangGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[KetQuaCanLamSang]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaCanLamSang_DocStaff] FOREIGN KEY([BacSiThucHienGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
GO
ALTER TABLE [dbo].[KetQuaCanLamSang] CHECK CONSTRAINT [FK_KetQuaCanLamSang_DocStaff]
GO
ALTER TABLE [dbo].[KetQuaCanLamSang]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaCanLamSang_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KetQuaCanLamSang] CHECK CONSTRAINT [FK_KetQuaCanLamSang_Patient]
GO
ALTER TABLE [dbo].[KetQuaCanLamSang]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaCanLamSang_Services] FOREIGN KEY([ServiceGUID])
REFERENCES [dbo].[Services] ([ServiceGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KetQuaCanLamSang] CHECK CONSTRAINT [FK_KetQuaCanLamSang_Services]
GO
/****** Object:  View [dbo].[KetQuaCanLamSangView]    Script Date: 11/09/2012 08:58:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[KetQuaCanLamSangView]
AS
SELECT     dbo.KetQuaCanLamSang.KetQuaCanLamSangGUID, dbo.KetQuaCanLamSang.PatientGUID, dbo.KetQuaCanLamSang.ServiceGUID, 
                      dbo.KetQuaCanLamSang.BacSiThucHienGUID, dbo.KetQuaCanLamSang.NgayKham, dbo.KetQuaCanLamSang.Note, 
                      dbo.KetQuaCanLamSang.IsNormalOrNegative, dbo.KetQuaCanLamSang.Normal, dbo.KetQuaCanLamSang.Abnormal, dbo.KetQuaCanLamSang.Positive,
                       dbo.KetQuaCanLamSang.CreatedDate, dbo.KetQuaCanLamSang.CreatedBy, dbo.KetQuaCanLamSang.UpdatedDate, 
                      dbo.KetQuaCanLamSang.UpdatedBy, dbo.KetQuaCanLamSang.DeletedDate, dbo.KetQuaCanLamSang.DeletedBy, 
                      dbo.KetQuaCanLamSang.Status AS KetQuaCanLamSangStatus, dbo.Services.Code, dbo.Services.Name, dbo.Services.EnglishName, 
                      dbo.Services.Type, dbo.Services.Price, dbo.Services.Status AS ServiceStatus, dbo.DocStaffView.AvailableToWork, dbo.DocStaffView.FullName, 
                      dbo.KetQuaCanLamSang.Negative
FROM         dbo.KetQuaCanLamSang INNER JOIN
                      dbo.Services ON dbo.KetQuaCanLamSang.ServiceGUID = dbo.Services.ServiceGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.KetQuaCanLamSang.BacSiThucHienGUID = dbo.DocStaffView.DocStaffGUID

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[UserGroup_PermissionView]
AS
SELECT     dbo.[Function].FunctionCode, dbo.[Function].FunctionName, dbo.UserGroup_Permission.UserGroup_PermissionGUID, 
                      dbo.UserGroup_Permission.UserGroupGUID, dbo.UserGroup_Permission.FunctionGUID, dbo.UserGroup_Permission.IsView, 
                      dbo.UserGroup_Permission.IsAdd, dbo.UserGroup_Permission.IsEdit, dbo.UserGroup_Permission.IsDelete, dbo.UserGroup_Permission.IsPrint, 
                      dbo.UserGroup_Permission.IsImport, dbo.UserGroup_Permission.IsExport, dbo.UserGroup_Permission.IsConfirm, dbo.UserGroup_Permission.IsLock, 
                      dbo.UserGroup_Permission.IsExportAll, dbo.UserGroup_Permission.IsCreateReport, dbo.UserGroup_Permission.IsUpload, 
                      dbo.UserGroup_Permission.CreatedDate, dbo.UserGroup_Permission.CreatedBy, dbo.UserGroup_Permission.UpdatedDate, 
                      dbo.UserGroup_Permission.UpdatedBy, dbo.UserGroup_Permission.DeletedDate, dbo.UserGroup_Permission.DeletedBy
FROM         dbo.UserGroup_Permission INNER JOIN
                      dbo.[Function] ON dbo.UserGroup_Permission.FunctionGUID = dbo.[Function].FunctionGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO




















