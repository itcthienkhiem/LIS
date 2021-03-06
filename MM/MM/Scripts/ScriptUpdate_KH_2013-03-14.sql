USE MM
GO
ALTER TABLE dbo.ChiTietToaThuoc
ALTER COLUMN ThuocGUID uniqueidentifier NULL
GO
ALTER TABLE dbo.ChiTietToaThuoc
ADD [TenThuocNgoai] [nvarchar](500) NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietToaThuocView]
AS
SELECT     dbo.ChiTietToaThuoc.ChiTietToaThuocGUID, dbo.ChiTietToaThuoc.ToaThuocGUID, dbo.ChiTietToaThuoc.ThuocGUID, dbo.ChiTietToaThuoc.Note, 
                      dbo.ChiTietToaThuoc.CreatedDate, dbo.ChiTietToaThuoc.CreatedBy, dbo.ChiTietToaThuoc.UpdatedDate, dbo.ChiTietToaThuoc.UpdatedBy, 
                      dbo.ChiTietToaThuoc.DeletedDate, dbo.ChiTietToaThuoc.DeletedBy, dbo.ChiTietToaThuoc.Status AS ChiTietToaThuocStatus, dbo.Thuoc.MaThuoc, 
                      dbo.Thuoc.DonViTinh, ISNULL(dbo.Thuoc.Status, 0) AS ThuocStatus, dbo.ChiTietToaThuoc.SoLuong, dbo.ChiTietToaThuoc.LieuDung, 
                      dbo.ChiTietToaThuoc.Sang, dbo.ChiTietToaThuoc.Trua, dbo.ChiTietToaThuoc.Chieu, dbo.ChiTietToaThuoc.Toi, dbo.ChiTietToaThuoc.TruocAn, 
                      dbo.ChiTietToaThuoc.SauAn, dbo.ChiTietToaThuoc.Khac_TruocSauAn, dbo.ChiTietToaThuoc.Uong, dbo.ChiTietToaThuoc.Boi, dbo.ChiTietToaThuoc.Dat, 
                      dbo.ChiTietToaThuoc.Khac_CachDung, dbo.ChiTietToaThuoc.SangNote, dbo.ChiTietToaThuoc.TruaNote, dbo.ChiTietToaThuoc.ChieuNote, 
                      dbo.ChiTietToaThuoc.ToiNote, dbo.ChiTietToaThuoc.TruocAnNote, dbo.ChiTietToaThuoc.SauAnNote, dbo.ChiTietToaThuoc.Khac_TruocSauAnNote, 
                      dbo.ChiTietToaThuoc.UongNote, dbo.ChiTietToaThuoc.BoiNote, dbo.ChiTietToaThuoc.DatNote, dbo.ChiTietToaThuoc.Khac_CachDungNote, 
                      ISNULL(dbo.Thuoc.TenThuoc, dbo.ChiTietToaThuoc.TenThuocNgoai) AS TenThuoc
FROM         dbo.ChiTietToaThuoc LEFT OUTER JOIN
                      dbo.Thuoc ON dbo.ChiTietToaThuoc.ThuocGUID = dbo.Thuoc.ThuocGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


