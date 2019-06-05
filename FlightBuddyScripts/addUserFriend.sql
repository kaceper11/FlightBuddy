USE [FlightBuddy]
GO

INSERT INTO [dbo].[UserFriend]
           ([id]
           ,[createdAt]
           ,[updatedAt]
           ,[deleted]
           ,[UserId]
           ,[FriendId])
     VALUES
           ('71495c32-7532-47a9-b764-278ad81b466f'
           ,GETDATE()
           ,GETDATE()
           ,0
           ,'eef5869d-d91f-471b-a4d8-ff28e0b11f71'
           ,'2bed6dc0-0068-4126-aa46-40a5ba937df0')
GO


