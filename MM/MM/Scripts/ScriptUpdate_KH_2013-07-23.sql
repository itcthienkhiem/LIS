USE MM
GO
ALTER TABLE [Tracking]
ADD [ComputerName] [nvarchar](255) NULL
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[TrackingView]
AS
SELECT     dbo.Tracking.TrackingGUID, dbo.Tracking.TrackingDate, dbo.Tracking.DocStaffGUID, dbo.Tracking.ActionType, dbo.Tracking.Description, dbo.DocStaffView.FirstName, 
                      dbo.DocStaffView.SurName, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS FullName, dbo.DocStaffView.GenderAsStr, dbo.DocStaffView.Address, 
                      dbo.DocStaffView.DobStr, dbo.DocStaffView.Status, dbo.DocStaffView.AvailableToWork, dbo.DocStaffView.Archived, dbo.Tracking.Action, dbo.Tracking.TrackingType, 
                      CASE ActionType WHEN 0 THEN N'Thêm' WHEN 1 THEN N'Sửa' WHEN 2 THEN N'Xóa' END AS ActionTypeStr, dbo.Tracking.ComputerName
FROM         dbo.Tracking LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Tracking.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'4c8b59e8-0e18-4070-ba41-51d0f0d49408', N'TraHoSo', N'Trả hồ sơ')
GO
ALTER TABLE [ContractMember]
ADD [IsTraHoSo] [bit] NOT NULL DEFAULT ((0)),
	[NgayTra] [datetime] NULL
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ContractMemberView]
AS
SELECT     dbo.ContractMember.ContractMemberGUID, dbo.ContractMember.CompanyMemberGUID, dbo.ContractMember.CompanyContractGUID, 
                      dbo.ContractMember.CreatedDate, dbo.ContractMember.CreatedBy, dbo.ContractMember.UpdatedBy, dbo.ContractMember.UpdatedDate, 
                      dbo.ContractMember.DeletedDate, dbo.ContractMember.DeletedBy, dbo.ContractMember.Status, dbo.CompanyMemberView.PatientGUID, 
                      dbo.CompanyMemberView.CompanyGUID, dbo.CompanyMemberView.FullName, dbo.CompanyMemberView.FileNum, dbo.CompanyMemberView.Address, 
                      dbo.CompanyMemberView.GenderAsStr, dbo.CompanyMemberView.FAX, dbo.CompanyMemberView.Email, dbo.CompanyMemberView.Mobile, 
                      dbo.CompanyMemberView.WorkPhone, dbo.CompanyMemberView.HomePhone, dbo.CompanyMemberView.IdentityCard, dbo.CompanyMemberView.DobStr, 
                      dbo.CompanyMemberView.Occupation, dbo.CompanyMemberView.Status AS CompanyMemberStatus, dbo.CompanyMemberView.Source, 
                      dbo.CompanyMemberView.Archived, dbo.CompanyMemberView.FirstName, dbo.CompanyMemberView.SurName, dbo.CompanyMemberView.Di_Ung_Thuoc, 
                      dbo.CompanyMemberView.Thuoc_Di_Ung, dbo.CompanyMemberView.Dot_Quy, dbo.CompanyMemberView.Benh_Tim_Mach, dbo.CompanyMemberView.Benh_Lao, 
                      dbo.CompanyMemberView.Dai_Thao_Duong, dbo.CompanyMemberView.Dai_Duong_Dang_Dieu_Tri, dbo.CompanyMemberView.Viem_Gan_B, 
                      dbo.CompanyMemberView.Viem_Gan_C, dbo.CompanyMemberView.Viem_Gan_Dang_Dieu_Tri, dbo.CompanyMemberView.Ung_Thu, 
                      dbo.CompanyMemberView.Co_Quan_Ung_Thu, dbo.CompanyMemberView.Dong_Kinh, dbo.CompanyMemberView.Hen_Suyen, dbo.CompanyMemberView.Benh_Khac, 
                      dbo.CompanyMemberView.Benh_Gi, dbo.CompanyMemberView.Thuoc_Dang_Dung, dbo.CompanyMemberView.Hut_Thuoc, dbo.CompanyMemberView.Uong_Ruou, 
                      dbo.CompanyMemberView.Tinh_Trang_Gia_Dinh, dbo.CompanyMemberView.Chich_Ngua_Viem_Gan_B, dbo.CompanyMemberView.Chich_Ngua_Uon_Van, 
                      dbo.CompanyMemberView.Dang_Co_Thai, dbo.CompanyMemberView.Chich_Ngua_Cum, dbo.CompanyMemberView.PatientHistoryGUID, 
                      dbo.CompanyMemberView.CompanyName, dbo.ContractMember.IsTraHoSo, dbo.ContractMember.NgayTra, 
                      CASE IsTraHoSo WHEN 0 THEN N'Chưa trả' WHEN 1 THEN N'Đã trả' END AS TraHoSo
FROM         dbo.ContractMember INNER JOIN
                      dbo.CompanyMemberView ON dbo.ContractMember.CompanyMemberGUID = dbo.CompanyMemberView.CompanyMemberGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO









