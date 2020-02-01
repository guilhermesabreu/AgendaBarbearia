USE [MvcCRUDDB]
GO

/****** Object:  Table [dbo].[login]    Script Date: 01/02/2020 12:48:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[login](
	[id_login] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](200) NULL,
	[senha] [varchar](50) NULL,
	[data_cadastro] [datetime] NULL,
	[login_email] [varchar](100) NULL,
 CONSTRAINT [PK_login] PRIMARY KEY CLUSTERED 
(
	[id_login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

