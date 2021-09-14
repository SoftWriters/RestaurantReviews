
DROP TABLE IF EXISTS "Users";
DROP TABLE IF EXISTS "Restaurants";
DROP TABLE IF EXISTS "Reviews";

CREATE TABLE "Users" (
    "UserID" INTEGER PRIMARY KEY,
    "UserName" NVARCHAR(20) NOT NULL,
    "Password" NVARCHAR(32) NOT NULL,
    "Email" NVARCHAR(50) NOT NULL
);

CREATE TABLE "Restaurants" (
    "RestaurantID" INTEGER PRIMARY KEY,
    "Name" NVARCHAR(30) NOT NULL,
    "Address" NVARCHAR(60) NOT NULL, 
    "City" NVARCHAR(20) NOT NULL,
    "State" NVARCHAR(2) NOT NULL,
    "Phone" NVARCHAR(15) NOT NULL
);

CREATE TABLE "Reviews" (
    "ReviewID" INTEGER PRIMARY KEY,
    "RestaurantID" INTEGER NOT NULL,
    "UserID" INTEGER NOT NULL,
    "Timestamp" DATETIME NOT NULL,
    "Text" NVARCHAR(500) NOT NULL,
    "Rating" BYTE NOT NULL,
    CONSTRAINT "FK_Reviews_Restaurants" FOREIGN KEY (
		"RestaurantID"
	) 
    REFERENCES "Restaurants" (
		"RestaurantID"
	),
	CONSTRAINT "FK_Reviews_Users" FOREIGN KEY (
		"UserID"
	) 
    REFERENCES "Users" (
		"UserID"
	)
);

INSERT INTO "Users" ('UserName', 'Password', 'Email') VALUES ('daryl', 'daryl', 'daryl@test.com');
INSERT INTO "Users" ('UserName', 'Password', 'Email') VALUES ('liz', 'liz', 'liz@test.com');

INSERT INTO "Restaurants" ('Name', 'Address', 'City', 'State', 'Phone') VALUES ('Hells Kitchen', '100 Las Vegas Blvd.', 'Las Vegas', 'NV', '111 222-3333');

INSERT INTO "Reviews" ('RestaurantID', 'UserID', 'Timestamp', 'Text', 'Rating') VALUES (1, 1, CURRENT_TIMESTAMP, 'Great scallops and Beef Wellington. The chef yells a lot and throws raw food at the wall.', 5);
