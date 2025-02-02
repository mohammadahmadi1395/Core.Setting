USE [Setting]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentID] [bigint] NULL,
	[BranchAddressID] [bigint] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[HeadPersonID] [bigint] NULL,
	[BranchPhoneNo] [nvarchar](50) NULL,
	[BranchEmail] [nvarchar](50) NULL,
	[IsCentral] [bit] NOT NULL CONSTRAINT [DF_Branch_IsCentral]  DEFAULT ((0)),
	[Comment] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NULL,
	[LeftIndex] [bigint] NULL,
	[RightIndex] [bigint] NULL,
	[Depth] [bigint] NULL,
	[OldCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BranchAddress]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchAddress](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ZoneID] [bigint] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_BranchAddress_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_BranchAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BranchRegionWork]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchRegionWork](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BranchID] [bigint] NOT NULL,
	[ZoneID] [bigint] NOT NULL,
	[IsDeleted] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BranchAreaWork] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Currency]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Symbol] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExchangeRate]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeRate](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FromCurrencyID] [bigint] NOT NULL,
	[ToCurrencyID] [bigint] NOT NULL,
	[Ratio] [float] NOT NULL,
	[Year] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ExchangeRate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormType]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[EnumID] [int] NULL,
	[SubSystemID] [bigint] NOT NULL,
	[PublicCode] [nvarchar](50) NOT NULL,
	[Coment] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_FormType_IsDeleted]  DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_FormType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GeneratedForm]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneratedForm](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PublicCode] [nvarchar](50) NOT NULL,
	[PrivateCode] [nvarchar](50) NOT NULL,
	[SubsystemID] [bigint] NOT NULL,
	[UniqeCode] [bigint] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_GeneratedForm_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_GeneratedForm] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EntityID] [int] NOT NULL,
	[ActionTypeID] [int] NOT NULL,
	[RecordID] [bigint] NULL,
	[Message] [nvarchar](max) NULL,
	[GroupID] [bigint] NULL,
	[RegistrantPersonID] [bigint] NULL,
	[RegistrantPersonFullName] [nvarchar](100) NULL,
	[GroupName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrganizationalChart]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationalChart](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_OrganizationalChart_IsDeleted]  DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL,
	[ParentID] [bigint] NULL,
	[Code] [nvarchar](50) NULL,
	[LeftIndex] [bigint] NULL,
	[RightIndex] [bigint] NULL,
	[Depth] [bigint] NULL,
	[OldCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_OrganizationalChart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Prefix]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prefix](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[IsDefault] [bit] NOT NULL CONSTRAINT [DF_Prefix_IsDefault]  DEFAULT ((0)),
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Prefix_IsDeleted]  DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Prefix] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rule]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rule](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Type] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_PublicRule_IsDeleted]  DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PublicRule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RuleTag]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RuleTag](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[RuleID] [bigint] NOT NULL,
	[FormTypeID] [bigint] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_RuleTag_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_RuleTag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Statement]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statement](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](50) NOT NULL,
	[PersianText] [nvarchar](50) NOT NULL,
	[EnglishText] [nvarchar](50) NOT NULL,
	[ArabicText] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_Statement] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatementSubsystem]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementSubsystem](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatementID] [bigint] NOT NULL,
	[SubsystemID] [bigint] NOT NULL,
 CONSTRAINT [PK_StatementSubsystem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subpart]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subpart](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsSystem] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[SubsystemID] [bigint] NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Subpart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subsystem]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subsystem](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ShortName] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IsSystem] [bit] NULL,
	[IsActive] [bit] NULL,
	[RunOrder] [int] NULL,
	[IsPart] [bit] NOT NULL CONSTRAINT [DF_Subsystem_IsPart]  DEFAULT ((0)),
 CONSTRAINT [PK_Subsystem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Typeoforganization]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Typeoforganization](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Typeoforganization_IsDeleted]  DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Typeoforganization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Zone]    Script Date: 6/16/2019 10:05:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zone](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[ParentID] [bigint] NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Zone_IsDeleted]  DEFAULT ((0)),
	[LeftIndex] [bigint] NULL,
	[RightIndex] [bigint] NULL,
	[Depth] [bigint] NULL,
	[OldCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Zone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_Branch] FOREIGN KEY([ID])
