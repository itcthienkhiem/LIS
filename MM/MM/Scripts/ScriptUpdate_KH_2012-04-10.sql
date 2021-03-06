USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'f7e04917-a154-4fca-9de3-6e567b22029f', N'Booking', N'Lịch hẹn')
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 04/10/2012 10:11:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingGUID] [uniqueidentifier] NOT NULL,
	[BookingDate] [datetime] NOT NULL,
	[Company] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MorningCount] [int] NOT NULL,
	[AfternoonCount] [int] NOT NULL,
	[EveningCount] [int] NOT NULL,
	[Pax] [int] NOT NULL,
	[BookingType] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_Booking_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[BookingGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BookingView]    Script Date: 04/10/2012 10:11:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BookingView]
AS
SELECT     dbo.Booking.BookingGUID, dbo.Booking.BookingDate, dbo.Booking.Company, dbo.Booking.MorningCount, dbo.Booking.AfternoonCount, 
                      dbo.Booking.EveningCount, dbo.Booking.Pax, 
                      dbo.Booking.BookingType, dbo.Booking.CreatedDate, dbo.Booking.CreatedBy, dbo.Booking.UpdatedDate, dbo.Booking.UpdatedBy, 
                      dbo.Booking.DeletedDate, dbo.Booking.DeletedBy, dbo.Booking.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS Sales, 
                      dbo.DocStaffView.Archived
FROM         dbo.Booking LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Booking.CreatedBy = dbo.DocStaffView.DocStaffGUID

GO
