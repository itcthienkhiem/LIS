USE MM
GO
/****** Object:  Table [dbo].[CongTacNgoaiGio]    Script Date: 10/08/2012 09:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CongTacNgoaiGio](
	[CongTacNgoaiGioGUID] [uniqueidentifier] NOT NULL,
	[Ngay] [datetime] NOT NULL,
	[DocStaffGUID] [uniqueidentifier] NOT NULL,
	[MucDich] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GioVao] [datetime] NULL,
	[GioRa] [datetime] NULL,
	[KetQuaDanhGia] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NguoiDeXuatGUID] [uniqueidentifier] NOT NULL,
	[GhiChu] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_CongTacNgoaiGio_Status_1]  DEFAULT ((0)),
 CONSTRAINT [PK_CongTacNgoaiGio] PRIMARY KEY CLUSTERED 
(
	[CongTacNgoaiGioGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CongTacNgoaiGio]  WITH CHECK ADD  CONSTRAINT [FK_CongTacNgoaiGio_DocStaff] FOREIGN KEY([DocStaffGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CongTacNgoaiGio] CHECK CONSTRAINT [FK_CongTacNgoaiGio_DocStaff]
GO
/****** Object:  View [dbo].[CongTacNgoaiGioView]    Script Date: 10/08/2012 09:02:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CongTacNgoaiGioView]
AS
SELECT     dbo.CongTacNgoaiGio.CongTacNgoaiGioGUID, dbo.CongTacNgoaiGio.Ngay, dbo.CongTacNgoaiGio.DocStaffGUID, dbo.CongTacNgoaiGio.MucDich, 
                      dbo.CongTacNgoaiGio.GioVao, dbo.CongTacNgoaiGio.GioRa, dbo.CongTacNgoaiGio.KetQuaDanhGia, dbo.CongTacNgoaiGio.NguoiDeXuatGUID, 
                      dbo.CongTacNgoaiGio.GhiChu, dbo.CongTacNgoaiGio.CreatedDate, dbo.CongTacNgoaiGio.CreatedBy, dbo.CongTacNgoaiGio.UpdatedDate, 
                      dbo.CongTacNgoaiGio.UpdatedBy, dbo.CongTacNgoaiGio.DeletedDate, dbo.CongTacNgoaiGio.DeletedBy, dbo.CongTacNgoaiGio.Status, 
                      dbo.DocStaffView.FullName, DocStaffView_1.FullName AS TenNguoiDeXuat
FROM         dbo.CongTacNgoaiGio INNER JOIN
                      dbo.DocStaffView ON dbo.CongTacNgoaiGio.DocStaffGUID = dbo.DocStaffView.DocStaffGUID INNER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.CongTacNgoaiGio.NguoiDeXuatGUID = DocStaffView_1.DocStaffGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'f42980b8-38db-4e83-8057-dd69e589e19c', N'CongTacNgoaiGio', N'Công tác ngoài giờ')
GO






