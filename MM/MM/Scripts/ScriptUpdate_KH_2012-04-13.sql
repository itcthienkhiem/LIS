USE MM
GO
ALTER TABLE Booking 
ADD [InOut] [nvarchar](255) NULL
GO
ALTER TABLE [NhatKyLienHeCongTy] 
ADD [Highlight] [bit] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[BookingView]
AS
SELECT     dbo.Booking.BookingGUID, dbo.Booking.BookingDate, dbo.Booking.Company, dbo.Booking.MorningCount, dbo.Booking.AfternoonCount, dbo.Booking.EveningCount, 
                      dbo.Booking.Pax, dbo.Booking.BookingType, dbo.Booking.CreatedDate, dbo.Booking.CreatedBy, dbo.Booking.UpdatedDate, dbo.Booking.UpdatedBy, 
                      dbo.Booking.DeletedDate, dbo.Booking.DeletedBy, dbo.Booking.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS Sales, dbo.DocStaffView.Archived, 
                      dbo.Booking.InOut
FROM         dbo.Booking LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Booking.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER TABLE [NhatKyLienHeCongTy] 
ADD [SoNgay] [nvarchar](50) NULL
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
                      dbo.NhatKyLienHeCongTy.DiaChi, dbo.NhatKyLienHeCongTy.Email, dbo.NhatKyLienHeCongTy.ThangKham, dbo.NhatKyLienHeCongTy.Highlight, 
                      dbo.NhatKyLienHeCongTy.SoNgay
FROM         dbo.NhatKyLienHeCongTy LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.NhatKyLienHeCongTy.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.NhatKyLienHeCongTy.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO