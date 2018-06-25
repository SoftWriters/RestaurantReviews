CREATE TABLE [dbo].[RestaurantReview]
(
	[SystemId] INT NOT NULL IDENTITY(1,1),
	[RestaurantSystemId] INT NOT NULL,
	[ReviewerSystemId] INT NOT NULL,
	[ReviewDate] DATETIME NOT NULL,
	[NumberOfStars] TINYINT NOT NULL CONSTRAINT [CHK_RestaurantReview_NumberOfStars] CHECK ([NumberOfStars] >= 1 AND [NumberOfStars] <= 5),
	[Text] NVARCHAR(MAX) NULL,
	[CreatedDate] DATETIME NOT NULL CONSTRAINT [DF_RestaurantReview_CreatedDate] DEFAULT(GETUTCDATE()),
	[ModifiedDate] DATETIME NOT NULL CONSTRAINT [DF_RestaurantReview_ModifiedDate] DEFAULT(GETUTCDATE()),
	CONSTRAINT [PK_RestaurantReview] PRIMARY KEY CLUSTERED ([SystemId]),
	CONSTRAINT [FK_RestaurantReview_Restaurant] FOREIGN KEY ([RestaurantSystemId]) REFERENCES [Restaurant]([SystemId]),
	CONSTRAINT [FK_RestaurantReview_Reviewer] FOREIGN KEY ([ReviewerSystemId]) REFERENCES [Reviewer]([SystemId])
)
GO

CREATE UNIQUE INDEX [AK1_RestaurantReview] ON [dbo].[RestaurantReview]
([RestaurantSystemId], [ReviewerSystemId], [ReviewDate])
GO