REFERENCES [dbo].[Branch] ([ID])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_Branch]
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_BranchAddress] FOREIGN KEY([BranchAddressID])
REFERENCES [dbo].[BranchAddress] ([ID])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_BranchAddress]
GO
ALTER TABLE [dbo].[BranchAddress]  WITH CHECK ADD  CONSTRAINT [FK_BranchAddress_Zone] FOREIGN KEY([ZoneID])
REFERENCES [dbo].[Zone] ([ID])
GO
ALTER TABLE [dbo].[BranchAddress] CHECK CONSTRAINT [FK_BranchAddress_Zone]
GO
ALTER TABLE [dbo].[BranchRegionWork]  WITH CHECK ADD  CONSTRAINT [FK_BranchRegionWork_Branch] FOREIGN KEY([BranchID])
REFERENCES [dbo].[Branch] ([ID])
GO
ALTER TABLE [dbo].[BranchRegionWork] CHECK CONSTRAINT [FK_BranchRegionWork_Branch]
GO
ALTER TABLE [dbo].[BranchRegionWork]  WITH CHECK ADD  CONSTRAINT [FK_BranchRegionWork_Zone] FOREIGN KEY([ZoneID])
REFERENCES [dbo].[Zone] ([ID])
GO
ALTER TABLE [dbo].[BranchRegionWork] CHECK CONSTRAINT [FK_BranchRegionWork_Zone]
GO
ALTER TABLE [dbo].[ExchangeRate]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeRate_Currency] FOREIGN KEY([FromCurrencyID])
REFERENCES [dbo].[Currency] ([ID])
GO
ALTER TABLE [dbo].[ExchangeRate] CHECK CONSTRAINT [FK_ExchangeRate_Currency]
GO
ALTER TABLE [dbo].[ExchangeRate]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeRate_Currency1] FOREIGN KEY([ToCurrencyID])
REFERENCES [dbo].[Currency] ([ID])
GO
ALTER TABLE [dbo].[ExchangeRate] CHECK CONSTRAINT [FK_ExchangeRate_Currency1]
GO
ALTER TABLE [dbo].[FormType]  WITH CHECK ADD  CONSTRAINT [FK_FormType_Subsystem] FOREIGN KEY([SubSystemID])
REFERENCES [dbo].[Subsystem] ([ID])
GO
ALTER TABLE [dbo].[FormType] CHECK CONSTRAINT [FK_FormType_Subsystem]
GO
ALTER TABLE [dbo].[OrganizationalChart]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationalChart_OrganizationalChart] FOREIGN KEY([ParentID])
REFERENCES [dbo].[OrganizationalChart] ([ID])
GO
ALTER TABLE [dbo].[OrganizationalChart] CHECK CONSTRAINT [FK_OrganizationalChart_OrganizationalChart]
GO
ALTER TABLE [dbo].[RuleTag]  WITH CHECK ADD  CONSTRAINT [FK_RuleTag_FormType] FOREIGN KEY([FormTypeID])
REFERENCES [dbo].[FormType] ([ID])
GO
ALTER TABLE [dbo].[RuleTag] CHECK CONSTRAINT [FK_RuleTag_FormType]
GO
ALTER TABLE [dbo].[RuleTag]  WITH CHECK ADD  CONSTRAINT [FK_RuleTag_Rule] FOREIGN KEY([RuleID])
REFERENCES [dbo].[Rule] ([ID])
GO
ALTER TABLE [dbo].[RuleTag] CHECK CONSTRAINT [FK_RuleTag_Rule]
GO
ALTER TABLE [dbo].[StatementSubsystem]  WITH CHECK ADD  CONSTRAINT [FK_StatementSubsystem_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([ID])
GO
ALTER TABLE [dbo].[StatementSubsystem] CHECK CONSTRAINT [FK_StatementSubsystem_Statement]
GO
ALTER TABLE [dbo].[StatementSubsystem]  WITH CHECK ADD  CONSTRAINT [FK_StatementSubsystem_Subsystem] FOREIGN KEY([SubsystemID])
REFERENCES [dbo].[Subsystem] ([ID])
GO
ALTER TABLE [dbo].[StatementSubsystem] CHECK CONSTRAINT [FK_StatementSubsystem_Subsystem]
GO
ALTER TABLE [dbo].[Subpart]  WITH CHECK ADD  CONSTRAINT [FK_Subpart_Subsystem] FOREIGN KEY([SubsystemID])
REFERENCES [dbo].[Subsystem] ([ID])
GO
ALTER TABLE [dbo].[Subpart] CHECK CONSTRAINT [FK_Subpart_Subsystem]
GO
ALTER TABLE [dbo].[Zone]  WITH CHECK ADD  CONSTRAINT [FK_Zone_Zone] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Zone] ([ID])
GO
ALTER TABLE [dbo].[Zone] CHECK CONSTRAINT [FK_Zone_Zone]
GO
