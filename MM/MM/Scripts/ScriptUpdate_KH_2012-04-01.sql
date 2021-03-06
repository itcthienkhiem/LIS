USE MM
GO
ALTER TABLE NhatKyLienHeCongTy 
ALTER COLUMN ThangKham NVARCHAR(50)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[NhatKyLienHeCongTyView]
AS
SELECT     dbo.NhatKyLienHeCongTy.NhatKyLienHeCongTyGUID, dbo.NhatKyLienHeCongTy.DocStaffGUID, dbo.NhatKyLienHeCongTy.NgayGioLienHe, 
                      dbo.NhatKyLienHeCongTy.CongTyLienHe, dbo.NhatKyLienHeCongTy.NoiDungLienHe, dbo.NhatKyLienHeCongTy.CreatedDate, dbo.NhatKyLienHeCongTy.CreatedBy, 
                      dbo.NhatKyLienHeCongTy.UpdatedDate, dbo.NhatKyLienHeCongTy.UpdatedBy, dbo.NhatKyLienHeCongTy.DeletedDate, dbo.NhatKyLienHeCongTy.DeletedBy, 
                      dbo.NhatKyLienHeCongTy.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS FullName, dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, 
                      dbo.DocStaffView.Status AS DocStaffStatus, dbo.NhatKyLienHeCongTy.Note, 
                      CASE dbo.NhatKyLienHeCongTy.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat, 
                      dbo.NhatKyLienHeCongTy.TenNguoiLienHe, dbo.NhatKyLienHeCongTy.SoDienThoaiLienHe, dbo.NhatKyLienHeCongTy.SoNguoiKham, 
                      dbo.NhatKyLienHeCongTy.DiaChi, dbo.NhatKyLienHeCongTy.Email, dbo.NhatKyLienHeCongTy.ThangKham
FROM         dbo.NhatKyLienHeCongTy LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.NhatKyLienHeCongTy.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.NhatKyLienHeCongTy.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
