USE [MM]
GO
ALTER TABLE KetQuaNoiSoi
ADD [ThucQuan] [nvarchar](255) NULL,
	[DaDay] [nvarchar](500) NULL,
	[HangVi] [nvarchar](255) NULL,
	[MonVi] [nvarchar](255) NULL,
	[HanhTaTrang] [nvarchar](255) NULL,
	[Clotest] [nvarchar](255) NULL,
	[TrucTrang] [nvarchar](255) NULL,
	[DaiTrangTrai] [nvarchar](255) NULL,
	[DaiTrangGocLach] [nvarchar](255) NULL,
	[DaiTrangNgang] [nvarchar](255) NULL,
	[DaiTrangGocGan] [nvarchar](255) NULL,
	[DaiTrangPhai] [nvarchar](255) NULL,
	[ManhTrang] [nvarchar](255) NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaNoiSoiView]
AS
SELECT     dbo.KetQuaNoiSoi.KetQuaNoiSoiGUID, dbo.KetQuaNoiSoi.SoPhieu, dbo.KetQuaNoiSoi.NgayKham, dbo.KetQuaNoiSoi.PatientGUID, dbo.KetQuaNoiSoi.LyDoKham, 
                      dbo.KetQuaNoiSoi.BacSiChiDinh, dbo.KetQuaNoiSoi.BacSiSoi, dbo.KetQuaNoiSoi.KetLuan, dbo.KetQuaNoiSoi.DeNghi, dbo.KetQuaNoiSoi.LoaiNoiSoi, 
                      dbo.KetQuaNoiSoi.OngTaiTrai, dbo.KetQuaNoiSoi.MangNhiTrai, dbo.KetQuaNoiSoi.MangNhiPhai, dbo.KetQuaNoiSoi.CanBuaTrai, dbo.KetQuaNoiSoi.CanBuaPhai, 
                      dbo.KetQuaNoiSoi.HomNhiTrai, dbo.KetQuaNoiSoi.HomNhiPhai, dbo.KetQuaNoiSoi.ValsavaTrai, dbo.KetQuaNoiSoi.ValsavaPhai, dbo.KetQuaNoiSoi.NiemMacTrai, 
                      dbo.KetQuaNoiSoi.NiemMacPhai, dbo.KetQuaNoiSoi.VachNganTrai, dbo.KetQuaNoiSoi.VachNganPhai, dbo.KetQuaNoiSoi.KheTrenTrai, 
                      dbo.KetQuaNoiSoi.KheTrenPhai, dbo.KetQuaNoiSoi.KheGiuaTrai, dbo.KetQuaNoiSoi.KheGiuaPhai, dbo.KetQuaNoiSoi.CuonGiuaTrai, dbo.KetQuaNoiSoi.CuonGiuaPhai, 
                      dbo.KetQuaNoiSoi.CuonDuoiTrai, dbo.KetQuaNoiSoi.CuonDuoiPhai, dbo.KetQuaNoiSoi.BongSangTrai, dbo.KetQuaNoiSoi.BongSangPhai, dbo.KetQuaNoiSoi.VomTrai, 
                      dbo.KetQuaNoiSoi.VomPhai, dbo.KetQuaNoiSoi.Amydale, dbo.KetQuaNoiSoi.XoangLe, dbo.KetQuaNoiSoi.MiengThucQuan, dbo.KetQuaNoiSoi.SunPheu, 
                      dbo.KetQuaNoiSoi.DayThanh, dbo.KetQuaNoiSoi.BangThanhThat, dbo.KetQuaNoiSoi.OngTaiNgoai, dbo.KetQuaNoiSoi.MangNhi, dbo.KetQuaNoiSoi.NiemMac, 
                      dbo.KetQuaNoiSoi.VachNgan, dbo.KetQuaNoiSoi.KheTren, dbo.KetQuaNoiSoi.KheGiua, dbo.KetQuaNoiSoi.MomMoc_BongSang, dbo.KetQuaNoiSoi.Vom, 
                      dbo.KetQuaNoiSoi.ThanhQuan, dbo.KetQuaNoiSoi.Hinh1, dbo.KetQuaNoiSoi.Hinh2, dbo.KetQuaNoiSoi.Hinh3, dbo.KetQuaNoiSoi.Hinh4, 
                      dbo.KetQuaNoiSoi.CreatedDate, dbo.KetQuaNoiSoi.CreatedBy, dbo.KetQuaNoiSoi.UpdatedDate, dbo.KetQuaNoiSoi.UpdatedBy, dbo.KetQuaNoiSoi.DeletedDate, 
                      dbo.KetQuaNoiSoi.DeletedBy, dbo.KetQuaNoiSoi.Status, dbo.BacSiChiDinhView.FullName AS TenBacSiChiDinh, dbo.BacSiNoiSoiView.FullName AS TenBacSiNoiSoi, 
                      dbo.BacSiChiDinhView.Archived AS BSCDArchived, dbo.BacSiNoiSoiView.Archived AS BSNSArchived, 
                      CASE LoaiNoiSoi WHEN 0 THEN N'Tai' WHEN 1 THEN N'Mũi' WHEN 2 THEN N'Họng - Thanh quản' WHEN 3 THEN N'Tai mũi họng' WHEN 4 THEN N'Tổng quát' WHEN 5 THEN N'Dạ dày' WHEN 6 THEN N'Trực tràng' END AS LoaiNoiSoiStr,
                       dbo.KetQuaNoiSoi.OngTaiPhai, dbo.KetQuaNoiSoi.MomMocTrai, dbo.KetQuaNoiSoi.MomMocPhai, dbo.KetQuaNoiSoi.ImageName1, dbo.KetQuaNoiSoi.ImageName2, 
                      dbo.KetQuaNoiSoi.ImageName3, dbo.KetQuaNoiSoi.ImageName4, dbo.KetQuaNoiSoi.ThucQuan, dbo.KetQuaNoiSoi.DaDay, dbo.KetQuaNoiSoi.HangVi, 
                      dbo.KetQuaNoiSoi.MonVi, dbo.KetQuaNoiSoi.HanhTaTrang, dbo.KetQuaNoiSoi.Clotest, dbo.KetQuaNoiSoi.TrucTrang, dbo.KetQuaNoiSoi.DaiTrangTrai, 
                      dbo.KetQuaNoiSoi.DaiTrangGocLach, dbo.KetQuaNoiSoi.DaiTrangNgang, dbo.KetQuaNoiSoi.DaiTrangGocGan, dbo.KetQuaNoiSoi.DaiTrangPhai, 
                      dbo.KetQuaNoiSoi.ManhTrang
FROM         dbo.KetQuaNoiSoi INNER JOIN
                      dbo.BacSiNoiSoiView ON dbo.KetQuaNoiSoi.BacSiSoi = dbo.BacSiNoiSoiView.DocStaffGUID LEFT OUTER JOIN
                      dbo.BacSiChiDinhView ON dbo.KetQuaNoiSoi.BacSiChiDinh = dbo.BacSiChiDinhView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO











