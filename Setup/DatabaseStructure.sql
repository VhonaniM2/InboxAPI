USE [EmailServerDB]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 2021/10/11 10:25:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND type in (N'U'))
DROP TABLE [dbo].[UserProfile]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 2021/10/11 10:25:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserProfile](
	[UserID] [uniqueidentifier] NOT NULL,
	[Username] [varchar](100) NULL,
	[Password] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
	[FullNames] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [EmailServerDB]
GO

ALTER TABLE [dbo].[InboxMessages] DROP CONSTRAINT [FK_UserID_UserProfile]
GO

/****** Object:  Table [dbo].[InboxMessages]    Script Date: 2021/10/11 10:25:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InboxMessages]') AND type in (N'U'))
DROP TABLE [dbo].[InboxMessages]
GO

/****** Object:  Table [dbo].[InboxMessages]    Script Date: 2021/10/11 10:25:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InboxMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Message] [varchar](max) NULL,
	[FromAddress] [varchar](50) NULL,
	[ToAddress] [varchar](max) NULL,
	[Deleted] [bit] NOT NULL,
	[LabelId] [int] NULL,
	[DateReceived] [datetime] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_InboxMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[InboxMessages]  WITH CHECK ADD  CONSTRAINT [FK_UserID_UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserID])
GO

ALTER TABLE [dbo].[InboxMessages] CHECK CONSTRAINT [FK_UserID_UserProfile]
GO

USE [EmailServerDB]
GO

/****** Object:  Table [dbo].[Label]    Script Date: 2021/10/11 10:25:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Label]') AND type in (N'U'))
DROP TABLE [dbo].[Label]
GO

/****** Object:  Table [dbo].[Label]    Script Date: 2021/10/11 10:25:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Label](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Label] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO






