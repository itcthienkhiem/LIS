USE MM
GO
/****** Object:  Table [dbo].[Tracking]    Script Date: 01/04/2012 11:26:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tracking](
	[TrackingGUID] [uniqueidentifier] NOT NULL,
	[TrackingDate] [datetime] NOT NULL,
	[DocStaffGUID] [uniqueidentifier] NOT NULL,
	[ActionType] [tinyint] NOT NULL,
	[Action] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrackingType] [tinyint] NULL,
 CONSTRAINT [PK_Tracking] PRIMARY KEY CLUSTERED 
(
	[TrackingGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TrackingView]    Script Date: 01/04/2012 11:26:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TrackingView]
AS
SELECT     dbo.Tracking.TrackingGUID, dbo.Tracking.TrackingDate, dbo.Tracking.DocStaffGUID, dbo.Tracking.ActionType, dbo.Tracking.Description, 
                      dbo.DocStaffView.FirstName, dbo.DocStaffView.SurName, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS FullName, dbo.DocStaffView.GenderAsStr, 
                      dbo.DocStaffView.Address, dbo.DocStaffView.DobStr, dbo.DocStaffView.Status, dbo.DocStaffView.AvailableToWork, dbo.DocStaffView.Archived, 
                      dbo.Tracking.Action, dbo.Tracking.TrackingType
FROM         dbo.Tracking LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Tracking.DocStaffGUID = dbo.DocStaffView.DocStaffGUID

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CompanyMemberView]
AS
SELECT     dbo.CompanyMember.CompanyMemberGUID, dbo.CompanyMember.CompanyGUID, dbo.CompanyMember.PatientGUID, 
                      dbo.CompanyMember.CreatedDate, dbo.CompanyMember.CreatedBy, dbo.CompanyMember.UpdatedDate, dbo.CompanyMember.UpdatedBy, 
                      dbo.CompanyMember.DeletedDate, dbo.CompanyMember.DeletedBy, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.Occupation, 
                      dbo.PatientView.IdentityCard, dbo.PatientView.HomePhone, dbo.PatientView.WorkPhone, dbo.PatientView.Mobile, dbo.PatientView.Email, 
                      dbo.PatientView.FAX, dbo.PatientView.GenderAsStr, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.CompanyMember.Status, 
                      dbo.PatientView.Archived, dbo.PatientView.Source, dbo.PatientView.FirstName, dbo.PatientView.SurName, dbo.PatientView.Di_Ung_Thuoc, 
                      dbo.PatientView.Thuoc_Di_Ung, dbo.PatientView.Dot_Quy, dbo.PatientView.Benh_Tim_Mach, dbo.PatientView.Benh_Lao, 
                      dbo.PatientView.Dai_Thao_Duong, dbo.PatientView.Dai_Duong_Dang_Dieu_Tri, dbo.PatientView.Viem_Gan_B, dbo.PatientView.Viem_Gan_C, 
                      dbo.PatientView.Viem_Gan_Dang_Dieu_Tri, dbo.PatientView.Ung_Thu, dbo.PatientView.Co_Quan_Ung_Thu, dbo.PatientView.Dong_Kinh, 
                      dbo.PatientView.Hen_Suyen, dbo.PatientView.Benh_Khac, dbo.PatientView.Benh_Gi, dbo.PatientView.Thuoc_Dang_Dung, dbo.PatientView.Hut_Thuoc, 
                      dbo.PatientView.Uong_Ruou, dbo.PatientView.Tinh_Trang_Gia_Dinh, dbo.PatientView.Chich_Ngua_Viem_Gan_B, 
                      dbo.PatientView.Chich_Ngua_Uon_Van, dbo.PatientView.Chich_Ngua_Cum, dbo.PatientView.Dang_Co_Thai, dbo.PatientView.PatientHistoryGUID, 
                      dbo.PatientView.CompanyName
FROM         dbo.CompanyMember INNER JOIN
                      dbo.PatientView ON dbo.CompanyMember.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
                      dbo.CompanyMemberView.CompanyGUID, dbo.CompanyMemberView.FullName, dbo.CompanyMemberView.FileNum, 
                      dbo.CompanyMemberView.Address, dbo.CompanyMemberView.GenderAsStr, dbo.CompanyMemberView.FAX, dbo.CompanyMemberView.Email, 
                      dbo.CompanyMemberView.Mobile, dbo.CompanyMemberView.WorkPhone, dbo.CompanyMemberView.HomePhone, 
                      dbo.CompanyMemberView.IdentityCard, dbo.CompanyMemberView.DobStr, dbo.CompanyMemberView.Occupation, 
                      dbo.CompanyMemberView.Status AS CompanyMemberStatus, dbo.CompanyMemberView.Source, dbo.CompanyMemberView.Archived, 
                      dbo.CompanyMemberView.FirstName, dbo.CompanyMemberView.SurName, dbo.CompanyMemberView.Di_Ung_Thuoc, 
                      dbo.CompanyMemberView.Thuoc_Di_Ung, dbo.CompanyMemberView.Dot_Quy, dbo.CompanyMemberView.Benh_Tim_Mach, 
                      dbo.CompanyMemberView.Benh_Lao, dbo.CompanyMemberView.Dai_Thao_Duong, dbo.CompanyMemberView.Dai_Duong_Dang_Dieu_Tri, 
                      dbo.CompanyMemberView.Viem_Gan_B, dbo.CompanyMemberView.Viem_Gan_C, dbo.CompanyMemberView.Viem_Gan_Dang_Dieu_Tri, 
                      dbo.CompanyMemberView.Ung_Thu, dbo.CompanyMemberView.Co_Quan_Ung_Thu, dbo.CompanyMemberView.Dong_Kinh, 
                      dbo.CompanyMemberView.Hen_Suyen, dbo.CompanyMemberView.Benh_Khac, dbo.CompanyMemberView.Benh_Gi, 
                      dbo.CompanyMemberView.Thuoc_Dang_Dung, dbo.CompanyMemberView.Hut_Thuoc, dbo.CompanyMemberView.Uong_Ruou, 
                      dbo.CompanyMemberView.Tinh_Trang_Gia_Dinh, dbo.CompanyMemberView.Chich_Ngua_Viem_Gan_B, 
                      dbo.CompanyMemberView.Chich_Ngua_Uon_Van, dbo.CompanyMemberView.Dang_Co_Thai, dbo.CompanyMemberView.Chich_Ngua_Cum, 
                      dbo.CompanyMemberView.PatientHistoryGUID, dbo.CompanyMemberView.CompanyName
FROM         dbo.ContractMember INNER JOIN
                      dbo.CompanyMemberView ON dbo.ContractMember.CompanyMemberGUID = dbo.CompanyMemberView.CompanyMemberGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
