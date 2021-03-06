USE [MM]
GO
ALTER TABLE CompanyContract
ADD EndDate datetime NULL
GO
ALTER TABLE Contact
ADD CompanyName nvarchar(255) NULL
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'28700972-7aca-46dd-b765-243a571a6269', N'DichVuTuTuc', N'Báo cáo dịch vụ tự túc')
GO


ALTER VIEW [dbo].[CompanyContractView]
AS
SELECT     dbo.Company.CompanyGUID, dbo.Company.MaCty, dbo.Company.TenCty, dbo.Company.DiaChi, dbo.Company.Dienthoai, dbo.Company.Fax, 
                      dbo.Company.Website, dbo.CompanyContract.CompanyContractGUID, dbo.CompanyContract.ContractName, dbo.CompanyContract.Completed, 
                      dbo.CompanyContract.CreatedDate, dbo.CompanyContract.CreatedBy, dbo.CompanyContract.UpdatedDate, dbo.CompanyContract.UpdatedBy, 
                      dbo.CompanyContract.DeletedDate, dbo.CompanyContract.DeletedBy, dbo.CompanyContract.Status AS ContractStatus, 
                      dbo.Company.Status AS CompanyStatus, dbo.CompanyContract.BeginDate, dbo.CompanyContract.ContractCode, dbo.CompanyContract.EndDate
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PatientView]
AS
SELECT     dbo.Contact.ContactGUID, dbo.Contact.Title, dbo.Contact.FirstName, dbo.Contact.SurName, dbo.Contact.KnownAs, dbo.Contact.MiddleName, 
                      dbo.Contact.AliasFirstName, dbo.Contact.AliasSurName, dbo.Contact.Dob, dbo.Contact.PreferredName, dbo.Contact.Occupation, 
                      dbo.Contact.IdentityCard, dbo.Contact.Archived, dbo.Contact.DateArchived, dbo.Contact.Note, dbo.Contact.HomePhone, dbo.Contact.WorkPhone, 
                      dbo.Contact.Mobile, dbo.Contact.Email, dbo.Contact.FAX, dbo.Contact.CreatedDate, dbo.Contact.CreatedBy, dbo.Contact.UpdatedDate, 
                      dbo.Contact.UpdatedBy, dbo.Contact.DeletedDate, dbo.Contact.DeletedBy, dbo.Contact.Gender, dbo.Contact.Address, dbo.Contact.Ward, 
                      dbo.Contact.District, dbo.Contact.City, dbo.Patient.FileNum, dbo.Patient.BarCode, dbo.Patient.Picture, dbo.Patient.HearFrom, dbo.Patient.Salutation, 
                      dbo.Patient.LastSeenDate, dbo.Patient.LastSeenDocGUID, dbo.Patient.DateDeceased, dbo.Patient.LastVisitGUID, 
                      CASE Gender WHEN 0 THEN N'Nam' WHEN 1 THEN N'Nữ' END AS GenderAsStr, dbo.Patient.PatientGUID, dbo.Contact.DobStr, dbo.Contact.FullName, 
                      dbo.PatientHistory.Di_Ung_Thuoc, dbo.PatientHistory.Thuoc_Di_Ung, dbo.PatientHistory.Dot_Quy, dbo.PatientHistory.Benh_Tim_Mach, 
                      dbo.PatientHistory.Benh_Lao, dbo.PatientHistory.Dai_Thao_Duong, dbo.PatientHistory.Dai_Duong_Dang_Dieu_Tri, dbo.PatientHistory.Viem_Gan_B, 
                      dbo.PatientHistory.Viem_Gan_C, dbo.PatientHistory.Viem_Gan_Dang_Dieu_Tri, dbo.PatientHistory.Ung_Thu, dbo.PatientHistory.Co_Quan_Ung_Thu, 
                      dbo.PatientHistory.Dong_Kinh, dbo.PatientHistory.Hen_Suyen, dbo.PatientHistory.Benh_Khac, dbo.PatientHistory.Benh_Gi, 
                      dbo.PatientHistory.Thuoc_Dang_Dung, dbo.PatientHistory.Hut_Thuoc, dbo.PatientHistory.Uong_Ruou, dbo.PatientHistory.Tinh_Trang_Gia_Dinh, 
                      dbo.PatientHistory.Chich_Ngua_Viem_Gan_B, dbo.PatientHistory.Chich_Ngua_Uon_Van, dbo.PatientHistory.Chich_Ngua_Cum, 
                      dbo.PatientHistory.Dang_Co_Thai, dbo.PatientHistory.PatientHistoryGUID, dbo.Contact.Source, dbo.Contact.CompanyName
