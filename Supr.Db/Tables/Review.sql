CREATE TABLE [dbo].[Review]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	 [RestaurantId] INT NOT NULL, 
	 [Comments] TEXT NOT NULL, 
	 [Rating] INT NULL, 
	 [UserId] NVARCHAR(256) NULL, 
	 [Created] DATETIME NOT NULL DEFAULT getdate()
)

GO
