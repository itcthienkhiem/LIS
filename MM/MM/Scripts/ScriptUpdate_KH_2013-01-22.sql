USE MM
GO
ALTER TABLE CompanyContract
ADD [DatCoc] [float] NOT NULL DEFAULT ((0))
GO
ALTER TABLE DichVuLamThem
ADD [DaThuTien] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE DocStaff
ADD [ChuKy] [image] NULL
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
                      dbo.CompanyContract.DatCoc
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID LEFT OUTER JOIN
                      dbo.Lock ON dbo.CompanyContract.CompanyContractGUID = dbo.Lock.KeyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[DichVuLamThemView]
AS
SELECT     dbo.DichVuLamThem.DichVuLamThemGUID, dbo.DichVuLamThem.ContractMemberGUID, dbo.DichVuLamThem.ServiceGUID, dbo.DichVuLamThem.ActiveDate, 
                      dbo.DichVuLamThem.Price AS FixedPrice, dbo.DichVuLamThem.Discount, dbo.DichVuLamThem.Note, dbo.DichVuLamThem.CreatedDate, 
                      dbo.DichVuLamThem.CreatedBy, dbo.DichVuLamThem.UpdatedDate, dbo.DichVuLamThem.UpdatedBy, dbo.DichVuLamThem.DeletedDate, 
                      dbo.DichVuLamThem.DeletedBy, dbo.DichVuLamThem.Status, dbo.Services.Code, dbo.Services.Name, dbo.Services.EnglishName, dbo.Services.Price, 
                      dbo.Services.Status AS ServiceStatus, dbo.DichVuLamThem.DaThuTien
FROM         dbo.DichVuLamThem INNER JOIN
                      dbo.Services ON dbo.DichVuLamThem.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER TABLE YKienKhachHang
ADD [IsIN] [bit] NOT NULL DEFAULT ((0)),
	[SoTongDai] [nvarchar](50) NULL
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[YKienKhachHangView]
AS
SELECT     dbo.YKienKhachHang.YKienKhachHangGUID, dbo.YKienKhachHang.PatientGUID, dbo.YKienKhachHang.TenKhachHang, dbo.YKienKhachHang.SoDienThoai, 
                      dbo.YKienKhachHang.DiaChi, dbo.YKienKhachHang.YeuCau, dbo.YKienKhachHang.Nguon, dbo.YKienKhachHang.Note, dbo.YKienKhachHang.ContactDate, 
                      dbo.YKienKhachHang.ContactBy, dbo.YKienKhachHang.UpdatedDate, dbo.YKienKhachHang.UpdatedBy, dbo.YKienKhachHang.DeletedDate, 
                      dbo.YKienKhachHang.DeletedBy, dbo.YKienKhachHang.Status, 
                      CASE dbo.YKienKhachHang.ContactBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiTao, 
                      CASE dbo.YKienKhachHang.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat, 
                      dbo.YKienKhachHang.KetLuan, dbo.YKienKhachHang.NguoiKetLuan, DocStaffView_2.FullName AS TenNguoiKetLuan, dbo.YKienKhachHang.BacSiPhuTrachGUID, 
                      dbo.YKienKhachHang.DaXong, ISNULL(DocStaffView_3.FullName, N'') AS BacSiPhuTrach, 
                      CASE YKienKhachHang.DaXong WHEN 'False' THEN N'Chưa xong' ELSE N'Đã xong' END AS DaXongStr, dbo.YKienKhachHang.IsIN, dbo.YKienKhachHang.SoTongDai, 
                      CASE IsIN WHEN 'False' THEN 'OUT' ELSE 'IN' END AS InOut
FROM         dbo.YKienKhachHang LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_3 ON dbo.YKienKhachHang.BacSiPhuTrachGUID = DocStaffView_3.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_2 ON dbo.YKienKhachHang.NguoiKetLuan = DocStaffView_2.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.YKienKhachHang.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.YKienKhachHang.ContactBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  Table [dbo].[DichVuCon]    Script Date: 02/16/2013 02:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichVuCon](
	[DichVuConGUID] [uniqueidentifier] NOT NULL,
	[GiaDichVuHopDongGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_DichVuCon_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_DichVuCon] PRIMARY KEY CLUSTERED 
(
	[DichVuConGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[DichVuCon]  WITH CHECK ADD  CONSTRAINT [FK_DichVuCon_GiaDichVuHopDong] FOREIGN KEY([GiaDichVuHopDongGUID])
REFERENCES [dbo].[GiaDichVuHopDong] ([GiaDichVuHopDongGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DichVuCon]  WITH CHECK ADD  CONSTRAINT [FK_DichVuCon_Services] FOREIGN KEY([ServiceGUID])
REFERENCES [dbo].[Services] ([ServiceGUID])
GO
/****** Object:  View [dbo].[DichVuConView]    Script Date: 02/16/2013 02:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DichVuConView]
AS
SELECT     dbo.DichVuCon.DichVuConGUID, dbo.DichVuCon.ServiceGUID, dbo.DichVuCon.CreatedDate, dbo.DichVuCon.CreatedBy, dbo.DichVuCon.UpdatedDate, 
                      dbo.DichVuCon.UpdatedBy, dbo.DichVuCon.DeletedDate, dbo.DichVuCon.DeletedBy, dbo.DichVuCon.Status, dbo.Services.Code, dbo.Services.Name, 
                      dbo.Services.EnglishName, dbo.Services.Price, dbo.Services.Status AS ServiceStatus, dbo.DichVuCon.GiaDichVuHopDongGUID
FROM         dbo.DichVuCon INNER JOIN
                      dbo.Services ON dbo.DichVuCon.ServiceGUID = dbo.Services.ServiceGUID

GO