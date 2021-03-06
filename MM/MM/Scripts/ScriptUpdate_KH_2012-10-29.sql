USE MM
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
ALTER PROCEDURE [dbo].[spCapCuuTonKho]
	@TuNgay datetime,
	@DenNgay datetime,
	@MaCapCuuList nvarchar(4000)
AS
BEGIN
	DECLARE @MaCapCuu int
	DECLARE @TenCapCuu nvarchar(255)
	DECLARE @DVT nvarchar(50)
	DECLARE @KhoCapCuuGUID nvarchar(50)
	DECLARE @SoDu INT
	DECLARE @SLNhap INT
	DECLARE @SLXuat INT
	DECLARE @SLTon INT

	DECLARE @CapCuuTonKho table 
	(
		KhoCapCuuGUID nvarchar(50),
		TenCapCuu nvarchar(255),	
		DonViTinh nvarchar(50),
		SoDu int,
		SLNhap int,
		SLXuat int,
		SLTon int
	) 

	DECLARE CapCuu_Cursor CURSOR FOR 
	SELECT Data FROM  dbo.Split(@MaCapCuuList, ',')
	OPEN CapCuu_Cursor
	FETCH NEXT FROM CapCuu_Cursor 
	INTO @MaCapCuu
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SELECT @KhoCapCuuGUID = KhoCapCuuGUID, @TenCapCuu = TenCapCuu, @DVT = DonViTinh
		FROM KhoCapCuu WITH(NOLOCK) WHERE MaCapCuu = @MaCapCuu AND Status = 0
 
		IF (@KhoCapCuuGUID IS NOT NULL)
		BEGIN
			--SL Nhap Truoc Tu Ngay
			SET @SLNhap = (SELECT SUM(SoLuongNhap * SoLuongQuiDoi) FROM NhapKhoCapCuu WITH(NOLOCK)
			WHERE KhoCapCuuGUID = @KhoCapCuuGUID AND Status = 0 AND NgayNhap < @TuNgay)
			IF (@SLNhap IS NULL) SET @SLNhap = 0
			--PRINT @SLNhap

			--SL Xuat Truoc Tu Ngay
			SET @SLXuat = (SELECT SUM(SoLuong) FROM XuatKhoCapCuu WITH(NOLOCK)
			WHERE Status = 0 AND KhoCapCuuGUID = @KhoCapCuuGUID AND
			NgayXuat < @TuNgay)

			IF (@SLXuat IS NULL) SET @SLXuat = 0
			--PRINT @SLXuat

			-- So Du Dau
			SET @SoDu = @SLNhap - @SLXuat
			--Print @SoDu	

			--SL Nhap Trong Khoang Tu Ngay - Den Ngay
			SET @SLNhap = (SELECT SUM(SoLuongNhap * SoLuongQuiDoi) FROM NhapKhoCapCuu WITH(NOLOCK)
			WHERE KhoCapCuuGUID = @KhoCapCuuGUID AND Status = 0 AND NgayNhap BETWEEN @TuNgay AND @DenNgay)
			IF (@SLNhap IS NULL) SET @SLNhap = 0
			
			--SL Xuat Trong Khoang Tu Ngay - Den Ngay
			SET @SLXuat = (SELECT SUM(SoLuong) FROM XuatKhoCapCuu WITH(NOLOCK)
			WHERE Status = 0 AND KhoCapCuuGUID = @KhoCapCuuGUID AND
			NgayXuat BETWEEN @TuNgay AND @DenNgay)
			IF (@SLXuat IS NULL) SET @SLXuat = 0

			--SL Ton
			SET @SLTon = @SoDu + @SLNhap - @SLXuat

			INSERT INTO @CapCuuTonKho
			SELECT @KhoCapCuuGUID, @TenCapCuu, @DVT, @SoDu, @SLNhap, @SLXuat, @SLTon
		END
	    
		FETCH NEXT FROM CapCuu_Cursor 
		INTO @MaCapCuu
	END 
	CLOSE CapCuu_Cursor
	DEALLOCATE CapCuu_Cursor

	SELECT * FROM @CapCuuTonKho ORDER BY TenCapCuu
