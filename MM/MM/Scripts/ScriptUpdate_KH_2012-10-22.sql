USE MM
GO
/****** Object:  Table [dbo].[ThongBao]    Script Date: 10/22/2012 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongBao](
	[ThongBaoGUID] [uniqueidentifier] NOT NULL,
	[TenThongBao] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ThongBaoBuff] [image] NULL,
	[NgayDuyet1] [datetime] NULL,
	[ThongBaoBuff1] [image] NULL,
	[NgayDuyet2] [datetime] NULL,
	[ThongBaoBuff2] [image] NULL,
	[NgayDuyet3] [datetime] NULL,
	[ThongBaoBuff3] [image] NULL,
	[Path] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GhiChu] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ThongBao_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ThongBao] PRIMARY KEY CLUSTERED 
(
	[ThongBaoGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ThongBaoView]    Script Date: 10/25/2012 21:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ThongBaoView]
AS
SELECT     dbo.ThongBao.ThongBaoGUID, dbo.ThongBao.TenThongBao, dbo.ThongBao.ThongBaoBuff, dbo.ThongBao.NgayDuyet1, dbo.ThongBao.ThongBaoBuff1, 
                      dbo.ThongBao.NgayDuyet2, dbo.ThongBao.ThongBaoBuff2, dbo.ThongBao.NgayDuyet3, dbo.ThongBao.ThongBaoBuff3, dbo.ThongBao.CreatedDate, 
                      dbo.ThongBao.CreatedBy, dbo.ThongBao.UpdatedDate, dbo.ThongBao.UpdatedBy, dbo.ThongBao.DeletedDate, dbo.ThongBao.DeletedBy, dbo.ThongBao.Status, 
                      dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, 
                      CASE dbo.ThongBao.CreatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView.FullName END AS FullName, dbo.ThongBao.Path,
                       dbo.ThongBao.GhiChu
FROM         dbo.ThongBao LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.ThongBao.CreatedBy = dbo.DocStaffView.DocStaffGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'800cc1a4-a838-4331-9f4d-31936f4b2e43', N'ThongBao', N'Thông báo')











