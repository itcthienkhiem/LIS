USE [MM]
GO
/****** Object:  Table [dbo].[NhanXetKhamLamSang]    Script Date: 07/27/2015 10:14:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NhanXetKhamLamSang](
	[NhanXetKhamLamSangGUID] [uniqueidentifier] NOT NULL,
	[NhanXet] [nvarchar](max) NULL,
	[Loai] [int] NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_NhanXetKhamLamSang] PRIMARY KEY CLUSTERED 
(
	[NhanXetKhamLamSangGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[NhanXetKhamLamSang] ADD  CONSTRAINT [DF_NhanXetKhamLamSang_Loai]  DEFAULT ((0)) FOR [Loai]
GO

ALTER TABLE [dbo].[NhanXetKhamLamSang] ADD  CONSTRAINT [DF_NhanXetKhamLamSang_Status]  DEFAULT ((0)) FOR [Status]
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'b91eea29-14ce-47e5-a29a-7d37cdbdbd52', N'NhanXetKhamLamSang', N'Nhận xét khám lâm sàng')
GO
/****** Object:  View [dbo].[NhanXetKhamLamSangView]    Script Date: 07/27/2015 10:34:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NhanXetKhamLamSangView]
AS
SELECT     NhanXetKhamLamSangGUID, NhanXet, Loai, GhiChu, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE Loai WHEN 0 THEN N'Mắt' WHEN 1 THEN N'Tai mũi họng' WHEN 2 THEN N'Răng hàm mặt' WHEN 3 THEN N'Hô hấp' WHEN 4 THEN N'Tim mạch' WHEN 5 THEN N'Tiêu hóa'
                       WHEN 6 THEN N'Tiết niệu sinh dục' WHEN 7 THEN N'Cơ xương khớp' WHEN 8 THEN N'Da liễu' WHEN 9 THEN N'Thần kinh' WHEN 10 THEN N'Nội tiết' WHEN 11 THEN N'Khác'
                       WHEN 12 THEN N'Khám phụ khoa' END AS LoaiStr
FROM         dbo.NhanXetKhamLamSang

GO
