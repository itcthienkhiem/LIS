USE MM
GO
ALTER TABLE Patient
ADD [NgayKham] [datetime] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PatientView]
AS
SELECT     dbo.Contact.ContactGUID, dbo.Contact.Title, dbo.Contact.FirstName, dbo.Contact.SurName, dbo.Contact.KnownAs, dbo.Contact.MiddleName, 
                      dbo.Contact.AliasFirstName, dbo.Contact.AliasSurName, dbo.Contact.Dob, dbo.Contact.PreferredName, dbo.Contact.Occupation, dbo.Contact.IdentityCard, 
                      dbo.Contact.Archived, dbo.Contact.DateArchived, dbo.Contact.Note, dbo.Contact.HomePhone, dbo.Contact.WorkPhone, dbo.Contact.Mobile, dbo.Contact.Email, 
                      dbo.Contact.FAX, dbo.Contact.CreatedDate, dbo.Contact.CreatedBy, dbo.Contact.UpdatedDate, dbo.Contact.UpdatedBy, dbo.Contact.DeletedDate, 
                      dbo.Contact.DeletedBy, dbo.Contact.Gender, dbo.Contact.Address, dbo.Contact.Ward, dbo.Contact.District, dbo.Contact.City, dbo.Patient.FileNum, dbo.Patient.BarCode, 
                      dbo.Patient.Picture, dbo.Patient.HearFrom, dbo.Patient.Salutation, dbo.Patient.LastSeenDate, dbo.Patient.LastSeenDocGUID, dbo.Patient.DateDeceased, 
                      dbo.Patient.LastVisitGUID, CASE Gender WHEN 0 THEN N'Nam' WHEN 1 THEN N'Nữ' WHEN 2 THEN N'' END AS GenderAsStr, dbo.Patient.PatientGUID, 
                      dbo.Contact.DobStr, dbo.Contact.FullName, dbo.PatientHistory.Di_Ung_Thuoc, dbo.PatientHistory.Thuoc_Di_Ung, dbo.PatientHistory.Dot_Quy, 
                      dbo.PatientHistory.Benh_Tim_Mach, dbo.PatientHistory.Benh_Lao, dbo.PatientHistory.Dai_Thao_Duong, dbo.PatientHistory.Dai_Duong_Dang_Dieu_Tri, 
                      dbo.PatientHistory.Viem_Gan_B, dbo.PatientHistory.Viem_Gan_C, dbo.PatientHistory.Viem_Gan_Dang_Dieu_Tri, dbo.PatientHistory.Ung_Thu, 
                      dbo.PatientHistory.Co_Quan_Ung_Thu, dbo.PatientHistory.Dong_Kinh, dbo.PatientHistory.Hen_Suyen, dbo.PatientHistory.Benh_Khac, dbo.PatientHistory.Benh_Gi, 
                      dbo.PatientHistory.Thuoc_Dang_Dung, dbo.PatientHistory.Hut_Thuoc, dbo.PatientHistory.Uong_Ruou, dbo.PatientHistory.Tinh_Trang_Gia_Dinh, 
                      dbo.PatientHistory.Chich_Ngua_Viem_Gan_B, dbo.PatientHistory.Chich_Ngua_Uon_Van, dbo.PatientHistory.Chich_Ngua_Cum, dbo.PatientHistory.Dang_Co_Thai, 
                      dbo.PatientHistory.PatientHistoryGUID, dbo.Contact.Source, dbo.Contact.CompanyName, dbo.Patient.NgayKham
FROM         dbo.Contact INNER JOIN
                      dbo.Patient ON dbo.Contact.ContactGUID = dbo.Patient.ContactGUID INNER JOIN
                      dbo.PatientHistory ON dbo.Patient.PatientGUID = dbo.PatientHistory.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
	P.DobStr, P.GenderAsStr, P.Address 
	INTO #TEMP
	FROM PatientView P, ServiceHistory S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.ActivedDate BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr 
	UNION
	SELECT P.PatientGUID, Max(S.NgayCanDo) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, CanDo S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayCanDo BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr	
	UNION
	SELECT P.PatientGUID, Max(S.NgayKetLuan) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, KetLuan S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKetLuan BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, KetQuaLamSang S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, KetQuaNoiSoi S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, KetQuaSoiCTC S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, KetQuaXetNghiem_CellDyn3200 S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, KetQuaXetNghiem_Hitachi917 S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, KetQuaXetNghiem_Manual S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT P.PatientGUID, Max(S.Ngay) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, LoiKhuyen S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.Ngay BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT P.PatientGUID, Max(S.NgayKeToa) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, ToaThuoc S 
	WHERE P.PatientGUID = S.BenhNhan AND 
	S.NgayKeToa BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr
	UNION
	SELECT PatientGUID, NgayKham, FileNum, FullName, DobStr, GenderAsStr, Address
	FROM PatientView
	WHERE NgayKham IS NOT NULL AND NgayKham BETWEEN @FromDate AND @ToDate AND
	FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False'  

	SELECT PatientGUID, Max(NgayKham) AS NgayKham, FileNum, FullName, 
	DobStr, GenderAsStr, Address 
	FROM #TEMP
	GROUP BY PatientGUID, FullName, FileNum, GenderAsStr, Address, DobStr
	ORDER BY NgayKham
END




