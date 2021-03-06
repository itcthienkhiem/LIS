USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'462144cd-d85c-4f82-9dce-1de6f1c615cb', N'BenhNhanNgoaiGoiKham', N'Bệnh nhân ngoài gói khám')
GO
/****** Object:  Table [dbo].[BenhNhanNgoaiGoiKham]    Script Date: 11/01/2012 10:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BenhNhanNgoaiGoiKham](
	[BenhNhanNgoaiGoiKhamGUID] [uniqueidentifier] NOT NULL,
	[NgayKham] [datetime] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[LanDau] [tinyint] NOT NULL CONSTRAINT [DF_BenhNhanNgoaiGoiKham_LanDau]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_BenhNhanNgoaiGoiKham_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_BenhNhanNgoaiGoiKham] PRIMARY KEY CLUSTERED 
(
	[BenhNhanNgoaiGoiKhamGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BenhNhanNgoaiGoiKham]  WITH CHECK ADD  CONSTRAINT [FK_BenhNhanNgoaiGoiKham_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BenhNhanNgoaiGoiKham] CHECK CONSTRAINT [FK_BenhNhanNgoaiGoiKham_Patient]
GO
ALTER TABLE [dbo].[BenhNhanNgoaiGoiKham]  WITH CHECK ADD  CONSTRAINT [FK_BenhNhanNgoaiGoiKham_Services] FOREIGN KEY([ServiceGUID])
REFERENCES [dbo].[Services] ([ServiceGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BenhNhanNgoaiGoiKham] CHECK CONSTRAINT [FK_BenhNhanNgoaiGoiKham_Services]
GO
/****** Object:  View [dbo].[BenhNhanNgoaiGoiKhamView]    Script Date: 11/01/2012 21:20:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BenhNhanNgoaiGoiKhamView]
AS
SELECT     dbo.BenhNhanNgoaiGoiKham.BenhNhanNgoaiGoiKhamGUID, dbo.BenhNhanNgoaiGoiKham.NgayKham, dbo.BenhNhanNgoaiGoiKham.PatientGUID, 
                      dbo.BenhNhanNgoaiGoiKham.ServiceGUID, dbo.BenhNhanNgoaiGoiKham.LanDau, dbo.BenhNhanNgoaiGoiKham.CreatedDate, 
                      dbo.BenhNhanNgoaiGoiKham.CreatedBy, dbo.BenhNhanNgoaiGoiKham.UpdatedDate, dbo.BenhNhanNgoaiGoiKham.UpdatedBy, 
                      dbo.BenhNhanNgoaiGoiKham.DeletedDate, dbo.BenhNhanNgoaiGoiKham.DeletedBy, dbo.BenhNhanNgoaiGoiKham.Status, dbo.Services.Code, dbo.Services.Name, 
                      dbo.Services.EnglishName, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.Address, dbo.PatientView.FileNum, 
                      dbo.PatientView.Mobile, dbo.PatientView.Email, dbo.Services.Status AS ServiceStatus, dbo.PatientView.Archived, 
                      CASE dbo.BenhNhanNgoaiGoiKham.LanDau WHEN 0 THEN N'Lần đầu' ELSE N'Tái khám' END AS LanDauStr, 
                      CASE dbo.BenhNhanNgoaiGoiKham.CreatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView.FullName END AS NguoiTao
FROM         dbo.BenhNhanNgoaiGoiKham INNER JOIN
                      dbo.Services ON dbo.BenhNhanNgoaiGoiKham.ServiceGUID = dbo.Services.ServiceGUID INNER JOIN
                      dbo.PatientView ON dbo.BenhNhanNgoaiGoiKham.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.BenhNhanNgoaiGoiKham.CreatedBy = dbo.DocStaffView.DocStaffGUID

GO


















