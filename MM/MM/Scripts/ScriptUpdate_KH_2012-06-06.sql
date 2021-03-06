USE MM
GO
ALTER TABLE XetNghiem_Hitachi917 
ADD [GroupID] [int] NOT NULL DEFAULT ((0)),
	[Order] [int] NOT NULL DEFAULT ((0))
GO
ALTER TABLE XetNghiem_Manual
ADD [GroupName] [nvarchar](50) NULL,
	[IsPosNeg] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE ChiTietKetQuaXetNghiem_Hitachi917 
ADD [LamThem] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE ChiTietKetQuaXetNghiem_CellDyn3200 
ADD [LamThem] [bit] NOT NULL DEFAULT ((0))
GO
ALTER TABLE ChiTietKetQuaXetNghiem_Manual
ADD [LamThem] [bit] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietKetQuaXetNghiem_Hitachi917View]
AS
SELECT     dbo.ChiTietKetQuaXetNghiem_Hitachi917.ChiTietKQXN_Hitachi917GUID, dbo.ChiTietKetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.TestNum, dbo.ChiTietKetQuaXetNghiem_Hitachi917.TestResult, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.AlarmCode, dbo.ChiTietKetQuaXetNghiem_Hitachi917.TinhTrang, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.CreatedDate, dbo.ChiTietKetQuaXetNghiem_Hitachi917.CreatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.UpdatedDate, dbo.ChiTietKetQuaXetNghiem_Hitachi917.UpdatedBy, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.DeletedDate, dbo.ChiTietKetQuaXetNghiem_Hitachi917.DeletedBy, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.Status, dbo.XetNghiem_Hitachi917.TenXetNghiem, dbo.XetNghiem_Hitachi917.Fullname, 
                      dbo.XetNghiem_Hitachi917.Type, dbo.XetNghiem_Hitachi917.XetNghiemGUID, dbo.KetQuaXetNghiem_Hitachi917.PatientGUID, 
                      dbo.KetQuaXetNghiem_Hitachi917.NgayXN, dbo.KetQuaXetNghiem_Hitachi917.Status AS KQXNStatus, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.FromValue, dbo.ChiTietKetQuaXetNghiem_Hitachi917.ToValue, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.DoiTuong, dbo.ChiTietKetQuaXetNghiem_Hitachi917.DonVi, dbo.ChiTietKetQuaXetNghiem_Hitachi917.DaIn, 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.DaUpload, dbo.XetNghiem_Hitachi917.GroupID, dbo.XetNghiem_Hitachi917.[Order], 
                      dbo.ChiTietKetQuaXetNghiem_Hitachi917.LamThem
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
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DeletedDate, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DeletedBy, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.Status, dbo.XetNghiem_CellDyn3200.FullName, dbo.XetNghiem_CellDyn3200.Type, 
                      dbo.XetNghiem_CellDyn3200.FromValue, dbo.XetNghiem_CellDyn3200.ToValue, dbo.XetNghiem_CellDyn3200.ToPercent, 
                      dbo.XetNghiem_CellDyn3200.DonVi, dbo.XetNghiem_CellDyn3200.FromPercent, dbo.XetNghiem_CellDyn3200.[Order], 
                      dbo.XetNghiem_CellDyn3200.GroupID, dbo.KetQuaXetNghiem_CellDyn3200.NgayXN, dbo.KetQuaXetNghiem_CellDyn3200.PatientGUID, 
                      dbo.KetQuaXetNghiem_CellDyn3200.Status AS KQXNStatus, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.FromValue AS FromValue2, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.ToValue AS ToValue2, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DoiTuong AS DoiTuong2, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DonVi AS DonVi2, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DaIn, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.FromPercent AS FromPercent2, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.ToPercent AS ToPercent2, 
                      dbo.ChiTietKetQuaXetNghiem_CellDyn3200.DaUpload, dbo.ChiTietKetQuaXetNghiem_CellDyn3200.LamThem
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
                      dbo.ChiTietKetQuaXetNghiem_Manual.XetNghiem_ManualGUID, dbo.ChiTietKetQuaXetNghiem_Manual.TestResult, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.TinhTrang, dbo.ChiTietKetQuaXetNghiem_Manual.CreatedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.CreatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.UpdatedBy, dbo.ChiTietKetQuaXetNghiem_Manual.DeletedDate, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.DeletedBy, dbo.ChiTietKetQuaXetNghiem_Manual.Status, dbo.XetNghiem_Manual.TenXetNghiem, 
                      dbo.XetNghiem_Manual.Fullname, dbo.XetNghiem_Manual.Type, dbo.XetNghiem_Manual.Status AS XetNghiemStatus, 
                      dbo.KetQuaXetNghiem_Manual.NgayXN, dbo.KetQuaXetNghiem_Manual.PatientGUID, dbo.KetQuaXetNghiem_Manual.Status AS KQXNStatus, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.DaIn, dbo.ChiTietKetQuaXetNghiem_Manual.FromValue, dbo.ChiTietKetQuaXetNghiem_Manual.ToValue, 
                      dbo.ChiTietKetQuaXetNghiem_Manual.DoiTuong, dbo.ChiTietKetQuaXetNghiem_Manual.DonVi, dbo.XetNghiem_Manual.[Order], 
                      dbo.XetNghiem_Manual.GroupID, dbo.ChiTietKetQuaXetNghiem_Manual.DaUpload, dbo.ChiTietKetQuaXetNghiem_Manual.LamThem
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
DECLARE @FromValue float
DECLARE @ToValue float
SELECT @FromValue = CT.FromValue, @ToValue = CT.ToValue FROM ChiTietXetNghiem_Hitachi917 CT, XetNghiem_Hitachi917 XN
WHERE XN.XetNghiemGUID = CT.XetNghiemGUID AND TestNum = 17 AND
DoiTuong = 0

