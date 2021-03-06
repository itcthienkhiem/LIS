USE MM
GO
/****** Object:  Table [dbo].[PhieuThuHopDong]    Script Date: 03/12/2012 15:36:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuThuHopDong](
	[PhieuThuHopDongGUID] [uniqueidentifier] NOT NULL,
	[HopDongGUID] [uniqueidentifier] NOT NULL,
	[MaPhieuThuHopDong] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TenNguoiNop] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenCongTy] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayThu] [datetime] NOT NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsExported] [bit] NOT NULL CONSTRAINT [DF_PhieuThuHopDong_IsExported]  DEFAULT ((0)),
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_PhieuThuHopDong_Status]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PhieuThuHopDong] PRIMARY KEY CLUSTERED 
(
	[PhieuThuHopDongGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PhieuThuHopDong]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThuHopDong_CompanyContract] FOREIGN KEY([HopDongGUID])
REFERENCES [dbo].[CompanyContract] ([CompanyContractGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuThuHopDong] CHECK CONSTRAINT [FK_PhieuThuHopDong_CompanyContract]
GO
/****** Object:  Table [dbo].[ChiTietPhieuThuHopDong]    Script Date: 03/12/2012 15:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuThuHopDong](
	[ChiTietPhieuThuHopDongGUID] [uniqueidentifier] NOT NULL,
	[PhieuThuHopDongGUID] [uniqueidentifier] NOT NULL,
	[DichVu] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonGia] [float] NOT NULL,
	[SoLuong] [float] NOT NULL,
	[Giam] [float] NOT NULL,
	[ThanhTien] [float] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietPhieuThuHopDong_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietPhieuThuHopDong] PRIMARY KEY CLUSTERED 
(
	[ChiTietPhieuThuHopDongGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietPhieuThuHopDong]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuThuHopDong_PhieuThuHopDong] FOREIGN KEY([PhieuThuHopDongGUID])
REFERENCES [dbo].[PhieuThuHopDong] ([PhieuThuHopDongGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuThuHopDong] CHECK CONSTRAINT [FK_ChiTietPhieuThuHopDong_PhieuThuHopDong]
GO
/****** Object:  Table [dbo].[HoaDonHopDong]    Script Date: 03/12/2012 15:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonHopDong](
	[HoaDonHopDongGUID] [uniqueidentifier] NOT NULL,
	[PhieuThuHopDongGUIDList] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoHoaDon] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayXuatHoaDon] [datetime] NULL,
	[TenNguoiMuaHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenDonVi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaSoThue] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoTaiKhoan] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HinhThucThanhToan] [tinyint] NULL,
	[VAT] [float] NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_HoaDonHopDong_Status]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_HoaDonHopDong] PRIMARY KEY CLUSTERED 
(
	[HoaDonHopDongGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDonHopDong]    Script Date: 03/12/2012 15:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonHopDong](
	[ChiTietHoaDonHopDongGUID] [uniqueidentifier] NOT NULL,
	[HoaDonHopDongGUID] [uniqueidentifier] NOT NULL,
	[TenMatHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonViTinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [float] NOT NULL,
	[ThanhTien] [float] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietHoaDonHopDong_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietHoaDonHopDong] PRIMARY KEY CLUSTERED 
(
	[ChiTietHoaDonHopDongGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietHoaDonHopDong]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDonHopDong_HoaDonHopDong] FOREIGN KEY([HoaDonHopDongGUID])
REFERENCES [dbo].[HoaDonHopDong] ([HoaDonHopDongGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDonHopDong] CHECK CONSTRAINT [FK_ChiTietHoaDonHopDong_HoaDonHopDong]
GO
/****** Object:  View [dbo].[HoaDonHopDongView]    Script Date: 03/12/2012 15:37:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[HoaDonHopDongView]
AS
SELECT     HoaDonHopDongGUID, PhieuThuHopDongGUIDList, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, 
                      HinhThucThanhToan, VAT, Notes, Status, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr
FROM         dbo.HoaDonHopDong

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'a7b7da37-278b-44ac-8974-284cc68d8485', N'PhieuThuHopDong', N'Phiếu thu hợp đồng')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'e1f9f5d1-4239-4b4a-82d3-fce1cb025152', N'HoaDonHopDong', N'Hóa đơn hợp đồng')
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
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND 
	M.Status = 0 AND L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND
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
ALTER PROCEDURE [dbo].[spGetCheckList]
	@PatientGUID nvarchar(50)
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
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	M.Status = 0 AND L.Status = 0 AND C.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name], BeginDate, EndDate, Checked 
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
ALTER PROCEDURE [dbo].[spGetCheckListByContract]
	@ContractGUID nvarchar(50),
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Using, C.Completed
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	M.Status = 0 AND L.Status = 0 AND C.CompanyContractGUID = @ContractGUID

	UPDATE TMP
	SET Using = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name], EnglishName,
	BeginDate, EndDate, Using, CAST(0 AS Bit) AS Checked
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
                       CASE dbo.ServiceHistory.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat
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
