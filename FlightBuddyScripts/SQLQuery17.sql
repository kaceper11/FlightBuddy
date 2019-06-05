/****** Object:  Table [dbo].[UserFriend]    Script Date: 11.05.2019 19:45:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserFriend](
	[UserId] [nvarchar](50) NOT NULL,
	[FriendId] [nvarchar](50) NOT NULL,
	[deleted] [bit] NULL,
	[version] [timestamp] NOT NULL,
	[createdAt] [datetimeoffset](7) NOT NULL,
	[updatedAt] [datetimeoffset](7) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserFriend] ADD  CONSTRAINT [DF__UserFrien__delet__1DB06A4F]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[UserFriend] ADD  CONSTRAINT [DF__UserFrien__creat__1EA48E88]  DEFAULT (CONVERT([datetimeoffset](7),sysutcdatetime(),(0))) FOR [createdAt]
GO

ALTER TABLE [dbo].[UserFriend] ADD  CONSTRAINT [DF__UserFrien__updat__1F98B2C1]  DEFAULT (CONVERT([datetimeoffset](7),sysutcdatetime(),(0))) FOR [updatedAt]
GO

ALTER TABLE [dbo].[UserFriend]  WITH CHECK ADD  CONSTRAINT [FK_UserFriend_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserFriend] CHECK CONSTRAINT [FK_UserFriend_User]
GO

ALTER TABLE [dbo].[UserFriend]  WITH CHECK ADD  CONSTRAINT [FK_UserFriend_UserFriend] FOREIGN KEY([FriendId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserFriend] CHECK CONSTRAINT [FK_UserFriend_UserFriend]
GO