END
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
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
ALTER PROCEDURE [dbo].[spCheckContractByService]
	@ContractGUID nvarchar(50),
	@ServiceGUID nvarchar(50),
	@Result int output
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed, M.PatientGUID 
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMember M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK), Services S WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	--M.PatientGUID = @PatientGUID AND 
	L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate) AND
	L.ServiceGUID = @ServiceGUID AND C.CompanyContractGUID = @ContractGUID

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = TMP.PatientGUID AND S.Status = 0  AND 
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate) AND
	S.ServiceGUID = @ServiceGUID

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = TMP.PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate) AND
	S.ServiceGUID = @ServiceGUID

	DECLARE @ServiceCount int
	DECLARE @UsingCount int
	SET @ServiceCount = 0
	SET @UsingCount = 0

	SET @ServiceCount = (SELECT Count(*) FROM TMP)
	SET @UsingCount = (SELECT Count(*) FROM TMP WHERE Checked = 'True')

	IF (@UsingCount = 0)
		SET @Result = 0
	ELSE IF (@UsingCount < @ServiceCount)
		SET @Result = 1
	ELSE
		SET @Result = 2

	DROP TABLE TMP
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
ALTER PROCEDURE [dbo].[spCheckMember]
	@PatientGUID nvarchar(50),
	@Result int output
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed 
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMember M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK), Services S WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND 
	L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0  AND 
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	DECLARE @ServiceCount int
	DECLARE @UsingCount int
	SET @ServiceCount = 0
	SET @UsingCount = 0

	SET @ServiceCount = (SELECT Count(*) FROM TMP)
	SET @UsingCount = (SELECT Count(*) FROM TMP WHERE Checked = 'True')

	IF (@UsingCount = 0)
		SET @Result = 0
	ELSE IF (@UsingCount < @ServiceCount)
		SET @Result = 1
	ELSE
		SET @Result = 2

	DROP TABLE TMP
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
ALTER PROCEDURE [dbo].[spCheckMemberByService]
	@PatientGUID nvarchar(50),
	@ServiceGUID nvarchar(50),
	@Result int output
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed 
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMember M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK), Services S WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND 
	L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate) AND
	L.ServiceGUID = @ServiceGUID

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0  AND 
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate) AND
	S.ServiceGUID = @ServiceGUID

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate) AND
	S.ServiceGUID = @ServiceGUID

	DECLARE @ServiceCount int
	DECLARE @UsingCount int
	SET @ServiceCount = 0
	SET @UsingCount = 0

	SET @ServiceCount = (SELECT Count(*) FROM TMP)
	SET @UsingCount = (SELECT Count(*) FROM TMP WHERE Checked = 'True')

	IF (@UsingCount = 0)
		SET @Result = 0
	ELSE IF (@UsingCount < @ServiceCount)
		SET @Result = 1
	ELSE
		SET @Result = 2

	DROP TABLE TMP
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
ALTER PROCEDURE [dbo].[spDanhSachNhanVienChuaDenKham]
	@FromDate datetime,
	@ToDate datetime,
	@MaBenhNhan nvarchar(50)
AS
BEGIN
	
	SELECT P.PatientGUID, NULL AS NgayKham, P.FileNum, P.FullName, P.DobStr, P.GenderAsStr, P.Address, P.Mobile
	FROM PatientView P WITH(NOLOCK)
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
ALTER PROCEDURE [dbo].[spDanhSachNhanVienDenKham]
	@FromDate datetime,
	@ToDate datetime,
	@MaBenhNhan nvarchar(50)
