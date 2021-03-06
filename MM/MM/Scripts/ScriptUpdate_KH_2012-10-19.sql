USE MM
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spDanhSachNhanVienDenKham]
	@FromDate datetime,
	@ToDate datetime,
	@MaBenhNhan nvarchar(50)
AS
BEGIN
	
	SELECT P.PatientGUID, Max(S.ActivedDate) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile
	INTO #TEMP
	FROM PatientView P, ServiceHistory S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.ActivedDate BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayCanDo) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, CanDo S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayCanDo BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile	
	UNION
	SELECT P.PatientGUID, Max(S.NgayKetLuan) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, KetLuan S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKetLuan BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, KetQuaLamSang S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, KetQuaNoiSoi S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, KetQuaSoiCTC S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, KetQuaXetNghiem_CellDyn3200 S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, KetQuaXetNghiem_Hitachi917 S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, KetQuaXetNghiem_Manual S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.Ngay) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, LoiKhuyen S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.Ngay BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKeToa) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P, ToaThuoc S 
	WHERE P.PatientGUID = S.BenhNhan AND 
	S.NgayKeToa BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT PatientGUID, NgayKham, FileNum, FullName, DobStr, GenderAsStr, Address, Mobile
	FROM PatientView
	WHERE NgayKham IS NOT NULL AND NgayKham BETWEEN @FromDate AND @ToDate AND
	FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False'  

	SELECT PatientGUID, Max(NgayKham) AS NgayKham, FileNum, FullName, 
	DobStr, GenderAsStr, Address, Mobile 
	FROM #TEMP
	GROUP BY PatientGUID, FullName, FileNum, GenderAsStr, Address, DobStr, Mobile
	ORDER BY NgayKham
END
GO
/****** Object:  StoredProcedure [dbo].[spDanhSachNhanVienChuaDenKham]    Script Date: 10/19/2012 23:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDanhSachNhanVienChuaDenKham]
	@FromDate datetime,
	@ToDate datetime,
	@MaBenhNhan nvarchar(50)
AS
BEGIN
	
	SELECT P.PatientGUID, NULL AS NgayKham, P.FileNum, P.FullName, P.DobStr, P.GenderAsStr, P.Address, P.Mobile
	FROM PatientView P
	WHERE P.FileNum LIKE N'' + @MaBenhNhan + '%' AND P.Archived = 'False' AND
	(P.NgayKham IS NULL OR P.NgayKham < @FromDate OR P.NgayKham > @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM ServiceHistory S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.ActivedDate BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM CanDo S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayCanDo BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetLuan S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKetLuan BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaLamSang S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKham BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaNoiSoi S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKham BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaSoiCTC S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKham BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaXetNghiem_CellDyn3200 S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayXN BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaXetNghiem_Hitachi917 S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayXN BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaXetNghiem_Manual S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayXN BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM LoiKhuyen S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.Ngay BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.BenhNhan FROM ToaThuoc S 
	WHERE P.PatientGUID = S.BenhNhan AND S.Status = 0 AND S.NgayKeToa BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.BenhNhan FROM ToaThuoc S 
	WHERE P.PatientGUID = S.BenhNhan AND S.Status = 0 AND S.NgayKeToa BETWEEN @FromDate AND @ToDate)
END
GO
/****** Object:  Table [dbo].[ThongTinKhachHang]    Script Date: 10/20/2012 10:15:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinKhachHang](
	[ThongTinKhachHangGUID] [uniqueidentifier] NOT NULL,
	[TenKhachHang] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TenDonVi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaSoThue] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoTaiKhoan] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HinhThucThanhToan] [tinyint] NULL CONSTRAINT [DF_ThongTinKhachHang_HinhThucThanhToan]  DEFAULT ((0)),
 CONSTRAINT [PK_ThongTinKhachHang] PRIMARY KEY CLUSTERED 
(
	[ThongTinKhachHangGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]











