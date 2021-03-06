USE MM
GO
DROP TABLE [Settings]
GO
/****** Object:  Table [dbo].[QuanLySoHoaDon]    Script Date: 03/03/2012 00:35:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuanLySoHoaDon](
	[QuanLySoHoaDonGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_QuanLySoHoaDon_QuanLySoHoaDonGUID]  DEFAULT (newid()),
	[SoHoaDon] [int] NOT NULL,
	[DaXuat] [bit] NOT NULL CONSTRAINT [DF_QuanLySoHoaDon_DaXuat]  DEFAULT ((0)),
	[XuatTruoc] [bit] NOT NULL CONSTRAINT [DF_QuanLySoHoaDon_XuatTruoc]  DEFAULT ((0)),
 CONSTRAINT [PK_QuanLySoHoaDon] PRIMARY KEY CLUSTERED 
(
	[QuanLySoHoaDonGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