AS
BEGIN
	
	SELECT P.PatientGUID, Max(S.ActivedDate) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile
	INTO #TEMP
	FROM PatientView P WITH(NOLOCK), ServiceHistory S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.ActivedDate BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayCanDo) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), CanDo S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayCanDo BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile	
	UNION
	SELECT P.PatientGUID, Max(S.NgayKetLuan) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetLuan S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKetLuan BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaLamSang S WITH(NOLOCK)
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaNoiSoi S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaSoiCTC S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaXetNghiem_CellDyn3200 S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaXetNghiem_Hitachi917 S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayXN) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaXetNghiem_Manual S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayXN BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.Ngay) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), LoiKhuyen S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.Ngay BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKeToa) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), ToaThuoc S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.BenhNhan AND 
	S.NgayKeToa BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT PatientGUID, NgayKham, FileNum, FullName, DobStr, GenderAsStr, Address, Mobile
	FROM PatientView WITH(NOLOCK)
	WHERE NgayKham IS NOT NULL AND NgayKham BETWEEN @FromDate AND @ToDate AND
	FileNum LIKE N'' + @MaBenhNhan + '%' AND Archived = 'False'  

	SELECT PatientGUID, Max(NgayKham) AS NgayKham, FileNum, FullName, 
	DobStr, GenderAsStr, Address, Mobile 
	FROM #TEMP
	GROUP BY PatientGUID, FullName, FileNum, GenderAsStr, Address, DobStr, Mobile
	ORDER BY NgayKham
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
ALTER PROCEDURE [dbo].[spDichVuHopDong]
	@ContractGUID nvarchar(50),
	@TuNgay dateTime,
	@DenNgay datetime,
	@Type int --0: All, 1: Chua kham; 2: Da kham
AS
BEGIN
	DECLARE @BeginDate datetime
	DECLARE @EndDate  datetime
	DECLARE @FromDate  datetime
	DECLARE @ToDate  datetime

	SET @FromDate = @TuNgay
	SET @ToDate = @DenNgay

	SELECT @BeginDate = BeginDate, @EndDate = EndDate FROM CompanyContract WITH(NOLOCK)
	WHERE CompanyContractGUID = @ContractGUID

	IF (@TuNgay < @BeginDate) 
	BEGIN
		SET @TuNgay = @BeginDate
	END

	IF (@EndDate IS NOT NULL AND @DenNgay > @EndDate)
	BEGIN
		SET @DenNgay = @EndDate
	END
	
	IF (@Type = 0)
	BEGIN
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		NULL AS NgayKham, FirstName, Mobile, N'Chưa khám' AS TinhTrang
		FROM ContractMemberView M WITH(NOLOCK), CompanyContract C WITH(NOLOCK)
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		C.CompanyContractGUID = @ContractGUID AND 
		NOT EXISTS (SELECT TOP 1 * FROM ServiceHistory WHERE PatientGUID = M.PatientGUID AND
		ActivedDate BETWEEN @TuNgay AND @DenNgay AND ServiceHistory.Status = 0) AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		UNION
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		Min(ActivedDate) AS NgayKham, FirstName, Mobile, N'Đã khám' AS TinhTrang
		FROM ContractMemberView M WITH(NOLOCK), CompanyContract C WITH(NOLOCK), ServiceHistory S WITH(NOLOCK)
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		M.PatientGUID = S.PatientGUID AND
		C.CompanyContractGUID = @ContractGUID AND 
		S.ActivedDate BETWEEN @TuNgay AND @DenNgay AND S.Status = 0 AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		GROUP BY ContractName, FullName, FirstName, Mobile
		ORDER BY FirstName, FullName
	END
	ELSE IF (@Type = 1)
	BEGIN
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		NULL AS NgayKham, FirstName, Mobile, N'Chưa khám' AS TinhTrang
		FROM ContractMemberView M WITH(NOLOCK), CompanyContract C WITH(NOLOCK)
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		C.CompanyContractGUID = @ContractGUID AND 
		NOT EXISTS (SELECT TOP 1 * FROM ServiceHistory WHERE PatientGUID = M.PatientGUID AND
		ActivedDate BETWEEN @TuNgay AND @DenNgay AND ServiceHistory.Status = 0) AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		ORDER BY FirstName, FullName
	END
	ELSE
	BEGIN
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		Min(ActivedDate) AS NgayKham, FirstName, Mobile, N'Đã khám' AS TinhTrang
		FROM ContractMemberView M WITH(NOLOCK), CompanyContract C WITH(NOLOCK), ServiceHistory S WITH(NOLOCK)
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		M.PatientGUID = S.PatientGUID AND
		C.CompanyContractGUID = @ContractGUID AND 
		S.ActivedDate BETWEEN @TuNgay AND @DenNgay AND S.Status = 0 AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		GROUP BY ContractName, FullName, FirstName, Mobile
		ORDER BY FirstName, FullName
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