FROM         dbo.Contact INNER JOIN
                      dbo.Patient ON dbo.Contact.ContactGUID = dbo.Patient.ContactGUID INNER JOIN
                      dbo.PatientHistory ON dbo.Patient.PatientGUID = dbo.PatientHistory.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  StoredProcedure [dbo].[spDichVuTuTuc]    Script Date: 12/27/2011 14:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDichVuTuTuc]
	@TuNgay dateTime,
	@DenNgay datetime
AS
BEGIN
	SELECT @TuNgay AS TuNgay, @DenNgay AS DenNgay, P.FullName, P.FirstName, 
	Min(S.ActivedDate) AS NgayKham, ISNULL(P.CompanyName, N'Tự túc') AS CompanyName, P.Mobile  
	FROM ServiceHistory S, PatientView P
	WHERE S.PatientGUID = P.PatientGUID AND
	S.ActivedDate BETWEEN @TuNgay AND @DenNgay AND S.Status = 0 AND
	S.Status = 0 AND P.Archived = 'False' AND NOT EXISTS 
	(SELECT TOP 1 L.ServiceGUID
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
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
	CAST(0 AS bit) AS Checked, C.Completed
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	M.Status = 0 AND L.Status = 0 AND C.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name], BeginDate, Checked 
	FROM TMP, Services S
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Checked, S.[Name]

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
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = @PatientGUID AND 
	M.Status = 0 AND L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = @PatientGUID AND S.Status = 0  AND 
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
ALTER PROCEDURE [dbo].[spGetCheckListByContract]
	@ContractGUID nvarchar(50),
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Using, C.Completed
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	M.Status = 0 AND L.Status = 0 AND C.CompanyContractGUID = @ContractGUID

	UPDATE TMP
	SET Using = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name],
	BeginDate, Using, CAST(0 AS Bit) AS Checked
	FROM TMP, Services S
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

	SELECT @BeginDate = BeginDate, @EndDate = EndDate FROM CompanyContract
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
		FROM ContractMemberView M, CompanyContract C
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		C.CompanyContractGUID = @ContractGUID AND 
		NOT EXISTS (SELECT TOP 1 * FROM ServiceHistory WHERE PatientGUID = M.PatientGUID AND
		ActivedDate BETWEEN @TuNgay AND @DenNgay AND ServiceHistory.Status = 0) AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		UNION
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		Min(ActivedDate) AS NgayKham, FirstName, Mobile, N'Đã khám' AS TinhTrang
		FROM ContractMemberView M, CompanyContract C, ServiceHistory S
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
		FROM ContractMemberView M, CompanyContract C
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
		FROM ContractMemberView M, CompanyContract C, ServiceHistory S
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
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChiTietToaThuoc_Thuoc]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChiTietToaThuoc]'))
ALTER TABLE [dbo].[ChiTietToaThuoc] DROP CONSTRAINT [FK_ChiTietToaThuoc_Thuoc]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChiTietToaThuoc_ToaThuoc]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChiTietToaThuoc]'))
ALTER TABLE [dbo].[ChiTietToaThuoc] DROP CONSTRAINT [FK_ChiTietToaThuoc_ToaThuoc]
GO
/****** Object:  Table [dbo].[ChiTietToaThuoc]    Script Date: 12/28/2011 12:34:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChiTietToaThuoc]') AND type in (N'U'))
DROP TABLE [dbo].[ChiTietToaThuoc]
GO
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ToaThuoc_DocStaff]') AND parent_object_id = OBJECT_ID(N'[dbo].[ToaThuoc]'))
ALTER TABLE [dbo].[ToaThuoc] DROP CONSTRAINT [FK_ToaThuoc_DocStaff]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ToaThuoc_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[ToaThuoc]'))
ALTER TABLE [dbo].[ToaThuoc] DROP CONSTRAINT [FK_ToaThuoc_Patient]
GO
/****** Object:  Table [dbo].[ToaThuoc]    Script Date: 12/28/2011 12:35:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ToaThuoc]') AND type in (N'U'))
DROP TABLE [dbo].[ToaThuoc]
GO
GO
/****** Object:  Table [dbo].[ToaThuoc]    Script Date: 12/28/2011 12:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToaThuoc](
	[ToaThuocGUID] [uniqueidentifier] NOT NULL,
	[MaToaThuoc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NgayKeToa] [datetime] NULL,
	[NgayKham] [datetime] NULL,
	[NgayTaiKham] [datetime] NULL,
	[BacSiKeToa] [uniqueidentifier] NOT NULL,
	[BenhNhan] [uniqueidentifier] NOT NULL,
	[ChanDoan] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Loai] [tinyint] NOT NULL CONSTRAINT [DF_ToaThuoc_Loai]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ToaThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ToaThuoc] PRIMARY KEY CLUSTERED 
(
	[ToaThuocGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ToaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ToaThuoc_DocStaff] FOREIGN KEY([BacSiKeToa])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
GO
ALTER TABLE [dbo].[ToaThuoc] CHECK CONSTRAINT [FK_ToaThuoc_DocStaff]
GO
ALTER TABLE [dbo].[ToaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ToaThuoc_Patient] FOREIGN KEY([BenhNhan])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ToaThuoc] CHECK CONSTRAINT [FK_ToaThuoc_Patient]
GO
/****** Object:  Table [dbo].[ChiTietToaThuoc]    Script Date: 12/28/2011 12:36:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietToaThuoc](
	[ChiTietToaThuocGUID] [uniqueidentifier] NOT NULL,
	[ToaThuocGUID] [uniqueidentifier] NOT NULL,
	[ThuocGUID] [uniqueidentifier] NOT NULL,
	[SoLuong] [int] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_SoLuong]  DEFAULT ((0)),
	[LieuDung] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Sang] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Sang]  DEFAULT ((0)),
	[Trua] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Trua]  DEFAULT ((0)),
	[Chieu] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Chieu]  DEFAULT ((0)),
	[Toi] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Toi]  DEFAULT ((0)),
	[TruocAn] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_TruocAn]  DEFAULT ((0)),
	[SauAn] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_SauAn]  DEFAULT ((0)),
	[Khac_TruocSauAn] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Khac_TruocSauAn]  DEFAULT ((0)),
	[Uong] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Uong]  DEFAULT ((0)),
	[Boi] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Boi]  DEFAULT ((0)),
	[Dat] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Dat]  DEFAULT ((0)),
	[Khac_CachDung] [bit] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Khac_CachDung]  DEFAULT ((0)),
	[SangNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TruaNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ChieuNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ToiNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TruocAnNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SauAnNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Khac_TruocSauAnNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UongNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BoiNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DatNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Khac_CachDungNote] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietToaThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietToaThuoc] PRIMARY KEY CLUSTERED 
(
	[ChiTietToaThuocGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietToaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietToaThuoc_Thuoc] FOREIGN KEY([ThuocGUID])
REFERENCES [dbo].[Thuoc] ([ThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietToaThuoc] CHECK CONSTRAINT [FK_ChiTietToaThuoc_Thuoc]
GO
ALTER TABLE [dbo].[ChiTietToaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietToaThuoc_ToaThuoc] FOREIGN KEY([ToaThuocGUID])
REFERENCES [dbo].[ToaThuoc] ([ToaThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietToaThuoc] CHECK CONSTRAINT [FK_ChiTietToaThuoc_ToaThuoc]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ToaThuocView]
AS
SELECT     dbo.ToaThuoc.ToaThuocGUID, dbo.ToaThuoc.MaToaThuoc, dbo.ToaThuoc.NgayKeToa, dbo.ToaThuoc.BacSiKeToa, dbo.ToaThuoc.BenhNhan, 
                      dbo.ToaThuoc.Note, dbo.ToaThuoc.CreatedDate, dbo.ToaThuoc.CreatedBy, dbo.ToaThuoc.UpdatedDate, dbo.ToaThuoc.UpdatedBy, 
                      dbo.ToaThuoc.DeletedDate, dbo.ToaThuoc.DeletedBy, dbo.ToaThuoc.Status, dbo.DocStaffView.FullName AS TenBacSi, 
                      dbo.PatientView.FullName AS TenBenhNhan, dbo.PatientView.GenderAsStr, dbo.PatientView.DobStr, dbo.PatientView.FileNum, 
                      dbo.PatientView.Address, dbo.ToaThuoc.NgayKham, dbo.ToaThuoc.NgayTaiKham, dbo.ToaThuoc.ChanDoan, dbo.ToaThuoc.Loai, 
                      dbo.PatientView.Mobile, dbo.PatientView.HomePhone, dbo.PatientView.WorkPhone, 
                      CASE Loai WHEN 0 THEN N'Chung' WHEN 1 THEN N'Sản khoa' END AS LoaiStr
FROM         dbo.ToaThuoc INNER JOIN
                      dbo.DocStaffView ON dbo.ToaThuoc.BacSiKeToa = dbo.DocStaffView.DocStaffGUID INNER JOIN
                      dbo.PatientView ON dbo.ToaThuoc.BenhNhan = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietToaThuocView]
AS
SELECT     dbo.ChiTietToaThuoc.ChiTietToaThuocGUID, dbo.ChiTietToaThuoc.ToaThuocGUID, dbo.ChiTietToaThuoc.ThuocGUID, dbo.ChiTietToaThuoc.Note, 
                      dbo.ChiTietToaThuoc.CreatedDate, dbo.ChiTietToaThuoc.CreatedBy, dbo.ChiTietToaThuoc.UpdatedDate, dbo.ChiTietToaThuoc.UpdatedBy, 
                      dbo.ChiTietToaThuoc.DeletedDate, dbo.ChiTietToaThuoc.DeletedBy, dbo.ChiTietToaThuoc.Status AS ChiTietToaThuocStatus, dbo.Thuoc.MaThuoc, 
                      dbo.Thuoc.TenThuoc, dbo.Thuoc.DonViTinh, dbo.Thuoc.Status AS ThuocStatus, dbo.ChiTietToaThuoc.SoLuong, dbo.ChiTietToaThuoc.LieuDung, 
                      dbo.ChiTietToaThuoc.Sang, dbo.ChiTietToaThuoc.Trua, dbo.ChiTietToaThuoc.Chieu, dbo.ChiTietToaThuoc.Toi, dbo.ChiTietToaThuoc.TruocAn, 
                      dbo.ChiTietToaThuoc.SauAn, dbo.ChiTietToaThuoc.Khac_TruocSauAn, dbo.ChiTietToaThuoc.Uong, dbo.ChiTietToaThuoc.Boi, dbo.ChiTietToaThuoc.Dat, 
                      dbo.ChiTietToaThuoc.Khac_CachDung, dbo.ChiTietToaThuoc.SangNote, dbo.ChiTietToaThuoc.TruaNote, dbo.ChiTietToaThuoc.ChieuNote, 
                      dbo.ChiTietToaThuoc.ToiNote, dbo.ChiTietToaThuoc.TruocAnNote, dbo.ChiTietToaThuoc.SauAnNote, dbo.ChiTietToaThuoc.Khac_TruocSauAnNote, 
                      dbo.ChiTietToaThuoc.UongNote, dbo.ChiTietToaThuoc.BoiNote, dbo.ChiTietToaThuoc.DatNote, dbo.ChiTietToaThuoc.Khac_CachDungNote
FROM         dbo.ChiTietToaThuoc INNER JOIN
                      dbo.Thuoc ON dbo.ChiTietToaThuoc.ThuocGUID = dbo.Thuoc.ThuocGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  Table [dbo].[ChiDinh]    Script Date: 12/29/2011 13:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiDinh](
	[ChiDinhGUID] [uniqueidentifier] NOT NULL,
	[MaChiDinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NgayChiDinh] [datetime] NOT NULL,
	[BacSiChiDinhGUID] [uniqueidentifier] NOT NULL,
	[BenhNhanGUID] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiDinh_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiDinh] PRIMARY KEY CLUSTERED 
(
	[ChiDinhGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiDinh]  WITH CHECK ADD  CONSTRAINT [FK_ChiDinh_DocStaff] FOREIGN KEY([BacSiChiDinhGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiDinh] CHECK CONSTRAINT [FK_ChiDinh_DocStaff]
GO
ALTER TABLE [dbo].[ChiDinh]  WITH CHECK ADD  CONSTRAINT [FK_ChiDinh_Patient] FOREIGN KEY([BenhNhanGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
GO
ALTER TABLE [dbo].[ChiDinh] CHECK CONSTRAINT [FK_ChiDinh_Patient]
GO
/****** Object:  Table [dbo].[ChiTietChiDinh]    Script Date: 12/29/2011 12:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietChiDinh](
	[ChiTietChiDinhGUID] [uniqueidentifier] NOT NULL,
	[ChiDinhGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietChiDinh_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietChiDinh] PRIMARY KEY CLUSTERED 
(
	[ChiTietChiDinhGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietChiDinh]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietChiDinh_ChiDinh] FOREIGN KEY([ChiDinhGUID])
REFERENCES [dbo].[ChiDinh] ([ChiDinhGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietChiDinh] CHECK CONSTRAINT [FK_ChiTietChiDinh_ChiDinh]
GO
ALTER TABLE [dbo].[ChiTietChiDinh]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietChiDinh_Services] FOREIGN KEY([ServiceGUID])
REFERENCES [dbo].[Services] ([ServiceGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietChiDinh] CHECK CONSTRAINT [FK_ChiTietChiDinh_Services]
GO
/****** Object:  Table [dbo].[DichVuChiDinh]    Script Date: 12/29/2011 12:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichVuChiDinh](
	[DichVuChiDinhGUID] [uniqueidentifier] NOT NULL,
	[ChiTietChiDinhGUID] [uniqueidentifier] NOT NULL,
	[ServiceHistoryGUID] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CraetedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_DichVuChiDinh_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_DichVuChiDinh] PRIMARY KEY CLUSTERED 
(
	[DichVuChiDinhGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[DichVuChiDinh]  WITH CHECK ADD  CONSTRAINT [FK_DichVuChiDinh_ChiTietChiDinh] FOREIGN KEY([ChiTietChiDinhGUID])
REFERENCES [dbo].[ChiTietChiDinh] ([ChiTietChiDinhGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DichVuChiDinh] CHECK CONSTRAINT [FK_DichVuChiDinh_ChiTietChiDinh]
GO
ALTER TABLE [dbo].[DichVuChiDinh]  WITH CHECK ADD  CONSTRAINT [FK_DichVuChiDinh_ServiceHistory] FOREIGN KEY([ServiceHistoryGUID])
REFERENCES [dbo].[ServiceHistory] ([ServiceHistoryGUID])
GO
ALTER TABLE [dbo].[DichVuChiDinh] CHECK CONSTRAINT [FK_DichVuChiDinh_ServiceHistory]
GO
ALTER TABLE Permission
ADD IsConfirm bit NOT NULL CONSTRAINT [DF_Permission_IsConfirm] DEFAULT ((0))
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PermissionView]
AS
SELECT     dbo.Permission.PermissionGUID, dbo.Permission.LogonGUID, dbo.[Function].FunctionGUID, dbo.[Function].FunctionCode, dbo.Permission.IsView, 
                      dbo.Permission.IsAdd, dbo.Permission.IsEdit, dbo.Permission.IsDelete, dbo.Permission.IsPrint, dbo.Permission.IsExport, 
                      dbo.Permission.CreatedDate, dbo.Permission.CreatedBy, dbo.Permission.UpdatedDate, dbo.Permission.UpdatedBy, dbo.Permission.DeletedDate, 
                      dbo.Permission.DeletedBy, dbo.[Function].FunctionName, dbo.Permission.IsImport, dbo.Permission.IsConfirm
FROM         dbo.Permission INNER JOIN
                      dbo.[Function] ON dbo.Permission.FunctionGUID = dbo.[Function].FunctionGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'0518644d-03d5-479f-90f2-144b1f0cfd3e', N'ChiDinh', N'Chỉ định')
GO
/****** Object:  View [dbo].[ChiDinhView]    Script Date: 12/29/2011 13:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiDinhView]
AS
SELECT     dbo.ChiDinh.ChiDinhGUID, dbo.ChiDinh.NgayChiDinh, dbo.ChiDinh.BacSiChiDinhGUID, dbo.ChiDinh.BenhNhanGUID, dbo.ChiDinh.CreatedDate, 
                      dbo.ChiDinh.CreatedBy, dbo.ChiDinh.UpdatedDate, dbo.ChiDinh.UpdatedBy, dbo.ChiDinh.DeletedBy, dbo.ChiDinh.DeletedDate, 
                      dbo.DocStaffView.FullName, dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, dbo.DocStaffView.Archived, dbo.ChiDinh.Status, 
                      dbo.ChiDinh.MaChiDinh
FROM         dbo.ChiDinh INNER JOIN
                      dbo.DocStaffView ON dbo.ChiDinh.BacSiChiDinhGUID = dbo.DocStaffView.DocStaffGUID

GO
/****** Object:  View [dbo].[ChiTietChiDinhView]    Script Date: 12/29/2011 12:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietChiDinhView]
AS
SELECT     dbo.ChiTietChiDinh.ChiTietChiDinhGUID, dbo.ChiTietChiDinh.ChiDinhGUID, dbo.ChiTietChiDinh.ServiceGUID, dbo.ChiTietChiDinh.CreatedDate, 
                      dbo.ChiTietChiDinh.CreatedBy, dbo.ChiTietChiDinh.UpdatedDate, dbo.ChiTietChiDinh.UpdatedBy, dbo.ChiTietChiDinh.DeletedDate, 
                      dbo.ChiTietChiDinh.DeletedBy, dbo.ChiTietChiDinh.Status AS CTCDStatus, dbo.Services.Status AS ServiceStatus, dbo.Services.Code, 
                      dbo.Services.Name, dbo.Services.Price
FROM         dbo.ChiTietChiDinh INNER JOIN
                      dbo.Services ON dbo.ChiTietChiDinh.ServiceGUID = dbo.Services.ServiceGUID

GO
/****** Object:  View [dbo].[DichVuChiDinhView]    Script Date: 12/29/2011 14:38:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DichVuChiDinhView]
AS
SELECT     dbo.DichVuChiDinh.DichVuChiDinhGUID, dbo.DichVuChiDinh.ChiTietChiDinhGUID, dbo.DichVuChiDinh.ServiceHistoryGUID, 
                      dbo.ServiceHistoryView.Name, dbo.ServiceHistoryView.Price, dbo.ServiceHistoryView.Code, dbo.ServiceHistoryView.ServiceGUID, 
                      dbo.ServiceHistoryView.Status
FROM         dbo.DichVuChiDinh INNER JOIN
                      dbo.ServiceHistoryView ON dbo.DichVuChiDinh.ServiceHistoryGUID = dbo.ServiceHistoryView.ServiceHistoryGUID

GO