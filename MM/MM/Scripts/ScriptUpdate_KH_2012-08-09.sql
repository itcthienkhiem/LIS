USE MM
GO
/****** Object:  Table [dbo].[TiemNgua]    Script Date: 08/09/2012 16:26:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiemNgua](
	[TiemNguaGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[Lan1] [datetime] NULL,
	[Lan2] [datetime] NULL,
	[Lan3] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_TiemNgua_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_TiemNgua] PRIMARY KEY CLUSTERED 
(
	[TiemNguaGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TiemNgua]  WITH CHECK ADD  CONSTRAINT [FK_TiemNgua_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TiemNgua] CHECK CONSTRAINT [FK_TiemNgua_Patient]
GO
/****** Object:  View [dbo].[TiemNguaView]    Script Date: 08/09/2012 16:26:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TiemNguaView]
AS
SELECT     dbo.TiemNgua.TiemNguaGUID, dbo.TiemNgua.PatientGUID, dbo.TiemNgua.Lan1, dbo.TiemNgua.Lan2, dbo.TiemNgua.Lan3, 
                      dbo.TiemNgua.CreatedDate, dbo.TiemNgua.UpdatedDate, dbo.TiemNgua.DeletedDate, dbo.TiemNgua.Status, dbo.PatientView.FullName, 
                      dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.PatientView.Archived, 
                      dbo.PatientView.IdentityCard, dbo.PatientView.Mobile, dbo.PatientView.WorkPhone, dbo.PatientView.HomePhone, dbo.PatientView.Email, 
                      dbo.TiemNgua.CreatedBy, dbo.TiemNgua.UpdatedBy, dbo.TiemNgua.DeletedBy
FROM         dbo.TiemNgua INNER JOIN
                      dbo.PatientView ON dbo.TiemNgua.PatientGUID = dbo.PatientView.PatientGUID
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'0196682a-773e-4379-9847-7b6182dc5d78', N'TiemNgua', N'Tiêm ngừa')

