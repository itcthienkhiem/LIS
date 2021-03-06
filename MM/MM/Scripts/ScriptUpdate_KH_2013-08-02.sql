USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'1789c09c-4f46-4976-b9cc-9dc57e6b16bc', N'HuyThuoc', N'Hủy thuốc')
GO
/****** Object:  Table [dbo].[HuyThuoc]    Script Date: 08/02/2013 08:45:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HuyThuoc](
	[HuyThuocGUID] [uniqueidentifier] NOT NULL,
	[ThuocGUID] [uniqueidentifier] NOT NULL,
	[NgayHuy] [datetime] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_HuyThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_HuyThuoc] PRIMARY KEY CLUSTERED 
(
	[HuyThuocGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[HuyThuoc]  WITH CHECK ADD  CONSTRAINT [FK_HuyThuoc_Thuoc] FOREIGN KEY([ThuocGUID])
REFERENCES [dbo].[Thuoc] ([ThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HuyThuoc] CHECK CONSTRAINT [FK_HuyThuoc_Thuoc]
GO
GO
/****** Object:  Table [dbo].[ChiTietHuyThuoc]    Script Date: 08/03/2013 10:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHuyThuoc](
	[ChiTietHuyThuocGUID] [uniqueidentifier] NOT NULL,
	[HuyThuocGUID] [uniqueidentifier] NOT NULL,
	[LoThuocGUID] [uniqueidentifier] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietHuyThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietHuyThuoc] PRIMARY KEY CLUSTERED 
(
	[ChiTietHuyThuocGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietHuyThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHuyThuoc_HuyThuoc] FOREIGN KEY([HuyThuocGUID])
REFERENCES [dbo].[HuyThuoc] ([HuyThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHuyThuoc] CHECK CONSTRAINT [FK_ChiTietHuyThuoc_HuyThuoc]
GO
ALTER TABLE [dbo].[ChiTietHuyThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHuyThuoc_LoThuoc] FOREIGN KEY([LoThuocGUID])
REFERENCES [dbo].[LoThuoc] ([LoThuocGUID])
GO
ALTER TABLE [dbo].[ChiTietHuyThuoc] CHECK CONSTRAINT [FK_ChiTietHuyThuoc_LoThuoc]
GO
/****** Object:  View [dbo].[HuyThuocView]    Script Date: 08/02/2013 08:45:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[HuyThuocView]
AS
SELECT     dbo.HuyThuoc.HuyThuocGUID, dbo.HuyThuoc.ThuocGUID, dbo.HuyThuoc.NgayHuy, dbo.HuyThuoc.SoLuong, dbo.HuyThuoc.CreatedDate, dbo.HuyThuoc.CreatedBy, 
                      dbo.HuyThuoc.UpdatedDate, dbo.HuyThuoc.UpdatedBy, dbo.HuyThuoc.DeletedDate, dbo.HuyThuoc.DeletedBy, dbo.HuyThuoc.Note, dbo.HuyThuoc.Status, 
                      dbo.Thuoc.MaThuoc, dbo.Thuoc.TenThuoc, dbo.Thuoc.DonViTinh, dbo.Thuoc.BietDuoc, dbo.Thuoc.HoatChat, dbo.Thuoc.HamLuong
FROM         dbo.HuyThuoc INNER JOIN
                      dbo.Thuoc ON dbo.HuyThuoc.ThuocGUID = dbo.Thuoc.ThuocGUID

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
	DECLARE @SLHuy INT

	DECLARE @ThuocTonKho table 
	(
		ThuocGUID nvarchar(50),
		MaThuoc nvarchar(100),
		TenThuoc nvarchar(255),	
		DonViTinh nvarchar(50),
		SoDu int,
		SLNhap int,
		SLXuat int,
		SLHuy int,
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

			--SL Huy Truoc Tu Ngay
			SET @SLHuy = (SELECT SUM(SoLuong) FROM HuyThuoc WITH(NOLOCK)
			WHERE Status = 0 AND ThuocGUID = @ThuocGUID AND NgayHuy < @TuNgay)
			IF (@SLHuy IS NULL) SET @SLHuy = 0
			--PRINT @SLHuy

			-- So Du Dau
			SET @SoDu = @SLNhap - @SLXuat - @SLHuy
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

			--SL Huy Trong Khoang Tu Ngay - Den Ngay
			SET @SLHuy = (SELECT SUM(SoLuong) FROM HuyThuoc WITH(NOLOCK)
			WHERE Status = 0 AND ThuocGUID = @ThuocGUID AND NgayHuy BETWEEN @TuNgay AND @DenNgay)
			IF (@SLHuy IS NULL) SET @SLHuy = 0
			--PRINT @SLHuy

			--SL Ton
			SET @SLTon = @SoDu + @SLNhap - @SLXuat - @SLHuy

			INSERT INTO @ThuocTonKho
			SELECT @ThuocGUID, @MaThuoc, @TenThuoc, @DVT, @SoDu, @SLNhap, @SLXuat, @SLHuy, @SLTon
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










