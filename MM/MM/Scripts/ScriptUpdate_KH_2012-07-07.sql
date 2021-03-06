USE MM
GO
UPDATE XetNghiem_Manual
SET [Type] = N'Khac', GroupName = N'SPERMOGRAMME (TINH DỊCH ĐỒ)'
WHERE FullName = N'Hình dạng bình thường'
GO
/****** Object:  Table [dbo].[BenhNhanThanThuoc]    Script Date: 07/07/2012 23:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BenhNhanThanThuoc](
	[BenhNhanThanThuocGUID] [uniqueidentifier] NOT NULL,
	[DocStaffGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BenhNhanThanThuoc] PRIMARY KEY CLUSTERED 
(
	[BenhNhanThanThuocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[BenhNhanThanThuoc]  WITH CHECK ADD  CONSTRAINT [FK_BenhNhanThanThuoc_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'f6173046-21af-46a7-8d20-d3cd85e51136', N'BenhNhanThanThuoc', N'Bệnh nhân thân thuộc')