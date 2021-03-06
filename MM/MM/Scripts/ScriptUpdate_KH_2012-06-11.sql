USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'd141173b-2da7-4c97-bc6c-ec385022d0a2', N'ChiTietPhieuThuDichVu', N'Chi tiết phiếu thu dịch vụ')
GO
/****** Object:  View [dbo].[ChiTietPhieuThuDichVuView]    Script Date: 06/11/2012 08:54:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietPhieuThuDichVuView]
AS
SELECT     dbo.Services.Status AS ServiceStatus, dbo.ReceiptDetail.ReceiptDetailGUID, dbo.ReceiptDetail.ReceiptGUID, dbo.ReceiptDetail.ServiceHistoryGUID, 
                      dbo.ReceiptDetail.CreatedDate, dbo.ReceiptDetail.CreatedBy, dbo.ReceiptDetail.UpdatedDate, dbo.ReceiptDetail.UpdatedBy, 
                      dbo.ReceiptDetail.DeletedDate, dbo.ReceiptDetail.DeletedBy, dbo.ServiceHistory.Price, dbo.ServiceHistory.Discount, dbo.ServiceHistory.Note, 
                      dbo.Services.ServiceGUID, dbo.Services.Code, dbo.Services.Name, dbo.ReceiptDetail.Status AS ReceiptDetailStatus, 
                      dbo.ServiceHistory.Status AS ServiceHistoryStatus, dbo.ServiceHistory.GiaVon, dbo.Receipt.ReceiptCode, dbo.Receipt.ReceiptDate, 
                      dbo.Receipt.ChuaThuTien, dbo.Receipt.Notes, dbo.Receipt.IsExportedInVoice, dbo.Receipt.Status, dbo.PatientView.PatientGUID, 
                      dbo.PatientView.DobStr, dbo.PatientView.FullName, dbo.PatientView.GenderAsStr, dbo.PatientView.FileNum, dbo.PatientView.Address, 
                      dbo.PatientView.Archived
FROM         dbo.ReceiptDetail INNER JOIN
                      dbo.ServiceHistory ON dbo.ReceiptDetail.ServiceHistoryGUID = dbo.ServiceHistory.ServiceHistoryGUID INNER JOIN
                      dbo.Services ON dbo.ServiceHistory.ServiceGUID = dbo.Services.ServiceGUID INNER JOIN
                      dbo.Receipt ON dbo.ReceiptDetail.ReceiptGUID = dbo.Receipt.ReceiptGUID INNER JOIN
                      dbo.PatientView ON dbo.Receipt.PatientGUID = dbo.PatientView.PatientGUID

GO