CREATE TABLE [dbo].[Review] (
	[RestaurantId] UNIQUEIDENTIFIER NOT NULL,
	[ReviewId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[UserName] NVARCHAR(100) NOT NULL,
	[Rating] INT NOT NULL,
	[Details] NVARCHAR(4000) NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[CreationDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_Review_ReviewId] PRIMARY KEY NONCLUSTERED ([ReviewId] ASC),
	CONSTRAINT [FK_Review_RestaurantId] FOREIGN KEY([RestaurantId]) REFERENCES [dbo].[Restaurant]([RestaurantId]));
GO;

CREATE UNIQUE CLUSTERED INDEX [CIX_Review_CreationDate] ON [dbo].[Review]([CreationDate]);

GO;