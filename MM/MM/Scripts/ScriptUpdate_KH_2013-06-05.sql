USE MM
GO
ALTER TABLE ReceiptDetail
ADD [SoLuong] [int] NOT NULL DEFAULT ((1))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ReceiptDetailView]
AS
SELECT     dbo.Services.Status AS ServiceStatus, dbo.ReceiptDetail.ReceiptDetailGUID, dbo.ReceiptDetail.ReceiptGUID, dbo.ReceiptDetail.ServiceHistoryGUID, 
                      dbo.ReceiptDetail.CreatedDate, dbo.ReceiptDetail.CreatedBy, dbo.ReceiptDetail.UpdatedDate, dbo.ReceiptDetail.UpdatedBy, dbo.ReceiptDetail.DeletedDate, 
                      dbo.ReceiptDetail.DeletedBy, dbo.ServiceHistory.Price, dbo.ServiceHistory.Discount, dbo.ServiceHistory.Note, dbo.Services.ServiceGUID, dbo.Services.Code, 
                      dbo.Services.Name, dbo.ReceiptDetail.Status AS ReceiptDetailStatus, dbo.ServiceHistory.Status AS ServiceHistoryStatus, dbo.ServiceHistory.GiaVon, 
                      dbo.ReceiptDetail.SoLuong
FROM         dbo.ReceiptDetail INNER JOIN
                      dbo.ServiceHistory ON dbo.ReceiptDetail.ServiceHistoryGUID = dbo.ServiceHistory.ServiceHistoryGUID INNER JOIN
                      dbo.Services ON dbo.ServiceHistory.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
GO

/****** Object:  StoredProcedure [dbo].[spDoanhThuNhanVienChiTiet]    Script Date: 06/08/2013 11:50:50 ******/
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
/****** Object:  StoredProcedure [dbo].[spDoanhThuNhanVienTongHop]    Script Date: 06/08/2013 11:50:57 ******/
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



