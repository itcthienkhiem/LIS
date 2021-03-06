USE MM
GO
ALTER TABLE ServiceHistory
ADD [RootPatientGUID] [uniqueidentifier] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ServiceHistoryView]
AS
SELECT     dbo.ServiceHistory.PatientGUID, dbo.ServiceHistory.Price AS FixedPrice, dbo.ServiceHistory.Note, dbo.Services.ServiceGUID, dbo.Services.Code, 
                      dbo.Services.Name, dbo.Services.Price, dbo.ServiceHistory.CreatedDate, dbo.ServiceHistory.CreatedBy, dbo.ServiceHistory.UpdatedDate, 
                      dbo.ServiceHistory.UpdatedBy, dbo.ServiceHistory.DeletedDate, dbo.ServiceHistory.DeletedBy, dbo.DocStaff.AvailableToWork, 
                      dbo.ServiceHistory.ServiceHistoryGUID, dbo.ServiceHistory.Status, dbo.ServiceHistory.ActivedDate, dbo.Contact.FullName, 
                      dbo.ServiceHistory.IsExported, dbo.ServiceHistory.Discount, dbo.ServiceHistory.IsNormalOrNegative, dbo.ServiceHistory.Normal, 
                      dbo.ServiceHistory.Abnormal, dbo.ServiceHistory.Negative, dbo.ServiceHistory.Positive, dbo.Services.EnglishName, dbo.Services.Type, 
                      dbo.ServiceHistory.GiaVon, dbo.PatientView.Archived, dbo.PatientView.FullName AS TenBenhNhanChuyenNhuong, 
                      dbo.PatientView.FileNum AS MaBenhNhanChuyenNhuong, dbo.ServiceHistory.RootPatientGUID, dbo.ServiceHistory.DocStaffGUID
FROM         dbo.Services INNER JOIN
                      dbo.ServiceHistory ON dbo.Services.ServiceGUID = dbo.ServiceHistory.ServiceGUID LEFT OUTER JOIN
                      dbo.Contact INNER JOIN
                      dbo.DocStaff ON dbo.Contact.ContactGUID = dbo.DocStaff.ContactGUID ON 
                      dbo.ServiceHistory.DocStaffGUID = dbo.DocStaff.DocStaffGUID LEFT OUTER JOIN
                      dbo.PatientView ON dbo.ServiceHistory.RootPatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

