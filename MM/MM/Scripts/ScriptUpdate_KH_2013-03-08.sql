USE MM
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[GiaThuocView]
AS
SELECT     dbo.GiaThuoc.GiaThuocGUID, dbo.GiaThuoc.ThuocGUID, dbo.GiaThuoc.GiaBan, dbo.GiaThuoc.NgayApDung, dbo.GiaThuoc.CreatedDate, dbo.GiaThuoc.CreatedBy, 
                      dbo.GiaThuoc.UpdatedDate, dbo.GiaThuoc.UpdatedBy, dbo.GiaThuoc.DeletedDate, dbo.GiaThuoc.DeletedBy, dbo.GiaThuoc.Status AS GiaThuocStatus, 
                      dbo.Thuoc.MaThuoc, dbo.Thuoc.TenThuoc, dbo.Thuoc.DonViTinh, dbo.Thuoc.Status AS ThuocStatus, dbo.GiaThuoc.Note, dbo.Thuoc.BietDuoc
FROM         dbo.GiaThuoc INNER JOIN
                      dbo.Thuoc ON dbo.GiaThuoc.ThuocGUID = dbo.Thuoc.ThuocGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  Table [dbo].[SMSLog]    Script Date: 03/10/2013 20:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSLog](
	[SMSLogGUID] [uniqueidentifier] NOT NULL,
	[Ngay] [datetime] NOT NULL,
	[NoiDung] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mobile] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[DocStaffGUID] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL CONSTRAINT [DF_SMSLog_Status]  DEFAULT ((0)),
	[Notes] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_SMSLog] PRIMARY KEY CLUSTERED 
(
	[SMSLogGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SMSLogView]    Script Date: 03/10/2013 21:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SMSLogView]
AS
SELECT     dbo.SMSLog.SMSLogGUID, dbo.SMSLog.Ngay, dbo.SMSLog.NoiDung, dbo.SMSLog.Mobile, dbo.SMSLog.PatientGUID, dbo.SMSLog.DocStaffGUID, 
                      dbo.SMSLog.Status, dbo.SMSLog.Notes, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.FileNum, 
                      dbo.PatientView.Address, 
                      CASE dbo.SMSLog.DocStaffGUID WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiGui
FROM         dbo.SMSLog INNER JOIN
                      dbo.PatientView ON dbo.SMSLog.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.SMSLog.DocStaffGUID = dbo.DocStaffView.DocStaffGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'e8842c74-6060-414f-a57f-a85b3db4639f', N'SMSLog', N'SMS Log')
GO
ALTER TABLE Company
ADD [MaSoThue] [nvarchar](50) NULL
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CompanyContractView]
AS
SELECT     dbo.Company.CompanyGUID, dbo.Company.MaCty, dbo.Company.TenCty, dbo.Company.DiaChi, dbo.Company.Dienthoai, dbo.Company.Fax, dbo.Company.Website, 
                      dbo.CompanyContract.CompanyContractGUID, dbo.CompanyContract.ContractName, dbo.CompanyContract.Completed, dbo.CompanyContract.CreatedDate, 
                      dbo.CompanyContract.CreatedBy, dbo.CompanyContract.UpdatedDate, dbo.CompanyContract.UpdatedBy, dbo.CompanyContract.DeletedDate, 
                      dbo.CompanyContract.DeletedBy, dbo.CompanyContract.Status AS ContractStatus, dbo.Company.Status AS CompanyStatus, dbo.CompanyContract.BeginDate, 
                      dbo.CompanyContract.ContractCode, dbo.CompanyContract.EndDate, ISNULL(dbo.Lock.Status, 0) AS Lock, dbo.CompanyContract.SoTien, 
                      dbo.CompanyContract.DatCoc, dbo.Company.MaSoThue
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID LEFT OUTER JOIN
                      dbo.Lock ON dbo.CompanyContract.CompanyContractGUID = dbo.Lock.KeyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO