USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'9d477a71-6945-43bc-af39-88420c21dc91', N'InMauHoSo', N'In mẫu hồ sơ')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'4f6b749e-0e5e-48fb-9767-91d8611e13c7', N'CauHinhDichVuXetNghiem', N'Cấu hình dịch vụ xét nghiệm')
GO
/****** Object:  Table [dbo].[CauHinhDichVuXetNghiem]    Script Date: 06/20/2013 21:54:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CauHinhDichVuXetNghiem](
	[CauHinhDichVuXetNghiemGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[Normal_Abnormal] [bit] NOT NULL CONSTRAINT [DF_CauHinhDichVuXetNghiem_Normal_Abnormal]  DEFAULT ((0)),
	[Negative_Positive] [bit] NOT NULL CONSTRAINT [DF_CauHinhDichVuXetNghiem_Negative_Positive]  DEFAULT ((0)),
 CONSTRAINT [PK_CauHinhDichVuXetNghiem] PRIMARY KEY CLUSTERED 
(
	[CauHinhDichVuXetNghiemGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CauHinhDichVuXetNghiemView]    Script Date: 06/20/2013 22:39:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CauHinhDichVuXetNghiemView]
AS
SELECT     dbo.CauHinhDichVuXetNghiem.CauHinhDichVuXetNghiemGUID, ISNULL(dbo.CauHinhDichVuXetNghiem.Normal_Abnormal, CAST(0 AS bit)) AS Normal_Abnormal, 
                      ISNULL(dbo.CauHinhDichVuXetNghiem.Negative_Positive, CAST(0 AS bit)) AS Negative_Positive, dbo.Services.Code, dbo.Services.Name, dbo.Services.EnglishName, 
                      dbo.Services.Status, dbo.Services.ServiceGUID
FROM         dbo.CauHinhDichVuXetNghiem RIGHT OUTER JOIN
                      dbo.Services ON dbo.CauHinhDichVuXetNghiem.ServiceGUID = dbo.Services.ServiceGUID


