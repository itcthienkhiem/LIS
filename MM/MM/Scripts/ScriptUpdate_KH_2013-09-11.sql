USE MM
GO
UPDATE [dbo].[Function]
SET [FunctionName] = N'Thay đổi số hóa đơn xét nghiệm'
WHERE [FunctionGUID] = N'79a8ec40-b3da-46b7-ac13-23f43388263f'
GO
UPDATE [dbo].[Function]
SET [FunctionName] = N'Thay đổi số hóa đơn'
WHERE [FunctionGUID] = N'9d7bdbd5-60fc-40b3-b041-b1f36c0eaddb'
GO
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
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TMP') AND type in (N'U'))
		DROP TABLE TMP

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
	((TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate) OR
	(TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate))

	UPDATE TMP
	SET Using = 'True', NguoiChuyenNhuong = P.FullName 
	FROM ServiceHistory S, PatientView P
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = P.PatientGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	((TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate) OR
	(TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate))

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name], EnglishName,
	BeginDate, EndDate, Using, CAST(0 AS Bit) AS Checked, NguoiChuyenNhuong
	FROM TMP, Services S WITH(NOLOCK)
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
ALTER PROCEDURE [dbo].[spGetCheckList]
	@PatientGUID nvarchar(50)
AS
BEGIN
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TMP') AND type in (N'U'))
		DROP TABLE TMP

	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Checked, C.Completed, CAST('' AS nvarchar(255)) AS NguoiChuyenNhuong
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMember M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK), Services S WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	L.Status = 0 AND C.Status = 0 AND
	((C.Completed = 'False' AND C.BeginDate <= GetDate()) OR 
	 (C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate))

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	((TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate) OR
	(TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate))

	UPDATE TMP
	SET Checked = 'True', NguoiChuyenNhuong = P.FullName 
	FROM ServiceHistory S, PatientView P
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = P.PatientGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	((TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate) OR
	(TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate))

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
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



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
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TMP') AND type in (N'U'))
		DROP TABLE TMP

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
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TMP') AND type in (N'U'))
		DROP TABLE TMP

	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed 
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMember M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK), Services S WITH(NOLOCK)
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
ALTER PROCEDURE [dbo].[spCheckContractByService]
	@ContractGUID nvarchar(50),
	@ServiceGUID nvarchar(50),
	@Result int output
AS
BEGIN
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TMP') AND type in (N'U'))
		DROP TABLE TMP

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






