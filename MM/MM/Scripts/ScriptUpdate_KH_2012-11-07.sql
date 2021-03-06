USE MM
GO
DELETE FROM [Function] WHERE FunctionCode = 'XuatKhoCapCuu'
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'ad62caf9-bf02-4285-9198-07a7c5a7c535', N'PhieuThuCapCuu', N'Phiếu thu cấp cứu')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'cccb6998-6fc3-4eec-a66a-888fabff1811', N'GiaCapCuu', N'Giá cấp cứu')
GO
/****** Object:  Table [dbo].[PhieuThuCapCuu]    Script Date: 11/07/2012 12:22:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuThuCapCuu](
	[PhieuThuCapCuuGUID] [uniqueidentifier] NOT NULL,
	[MaPhieuThuCapCuu] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NgayThu] [datetime] NOT NULL,
	[MaBenhNhan] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenBenhNhan] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenCongTy] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsExported] [bit] NOT NULL,
	[ChuaThuTien] [bit] NOT NULL,
	[LyDoGiam] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_PhieuThuCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_PhieuThuCapCuu] PRIMARY KEY CLUSTERED 
(
	[PhieuThuCapCuuGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietPhieuThuCapCuu]    Script Date: 11/07/2012 12:22:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuThuCapCuu](
	[ChiTietPhieuThuCapCuuGUID] [uniqueidentifier] NOT NULL,
	[PhieuThuCapCuuGUID] [uniqueidentifier] NOT NULL,
	[KhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[DonGia] [float] NOT NULL,
	[SoLuong] [float] NOT NULL,
	[Giam] [float] NOT NULL,
	[ThanhTien] [float] NOT NULL,
	[DonGiaNhap] [float] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietPhieuThuCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietPhieuThuCapCuu] PRIMARY KEY CLUSTERED 
(
	[ChiTietPhieuThuCapCuuGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietPhieuThuCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuThuCapCuu_KhoCapCuu] FOREIGN KEY([KhoCapCuuGUID])
REFERENCES [dbo].[KhoCapCuu] ([KhoCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuThuCapCuu] CHECK CONSTRAINT [FK_ChiTietPhieuThuCapCuu_KhoCapCuu]
GO
ALTER TABLE [dbo].[ChiTietPhieuThuCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuThuCapCuu_PhieuThuCapCuu] FOREIGN KEY([PhieuThuCapCuuGUID])
REFERENCES [dbo].[PhieuThuCapCuu] ([PhieuThuCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuThuCapCuu] CHECK CONSTRAINT [FK_ChiTietPhieuThuCapCuu_PhieuThuCapCuu]
GO
/****** Object:  View [dbo].[PhieuThuCapCuuView]    Script Date: 11/07/2012 12:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PhieuThuCapCuuView]
AS
SELECT     dbo.PhieuThuCapCuu.PhieuThuCapCuuGUID, dbo.PhieuThuCapCuu.NgayThu, dbo.PhieuThuCapCuu.MaBenhNhan, 
                      dbo.PhieuThuCapCuu.TenBenhNhan, dbo.PhieuThuCapCuu.TenCongTy, dbo.PhieuThuCapCuu.DiaChi, dbo.PhieuThuCapCuu.IsExported, 
                      dbo.PhieuThuCapCuu.ChuaThuTien, dbo.PhieuThuCapCuu.LyDoGiam, dbo.PhieuThuCapCuu.Notes, dbo.PhieuThuCapCuu.CreatedDate, 
                      dbo.PhieuThuCapCuu.CreatedBy, dbo.PhieuThuCapCuu.UpdatedDate, dbo.PhieuThuCapCuu.UpdatedBy, dbo.PhieuThuCapCuu.DeletedDate, 
                      dbo.PhieuThuCapCuu.DeletedBy, dbo.PhieuThuCapCuu.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao,
					  dbo.PhieuThuCapCuu.MaPhieuThuCapCuu
FROM         dbo.PhieuThuCapCuu LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.PhieuThuCapCuu.CreatedBy = dbo.DocStaffView.DocStaffGUID

GO
/****** Object:  View [dbo].[ChiTietPhieuThuCapCuuView]    Script Date: 11/07/2012 12:22:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietPhieuThuCapCuuView]
AS
SELECT     dbo.ChiTietPhieuThuCapCuu.ChiTietPhieuThuCapCuuGUID, dbo.ChiTietPhieuThuCapCuu.PhieuThuCapCuuGUID, 
                      dbo.ChiTietPhieuThuCapCuu.KhoCapCuuGUID, dbo.ChiTietPhieuThuCapCuu.DonGia, dbo.ChiTietPhieuThuCapCuu.Giam, 
                      dbo.ChiTietPhieuThuCapCuu.ThanhTien, dbo.ChiTietPhieuThuCapCuu.DonGiaNhap, dbo.ChiTietPhieuThuCapCuu.CreatedDate, 
                      dbo.ChiTietPhieuThuCapCuu.UpdatedDate, dbo.ChiTietPhieuThuCapCuu.CreatedBy, dbo.ChiTietPhieuThuCapCuu.UpdatedBy, 
                      dbo.ChiTietPhieuThuCapCuu.DeletedDate, dbo.ChiTietPhieuThuCapCuu.DeletedBy, dbo.ChiTietPhieuThuCapCuu.Status AS CTPTCCStatus, 
                      dbo.KhoCapCuu.MaCapCuu, dbo.KhoCapCuu.TenCapCuu, dbo.KhoCapCuu.DonViTinh, dbo.KhoCapCuu.Status AS khoCapCuuStatus,
						dbo.ChiTietPhieuThuCapCuu.SoLuong
FROM         dbo.ChiTietPhieuThuCapCuu INNER JOIN
                      dbo.KhoCapCuu ON dbo.ChiTietPhieuThuCapCuu.KhoCapCuuGUID = dbo.KhoCapCuu.KhoCapCuuGUID

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
			SET @SLXuat = (SELECT SUM(CT.SoLuong) FROM PhieuThuCapCuu PT WITH(NOLOCK), 
			ChiTietPhieuThuCapCuu CT WITH(NOLOCK)
			WHERE PT.PhieuThuCapCuuGUID = CT.PhieuThuCapCuuGUID AND
			PT.Status = 0 AND CT.Status = 0 AND CT.KhoCapCuuGUID = @KhoCapCuuGUID AND
			PT.NgayThu < @TuNgay)

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
			SET @SLXuat = (SELECT SUM(CT.SoLuong) FROM PhieuThuCapCuu PT WITH(NOLOCK), 
			ChiTietPhieuThuCapCuu CT WITH(NOLOCK)
			WHERE PT.PhieuThuCapCuuGUID = CT.PhieuThuCapCuuGUID AND
			PT.Status = 0 AND CT.Status = 0 AND CT.KhoCapCuuGUID = @KhoCapCuuGUID AND
			PT.NgayThu BETWEEN @TuNgay AND @DenNgay)
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
/****** Object:  Table [dbo].[GiaCapCuu]    Script Date: 11/07/2012 14:31:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GiaCapCuu](
	[GiaCapCuuGUID] [uniqueidentifier] NOT NULL,
	[KhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[GiaBan] [float] NOT NULL,
	[NgayApDung] [datetime] NOT NULL,
	[Note] [varbinary](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_GiaCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_GiaCapCuu] PRIMARY KEY CLUSTERED 
(
	[GiaCapCuuGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[GiaCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_GiaCapCuu_KhoCapCuu] FOREIGN KEY([KhoCapCuuGUID])
REFERENCES [dbo].[KhoCapCuu] ([KhoCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GiaCapCuu] CHECK CONSTRAINT [FK_GiaCapCuu_KhoCapCuu]
GO
/****** Object:  View [dbo].[GiaCapCuuView]    Script Date: 11/07/2012 14:32:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GiaCapCuuView]
AS
SELECT     dbo.GiaCapCuu.GiaCapCuuGUID, dbo.GiaCapCuu.KhoCapCuuGUID, dbo.GiaCapCuu.GiaBan, dbo.GiaCapCuu.NgayApDung, dbo.GiaCapCuu.Note, 
                      dbo.GiaCapCuu.CreatedDate, dbo.GiaCapCuu.CreatedBy, dbo.GiaCapCuu.UpdatedDate, dbo.GiaCapCuu.UpdatedBy, dbo.GiaCapCuu.DeletedDate, 
                      dbo.GiaCapCuu.DeletedBy, dbo.GiaCapCuu.Status AS GiaCapCuuStatus, dbo.KhoCapCuu.TenCapCuu, dbo.KhoCapCuu.MaCapCuu, 
                      dbo.KhoCapCuu.DonViTinh, dbo.KhoCapCuu.Status AS KhoCapCuuStatus
FROM         dbo.GiaCapCuu INNER JOIN
                      dbo.KhoCapCuu ON dbo.GiaCapCuu.KhoCapCuuGUID = dbo.KhoCapCuu.KhoCapCuuGUID

GO
UPDATE NhapKhoCapCuu
SET SoLuongXuat = 0


















