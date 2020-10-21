CREATE TABLE [dbo].[Restaurants] (
    [RestaurantId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (255) NOT NULL,
    [Address]      NVARCHAR (255) NOT NULL,
    [City]         NVARCHAR (255) NOT NULL,
    [State]        CHAR (2)       NOT NULL,
    [ZipCode]      VARCHAR (10)   NOT NULL,
    CONSTRAINT [PK_Restaurants] PRIMARY KEY CLUSTERED ([RestaurantId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_City]
    ON [dbo].[Restaurants]([City] ASC);