ALTER PROCEDURE [dbo].[spDichVuTuTuc]
	@TuNgay dateTime,
	@DenNgay datetime
AS
BEGIN
	SELECT @TuNgay AS TuNgay, @DenNgay AS DenNgay, P.FullName, P.FirstName, 
	Min(S.ActivedDate) AS NgayKham, ISNULL(P.CompanyName, N'Tự túc') AS CompanyName, P.Mobile  
	FROM ServiceHistory S WITH(NOLOCK), PatientView P WITH(NOLOCK)
	WHERE S.PatientGUID = P.PatientGUID AND
	S.ActivedDate BETWEEN @TuNgay AND @DenNgay AND S.Status = 0 AND
	S.Status = 0 AND P.Archived = 'False' AND NOT EXISTS 
	(SELECT TOP 1 L.ServiceGUID
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMember M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = P.PatientGUID AND L.ServiceGUID = S.ServiceGUID AND
	M.Status = 0 AND L.Status = 0 AND CM.Status = 0 AND C.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= S.ActivedDate OR 
	C.Completed = 'True' AND S.ActivedDate BETWEEN C.BeginDate AND C.EndDate))
	GROUP BY P.FullName, P.FirstName, ISNULL(P.CompanyName, N'Tự túc'), P.Mobile
	ORDER BY P.FirstName, P.FullName
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
		CAST(DATEPART(day ,ActivedDate) AS nvarchar) AS datetime) AS  ActivedDate, SUM(S.Price) AS Revenue 
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
		CAST(DATEPART(day ,ReceiptDate) AS nvarchar) AS datetime) AS  ActivedDate, SUM(S.Price) AS Revenue 
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
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, SUM(S.Price) AS Revenue 
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
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, SUM(S.Price) AS Revenue 
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


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spGetCheckList]
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Checked, C.Completed, CAST('' AS nvarchar(255)) AS NguoiChuyenNhuong
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMember M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK), Services S WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	L.Status = 0 AND C.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	UPDATE TMP
	SET Checked = 'True', NguoiChuyenNhuong = P.FullName 
	FROM ServiceHistory S, PatientView P
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = P.PatientGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, 
	Code, [Name], BeginDate, EndDate, Checked, NguoiChuyenNhuong
	FROM TMP, Services S WITH(NOLOCK)
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Checked, S.[Name]

	DROP TABLE TMP
END
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
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
ALTER PROCEDURE [dbo].[spGetCheckListByContract]
	@ContractGUID nvarchar(50),
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Using, C.Completed, CAST('' AS nvarchar(255)) AS NguoiChuyenNhuong
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMember M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK), Services S WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	L.Status = 0 AND C.CompanyContractGUID = @ContractGUID

	UPDATE TMP
	SET Using = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	UPDATE TMP
	SET Using = 'True', NguoiChuyenNhuong = P.FullName 
	FROM ServiceHistory S, PatientView P
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = P.PatientGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name], EnglishName,
	BeginDate, EndDate, Using, CAST(0 AS Bit) AS Checked, NguoiChuyenNhuong
	FROM TMP, Services S WITH(NOLOCK)
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Using, S.[Name]

	DROP TABLE TMP
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
ALTER PROCEDURE [dbo].[spGetDanhSachNhanVien]
	@ContractGUID nvarchar(50),
	@Type int
