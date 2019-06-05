USE [FlightBuddy]
GO

INSERT INTO [dbo].[UserFlight]
           ([id]
           ,[createdAt]
           ,[updatedAt]
           ,[deleted]
           ,[UserId]
           ,[FlightId])
     VALUES
           ('9e5e6cd1-43c8-49c4-a58c-f48c7715790f'
           ,GETDATE()
           ,GETDATE()
           ,0
           ,'eef5869d-d91f-471b-a4d8-ff28e0b11f71'
           ,'5d9e11f6-077a-4521-9d34-43fd6f2f96e6')
GO