UPDATE dbo.ChiTietKetQuaXetNghiem_Hitachi917
SET DoiTuong = 0, FromValue = @FromValue, ToValue = @ToValue
WHERE DoiTuong = 1 AND TestNum = 17
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 3, [Order] = 12
WHERE TenXetNghiem = 'RBC'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 3, [Order] = 13
WHERE TenXetNghiem = 'Hb'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 3, [Order] = 14
WHERE TenXetNghiem = 'Hct'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 3, [Order] = 15
WHERE TenXetNghiem = 'MCV'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 3, [Order] = 16
WHERE TenXetNghiem = 'MCH'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 3, [Order] = 17
WHERE TenXetNghiem = 'MCHC'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 3, [Order] = 18
WHERE TenXetNghiem = 'RDW'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 4, [Order] = 19
WHERE TenXetNghiem = 'PLT'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 4, [Order] = 20
WHERE TenXetNghiem = 'MPV'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 4, [Order] = 21
WHERE TenXetNghiem = 'PCT'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET GroupID = 4, [Order] = 22
WHERE TenXetNghiem = 'PDW'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Neu'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Mono'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Lym'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Eos'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Baso'
GO
UPDATE dbo.ChiTietKetQuaXetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Neu'
GO
UPDATE dbo.ChiTietKetQuaXetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Mono'
GO
UPDATE dbo.ChiTietKetQuaXetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Lym'
GO
UPDATE dbo.ChiTietKetQuaXetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Eos'
GO
UPDATE dbo.ChiTietKetQuaXetNghiem_CellDyn3200
SET DonVi = ''
WHERE TenXetNghiem = 'Baso'
GO
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [Order], [GroupID], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [DeletedDate], [DeletedBy], [Status]) VALUES (N'34f78f92-6c03-49a6-b183-2825e802fe70', N'Mono%', N'Mono%', N'Haematology', 0, 12, NULL, NULL, N'%M', 9, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [Order], [GroupID], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [DeletedDate], [DeletedBy], [Status]) VALUES (N'53450e3e-5824-4c9b-a064-33c32a7cae49', N'Neu%', N'Neu%', N'Haematology', 37, 80, NULL, NULL, N'%N', 7, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [Order], [GroupID], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [DeletedDate], [DeletedBy], [Status]) VALUES (N'dc86558b-0a0d-410e-a9ea-a615588cbe48', N'Lym%', N'Lym%', N'Haematology', 10, 50, NULL, NULL, N'%L', 8, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [Order], [GroupID], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [DeletedDate], [DeletedBy], [Status]) VALUES (N'68295b8d-2601-48eb-af97-dff24ca772cf', N'Eos%', N'Eos%', N'Haematology', 0, 7, NULL, NULL, N'%E', 10, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[XetNghiem_CellDyn3200] ([XetNghiemGUID], [TenXetNghiem], [FullName], [Type], [FromValue], [ToValue], [FromPercent], [ToPercent], [DonVi], [Order], [GroupID], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [DeletedDate], [DeletedBy], [Status]) VALUES (N'b748147b-13ae-4983-969d-e202ccf4412c', N'Baso%', N'Baso%', N'Haematology', 0, 2.5, NULL, NULL, N'%B', 11, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT INTO ChiTietKetQuaXetNghiem_CellDyn3200(ChiTietKQXN_CellDyn3200GUID, KQXN_CellDyn3200GUID, 
TenXetNghiem, TestResult, TinhTrang, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, 
DeletedBy, Status, DaIn, FromValue, ToValue, DoiTuong, DonVi, DaUpload, LamThem)
SELECT newid() AS ChiTietKQXN_CellDyn3200GUID, KQXN_CellDyn3200GUID, TenXetNghiem + '%', TestPercent AS TestResult, TinhTrang, CreatedDate, 
CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, DaIn, 
FromPercent AS FromValue, ToPercent AS ToValue, DoiTuong, DonVi, DaUpload, LamThem
FROM ChiTietKetQuaXetNghiem_CellDyn3200
WHERE TenXetNghiem IN ('Neu', 'Mono', 'Lym', 'Eos', 'Baso')
GO
DELETE FROM dbo.XetNghiem_Manual
GO
DELETE FROM dbo.KetQuaXetNghiem_Manual
GO