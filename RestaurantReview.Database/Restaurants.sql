CREATE TABLE [dbo].[Restaurants]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CityID] INT NOT NULL, 
    [Name] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Restaurants_ToCity] FOREIGN KEY (CityID) REFERENCES Cities(Id)
)