AS
BEGIN
	DECLARE @PatientGUID nvarchar(50)
	DECLARE @FileNum nvarchar(50)
	DECLARE @FullName nvarchar(255)
	DECLARE @Dob nvarchar(50)
	DECLARE @Gender nvarchar(3)
	DECLARE @Result int

	CREATE TABLE #TEMP
	(
		PatientGUID nvarchar(50),
		FileNum nvarchar(50),
		FullName nvarchar(255),
		Dob nvarchar(50),
		Gender nvarchar(3)
	)	

	DECLARE db_cursor CURSOR FOR  
	SELECT PatientGUID, FileNum, FullName, DobStr, GenderAsStr
	FROM dbo.ContractMemberView WITH(NOLOCK)
	WHERE CompanyContractGUID = @ContractGUID AND Status = 0 AND 
	CompanyMemberStatus = 0 --AND Archived = 'False'
	
	OPEN db_cursor   
	FETCH NEXT FROM db_cursor INTO @PatientGUID, @FileNum, @FullName, @Dob, @Gender 
	WHILE @@FETCH_STATUS = 0   
	BEGIN   
		EXEC dbo.spCheckMember @PatientGUID, @Result output
		
		IF(@Result = @Type)
		BEGIN
			INSERT INTO #TEMP
			VALUES (@PatientGUID, @FileNum, @FullName, @Dob, @Gender) 				
		END
		
		FETCH NEXT FROM db_cursor INTO @PatientGUID, @FileNum, @FullName, @Dob, @Gender    
	END   

	CLOSE db_cursor   
	DEALLOCATE db_cursor

	SELECT * FROM #TEMP ORDER BY FullName
	
	DROP TABLE #TEMP
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
ALTER PROCEDURE [dbo].[spMerge2Patients] 
(
	@KeepGUID nvarchar(50),
	@MergedGUID nvarchar(50),
	@DoneByGUID nvarchar(50)
)
AS
BEGIN TRANSACTION MergePatient
	--update CompanyMember
	UPDATE dbo.CompanyMember set PatientGUID=@KeepGUID  where PatientGUID=@MergedGUID
	IF @@ERROR <> 0
	BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.ContractMember Set CompanyMemberGUID=@KeepGUID  where CompanyMemberGUID=@MergedGUID
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.Receipt set PatientGUID = @KeepGUID where PatientGUID = @MergedGUID
	IF @@ERROR <> 0
	BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.ServiceHistory set PatientGUID=@KeepGUID  where PatientGUID = @MergedGUID
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.ToaThuoc set BenhNhan=@KeepGUID  where BenhNhan = @MergedGUID
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.PhieuThuThuoc set MaBenhNhan=@KeepGUID  where MaBenhNhan = @MergedGUID
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END

	--set deleted for 2nd patietn
	Update dbo.Contact Set Archived=1, DeletedDate = GETDATE(),DeletedBy=@DoneByGUID, Note= Note + ' Merged with patient ' + @KeepGUID   where ContactGUID= (select ContactGUID from dbo.Patient where PatientGUID= @MergedGUID)
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	COMMIT TRANSACTION MergePatient;
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
ALTER PROCEDURE [dbo].[spThuocTonKho]
	@TuNgay datetime,
	@DenNgay datetime,
	@MaThuocs nvarchar(4000)
