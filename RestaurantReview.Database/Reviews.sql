CREATE TABLE [dbo].[Reviews]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [RestaurantID] INT NOT NULL, 
    [UserID] INT NOT NULL, 
    [Rating] INT NOT NULL, 
    [Comments] TEXT NOT NULL, 
    [DateCreated] DATE NOT NULL, 
    CONSTRAINT [FK_Reviews_Restaurant] FOREIGN KEY (RestaurantID) REFERENCES Restaurants(Id), 
    CONSTRAINT [FK_Reviews_User] FOREIGN KEY (UserID) REFERENCES Users(Id)
)

GO

CREATE INDEX [IX_Reviews_UserID] ON [dbo].[Reviews] (UserID)

GO
