﻿CREATE TABLE [dbo].[Reviewer]
(
	[SystemId] INT NOT NULL IDENTITY(1,1),
	[Email] NVARCHAR(256) NOT NULL,
	[CreatedDate] DATETIME NOT NULL CONSTRAINT [DF_Reviewer_CreatedDate] DEFAULT(GETUTCDATE()),
	[ModifiedDate] DATETIME NOT NULL CONSTRAINT [DF_Reviewer_ModifiedDate] DEFAULT(GETUTCDATE()),
	CONSTRAINT [PK_Reviewer] PRIMARY KEY CLUSTERED ([SystemId])
)
GO

CREATE UNIQUE INDEX [AK1_Reviewer] ON [dbo].[Reviewer]
([Email])
GO