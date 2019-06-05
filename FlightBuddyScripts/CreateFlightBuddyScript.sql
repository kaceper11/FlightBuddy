USE [master]
GO
/****** Object:  Database [FlightBuddy]    Script Date: 04.06.2019 10:04:05 ******/
CREATE DATABASE [FlightBuddy]
 WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS
GO
ALTER DATABASE [FlightBuddy] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FlightBuddy].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FlightBuddy] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FlightBuddy] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FlightBuddy] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FlightBuddy] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FlightBuddy] SET ARITHABORT OFF 
GO
ALTER DATABASE [FlightBuddy] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FlightBuddy] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FlightBuddy] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FlightBuddy] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FlightBuddy] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FlightBuddy] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FlightBuddy] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FlightBuddy] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FlightBuddy] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FlightBuddy] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FlightBuddy] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [FlightBuddy] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FlightBuddy] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FlightBuddy] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FlightBuddy] SET  MULTI_USER 
GO
ALTER DATABASE [FlightBuddy] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FlightBuddy] SET ENCRYPTION ON
GO
ALTER DATABASE [FlightBuddy] SET QUERY_STORE = ON
GO
ALTER DATABASE [FlightBuddy] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [FlightBuddy]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 04.06.2019 10:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[id] [nvarchar](255) NOT NULL,
	[createdAt] [datetimeoffset](3) NOT NULL,
	[updatedAt] [datetimeoffset](3) NULL,
	[version] [timestamp] NOT NULL,
	[deleted] [bit] NULL,
	[FlightNumber] [nvarchar](50) NOT NULL,
	[AirlineCode] [nvarchar](50) NOT NULL,
	[OriginCode] [nvarchar](50) NOT NULL,
	[DestinationCode] [nvarchar](50) NOT NULL,
	[LeaveTimeAirport] [datetimeoffset](3) NOT NULL,
	[ArriveTimeAirport] [datetimeoffset](3) NOT NULL,
	[Destination] [nvarchar](50) NULL,
	[Origin] [nvarchar](50) NULL,
	[Airline] [nvarchar](50) NULL,
	[Route] [nvarchar](max) NULL,
	[Time] [nvarchar](max) NULL,
 CONSTRAINT [PK__Flight__3213E83E9E37E69E] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [createdAt]    Script Date: 04.06.2019 10:04:05 ******/
CREATE CLUSTERED INDEX [createdAt] ON [dbo].[Flight]
(
	[createdAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 04.06.2019 10:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [nvarchar](255) NOT NULL,
	[createdAt] [datetimeoffset](3) NOT NULL,
	[updatedAt] [datetimeoffset](3) NULL,
	[version] [timestamp] NOT NULL,
	[deleted] [bit] NULL,
	[Email] [nvarchar](50) NOT NULL,
	[MobileNumber] [nvarchar](30) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[ProfilePictureUrl] [nvarchar](255) NULL,
	[Bio] [nvarchar](500) NULL,
	[FacebookId] [nvarchar](100) NULL,
 CONSTRAINT [PK__User__3213E83E666831E3] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [createdAt]    Script Date: 04.06.2019 10:04:05 ******/
CREATE CLUSTERED INDEX [createdAt] ON [dbo].[User]
(
	[createdAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFlight]    Script Date: 04.06.2019 10:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFlight](
	[id] [nvarchar](255) NOT NULL,
	[createdAt] [datetimeoffset](3) NOT NULL,
	[updatedAt] [datetimeoffset](3) NULL,
	[version] [timestamp] NOT NULL,
	[deleted] [bit] NULL,
	[UserId] [nvarchar](max) NULL,
	[FlightId] [nvarchar](max) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [createdAt]    Script Date: 04.06.2019 10:04:05 ******/
CREATE CLUSTERED INDEX [createdAt] ON [dbo].[UserFlight]
(
	[createdAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFriend]    Script Date: 04.06.2019 10:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFriend](
	[id] [nvarchar](255) NOT NULL,
	[createdAt] [datetimeoffset](3) NOT NULL,
	[updatedAt] [datetimeoffset](3) NULL,
	[version] [timestamp] NOT NULL,
	[deleted] [bit] NULL,
	[UserId] [nvarchar](max) NULL,
	[FriendId] [nvarchar](max) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [createdAt]    Script Date: 04.06.2019 10:04:05 ******/
CREATE CLUSTERED INDEX [createdAt] ON [dbo].[UserFriend]
(
	[createdAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF_Flight_id]  DEFAULT (CONVERT([nvarchar](255),newid(),(0))) FOR [id]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF_Flight_createdAt]  DEFAULT (CONVERT([datetimeoffset](3),sysutcdatetime(),(0))) FOR [createdAt]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__deleted__51300E55]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_id]  DEFAULT (CONVERT([nvarchar](255),newid(),(0))) FOR [id]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_createdAt]  DEFAULT (CONVERT([datetimeoffset](3),sysutcdatetime(),(0))) FOR [createdAt]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__deleted__4B7734FF]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[UserFlight] ADD  CONSTRAINT [DF_UserFlight_id]  DEFAULT (CONVERT([nvarchar](255),newid(),(0))) FOR [id]
GO
ALTER TABLE [dbo].[UserFlight] ADD  CONSTRAINT [DF_UserFlight_createdAt]  DEFAULT (CONVERT([datetimeoffset](3),sysutcdatetime(),(0))) FOR [createdAt]
GO
ALTER TABLE [dbo].[UserFlight] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[UserFriend] ADD  CONSTRAINT [DF_UserFriend_id]  DEFAULT (CONVERT([nvarchar](255),newid(),(0))) FOR [id]
GO
ALTER TABLE [dbo].[UserFriend] ADD  CONSTRAINT [DF_UserFriend_createdAt]  DEFAULT (CONVERT([datetimeoffset](3),sysutcdatetime(),(0))) FOR [createdAt]
GO
ALTER TABLE [dbo].[UserFriend] ADD  DEFAULT ((0)) FOR [deleted]
GO
USE [master]
GO
ALTER DATABASE [FlightBuddy] SET  READ_WRITE 
GO
