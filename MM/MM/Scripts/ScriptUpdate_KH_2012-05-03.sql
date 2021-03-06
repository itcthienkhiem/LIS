USE MM
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaXetNghiem_Hitachi917View]
AS
SELECT     dbo.KetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID, dbo.KetQuaXetNghiem_Hitachi917.IDNum, dbo.KetQuaXetNghiem_Hitachi917.PatientGUID, 
                      dbo.KetQuaXetNghiem_Hitachi917.NgayXN, dbo.KetQuaXetNghiem_Hitachi917.OperationID, dbo.KetQuaXetNghiem_Hitachi917.CreatedDate, 
                      dbo.KetQuaXetNghiem_Hitachi917.CreatedBy, dbo.KetQuaXetNghiem_Hitachi917.UpdatedDate, dbo.KetQuaXetNghiem_Hitachi917.UpdatedBy, 
                      dbo.KetQuaXetNghiem_Hitachi917.DeletedDate, dbo.KetQuaXetNghiem_Hitachi917.DeletedBy, dbo.KetQuaXetNghiem_Hitachi917.Status, 
                      dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.FileNum, dbo.PatientView.GenderAsStr, dbo.PatientView.Archived, 
                      dbo.KetQuaXetNghiem_Hitachi917.Sex, dbo.KetQuaXetNghiem_Hitachi917.Age, dbo.KetQuaXetNghiem_Hitachi917.AgeUnit, 
                      dbo.PatientView.Address
FROM         dbo.KetQuaXetNghiem_Hitachi917 LEFT OUTER JOIN
                      dbo.PatientView ON dbo.KetQuaXetNghiem_Hitachi917.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaXetNghiem_CellDyn3200View]
AS
SELECT     dbo.KetQuaXetNghiem_CellDyn3200.KQXN_CellDyn3200GUID, dbo.KetQuaXetNghiem_CellDyn3200.MessageType, 
                      dbo.KetQuaXetNghiem_CellDyn3200.InstrumentType, dbo.KetQuaXetNghiem_CellDyn3200.SerialNum, 
                      dbo.KetQuaXetNghiem_CellDyn3200.SequenceNum, dbo.KetQuaXetNghiem_CellDyn3200.SpareField, 
                      dbo.KetQuaXetNghiem_CellDyn3200.SpecimenType, dbo.KetQuaXetNghiem_CellDyn3200.SpecimenID, 
                      dbo.KetQuaXetNghiem_CellDyn3200.SpecimenName, dbo.KetQuaXetNghiem_CellDyn3200.PatientID, 
                      dbo.KetQuaXetNghiem_CellDyn3200.SpecimenSex, dbo.KetQuaXetNghiem_CellDyn3200.SpecimenDOB, dbo.KetQuaXetNghiem_CellDyn3200.DrName, 
                      dbo.KetQuaXetNghiem_CellDyn3200.OperatorID, dbo.KetQuaXetNghiem_CellDyn3200.NgayXN, dbo.KetQuaXetNghiem_CellDyn3200.CollectionDate, 
                      dbo.KetQuaXetNghiem_CellDyn3200.CollectionTime, dbo.KetQuaXetNghiem_CellDyn3200.Comment, dbo.KetQuaXetNghiem_CellDyn3200.PatientGUID, 
                      dbo.KetQuaXetNghiem_CellDyn3200.CreatedDate, dbo.KetQuaXetNghiem_CellDyn3200.CreatedBy, dbo.KetQuaXetNghiem_CellDyn3200.UpdatedDate, 
                      dbo.KetQuaXetNghiem_CellDyn3200.UpdatedBy, dbo.KetQuaXetNghiem_CellDyn3200.DeletedDate, dbo.KetQuaXetNghiem_CellDyn3200.DeletedBy, 
                      dbo.KetQuaXetNghiem_CellDyn3200.Status, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, 
                      dbo.PatientView.FileNum, dbo.PatientView.Archived, dbo.PatientView.Address
FROM         dbo.KetQuaXetNghiem_CellDyn3200 LEFT OUTER JOIN
                      dbo.PatientView ON dbo.KetQuaXetNghiem_CellDyn3200.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaXetNghiem_ManualView]
AS
SELECT     dbo.KetQuaXetNghiem_Manual.KetQuaXetNghiemManualGUID, dbo.KetQuaXetNghiem_Manual.NgayXN, dbo.KetQuaXetNghiem_Manual.PatientGUID, 
                      dbo.KetQuaXetNghiem_Manual.CreatedDate, dbo.KetQuaXetNghiem_Manual.CreatedBy, dbo.KetQuaXetNghiem_Manual.UpdatedDate, 
                      dbo.KetQuaXetNghiem_Manual.UpdatedBy, dbo.KetQuaXetNghiem_Manual.DeletedDate, dbo.KetQuaXetNghiem_Manual.DeletedBy, 
                      dbo.KetQuaXetNghiem_Manual.Status, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.Archived, 
                      dbo.PatientView.FileNum, dbo.PatientView.Address
FROM         dbo.KetQuaXetNghiem_Manual LEFT OUTER JOIN
                      dbo.PatientView ON dbo.KetQuaXetNghiem_Manual.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

