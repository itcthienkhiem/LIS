USE MM
GO
DROP TABLE [dbo].[CongTacNgoaiGio]
GO
DROP VIEW [dbo].[CongTacNgoaiGioView]
GO
/****** Object:  Table [dbo].[CongTacNgoaiGio]    Script Date: 10/11/2012 20:55:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CongTacNgoaiGio](
	[CongTacNgoaiGioGUID] [uniqueidentifier] NOT NULL,
	[Ngay] [datetime] NOT NULL,
	[TenNguoiLam] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CongTacNgoaiGio]  WITH CHECK ADD  CONSTRAINT [FK_CongTacNgoaiGio_DocStaff] FOREIGN KEY([NguoiDeXuatGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])

GO
/****** Object:  Table [dbo].[LichKham]    Script Date: 10/09/2012 21:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichKham](
	[LichKhamGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_LichKham_LichKhamGUID]  DEFAULT (newid()),
	[Ngay] [datetime] NOT NULL,
	[Value] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Type] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LichKham] PRIMARY KEY CLUSTERED 
(
	[LichKhamGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_LichKham_Ngay_Type]    Script Date: 10/09/2012 21:27:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_LichKham_Ngay_Type] ON [dbo].[LichKham] 
(
	[Ngay] ASC,
	[Type] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CongTacNgoaiGio]  WITH CHECK ADD  CONSTRAINT [FK_CongTacNgoaiGio_DocStaff] FOREIGN KEY([DocStaffGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CongTacNgoaiGio] CHECK CONSTRAINT [FK_CongTacNgoaiGio_DocStaff]
GO
/****** Object:  View [dbo].[CongTacNgoaiGioView]    Script Date: 10/11/2012 20:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CongTacNgoaiGioView]
AS
SELECT     dbo.CongTacNgoaiGio.CongTacNgoaiGioGUID, dbo.CongTacNgoaiGio.Ngay, dbo.CongTacNgoaiGio.TenNguoiLam, dbo.CongTacNgoaiGio.MucDich, 
                      dbo.CongTacNgoaiGio.GioVao, dbo.CongTacNgoaiGio.GioRa, dbo.CongTacNgoaiGio.KetQuaDanhGia, dbo.CongTacNgoaiGio.NguoiDeXuatGUID, 
                      dbo.CongTacNgoaiGio.GhiChu, dbo.CongTacNgoaiGio.CreatedDate, dbo.CongTacNgoaiGio.CreatedBy, dbo.CongTacNgoaiGio.UpdatedDate, 
                      dbo.CongTacNgoaiGio.UpdatedBy, dbo.CongTacNgoaiGio.DeletedDate, dbo.CongTacNgoaiGio.DeletedBy, dbo.CongTacNgoaiGio.Status, 
                      dbo.DocStaffView.FullName AS TenNguoiDeXuat
FROM         dbo.CongTacNgoaiGio INNER JOIN
                      dbo.DocStaffView ON dbo.CongTacNgoaiGio.NguoiDeXuatGUID = dbo.DocStaffView.DocStaffGUID
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'f42980b8-38db-4e83-8057-dd69e589e19c', N'CongTacNgoaiGio', N'Công tác ngoài giờ')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'564dc2fe-a5e0-49fe-a79e-b2f85aaff3b0', N'LichKham', N'Lịch khám')
GO





