USE MM
GO
ALTER TABLE ServiceHistory
ADD [KhamTuTuc] [bit] NOT NULL  DEFAULT ((1))
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
                      dbo.ServiceHistory.ServiceHistoryGUID, dbo.ServiceHistory.Status, dbo.ServiceHistory.ActivedDate, dbo.Contact.FullName, 
                      dbo.ServiceHistory.IsExported, dbo.ServiceHistory.Discount, dbo.ServiceHistory.IsNormalOrNegative, dbo.ServiceHistory.Normal, 
                      dbo.ServiceHistory.Abnormal, dbo.ServiceHistory.Negative, dbo.ServiceHistory.Positive, dbo.Services.EnglishName, dbo.Services.Type, 
                      dbo.ServiceHistory.GiaVon, dbo.PatientView.Archived, dbo.PatientView.FullName AS TenBenhNhanChuyenNhuong, 
                      dbo.PatientView.FileNum AS MaBenhNhanChuyenNhuong, dbo.ServiceHistory.RootPatientGUID, dbo.ServiceHistory.DocStaffGUID, 
                      CASE dbo.ServiceHistory.CreatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiTao,
                       CASE dbo.ServiceHistory.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat,
                       dbo.Services.Status AS ServiceStatus, dbo.ServiceHistory.KhamTuTuc
FROM         dbo.Services INNER JOIN
                      dbo.ServiceHistory ON dbo.Services.ServiceGUID = dbo.ServiceHistory.ServiceGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.ServiceHistory.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.ServiceHistory.CreatedBy = dbo.DocStaffView.DocStaffGUID LEFT OUTER JOIN
                      dbo.Contact INNER JOIN
                      dbo.DocStaff ON dbo.Contact.ContactGUID = dbo.DocStaff.ContactGUID ON 
                      dbo.ServiceHistory.DocStaffGUID = dbo.DocStaff.DocStaffGUID LEFT OUTER JOIN
                      dbo.PatientView ON dbo.ServiceHistory.RootPatientGUID = dbo.PatientView.PatientGUID
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
ALTER PROCEDURE [dbo].[spGetCheckList]
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Checked, C.Completed, CAST('' AS nvarchar(255)) AS NguoiChuyenNhuong
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND S.Status = 0 AND
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
	FROM TMP, Services S
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Checked, S.[Name]

	DROP TABLE TMP
END
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go
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
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND S.Status = 0 AND
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
	FROM TMP, Services S
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Using, S.[Name]

	DROP TABLE TMP
END
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

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
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND S.Status = 0 AND
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
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spCheckMemberByService]
	@PatientGUID nvarchar(50),
	@ServiceGUID nvarchar(50),
	@Result int output
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed 
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND S.Status = 0 AND
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
	FROM dbo.ContractMemberView
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





















