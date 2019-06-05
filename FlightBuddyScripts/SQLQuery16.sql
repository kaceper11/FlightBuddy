/****** Object:  Table [dbo].[UserFlight]    Script Date: 11.05.2019 19:45:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserFlight](
	[UserId] [nvarchar](50) NOT NULL,
	[FlightId] [int] NOT NULL,
	[version] [timestamp] NOT NULL,
	[createdAt] [datetimeoffset](7) NOT NULL,
	[updatedAt] [datetimeoffset](7) NOT NULL,
	[deleted] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserFlight] ADD  CONSTRAINT [DF__UserFligh__creat__19DFD96B]  DEFAULT (CONVERT([datetimeoffset](7),sysutcdatetime(),(0))) FOR [createdAt]
GO

ALTER TABLE [dbo].[UserFlight] ADD  CONSTRAINT [DF__UserFligh__updat__1AD3FDA4]  DEFAULT (CONVERT([datetimeoffset](7),sysutcdatetime(),(0))) FOR [updatedAt]
GO

ALTER TABLE [dbo].[UserFlight] ADD  CONSTRAINT [DF__UserFligh__delet__22751F6C]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[UserFlight]  WITH CHECK ADD  CONSTRAINT [FK_UserFlight_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserFlight] CHECK CONSTRAINT [FK_UserFlight_User]
GO


