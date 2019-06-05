USE [FlightBuddy]
GO

INSERT INTO [dbo].[Flight]
           ([id]
           ,[createdAt]
           ,[updatedAt]
           ,[deleted]
           ,[FlightNumber]
           ,[AirlineCode]
           ,[OriginCode]
           ,[DestinationCode]
           ,[LeaveTimeAirport]
           ,[ArrivalTimeAirport])
     VALUES
           ('5d9e11f6-077a-4521-9d34-43fd6f2f96e6'
           ,GETDATE()
           ,GETDATE()
           ,0
           ,'123456'
           ,'GUT'
           ,'WAW'
           ,'MOD'
           ,GETDATE()
           ,DATEADD(hour, 4, GETDATE()))
GO


