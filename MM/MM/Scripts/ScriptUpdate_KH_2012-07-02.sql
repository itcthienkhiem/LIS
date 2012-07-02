USE MM
GO
ALTER TABLE XetNghiem_Manual
DROP CONSTRAINT DF_XetNghiem_Manual_IsPosNeg
GO
ALTER TABLE XetNghiem_Manual
DROP COLUMN IsPosNeg
GO
ALTER TABLE ChiTietXetNghiem_Manual
ADD [FromOperator] [char](2) NULL,
	[ToOperator] [char](2) NULL,
	[FromAge] [int] NULL,
	[ToAge] [int] NULL,
	[FromTime] [int] NULL,
	[ToTime] [int] NULL,
	[XValue] [float] NULL
GO
ALTER TABLE ChiTietKetQuaXetNghiem_Manual
ADD [FromOperator] [char](2) NULL,
	[ToOperator] [char](2) NULL,
	[FromAge] [int] NULL,
	[ToAge] [int] NULL,
	[FromTime] [int] NULL,
	[ToTime] [int] NULL,
	[XValue] [float] NULL,
	[HasHutThuoc] [bit] NULL,
	[GiaiDoan] [nvarchar](50) NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_ManualView]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_Manual.ChiTietKetQuaXetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.KetQuaXetNghiem_ManualGUID, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.TestResult, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.TinhTrang, dbo.ChiTietKetQuaXetNghiem_Manual.CreatedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.CreatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.DeletedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.DeletedBy, dbo.ChiTietKetQuaXetNghiem_Manual.Status, dbo.XetNghiem_Manual.TenXetNghiem, 
                      dbo.XetNghiem_Manual.Fullname, dbo.XetNghiem_Manual.Type, dbo.XetNghiem_Manual.Status AS XetNghiemStatus, 
                      dbo.KetQuaXetNghiem_Manual.NgayXN, dbo.KetQuaXetNghiem_Manual.PatientGUID, dbo.KetQuaXetNghiem_Manual.Status AS KQXNStatus, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.DaIn, dbo.ChiTietKetQuaXetNghiem_Manual.FromValue, dbo.ChiTietKetQuaXetNghiem_Manual.ToValue, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.DoiTuong, dbo.ChiTietKetQuaXetNghiem_Manual.DonVi, dbo.XetNghiem_Manual.[Order], 
                      dbo.XetNghiem_Manual.GroupID, dbo.ChiTietKetQuaXetNghiem_Manual.DaUpload, dbo.ChiTietKetQuaXetNghiem_Manual.LamThem, 
                      dbo.XetNghiem_Manual.GroupName, dbo.ChiTietKetQuaXetNghiem_Manual.HasHutThuoc, dbo.ChiTietKetQuaXetNghiem_Manual.GiaiDoan, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.FromOperator, dbo.ChiTietKetQuaXetNghiem_Manual.ToOperator, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.FromAge, dbo.ChiTietKetQuaXetNghiem_Manual.ToAge, dbo.ChiTietKetQuaXetNghiem_Manual.FromTime, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.ToTime, dbo.ChiTietKetQuaXetNghiem_Manual.XValue
FROM         dbo.ChiTietKetQuaXetNghiem_Manual INNER JOIN
                      dbo.XetNghiem_Manual ON 
                      dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID = dbo.XetNghiem_Manual.XetNghiem_ManualGUID INNER JOIN
                      dbo.KetQuaXetNghiem_Manual ON 
                      dbo.ChiTietKetQuaXetNghiem_Manual.KetQuaXetNghiem_ManualGUID = dbo.KetQuaXetNghiem_Manual.KetQuaXetNghiemManualGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO