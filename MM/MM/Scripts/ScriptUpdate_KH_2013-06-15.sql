USE MM
GO
ALTER TABLE ChiTietMauHoSo
ADD [HopDongGUID] [uniqueidentifier] NOT NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietMauHoSoView]
AS
SELECT     dbo.ChiTietMauHoSo.ChiTietMauHoSoGUID, dbo.ChiTietMauHoSo.MauHoSoGUID, dbo.ChiTietMauHoSo.HopDongGUID, dbo.ChiTietMauHoSo.ServiceGUID, 
                      dbo.Services.Code, dbo.Services.Name
FROM         dbo.ChiTietMauHoSo INNER JOIN
                      dbo.Services ON dbo.ChiTietMauHoSo.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER TABLE ServiceHistory
ADD [SoLuong] [int] NOT NULL DEFAULT ((1))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ServiceHistoryView]
AS
SELECT     dbo.ServiceHistory.PatientGUID, dbo.ServiceHistory.Price AS FixedPrice, dbo.ServiceHistory.Note, dbo.Services.ServiceGUID, dbo.Services.Code, 
                      dbo.Services.Name, dbo.Services.Price, dbo.ServiceHistory.CreatedDate, dbo.ServiceHistory.CreatedBy, dbo.ServiceHistory.UpdatedDate, 
                      dbo.ServiceHistory.UpdatedBy, dbo.ServiceHistory.DeletedDate, dbo.ServiceHistory.DeletedBy, dbo.DocStaff.AvailableToWork, 
                      dbo.ServiceHistory.ServiceHistoryGUID, dbo.ServiceHistory.Status, dbo.ServiceHistory.ActivedDate, dbo.Contact.FullName, dbo.ServiceHistory.IsExported, 
                      dbo.ServiceHistory.Discount, dbo.ServiceHistory.IsNormalOrNegative, dbo.ServiceHistory.Normal, dbo.ServiceHistory.Abnormal, dbo.ServiceHistory.Negative, 
                      dbo.ServiceHistory.Positive, dbo.Services.EnglishName, dbo.Services.Type, dbo.ServiceHistory.GiaVon, dbo.PatientView.Archived, 
                      dbo.PatientView.FullName AS TenBenhNhanChuyenNhuong, dbo.PatientView.FileNum AS MaBenhNhanChuyenNhuong, dbo.ServiceHistory.RootPatientGUID, 
                      dbo.ServiceHistory.DocStaffGUID, 
                      CASE dbo.ServiceHistory.CreatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiTao, 
                      CASE dbo.ServiceHistory.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat, 
                      dbo.Services.Status AS ServiceStatus, dbo.ServiceHistory.KhamTuTuc, dbo.ServiceHistory.HopDongGUID, dbo.ServiceHistory.SoLuong
FROM         dbo.Services INNER JOIN
                      dbo.ServiceHistory ON dbo.Services.ServiceGUID = dbo.ServiceHistory.ServiceGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.ServiceHistory.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.ServiceHistory.CreatedBy = dbo.DocStaffView.DocStaffGUID LEFT OUTER JOIN
                      dbo.Contact INNER JOIN
                      dbo.DocStaff ON dbo.Contact.ContactGUID = dbo.DocStaff.ContactGUID ON dbo.ServiceHistory.DocStaffGUID = dbo.DocStaff.DocStaffGUID LEFT OUTER JOIN
                      dbo.PatientView ON dbo.ServiceHistory.RootPatientGUID = dbo.PatientView.PatientGUID
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
	SH.Price AS Gia, CAST(0 as float) AS Giam, SH.Price * SH.SoLuong AS ThanhTien, '' AS NguoiNhanChuyenNhuong, 
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
	CAST(((S.FixedPrice - (S.FixedPrice * S.Discount)/100) * S.SoLuong) AS float) AS ThanhTien, 
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
	SH.Price AS Gia, SH.Discount AS Giam, CAST(((SH.Price - (SH.Price * SH.Discount)/100) * SH.SoLuong) AS float) AS ThanhTien,
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
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spDoanhThuNhanVienChiTiet]
	@FromDate datetime,
	@ToDate datetime,
	@DocStaffGUID nvarchar(50),
	@Type tinyint --0: ServiceHistory; 1: Receipt
