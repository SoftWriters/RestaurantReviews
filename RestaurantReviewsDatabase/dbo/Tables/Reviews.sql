CREATE TABLE [dbo].[Reviews] (
    [ReviewId]     INT            IDENTITY (1, 1) NOT NULL,
    [RestaurantId] INT            NOT NULL,
    [UserId]       INT            NOT NULL,
    [Content]      NVARCHAR (MAX) NOT NULL,
    [Active]       BIT            CONSTRAINT [DF_Active] DEFAULT ((1)) NOT NULL,
    [DateCreated]  DATETIME       CONSTRAINT [DF_DateCreated] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED ([ReviewId] ASC),
    CONSTRAINT [FK_Reviews_Restaurants] FOREIGN KEY ([RestaurantId]) REFERENCES [dbo].[Restaurants] ([RestaurantId]),
    CONSTRAINT [FK_Reviews_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Reviews]([UserId] ASC);
