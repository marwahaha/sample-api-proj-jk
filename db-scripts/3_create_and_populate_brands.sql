USE [fool]
GO

/****** Object:  Table [dbo].[Brand]    Script Date: 4/16/2019 8:59:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Brand](
	[BrandCode] [char](2) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nchar](100) NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[BrandCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

INSERT INTO [dbo].[Brand] ([BrandCode], [Name], [Description]) VALUES (N'AU',  N'Australia', N'estum. gravum si rarendum glavans')
INSERT INTO [dbo].[Brand] ([BrandCode], [Name], [Description]) VALUES (N'DE',  N'Germany', N'Sed plurissimum quo Pro quo, dolorum')
INSERT INTO [dbo].[Brand] ([BrandCode], [Name], [Description]) VALUES (N'GB',  N'Great Britain', 'quoque quis novum eggredior.')
INSERT INTO [dbo].[Brand] ([BrandCode], [Name], [Description]) VALUES (N'US',  N'United States', 'nomen fecundio, bono trepicandor imaginator linguens')
GO
