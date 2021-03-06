USE MM
GO
ALTER TABLE YKienKhachHang
ADD [KetLuan] [nvarchar](max) NULL,
	[NguoiKetLuan] [uniqueidentifier] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[YKienKhachHangView]
AS
SELECT     dbo.YKienKhachHang.YKienKhachHangGUID, dbo.YKienKhachHang.PatientGUID, dbo.YKienKhachHang.TenKhachHang, 
                      dbo.YKienKhachHang.SoDienThoai, dbo.YKienKhachHang.DiaChi, dbo.YKienKhachHang.YeuCau, dbo.YKienKhachHang.Nguon, 
                      dbo.YKienKhachHang.Note, dbo.YKienKhachHang.ContactDate, dbo.YKienKhachHang.ContactBy, dbo.YKienKhachHang.UpdatedDate, 
                      dbo.YKienKhachHang.UpdatedBy, dbo.YKienKhachHang.DeletedDate, dbo.YKienKhachHang.DeletedBy, dbo.YKienKhachHang.Status, 
                      CASE dbo.YKienKhachHang.ContactBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiTao,
                       CASE dbo.YKienKhachHang.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat,
                       dbo.YKienKhachHang.KetLuan, dbo.YKienKhachHang.NguoiKetLuan, DocStaffView_2.FullName AS TenNguoiKetLuan
FROM         dbo.YKienKhachHang LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_2 ON dbo.YKienKhachHang.NguoiKetLuan = DocStaffView_2.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.YKienKhachHang.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.YKienKhachHang.ContactBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
