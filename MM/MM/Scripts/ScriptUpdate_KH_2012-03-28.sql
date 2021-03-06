USE MM
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PatientView]
AS
SELECT     dbo.Contact.ContactGUID, dbo.Contact.Title, dbo.Contact.FirstName, dbo.Contact.SurName, dbo.Contact.KnownAs, dbo.Contact.MiddleName, 
                      dbo.Contact.AliasFirstName, dbo.Contact.AliasSurName, dbo.Contact.Dob, dbo.Contact.PreferredName, dbo.Contact.Occupation, 
                      dbo.Contact.IdentityCard, dbo.Contact.Archived, dbo.Contact.DateArchived, dbo.Contact.Note, dbo.Contact.HomePhone, dbo.Contact.WorkPhone, 
                      dbo.Contact.Mobile, dbo.Contact.Email, dbo.Contact.FAX, dbo.Contact.CreatedDate, dbo.Contact.CreatedBy, dbo.Contact.UpdatedDate, 
                      dbo.Contact.UpdatedBy, dbo.Contact.DeletedDate, dbo.Contact.DeletedBy, dbo.Contact.Gender, dbo.Contact.Address, dbo.Contact.Ward, 
                      dbo.Contact.District, dbo.Contact.City, dbo.Patient.FileNum, dbo.Patient.BarCode, dbo.Patient.Picture, dbo.Patient.HearFrom, dbo.Patient.Salutation, 
                      dbo.Patient.LastSeenDate, dbo.Patient.LastSeenDocGUID, dbo.Patient.DateDeceased, dbo.Patient.LastVisitGUID, 
                      CASE Gender WHEN 0 THEN N'Nam' WHEN 1 THEN N'Nữ' WHEN 2 THEN N'' END AS GenderAsStr, dbo.Patient.PatientGUID, dbo.Contact.DobStr, 
                      dbo.Contact.FullName, dbo.PatientHistory.Di_Ung_Thuoc, dbo.PatientHistory.Thuoc_Di_Ung, dbo.PatientHistory.Dot_Quy, 
                      dbo.PatientHistory.Benh_Tim_Mach, dbo.PatientHistory.Benh_Lao, dbo.PatientHistory.Dai_Thao_Duong, 
                      dbo.PatientHistory.Dai_Duong_Dang_Dieu_Tri, dbo.PatientHistory.Viem_Gan_B, dbo.PatientHistory.Viem_Gan_C, 
                      dbo.PatientHistory.Viem_Gan_Dang_Dieu_Tri, dbo.PatientHistory.Ung_Thu, dbo.PatientHistory.Co_Quan_Ung_Thu, dbo.PatientHistory.Dong_Kinh, 
                      dbo.PatientHistory.Hen_Suyen, dbo.PatientHistory.Benh_Khac, dbo.PatientHistory.Benh_Gi, dbo.PatientHistory.Thuoc_Dang_Dung, 
                      dbo.PatientHistory.Hut_Thuoc, dbo.PatientHistory.Uong_Ruou, dbo.PatientHistory.Tinh_Trang_Gia_Dinh, dbo.PatientHistory.Chich_Ngua_Viem_Gan_B, 
                      dbo.PatientHistory.Chich_Ngua_Uon_Van, dbo.PatientHistory.Chich_Ngua_Cum, dbo.PatientHistory.Dang_Co_Thai, 
                      dbo.PatientHistory.PatientHistoryGUID, dbo.Contact.Source, dbo.Contact.CompanyName
FROM         dbo.Contact INNER JOIN
                      dbo.Patient ON dbo.Contact.ContactGUID = dbo.Patient.ContactGUID INNER JOIN
                      dbo.PatientHistory ON dbo.Patient.PatientGUID = dbo.PatientHistory.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
GO
/****** Object:  Table [dbo].[KetQuaSoiCTC]    Script Date: 03/28/2012 16:12:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaSoiCTC](
	[KetQuaSoiCTCGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[BacSiSoi] [uniqueidentifier] NOT NULL,
	[NgayKham] [datetime] NOT NULL,
	[KetLuan] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DeNghi] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Hinh1] [image] NULL,
	[Hinh2] [image] NULL,
	[AmHo] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AmDao] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CTC] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BieuMoLat] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MoDem] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RanhGioiLatTru] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SauAcidAcetic] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SauLugol] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KetQuaSoiCTC_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KetQuaSoiCTC] PRIMARY KEY CLUSTERED 
(
	[KetQuaSoiCTCGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[KetQuaSoiCTC]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaSoiCTC_DocStaff] FOREIGN KEY([BacSiSoi])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
GO
ALTER TABLE [dbo].[KetQuaSoiCTC] CHECK CONSTRAINT [FK_KetQuaSoiCTC_DocStaff]
GO
ALTER TABLE [dbo].[KetQuaSoiCTC]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaSoiCTC_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KetQuaSoiCTC] CHECK CONSTRAINT [FK_KetQuaSoiCTC_Patient]
GO
GO
/****** Object:  View [dbo].[KetQuaSoiCTCView]    Script Date: 03/28/2012 16:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View [dbo].[KetQuaSoiCTCView]    Script Date: 03/28/2012 16:15:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[KetQuaSoiCTCView]
AS
SELECT     dbo.KetQuaSoiCTC.KetQuaSoiCTCGUID, dbo.KetQuaSoiCTC.PatientGUID, dbo.KetQuaSoiCTC.BacSiSoi, dbo.KetQuaSoiCTC.NgayKham, 
                      dbo.KetQuaSoiCTC.KetLuan, dbo.KetQuaSoiCTC.DeNghi, dbo.KetQuaSoiCTC.Hinh1, dbo.KetQuaSoiCTC.Hinh2, dbo.KetQuaSoiCTC.AmHo, 
                      dbo.KetQuaSoiCTC.AmDao, dbo.KetQuaSoiCTC.CTC, dbo.KetQuaSoiCTC.BieuMoLat, dbo.KetQuaSoiCTC.MoDem, dbo.KetQuaSoiCTC.RanhGioiLatTru, 
                      dbo.KetQuaSoiCTC.SauAcidAcetic, dbo.KetQuaSoiCTC.SauLugol, dbo.KetQuaSoiCTC.CreatedDate, dbo.KetQuaSoiCTC.CreatedBy, 
                      dbo.KetQuaSoiCTC.UpdatedDate, dbo.KetQuaSoiCTC.UpdatedBy, dbo.KetQuaSoiCTC.DeletedDate, dbo.KetQuaSoiCTC.DeletedBy, 
                      dbo.KetQuaSoiCTC.Status, dbo.DocStaffView.FullName, dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, dbo.DocStaffView.Archived
FROM         dbo.KetQuaSoiCTC INNER JOIN
                      dbo.DocStaffView ON dbo.KetQuaSoiCTC.BacSiSoi = dbo.DocStaffView.DocStaffGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'4ba97c97-607c-415b-aa0c-40bdb17543a3', N'KhamCTC', N'Khám CTC')
GO



















