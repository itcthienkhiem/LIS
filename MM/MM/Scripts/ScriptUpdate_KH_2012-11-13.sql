USE MM
GO
DELETE FROM PhieuThuCapCuu
GO
ALTER TABLE PhieuThuCapCuu
ADD [ToaCapCuuGUID] [uniqueidentifier] NULL
GO
ALTER TABLE [dbo].[PhieuThuCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThuCapCuu_ToaCapCuu] FOREIGN KEY([ToaCapCuuGUID])
REFERENCES [dbo].[ToaCapCuu] ([ToaCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuThuCapCuu] CHECK CONSTRAINT [FK_PhieuThuCapCuu_ToaCapCuu]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PhieuThuCapCuuView]
AS
SELECT     dbo.PhieuThuCapCuu.PhieuThuCapCuuGUID, dbo.PhieuThuCapCuu.NgayThu, dbo.PhieuThuCapCuu.MaBenhNhan, 
                      dbo.PhieuThuCapCuu.TenBenhNhan, dbo.PhieuThuCapCuu.TenCongTy, dbo.PhieuThuCapCuu.DiaChi, dbo.PhieuThuCapCuu.IsExported, 
                      dbo.PhieuThuCapCuu.ChuaThuTien, dbo.PhieuThuCapCuu.LyDoGiam, dbo.PhieuThuCapCuu.Notes, dbo.PhieuThuCapCuu.CreatedDate, 
                      dbo.PhieuThuCapCuu.CreatedBy, dbo.PhieuThuCapCuu.UpdatedDate, dbo.PhieuThuCapCuu.UpdatedBy, dbo.PhieuThuCapCuu.DeletedDate, 
                      dbo.PhieuThuCapCuu.DeletedBy, dbo.PhieuThuCapCuu.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, 
                      dbo.PhieuThuCapCuu.MaPhieuThuCapCuu, dbo.PhieuThuCapCuu.ToaCapCuuGUID, dbo.ToaCapCuu.MaToaCapCuu
FROM         dbo.PhieuThuCapCuu INNER JOIN
                      dbo.ToaCapCuu ON dbo.PhieuThuCapCuu.ToaCapCuuGUID = dbo.ToaCapCuu.ToaCapCuuGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuCapCuu.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

