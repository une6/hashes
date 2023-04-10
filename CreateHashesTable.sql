USE [hashdb]
GO

/****** Object:  Table [dbo].[hashtable]    Script Date: 7 Apr 2023 3:19:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[hashes](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NOT NULL,
	[sha1] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_hashes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[hashes] ADD  CONSTRAINT [DF_hashes_id]  DEFAULT (newsequentialid()) FOR [id]
GO


