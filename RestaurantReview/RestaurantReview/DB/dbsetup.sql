use master;
IF DB_ID('RestaurantReviewManager') IS NOT NULL
 DROP database RestaurantReviewManager

create database RestaurantReviewManager;
IF DB_ID('RestaurantReviewManager') IS NOT NULL
	use RestaurantReviewManager;


CREATE TABLE Restaurants(
	RestaurantId int identity(1,1) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	City VARCHAR(50) NOT NULL,

	CONSTRAINT PK_RestaurantId PRIMARY KEY (RestaurantId)
);

CREATE TABLE Users(
	UserId INT IDENTITY(1,1) NOT NULL,
	UserName VARCHAR(50) NOT NULL,

	CONSTRAINT PK_UserId PRIMARY KEY (UserId)
);

CREATE TABLE Reviews(
	ReviewId int identity(1,1) NOT NULL,
	RestaurantId INT NOT NULL,
	UserId INT NOT NULL,
	ReviewText VARCHAR(100) NOT NULL,

	CONSTRAINT PK_ReviewId PRIMARY KEY (ReviewId),
	CONSTRAINT FK_Reviews_RestaurantId FOREIGN KEY (RestaurantId) REFERENCES Restaurants(RestaurantId),
    CONSTRAINT FK_Reviews_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId) 

);