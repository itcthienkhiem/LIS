USE MM
GO
ALTER TABLE InvoiceDetail
ADD [Loai] [tinyint] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'8536d855-34c6-4d5e-8e3d-922d97e19602', N'ThongKeThuocXuatHoaDon', N'Thống kê thuốc xuất hóa đơn')


