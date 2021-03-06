USE MM
GO
ALTER TABLE TiemNgua
ADD [DaChich1] [bit] NULL,
	[DaChich2] [bit] NULL,
	[DaChich3] [bit] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[TiemNguaView]
AS
SELECT     dbo.TiemNgua.TiemNguaGUID, dbo.TiemNgua.PatientGUID, dbo.TiemNgua.Lan1, dbo.TiemNgua.Lan2, dbo.TiemNgua.Lan3, 
                      dbo.TiemNgua.CreatedDate, dbo.TiemNgua.UpdatedDate, dbo.TiemNgua.DeletedDate, dbo.TiemNgua.Status, dbo.PatientView.FullName, 
                      dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.PatientView.Archived, 
                      dbo.PatientView.IdentityCard, dbo.PatientView.Mobile, dbo.PatientView.WorkPhone, dbo.PatientView.HomePhone, dbo.PatientView.Email, 
                      dbo.TiemNgua.CreatedBy, dbo.TiemNgua.UpdatedBy, dbo.TiemNgua.DeletedBy, dbo.TiemNgua.DaChich1, dbo.TiemNgua.DaChich2, 
                      dbo.TiemNgua.DaChich3
FROM         dbo.TiemNgua INNER JOIN
                      dbo.PatientView ON dbo.TiemNgua.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





