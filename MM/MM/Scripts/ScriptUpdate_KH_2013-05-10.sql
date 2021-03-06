USE MM
GO
GO
/****** Object:  StoredProcedure [dbo].[spUpdateGiaHopDong2ServiceHistory]    Script Date: 05/10/2013 09:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateGiaHopDong2ServiceHistory]
	@ContractGUID nvarchar(50),
	@ServiceGUID nvarchar(50),
	@Gia float
AS
BEGIN
	SELECT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	C.Completed, M.PatientGUID
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMemberView M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	CM.Status = 0 AND
	L.Status = 0 AND C.CompanyContractGUID = @ContractGUID AND
	L.ServiceGUID = @ServiceGUID AND
	M.Status = 0 AND CM.Status = 0 AND M.Archived = 'False'

	UPDATE ServiceHistory
	SET Price = @Gia
	FROM TMP
	WHERE ServiceHistory.ServiceGUID = TMP.ServiceGUID AND
	ServiceHistory.IsExported = 'False' AND ServiceHistory.KhamTuTuc = 'False' AND
	ServiceHistory.PatientGUID = TMP.PatientGUID AND ServiceHistory.Status = 0 AND
	(TMP.Completed = 'False' AND ServiceHistory.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND ServiceHistory.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	UPDATE ServiceHistory
	SET Price = @Gia
	FROM TMP, PatientView P
	WHERE ServiceHistory.ServiceGUID = TMP.ServiceGUID AND
	ServiceHistory.PatientGUID = P.PatientGUID AND
	ServiceHistory.IsExported = 'False' AND ServiceHistory.KhamTuTuc = 'True' AND
	ServiceHistory.RootPatientGUID = TMP.PatientGUID AND ServiceHistory.Status = 0 AND
	(TMP.Completed = 'False' AND ServiceHistory.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND ServiceHistory.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	DROP TABLE TMP
END
GO
ALTER TABLE PhieuThuCapCuu
ADD [HinhThucThanhToan] [tinyint] NOT NULL DEFAULT ((0))
GO
ALTER TABLE PhieuThuHopDong
ADD [HinhThucThanhToan] [tinyint] NOT NULL DEFAULT ((0))
GO
ALTER TABLE PhieuThuThuoc
ADD [HinhThucThanhToan] [tinyint] NOT NULL DEFAULT ((0))
GO
ALTER TABLE Receipt
ADD [HinhThucThanhToan] [tinyint] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PhieuThuCapCuuView]
AS
SELECT     dbo.PhieuThuCapCuu.PhieuThuCapCuuGUID, dbo.PhieuThuCapCuu.NgayThu, dbo.PhieuThuCapCuu.MaBenhNhan, 
                      dbo.PhieuThuCapCuu.TenBenhNhan, dbo.PhieuThuCapCuu.TenCongTy, dbo.PhieuThuCapCuu.DiaChi, dbo.PhieuThuCapCuu.IsExported, 
                      dbo.PhieuThuCapCuu.ChuaThuTien, dbo.PhieuThuCapCuu.LyDoGiam, dbo.PhieuThuCapCuu.Notes, dbo.PhieuThuCapCuu.CreatedDate, 
                      dbo.PhieuThuCapCuu.CreatedBy, dbo.PhieuThuCapCuu.UpdatedDate, dbo.PhieuThuCapCuu.UpdatedBy, dbo.PhieuThuCapCuu.DeletedDate, 
                      dbo.PhieuThuCapCuu.DeletedBy, dbo.PhieuThuCapCuu.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, 
                      dbo.PhieuThuCapCuu.MaPhieuThuCapCuu, dbo.PhieuThuCapCuu.ToaCapCuuGUID, dbo.ToaCapCuu.MaToaCapCuu, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' WHEN 2 THEN N'Tiền mặt/Chuyển khoản' WHEN 3 THEN N'Bảo hiểm'
                       WHEN 4 THEN N'Cà thẻ' END AS HinhThucThanhToanStr, dbo.PhieuThuCapCuu.HinhThucThanhToan
FROM         dbo.PhieuThuCapCuu INNER JOIN
                      dbo.ToaCapCuu ON dbo.PhieuThuCapCuu.ToaCapCuuGUID = dbo.ToaCapCuu.ToaCapCuuGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuCapCuu.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PhieuThuHopDongView]
AS
SELECT     dbo.PhieuThuHopDong.PhieuThuHopDongGUID, dbo.PhieuThuHopDong.HopDongGUID, dbo.PhieuThuHopDong.MaPhieuThuHopDong, 
                      dbo.PhieuThuHopDong.TenNguoiNop, dbo.PhieuThuHopDong.TenCongTy, dbo.PhieuThuHopDong.DiaChi, dbo.PhieuThuHopDong.NgayThu, 
                      dbo.PhieuThuHopDong.Notes, dbo.PhieuThuHopDong.IsExported, dbo.PhieuThuHopDong.Status, dbo.PhieuThuHopDong.CreatedDate, 
                      dbo.PhieuThuHopDong.CreatedBy, dbo.PhieuThuHopDong.UpdatedDate, dbo.PhieuThuHopDong.UpdatedBy, dbo.PhieuThuHopDong.DeletedDate, 
                      dbo.PhieuThuHopDong.DeletedBy, dbo.PhieuThuHopDong.ChuaThuTien, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, 
                      dbo.CompanyContract.ContractCode, dbo.CompanyContract.ContractName, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' WHEN 2 THEN N'Tiền mặt/Chuyển khoản' WHEN 3 THEN N'Bảo hiểm'
                       WHEN 4 THEN N'Cà thẻ' END AS HinhThucThanhToanStr, dbo.PhieuThuHopDong.HinhThucThanhToan
FROM         dbo.PhieuThuHopDong INNER JOIN
                      dbo.CompanyContract ON dbo.PhieuThuHopDong.HopDongGUID = dbo.CompanyContract.CompanyContractGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuHopDong.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PhieuThuThuocView]
AS
SELECT     dbo.PhieuThuThuoc.PhieuThuThuocGUID, dbo.PhieuThuThuoc.ToaThuocGUID, dbo.PhieuThuThuoc.MaPhieuThuThuoc, dbo.PhieuThuThuoc.NgayThu, 
                      dbo.PhieuThuThuoc.MaBenhNhan, dbo.PhieuThuThuoc.TenBenhNhan, dbo.PhieuThuThuoc.TenCongTy, dbo.PhieuThuThuoc.DiaChi, 
                      dbo.PhieuThuThuoc.CreatedBy, dbo.PhieuThuThuoc.CreatedDate, dbo.PhieuThuThuoc.UpdatedBy, dbo.PhieuThuThuoc.UpdatedDate, 
                      dbo.PhieuThuThuoc.DeletedBy, dbo.PhieuThuThuoc.DeletedDate, dbo.PhieuThuThuoc.IsExported, dbo.PhieuThuThuoc.Status, 
                      dbo.PhieuThuThuoc.Notes, dbo.PhieuThuThuoc.ChuaThuTien, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, 
                      dbo.PhieuThuThuoc.LyDoGiam, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' WHEN 2 THEN N'Tiền mặt/Chuyển khoản' WHEN 3 THEN N'Bảo hiểm'
                       WHEN 4 THEN N'Cà thẻ' END AS HinhThucThanhToanStr, dbo.PhieuThuThuoc.HinhThucThanhToan
FROM         dbo.PhieuThuThuoc LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuThuoc.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ReceiptView]
AS
SELECT     dbo.PatientView.FullName, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.Receipt.ReceiptGUID, dbo.Receipt.PatientGUID, 
                      dbo.Receipt.ReceiptDate, dbo.Receipt.CreatedDate, dbo.Receipt.CreatedBy, dbo.Receipt.UpdatedDate, dbo.Receipt.UpdatedBy, 
                      dbo.Receipt.DeletedDate, dbo.Receipt.DeletedBy, dbo.Receipt.Status, dbo.Receipt.ReceiptCode, dbo.Receipt.IsExportedInVoice, 
                      dbo.PatientView.CompanyName, dbo.Receipt.Notes, dbo.Receipt.ChuaThuTien, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao, 
                      dbo.Receipt.LyDoGiam, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' WHEN 2 THEN N'Tiền mặt/Chuyển khoản' WHEN 3 THEN N'Bảo hiểm'
                       WHEN 4 THEN N'Cà thẻ' END AS HinhThucThanhToanStr, dbo.Receipt.HinhThucThanhToan
FROM         dbo.Receipt INNER JOIN
                      dbo.PatientView ON dbo.Receipt.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Receipt.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  Table [dbo].[ThucHienXetNghiem]    Script Date: 05/18/2013 22:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThucHienXetNghiem](
	[ThucHienXetNghiemGUID] [uniqueidentifier] NOT NULL,
	[NgayThucHien] [datetime] NOT NULL,
	[TenCongTy] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TenKhachHang] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TenDichVu] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoTien] [float] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ThucHienXetNghiem_Status]  DEFAULT ((0)),
	[SourcePath] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ThucHienXetNghiem] PRIMARY KEY CLUSTERED 
(
	[ThucHienXetNghiemGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'd9eb1168-a84b-4fe8-9fbf-07c4172dbbc5', N'DichVuXetNghiem', N'Dịch vụ xét nghiệm')
GO
CREATE NONCLUSTERED INDEX [_dta_index_ThucHienXetNghiem_7_85015484__K13_3] ON [dbo].[ThucHienXetNghiem] 
(
	[Status] ASC
)
INCLUDE ( [TenCongTy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_ThucHienXetNghiem_7_85015484__K13_K4] ON [dbo].[ThucHienXetNghiem] 
(
	[Status] ASC,
	[TenKhachHang] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_ThucHienXetNghiem_7_85015484__K13_K5] ON [dbo].[ThucHienXetNghiem] 
(
	[Status] ASC,
	[TenDichVu] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_85015484_4_13_2] ON [dbo].[ThucHienXetNghiem]([TenKhachHang], [Status], [NgayThucHien])
GO
CREATE STATISTICS [_dta_stat_85015484_5_13_2] ON [dbo].[ThucHienXetNghiem]([TenDichVu], [Status], [NgayThucHien])
GO
CREATE NONCLUSTERED INDEX [_dta_index_ThucHienXetNghiem_7_85015484__K4] ON [dbo].[ThucHienXetNghiem] 
(
	[TenKhachHang] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_85015484_2_13] ON [dbo].[ThucHienXetNghiem]([NgayThucHien], [Status])


