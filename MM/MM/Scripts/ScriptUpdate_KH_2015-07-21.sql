USE [MM]
GO
/****** Object:  Table [dbo].[GhiNhanTraNo]    Script Date: 07/22/2015 09:16:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GhiNhanTraNo](
	[GhiNhanTraNoGUID] [uniqueidentifier] NOT NULL,
	[MaPhieuThuGUID] [uniqueidentifier] NOT NULL,
	[NgayTra] [datetime] NOT NULL,
	[SoTien] [float] NOT NULL,
	[LoaiPT] [int] NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_GhiNhanTraNo] PRIMARY KEY CLUSTERED 
(
	[GhiNhanTraNoGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GhiNhanTraNo] ADD  CONSTRAINT [DF_GhiNhanTraNo_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  View [dbo].[GhiNhanTraNoView]    Script Date: 07/22/2015 16:23:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GhiNhanTraNoView]
AS
SELECT     dbo.GhiNhanTraNo.GhiNhanTraNoGUID, dbo.GhiNhanTraNo.MaPhieuThuGUID, dbo.GhiNhanTraNo.NgayTra, dbo.GhiNhanTraNo.SoTien, dbo.GhiNhanTraNo.LoaiPT, 
                      dbo.GhiNhanTraNo.CreatedDate, dbo.GhiNhanTraNo.CreatedBy, dbo.GhiNhanTraNo.UpdatedDate, dbo.GhiNhanTraNo.UpdatedBy, dbo.GhiNhanTraNo.DeletedDate, 
                      dbo.GhiNhanTraNo.DeletedBy, dbo.GhiNhanTraNo.Status, 
                      CASE dbo.GhiNhanTraNo.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat, 
                      CASE dbo.GhiNhanTraNo.CreatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiTao, 
                      dbo.GhiNhanTraNo.GhiChu
FROM         dbo.GhiNhanTraNo LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.GhiNhanTraNo.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.GhiNhanTraNo.CreatedBy = dbo.DocStaffView.DocStaffGUID

GO

INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'0ac61d03-46fb-4aff-a003-0646761f7b71', N'GhiNhanTraNo', N'Ghi nhận trả nợ')

GO
ALTER TABLE [Services]
ADD [Discount] [float] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ServiceView]
AS
SELECT     ServiceGUID, Code, Name, EnglishName, Price, Description, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, Type, StaffType, 
                      CASE ISNULL(StaffType, 9) 
                      WHEN 0 THEN N'Bác sĩ' WHEN 1 THEN N'Điều dưỡng' WHEN 2 THEN N'Lễ tân' WHEN 4 THEN N'Admin' WHEN 5 THEN N'Xét nghiệm' WHEN 6 THEN N'Thư ký y khoa' WHEN
                       7 THEN N'Sale' WHEN 8 THEN N'Kế toán' WHEN 9 THEN N'' WHEN 10 THEN N'Bác sĩ siêu âm' WHEN 11 THEN N'Bác sĩ ngoại tổng quát' WHEN 12 THEN N'Bác sĩ nội tổng quát'
                       WHEN 13 THEN N'Bác sĩ phụ khoa' END AS StaffTypeStr, Discount
FROM         dbo.Services
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

