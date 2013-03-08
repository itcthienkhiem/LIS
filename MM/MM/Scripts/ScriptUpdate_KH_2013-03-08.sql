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

