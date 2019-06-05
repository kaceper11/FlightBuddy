/****** Object:  Table [dbo].[Flight]    Script Date: 11.05.2019 19:44:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Flight](
	[FlightNumber] [nvarchar](10) NOT NULL,
	[AirlineCode] [nvarchar](10) NOT NULL,
	[OriginCode] [nvarchar](10) NOT NULL,
	[DestinationCode] [nvarchar](10) NOT NULL,
	[LeaveTimeAirport] [datetime] NOT NULL,
	[ArrivalTimeAirport] [datetime] NOT NULL,
	[deleted] [bit] NULL,
	[version] [timestamp] NOT NULL,
	[createdAt] [datetimeoffset](7) NOT NULL,
	[updatedAt] [datetimeoffset](7) NOT NULL)
-- CONSTRAINT [PK_Publish] PRIMARY KEY CLUSTERED 
--(
--	[Id] ASC
--)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__deleted__160F4887]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__createdA__17036CC0]  DEFAULT (CONVERT([datetimeoffset](7),sysutcdatetime(),(0))) FOR [createdAt]
GO

ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__updatedA__17F790F9]  DEFAULT (CONVERT([datetimeoffset](7),sysutcdatetime(),(0))) FOR [updatedAt]
GO


