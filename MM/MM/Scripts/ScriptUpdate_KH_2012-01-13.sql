USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'3f253441-e31e-4b4b-98a0-1f8ccadf7a8b', N'InKetQuaKhamSucKhoeTongQuat', N'In kết quả khám sức khỏe tổng quát')
GO
UPDATE Contact
SET CompanyName = N'Tự túc'
WHERE CompanyName IS NULL
GO
ALTER TABLE PhieuThuThuoc
ADD [TenCongTy] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ToaThuocView]
AS
SELECT     dbo.ToaThuoc.ToaThuocGUID, dbo.ToaThuoc.MaToaThuoc, dbo.ToaThuoc.NgayKeToa, dbo.ToaThuoc.BacSiKeToa, dbo.ToaThuoc.BenhNhan, dbo.ToaThuoc.Note, 
                      dbo.ToaThuoc.CreatedDate, dbo.ToaThuoc.CreatedBy, dbo.ToaThuoc.UpdatedDate, dbo.ToaThuoc.UpdatedBy, dbo.ToaThuoc.DeletedDate, dbo.ToaThuoc.DeletedBy, 
                      dbo.ToaThuoc.Status, dbo.DocStaffView.FullName AS TenBacSi, dbo.PatientView.FullName AS TenBenhNhan, dbo.PatientView.GenderAsStr, dbo.PatientView.DobStr, 
                      dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.ToaThuoc.NgayKham, dbo.ToaThuoc.NgayTaiKham, dbo.ToaThuoc.ChanDoan, dbo.ToaThuoc.Loai, 
                      dbo.PatientView.Mobile, dbo.PatientView.HomePhone, dbo.PatientView.WorkPhone, CASE Loai WHEN 0 THEN N'Chung' WHEN 1 THEN N'Sản khoa' END AS LoaiStr, 
                      dbo.PatientView.CompanyName
FROM         dbo.ToaThuoc INNER JOIN
                      dbo.DocStaffView ON dbo.ToaThuoc.BacSiKeToa = dbo.DocStaffView.DocStaffGUID INNER JOIN
                      dbo.PatientView ON dbo.ToaThuoc.BenhNhan = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO