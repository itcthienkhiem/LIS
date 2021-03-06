USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'93c8826a-ea46-4aa1-8a85-a7fd39e58b14', N'NhanVienTrungLap', N'Nhân viên trùng lắp')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'8fc14604-2f93-4529-93d9-51d48575b47a', N'ChuyenBenhAn', N'Chuyển bệnh án')
GO
ALTER TABLE dbo.CompanyContract
ADD [GiamGiaNam] [float] NOT NULL DEFAULT ((0)),
	[GiamGiaNu] [float] NOT NULL DEFAULT ((0)),
	[GiamGiaNuCoGD] [float] NOT NULL DEFAULT ((0))
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CompanyContractView]
AS
SELECT     dbo.Company.CompanyGUID, dbo.Company.MaCty, dbo.Company.TenCty, dbo.Company.DiaChi, dbo.Company.Dienthoai, dbo.Company.Fax, dbo.Company.Website, 
                      dbo.CompanyContract.CompanyContractGUID, dbo.CompanyContract.ContractName, dbo.CompanyContract.Completed, dbo.CompanyContract.CreatedDate, 
                      dbo.CompanyContract.CreatedBy, dbo.CompanyContract.UpdatedDate, dbo.CompanyContract.UpdatedBy, dbo.CompanyContract.DeletedDate, 
                      dbo.CompanyContract.DeletedBy, dbo.CompanyContract.Status AS ContractStatus, dbo.Company.Status AS CompanyStatus, dbo.CompanyContract.BeginDate, 
                      dbo.CompanyContract.ContractCode, dbo.CompanyContract.EndDate, ISNULL(dbo.Lock.Status, 0) AS Lock, dbo.CompanyContract.SoTien, 
                      dbo.CompanyContract.DatCoc, dbo.Company.MaSoThue, dbo.CompanyContract.NhanSuPhuTrach, dbo.CompanyContract.SoDienThoai, 
                      dbo.CompanyContract.NgayDatCoc, dbo.CompanyContract.GiamGiaNam, dbo.CompanyContract.GiamGiaNu, dbo.CompanyContract.GiamGiaNuCoGD
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID LEFT OUTER JOIN
                      dbo.Lock ON dbo.CompanyContract.CompanyContractGUID = dbo.Lock.KeyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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

	SELECT Loai, Max(SoLuong) AS MaxSoLuong FROM 
		(SELECT PatientGUID, Loai, Count(ServiceGUID) AS SoLuong 
		 FROM #TMP
		 GROUP BY PatientGUID, Loai) T
	GROUP BY Loai

	SELECT DISTINCT ServiceGUID, [Name], Loai
	FROM #TMP
	ORDER BY Loai, [Name]

	DROP TABLE #TMP
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO










