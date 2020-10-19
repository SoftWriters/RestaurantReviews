CREATE TABLE [dbo].[Restaurant] (
	[RestaurantId] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Name] NVARCHAR(100) NOT NULL,
	[AddressLine1] NVARCHAR(100) NULL,
	[AddressLine2] NVARCHAR(100) NULL,
    [City] NVARCHAR(100) NULL,
    [State] NVARCHAR(2) NULL,
    [ZipCode] NVARCHAR(10) NULL,
    [Phone] NVARCHAR(12) NULL,
	[Website] NVARCHAR(100) NULL,
	[Email] NVARCHAR(320) NULL,
	[Description] NVARCHAR(500) NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[CreationDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_Restaurant_RestaurantId] PRIMARY KEY NONCLUSTERED ([RestaurantId] ASC));
GO;

CREATE UNIQUE CLUSTERED INDEX [CIX_Restaurant_CreationDate] ON [dbo].[Restaurant]([CreationDate]);

GO;