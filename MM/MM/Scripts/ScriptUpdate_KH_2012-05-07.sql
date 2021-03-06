USE MM
GO
ALTER TABLE ChiTietKetQuaXetNghiem_Hitachi917 
ADD [DaIn] [bit] NOT NULL DEFAULT ((0)),
	[FromValue] [float] NULL,
	[ToValue] [float] NULL,
	[DoiTuong] [tinyint] NULL,
	[DonVi] [nvarchar](50) NULL
GO
ALTER TABLE ChiTietKetQuaXetNghiem_CellDyn3200 
ADD [DaIn] [bit] NOT NULL DEFAULT ((0)),
	[FromValue] [float] NULL,
	[ToValue] [float] NULL,
	[DoiTuong] [tinyint] NULL,
	[DonVi] [nvarchar](50) NULL,
	[FromPercent] [float] NULL,
	[ToPercent] [float] NULL
GO
ALTER TABLE ChiTietKetQuaXetNghiem_Manual 
ADD [DaIn] [bit] NOT NULL DEFAULT ((0)),
	[FromValue] [float] NULL,
	[ToValue] [float] NULL,
	[DoiTuong] [tinyint] NULL,
	[DonVi] [nvarchar](50) NULL
GO
ALTER TABLE XetNghiem_Manual 
ADD [Order] [int] NULL,
	[GroupID] [int] NULL
GO
ALTER TABLE XetNghiem_Hitachi917 
ADD [CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL DEFAULT ((0))
GO
ALTER TABLE XetNghiem_CellDyn3200 
ADD [CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL DEFAULT ((0))
GO
ALTER TABLE ChiTietXetNghiem_Hitachi917 
ADD [CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_Hitachi917View]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_Hitachi917.ChiTietKQXN_Hitachi917GUID, dbo.ChiTietKetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.TestNum, dbo.ChiTietKetQuaXetNghiem_Hitachi917.TestResult, dbo.ChiTietKetQuaXetNghiem_Hitachi917.AlarmCode, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.TinhTrang, dbo.ChiTietKetQuaXetNghiem_Hitachi917.CreatedDate, dbo.ChiTietKetQuaXetNghiem_Hitachi917.CreatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.UpdatedDate, dbo.ChiTietKetQuaXetNghiem_Hitachi917.UpdatedBy, dbo.ChiTietKetQuaXetNghiem_Hitachi917.DeletedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.DeletedBy, dbo.ChiTietKetQuaXetNghiem_Hitachi917.Status, dbo.XetNghiem_Hitachi917.TenXetNghiem, 
                      dbo.XetNghiem_Hitachi917.Fullname, dbo.XetNghiem_Hitachi917.Type, dbo.XetNghiem_Hitachi917.XetNghiemGUID, dbo.KetQuaXetNghiem_Hitachi917.PatientGUID, 
                      dbo.KetQuaXetNghiem_Hitachi917.NgayXN, dbo.KetQuaXetNghiem_Hitachi917.Status AS KQXNStatus, dbo.ChiTietKetQuaXetNghiem_Hitachi917.FromValue, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.ToValue, dbo.ChiTietKetQuaXetNghiem_Hitachi917.DoiTuong, dbo.ChiTietKetQuaXetNghiem_Hitachi917.DonVi, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.DaIn
FROM         dbo.ChiTietKetQuaXetNghiem_Hitachi917 INNER JOIN
                      dbo.KetQuaXetNghiem_Hitachi917 ON 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID = dbo.KetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID LEFT OUTER JOIN
                      dbo.XetNghiem_Hitachi917 ON dbo.ChiTietKetQuaXetNghiem_Hitachi917.TestNum = dbo.XetNghiem_Hitachi917.TestNum
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_CellDyn3200View]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_CellDyn3200.ChiTietKQXN_CellDyn3200GUID, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TenXetNghiem, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TestResult, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TestPercent, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TinhTrang, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.CreatedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.CreatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.UpdatedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.UpdatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DeletedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DeletedBy, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.Status, 
                      dbo.XetNghiem_CellDyn3200.FullName, dbo.XetNghiem_CellDyn3200.Type, dbo.XetNghiem_CellDyn3200.FromValue, dbo.XetNghiem_CellDyn3200.ToValue, 
                      dbo.XetNghiem_CellDyn3200.ToPercent, dbo.XetNghiem_CellDyn3200.DonVi, dbo.XetNghiem_CellDyn3200.FromPercent, dbo.XetNghiem_CellDyn3200.[Order], 
                      dbo.XetNghiem_CellDyn3200.GroupID, dbo.KetQuaXetNghiem_CellDyn3200.NgayXN, dbo.KetQuaXetNghiem_CellDyn3200.PatientGUID, 
                      dbo.KetQuaXetNghiem_CellDyn3200.Status AS KQXNStatus, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.FromValue AS FromValue2, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.ToValue AS ToValue2, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DoiTuong AS DoiTuong2, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DonVi AS DonVi2, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DaIn, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.FromPercent AS FromPercent2, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.ToPercent AS ToPercent2
FROM         dbo.ChiTietKetQuaXetNghiem_CellDyn3200 INNER JOIN
                      dbo.KetQuaXetNghiem_CellDyn3200 ON 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID = dbo.KetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID LEFT OUTER JOIN
                      dbo.XetNghiem_CellDyn3200 ON dbo.ChiTietKetQuaXetNghiem_CellDyn3200.TenXetNghiem = dbo.XetNghiem_CellDyn3200.TenXetNghiem
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_ManualView]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_Manual.ChiTietKetQuaXetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.KetQuaXetNghiem_ManualGUID, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.TestResult, dbo.ChiTietKetQuaXetNghiem_Manual.TinhTrang, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.CreatedDate, dbo.ChiTietKetQuaXetNghiem_Manual.CreatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.DeletedDate, dbo.ChiTietKetQuaXetNghiem_Manual.DeletedBy, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.Status, dbo.XetNghiem_Manual.TenXetNghiem, dbo.XetNghiem_Manual.Fullname, dbo.XetNghiem_Manual.Type, 
                      dbo.XetNghiem_Manual.Status AS XetNghiemStatus, dbo.KetQuaXetNghiem_Manual.NgayXN, dbo.KetQuaXetNghiem_Manual.PatientGUID, 
                      dbo.KetQuaXetNghiem_Manual.Status AS KQXNStatus, dbo.ChiTietKetQuaXetNghiem_Manual.DaIn, dbo.ChiTietKetQuaXetNghiem_Manual.FromValue, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.ToValue, dbo.ChiTietKetQuaXetNghiem_Manual.DoiTuong, dbo.ChiTietKetQuaXetNghiem_Manual.DonVi, 
                      dbo.XetNghiem_Manual.[Order], dbo.XetNghiem_Manual.GroupID
FROM         dbo.ChiTietKetQuaXetNghiem_Manual INNER JOIN
                      dbo.XetNghiem_Manual ON dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID = dbo.XetNghiem_Manual.XetNghiem_ManualGUID INNER JOIN
                      dbo.KetQuaXetNghiem_Manual ON 
                      dbo.ChiTietKetQuaXetNghiem_Manual.KetQuaXetNghiem_ManualGUID = dbo.KetQuaXetNghiem_Manual.KetQuaXetNghiemManualGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
UPDATE XetNghiem_Manual
SET GroupID = 0, [Order] = 0
GO
