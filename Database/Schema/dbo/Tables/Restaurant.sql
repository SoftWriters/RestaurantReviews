CREATE TABLE [dbo].[Restaurant]
(
	[SystemId] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(200) NOT NULL,
	[AddressLine1] NVARCHAR(200) NOT NULL,
	[AddressLine2] NVARCHAR(200) NOT NULL,
	[City] NVARCHAR(200) NOT NULL,
	[StateProvince] NVARCHAR(200) NOT NULL,
	[PostalCode] NVARCHAR(30) NOT NULL,
	[Country] NVARCHAR(200) NOT NULL,
	[CreatedDate] DATETIME NOT NULL CONSTRAINT [DF_Restaurant_CreatedDate] DEFAULT(GETUTCDATE()),
	[ModifiedDate] DATETIME NOT NULL CONSTRAINT [DF_Restaurant_ModifiedDate] DEFAULT(GETUTCDATE()),
	CONSTRAINT [PK_Restaurant] PRIMARY KEY CLUSTERED ([SystemId])
)
GO

CREATE UNIQUE INDEX [AK1_Restaurant] ON [dbo].[Restaurant]
([AddressLine1], [AddressLine2], [City], [StateProvince], [PostalCode],	[Country])
GO
