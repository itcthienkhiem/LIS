USE MM
GO
ALTER TABLE [Services]
ADD [StaffType] [tinyint] NULL
GO
ALTER TABLE [KetQuaLamSang]
ADD [PhuKhoaNote] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
/****** Object:  View [dbo].[ServiceView]    Script Date: 01/16/2012 21:31:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ServiceView]
AS
SELECT     ServiceGUID, Code, Name, EnglishName, Price, Description, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, Type, StaffType, 
                      CASE ISNULL(StaffType, 9) 
                      WHEN 0 THEN N'Bác sĩ' WHEN 1 THEN N'Điều dưỡng' WHEN 2 THEN N'Lễ tân' WHEN 4 THEN N'Admin' WHEN 5 THEN N'Xét nghiệm' WHEN 6 THEN N'Thư ký y khoa' WHEN
                       7 THEN N'Sale' WHEN 8 THEN N'Kế toán' WHEN 9 THEN N'' END AS StaffTypeStr
FROM         dbo.Services

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[UserView]
AS
SELECT     ISNULL(dbo.DocStaff.DocStaffGUID, '00000000-0000-0000-0000-000000000000') AS DocStaffGUID, ISNULL(dbo.Contact.FullName, 'Admin') 
                      AS FullName, ISNULL(dbo.DocStaff.AvailableToWork, 'True') AS AvailableToWork, ISNULL(dbo.DocStaff.StaffType, 4) AS StaffType, 
                      ISNULL(dbo.DocStaff.WorkType, 0) AS WorkType, dbo.Logon.LogonGUID, dbo.Logon.Status, dbo.Logon.Password, 
                      CASE ISNULL(dbo.DocStaff.StaffType, 4) 
                      WHEN 0 THEN N'Bác sĩ' WHEN 1 THEN N'Điều dưỡng' WHEN 2 THEN N'Lễ tân' WHEN 4 THEN N'Admin' WHEN 5 THEN N'Xét nghiệm' WHEN 6 THEN N'Thư ký y khoa'
                       WHEN 7 THEN N'Sale' WHEN 8 THEN N'Kế toán' WHEN 10 THEN N'Bác sĩ siêu âm' WHEN 11 THEN N'Bác sĩ ngoại tổng quát' WHEN 12 THEN N'Bác sĩ nội tổng quát'
                       END AS StaffTypeStr, dbo.Logon.CreatedDate, dbo.Logon.CreatedBy, dbo.Logon.UpdatedDate, dbo.Logon.UpdatedBy, dbo.Logon.DeletedDate, 
                      dbo.Logon.DeletedBy, dbo.Contact.FirstName, dbo.Contact.SurName
FROM         dbo.DocStaff INNER JOIN
                      dbo.Contact ON dbo.DocStaff.ContactGUID = dbo.Contact.ContactGUID RIGHT OUTER JOIN
                      dbo.Logon ON dbo.DocStaff.DocStaffGUID = dbo.Logon.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ServiceView]
AS
SELECT     ServiceGUID, Code, Name, EnglishName, Price, Description, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      Type, StaffType, CASE ISNULL(StaffType, 9) 
                      WHEN 0 THEN N'Bác sĩ' WHEN 1 THEN N'Điều dưỡng' WHEN 2 THEN N'Lễ tân' WHEN 4 THEN N'Admin' WHEN 5 THEN N'Xét nghiệm' WHEN 6 THEN N'Thư ký y khoa'
                       WHEN 7 THEN N'Sale' WHEN 8 THEN N'Kế toán' WHEN 9 THEN N'' WHEN 10 THEN N'Bác sĩ siêu âm' WHEN 11 THEN N'Bác sĩ ngoại tổng quát' WHEN
                       12 THEN N'Bác sĩ nội tổng quát' END AS StaffTypeStr
FROM         dbo.Services
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaLamSangView]
AS
SELECT     dbo.KetQuaLamSang.KetQuaLamSangGUID, dbo.KetQuaLamSang.NgayKham, dbo.KetQuaLamSang.PatientGUID, dbo.KetQuaLamSang.DocStaffGUID, 
                      dbo.KetQuaLamSang.CoQuan, dbo.KetQuaLamSang.Normal, dbo.KetQuaLamSang.Abnormal, dbo.KetQuaLamSang.Note, 
                      dbo.KetQuaLamSang.CreatedDate, dbo.KetQuaLamSang.CreatedBy, dbo.KetQuaLamSang.UpdatedDate, dbo.KetQuaLamSang.UpdatedBy, 
                      dbo.KetQuaLamSang.DeletedDate, dbo.KetQuaLamSang.DeletedBy, dbo.KetQuaLamSang.Status, dbo.DocStaffView.FullName, 
                      dbo.DocStaffView.FirstName, dbo.DocStaffView.SurName, dbo.DocStaffView.Archived, 
                      CASE CoQuan WHEN 0 THEN N'Eyes (Mắt)' WHEN 1 THEN N'Ear, Nose, Throat (Tai, mũi, họng)' WHEN 2 THEN N'Odontology (Răng, hàm, mặt)' WHEN 3 THEN
                       N'Respiratory system (Hô hấp)' WHEN 4 THEN N'Cardiovascular system (Tim mạch)' WHEN 5 THEN N'Gastro - intestinal system (Tiêu hóa)' WHEN 6 THEN
                       N'Genitourinary system (Tiết niệu, sinh dục)' WHEN 7 THEN N'Musculoskeletal system (Cơ, xương, khớp)' WHEN 8 THEN N'Dermatology (Da liễu)' WHEN 9 THEN
                       N'Neurological system (Thần kinh)' WHEN 10 THEN N'Endocrine system (Nội tiết)' WHEN 11 THEN N'Orthers (Các cơ quan khác)' WHEN 12 THEN N'Gynecology (Khám phụ khoa)'
                       END AS CoQuanStr, dbo.KetQuaLamSang.PARA, dbo.KetQuaLamSang.SoiTuoiHuyetTrang, dbo.KetQuaLamSang.NgayKinhChot, 
                      dbo.KetQuaLamSang.PhuKhoaNote
FROM         dbo.KetQuaLamSang INNER JOIN
                      dbo.DocStaffView ON dbo.KetQuaLamSang.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
