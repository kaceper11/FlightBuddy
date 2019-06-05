/****** Object:  Table [dbo].[User]    Script Date: 11.05.2019 19:45:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Email] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[MobileNumber] [nvarchar](50) NOT NULL,
	[deleted] [bit] NULL,
	[version] [timestamp] NOT NULL,
	[createdAt] [datetimeoffset](7) NOT NULL,
	[updatedAt] [datetimeoffset](7) NOT NULL,
	[Password] [nvarchar](50) NULL)
-- CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
--(
--	[Id] ASC
--)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__deleted__06CD04F7]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__createdAt__0C85DE4D]  DEFAULT (CONVERT([datetimeoffset](7),sysutcdatetime(),(0))) FOR [createdAt]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__updatedAt__0D7A0286]  DEFAULT (CONVERT([datetimeoffset](7),sysutcdatetime(),(0))) FOR [updatedAt]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Password]  DEFAULT (N'password') FOR [Password]
GO


