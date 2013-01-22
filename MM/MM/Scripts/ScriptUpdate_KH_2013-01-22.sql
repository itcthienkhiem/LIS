USE MM
GO
ALTER TABLE CompanyContract
ADD [DatCoc] [float] NOT NULL DEFAULT ((0))
GO
ALTER TABLE DichVuLamThem
ADD [DaThuTien] [bit] NOT NULL DEFAULT ((0))
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