AS
BEGIN
	IF (@Type = 0)
	BEGIN
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, 
		CAST(CAST(DATEPART(year ,ActivedDate) AS nvarchar) + '/' +
		CAST(DATEPART(month ,ActivedDate) AS nvarchar) + '/' +
		CAST(DATEPART(day ,ActivedDate) AS nvarchar) AS datetime) AS  ActivedDate, SUM(S.Price * S.SoLuong) AS Revenue 
		FROM ServiceHistory S WITH(NOLOCK), DocStaffView D WITH(NOLOCK)
		WHERE S.DocStaffGUID = D.DocStaffGUID AND 
		S.ActivedDate BETWEEN @FromDate AND @ToDate AND
		(@DocStaffGUID = '00000000-0000-0000-0000-000000000000' OR S.DocStaffGUID = @DocStaffGUID) AND
		S.Status = 0
		GROUP BY D.DocStaffGUID, D.FullName, CAST(CAST(DATEPART(year ,ActivedDate) AS nvarchar) + '/' +
		CAST(DATEPART(month ,ActivedDate) AS nvarchar) + '/' +
		CAST(DATEPART(day ,ActivedDate) AS nvarchar) AS datetime)
		ORDER BY D.FullName, ActivedDate
	END
	ELSE IF (@Type = 1)
	BEGIN
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, 
		CAST(CAST(DATEPART(year ,ReceiptDate) AS nvarchar) + '/' +
		CAST(DATEPART(month ,ReceiptDate) AS nvarchar) + '/' +
		CAST(DATEPART(day ,ReceiptDate) AS nvarchar) AS datetime) AS  ActivedDate, SUM(S.Price * T.SoLuong) AS Revenue 
		FROM ServiceHistory S WITH(NOLOCK), DocStaffView D WITH(NOLOCK), Receipt R WITH(NOLOCK), ReceiptDetail T WITH(NOLOCK)
		WHERE S.DocStaffGUID = D.DocStaffGUID AND R.ReceiptGUID = T.ReceiptGUID AND
		T.ServiceHistoryGUID = S.ServiceHistoryGUID AND R.Status = 0 AND
		R.ReceiptDate BETWEEN @FromDate AND @ToDate AND
		(@DocStaffGUID = '00000000-0000-0000-0000-000000000000' OR S.DocStaffGUID = @DocStaffGUID) 
		GROUP BY D.DocStaffGUID, D.FullName, CAST(CAST(DATEPART(year ,ReceiptDate) AS nvarchar) + '/' +
		CAST(DATEPART(month ,ReceiptDate) AS nvarchar) + '/' +
		CAST(DATEPART(day ,ReceiptDate) AS nvarchar) AS datetime)
		ORDER BY D.FullName, ActivedDate
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spDoanhThuNhanVienTongHop]
	@FromDate datetime,
	@ToDate datetime,
	@DocStaffGUID nvarchar(50),
	@Type tinyint --0: ServiceHistory; 1: Receipt
AS
BEGIN
	IF (@Type = 0)
	BEGIN
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, SUM(S.Price * S.SoLuong) AS Revenue 
		FROM ServiceHistory S WITH(NOLOCK), DocStaffView D WITH(NOLOCK)
		WHERE S.DocStaffGUID = D.DocStaffGUID AND 
		S.ActivedDate BETWEEN @FromDate AND @ToDate AND
		(@DocStaffGUID = '00000000-0000-0000-0000-000000000000' OR S.DocStaffGUID = @DocStaffGUID) AND
		S.Status = 0
		GROUP BY D.DocStaffGUID, D.FullName
		ORDER BY D.FullName
	END
	ELSE IF (@Type = 1)
	BEGIN
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, SUM(S.Price * T.SoLuong) AS Revenue 
		FROM ServiceHistory S WITH(NOLOCK), DocStaffView D WITH(NOLOCK), Receipt R WITH(NOLOCK), 
		ReceiptDetail T WITH(NOLOCK)
		WHERE S.DocStaffGUID = D.DocStaffGUID AND R.ReceiptGUID = T.ReceiptGUID AND
		T.ServiceHistoryGUID = S.ServiceHistoryGUID AND R.Status = 0 AND
		R.ReceiptDate BETWEEN @FromDate AND @ToDate AND
		(@DocStaffGUID = '00000000-0000-0000-0000-000000000000' OR S.DocStaffGUID = @DocStaffGUID) 
		GROUP BY D.DocStaffGUID, D.FullName
		ORDER BY D.FullName
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


