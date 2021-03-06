USE MM
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER PROCEDURE [dbo].[spCongNoHopDong]
	@HopDongGUID nvarchar(50)
AS
BEGIN
	SELECT PV.PatientGUID, PV.FirstName, PV.FullName, PV.DobStr, PV.GenderAsStr, S.ServiceGUID, S.[Name], 
	SH.Price AS Gia, CAST(0 as float) AS Giam, SH.Price AS ThanhTien, '' AS NguoiNhanChuyenNhuong, 
	CAST(1 AS bit) AS ChuaThuTien, CAST(0 as INT) AS Loai, PV.Tinh_Trang_Gia_Dinh
	INTO #TMP
	FROM CompanyContract HD WITH(NOLOCK), ContractMember CM WITH(NOLOCK), 
	CompanyMember NV WITH(NOLOCK), PatientView PV WITH(NOLOCK), 
	CompanyCheckList CL WITH(NOLOCK), ServiceHistory SH WITH(NOLOCK), Services S WITH(NOLOCK) 
	WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND 
	CM.CompanyMemberGUID = NV.CompanyMemberGUID AND 
	CL.ContractMemberGUID = CM.ContractMemberGUID AND 
	CL.ServiceGUID = SH.ServiceGUID AND NV.PatientGUID = SH.PatientGUID AND 
	SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND 
	PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND 
	S.Status = 0 AND SH.Status = 0 AND 
	HD.CompanyContractGUID = @HopDongGUID AND 
	SH.IsExported = 'False' AND SH.KhamTuTuc = 'False' AND 
	((HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate) OR 
	(HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate)) 
	UNION
	SELECT C.PatientGUID, C.FirstName, C.FullName, C.DobStr, C.GenderAsStr, S.ServiceGUID, S.[Name], 
	S.FixedPrice AS Gia, S.Discount AS Giam, 
	CAST((S.FixedPrice - (S.FixedPrice * S.Discount)/100) AS float) AS ThanhTien, 
	'' AS NguoiNhanChuyenNhuong, ISNULL(R.ChuaThuTien, CAST(1 AS BIT)) AS ChuaThuTien , 
	CAST(2 as INT) AS Loai, C.Tinh_Trang_Gia_Dinh
	FROM  dbo.Receipt R WITH(NOLOCK) INNER JOIN
      dbo.ReceiptDetail D WITH(NOLOCK) ON R.ReceiptGUID = D.ReceiptGUID RIGHT OUTER JOIN
      dbo.ServiceHistoryView S WITH(NOLOCK) ON D.ServiceHistoryGUID = S.ServiceHistoryGUID INNER JOIN
	  ContractMemberView C WITH(NOLOCK) ON S.PatientGUID = C.PatientGUID
	WHERE S.HopDongGUID = @HopDongGUID AND 
	C.CompanyContractGUID = @HopDongGUID AND
	S.Status = 0 AND S.ServiceStatus = 0 AND C.Archived = 'False' AND
	(R.ChuaThuTien IS NULL OR R.ChuaThuTien=1) AND 
	(R.Status IS NULL OR R.Status=0) AND (D.Status IS NULL OR D.Status=0)
	UNION
	SELECT PV.PatientGUID, PV.FirstName, PV.FullName, PV.DobStr, PV.GenderAsStr, S.ServiceGUID,S.[Name], 
	SH.Price AS Gia, SH.Discount AS Giam, CAST((SH.Price - (SH.Price * SH.Discount)/100) AS float) AS ThanhTien,
	PV2.FullName AS NguoiNhanChuyenNhuong, CAST(1 AS bit) AS ChuaThuTien, CAST(1 as INT) AS Loai, PV.Tinh_Trang_Gia_Dinh
	FROM CompanyContract HD WITH(NOLOCK), ContractMember CM WITH(NOLOCK), 
	CompanyMember NV WITH(NOLOCK), PatientView PV WITH(NOLOCK), 
	CompanyCheckList CL WITH(NOLOCK), ServiceHistory SH WITH(NOLOCK), 
	Services S WITH(NOLOCK), PatientView PV2 WITH(NOLOCK) 
	WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND 
	CM.CompanyMemberGUID = NV.CompanyMemberGUID AND CL.ContractMemberGUID = CM.ContractMemberGUID AND
	 CL.ServiceGUID = SH.ServiceGUID AND  NV.PatientGUID = SH.RootPatientGUID AND 
	SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND 
	PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND 
	S.Status = 0 AND SH.Status = 0 AND HD.CompanyContractGUID = @HopDongGUID AND 
	SH.IsExported = 'False' AND SH.KhamTuTuc = 'True' AND 
	((HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate) OR 
	(HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate)) AND 
	SH.PatientGUID = PV2.PatientGUID

	SELECT * FROM #TMP
	ORDER BY FirstName, FullName, PatientGUID, Loai, [Name] 

	SELECT Loai, Count(ServiceGUID) AS MaxSoLuong FROM 
		(SELECT DISTINCT Loai, ServiceGUID
		 FROM #TMP) T
	GROUP BY Loai

	SELECT DISTINCT ServiceGUID, [Name], Loai
	FROM #TMP
	ORDER BY Loai, [Name]

	DROP TABLE #TMP
END