AS
BEGIN
	DECLARE @MaThuoc nvarchar(100)
	DECLARE @TenThuoc nvarchar(255)
	DECLARE @DVT nvarchar(50)
	DECLARE @ThuocGUID nvarchar(50)
	DECLARE @SoDu INT
	DECLARE @SLNhap INT
	DECLARE @SLXuat INT
	DECLARE @SLTon INT

	DECLARE @ThuocTonKho table 
	(
		ThuocGUID nvarchar(50),
		MaThuoc nvarchar(100),
		TenThuoc nvarchar(255),	
		DonViTinh nvarchar(50),
		SoDu int,
		SLNhap int,
		SLXuat int,
		SLTon int
	) 

	DECLARE Thuoc_Cursor CURSOR FOR 
	SELECT Data FROM  dbo.Split(@MaThuocs, ',') --ORDER BY Data
	OPEN Thuoc_Cursor
	FETCH NEXT FROM Thuoc_Cursor 
	INTO @MaThuoc

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SELECT @ThuocGUID = ThuocGUID, @TenThuoc = TenThuoc, @DVT = DonViTinh
		FROM Thuoc WITH(NOLOCK) WHERE MaThuoc = @MaThuoc AND Status = 0

		IF (@ThuocGUID IS NOT NULL)
		BEGIN
			--SL Nhap Truoc Tu Ngay
			SET @SLNhap = (SELECT SUM(SoLuongNhap * SoLuongQuiDoi) FROM LoThuoc WITH(NOLOCK)
			WHERE ThuocGUID = @ThuocGUID AND Status = 0 AND CreatedDate < @TuNgay)
			IF (@SLNhap IS NULL) SET @SLNhap = 0
			--PRINT @SLNhap

			--SL Xuat Truoc Tu Ngay
			SET @SLXuat = (SELECT SUM(CT.SoLuong) FROM PhieuThuThuoc PT WITH(NOLOCK), 
			ChiTietPhieuThuThuoc CT WITH(NOLOCK)
			WHERE PT.PhieuThuThuocGUID = CT.PhieuThuThuocGUID AND
			PT.Status = 0 AND CT.Status = 0 AND CT.ThuocGUID = @ThuocGUID AND
			PT.NgayThu < @TuNgay)

			IF (@SLXuat IS NULL) SET @SLXuat = 0
			--PRINT @SLXuat

			-- So Du Dau
			SET @SoDu = @SLNhap - @SLXuat
			--Print @SoDu	

			--SL Nhap Trong Khoang Tu Ngay - Den Ngay
			SET @SLNhap = (SELECT SUM(SoLuongNhap * SoLuongQuiDoi) FROM LoThuoc WITH(NOLOCK)
			WHERE ThuocGUID = @ThuocGUID AND Status = 0 AND CreatedDate BETWEEN @TuNgay AND @DenNgay)
			IF (@SLNhap IS NULL) SET @SLNhap = 0
			
			--SL Xuat Trong Khoang Tu Ngay - Den Ngay
			SET @SLXuat = (SELECT SUM(CT.SoLuong) FROM PhieuThuThuoc PT WITH(NOLOCK), 
			ChiTietPhieuThuThuoc CT WITH(NOLOCK)
			WHERE PT.PhieuThuThuocGUID = CT.PhieuThuThuocGUID AND
			PT.Status = 0 AND CT.Status = 0 AND CT.ThuocGUID = @ThuocGUID AND
			PT.NgayThu BETWEEN @TuNgay AND @DenNgay)
			IF (@SLXuat IS NULL) SET @SLXuat = 0

			--SL Ton
			SET @SLTon = @SoDu + @SLNhap - @SLXuat

			INSERT INTO @ThuocTonKho
			SELECT @ThuocGUID, @MaThuoc, @TenThuoc, @DVT, @SoDu, @SLNhap, @SLXuat, @SLTon
		END
	    
		FETCH NEXT FROM Thuoc_Cursor 
		INTO @MaThuoc
	END 
	CLOSE Thuoc_Cursor
	DEALLOCATE Thuoc_Cursor

	SELECT * FROM @ThuocTonKho ORDER BY TenThuoc
END
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

















