USE [Rabota]
GO

/****** Object:  Table [dbo].[RABOTA]    Script Date: 23.10.2020 13:46:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RABOTA](
    [ID] [int] IDENTITY (1,1) NOT NULL,
	[Number] [int]  NOT NULL,
	[Summ] [decimal](18, 0) NOT NULL,
	[Date] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO

CREATE CLUSTERED INDEX RABOTA_ID 
   ON [dbo].[RABOTA] (Id)
GO

CREATE UNIQUE INDEX Unique_Number    
   ON [dbo].[RABOTA] (Number)
GO