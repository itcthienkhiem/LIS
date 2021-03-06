USE MM
GO
GO
/****** Object:  Table [dbo].[DiaChiCongTy]    Script Date: 06/03/2012 06:22:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiaChiCongTy](
	[DiaChiCongTyGUID] [uniqueidentifier] NOT NULL,
	[MaCongTy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_DiaChiCongTy_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_DiaChiCongTy] PRIMARY KEY CLUSTERED 
(
	[DiaChiCongTyGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'7C93E5A5-D13F-48BB-A85F-D357B2AE79AB', N'DiaChiCongTy', N'Danh mục địa chỉ công ty')

