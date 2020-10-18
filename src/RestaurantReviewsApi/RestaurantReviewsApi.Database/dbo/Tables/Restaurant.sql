CREATE TABLE [dbo].[Restaurant] (
	[SystemId] INT IDENTITY (1, 1) NOT NULL ,
	[RestaurantId] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Name] NVARCHAR(100) NOT NULL,
	[AddressLine1] NVARCHAR(100) NULL,
	[AddressLine2] NVARCHAR(100) NULL,
    [City] NVARCHAR(100) NULL,
    [State] NVARCHAR(2) NULL,
    [ZipCode] NVARCHAR(10) NULL,
    [Phone] NVARCHAR(12) NULL,
	[Website] NVARCHAR(100) NULL,
	[Email] NVARCHAR(100) NULL,
	[Description] NVARCHAR(500) NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Restaurant_RestaurantId] PRIMARY KEY NONCLUSTERED ([RestaurantId] ASC));

GO;

CREATE UNIQUE CLUSTERED INDEX [CIX_Restaurant_SystemId] ON [dbo].[Restaurant](SystemId);

GO;