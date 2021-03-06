USE MM
GO
ALTER TABLE CompanyContract
ADD [SoTien] [float] NOT NULL DEFAULT ((0))
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CompanyContractView]
AS
SELECT     dbo.Company.CompanyGUID, dbo.Company.MaCty, dbo.Company.TenCty, dbo.Company.DiaChi, dbo.Company.Dienthoai, dbo.Company.Fax, 
                      dbo.Company.Website, dbo.CompanyContract.CompanyContractGUID, dbo.CompanyContract.ContractName, dbo.CompanyContract.Completed, 
                      dbo.CompanyContract.CreatedDate, dbo.CompanyContract.CreatedBy, dbo.CompanyContract.UpdatedDate, dbo.CompanyContract.UpdatedBy, 
                      dbo.CompanyContract.DeletedDate, dbo.CompanyContract.DeletedBy, dbo.CompanyContract.Status AS ContractStatus, 
                      dbo.Company.Status AS CompanyStatus, dbo.CompanyContract.BeginDate, dbo.CompanyContract.ContractCode, dbo.CompanyContract.EndDate, 
                      ISNULL(dbo.Lock.Status, 0) AS Lock, dbo.CompanyContract.SoTien
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID LEFT OUTER JOIN
                      dbo.Lock ON dbo.CompanyContract.CompanyContractGUID = dbo.Lock.KeyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER TABLE dbo.BenhNhanNgoaiGoiKham
DROP COLUMN HopDongGUID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[BenhNhanNgoaiGoiKhamView]
AS
SELECT     dbo.BenhNhanNgoaiGoiKham.BenhNhanNgoaiGoiKhamGUID, dbo.BenhNhanNgoaiGoiKham.NgayKham, dbo.BenhNhanNgoaiGoiKham.PatientGUID, 
                      dbo.BenhNhanNgoaiGoiKham.ServiceGUID, dbo.BenhNhanNgoaiGoiKham.LanDau, dbo.BenhNhanNgoaiGoiKham.CreatedDate, 
                      dbo.BenhNhanNgoaiGoiKham.CreatedBy, dbo.BenhNhanNgoaiGoiKham.UpdatedDate, dbo.BenhNhanNgoaiGoiKham.UpdatedBy, 
                      dbo.BenhNhanNgoaiGoiKham.DeletedDate, dbo.BenhNhanNgoaiGoiKham.DeletedBy, dbo.BenhNhanNgoaiGoiKham.Status, dbo.Services.Code, 
                      dbo.Services.Name, dbo.Services.EnglishName, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, 
                      dbo.PatientView.Address, dbo.PatientView.FileNum, dbo.PatientView.Mobile, dbo.PatientView.Email, dbo.Services.Status AS ServiceStatus, 
                      dbo.PatientView.Archived, CASE dbo.BenhNhanNgoaiGoiKham.LanDau WHEN 0 THEN N'Lần đầu' ELSE N'Tái khám' END AS LanDauStr, 
                      CASE dbo.BenhNhanNgoaiGoiKham.CreatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView.FullName END AS NguoiTao
FROM         dbo.BenhNhanNgoaiGoiKham INNER JOIN
                      dbo.Services ON dbo.BenhNhanNgoaiGoiKham.ServiceGUID = dbo.Services.ServiceGUID INNER JOIN
                      dbo.PatientView ON dbo.BenhNhanNgoaiGoiKham.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.BenhNhanNgoaiGoiKham.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'2a43b652-1227-4b1c-8bc2-ce63bf90de95', N'BaoCaoCongNoHopDong', N'Báo cáo công nợ hợp đồng')
GO
GO
/****** Object:  Table [dbo].[DichVuLamThem]    Script Date: 01/16/2013 21:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichVuLamThem](
	[DichVuLamThemGUID] [uniqueidentifier] NOT NULL,
	[ContractMemberGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[ActiveDate] [datetime] NOT NULL,
	[Price] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_DichVuLamThem_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_DichVuLamThem] PRIMARY KEY CLUSTERED 
(
	[DichVuLamThemGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[DichVuLamThem]  WITH CHECK ADD  CONSTRAINT [FK_DichVuLamThem_ContractMember] FOREIGN KEY([ContractMemberGUID])
REFERENCES [dbo].[ContractMember] ([ContractMemberGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DichVuLamThem]  WITH CHECK ADD  CONSTRAINT [FK_DichVuLamThem_Services] FOREIGN KEY([ServiceGUID])
REFERENCES [dbo].[Services] ([ServiceGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[DichVuLamThemView]    Script Date: 01/16/2013 21:31:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DichVuLamThemView]
AS
SELECT     dbo.DichVuLamThem.DichVuLamThemGUID, dbo.DichVuLamThem.ContractMemberGUID, dbo.DichVuLamThem.ServiceGUID, dbo.DichVuLamThem.ActiveDate, 
                      dbo.DichVuLamThem.Price AS FixedPrice, dbo.DichVuLamThem.Discount, dbo.DichVuLamThem.Note, dbo.DichVuLamThem.CreatedDate, 
                      dbo.DichVuLamThem.CreatedBy, dbo.DichVuLamThem.UpdatedDate, dbo.DichVuLamThem.UpdatedBy, dbo.DichVuLamThem.DeletedDate, 
                      dbo.DichVuLamThem.DeletedBy, dbo.DichVuLamThem.Status, dbo.Services.Code, dbo.Services.Name, dbo.Services.EnglishName, dbo.Services.Price, 
                      dbo.Services.Status AS ServiceStatus
FROM         dbo.DichVuLamThem INNER JOIN
                      dbo.Services ON dbo.DichVuLamThem.ServiceGUID = dbo.Services.ServiceGUID

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PhieuThuHopDongView]
AS
SELECT     dbo.PhieuThuHopDong.PhieuThuHopDongGUID, dbo.PhieuThuHopDong.HopDongGUID, dbo.PhieuThuHopDong.MaPhieuThuHopDong, 
                      dbo.PhieuThuHopDong.TenNguoiNop, dbo.PhieuThuHopDong.TenCongTy, dbo.PhieuThuHopDong.DiaChi, dbo.PhieuThuHopDong.NgayThu, 
                      dbo.PhieuThuHopDong.Notes, dbo.PhieuThuHopDong.IsExported, dbo.PhieuThuHopDong.Status, dbo.PhieuThuHopDong.CreatedDate, 
                      dbo.PhieuThuHopDong.CreatedBy, dbo.PhieuThuHopDong.UpdatedDate, dbo.PhieuThuHopDong.UpdatedBy, dbo.PhieuThuHopDong.DeletedDate, 
                      dbo.PhieuThuHopDong.DeletedBy, dbo.PhieuThuHopDong.ChuaThuTien, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, 
                      dbo.CompanyContract.ContractCode, dbo.CompanyContract.ContractName
FROM         dbo.PhieuThuHopDong INNER JOIN
                      dbo.CompanyContract ON dbo.PhieuThuHopDong.HopDongGUID = dbo.CompanyContract.CompanyContractGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuHopDong.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

