
IF OBJECT_ID('dbo.tblRestaurant') IS NULL
BEGIN
	CREATE TABLE dbo.tblRestaurant (
		Id [int] IDENTITY(1, 1) NOT NULL,
		Name [varchar](100) NOT NULL,
		City [varchar](50) NOT NULL,
		CONSTRAINT [PK_tblRestaurant] PRIMARY KEY (
			[Id] ASC
		),
		CONSTRAINT [UNQ_tblRestaurant_Name_City] UNIQUE (
			[Name], [City]
		)
	);
END
GO

IF OBJECT_ID('dbo.tblReviewer') IS NULL
BEGIN
	CREATE TABLE dbo.tblReviewer (
		Id [int] IDENTITY(1, 1) NOT NULL,
		Name [varchar](100) NOT NULL,
		CONSTRAINT [PK_tblReviewer] PRIMARY KEY (
			[Id] ASC
		),
		CONSTRAINT [UNQ_tblReviewer_Name] UNIQUE (
			[Name]
		)
	);
END
GO

IF OBJECT_ID('dbo.tblReview') IS NULL
BEGIN
	CREATE TABLE dbo.tblReview (
		Id [int] IDENTITY(1, 1) NOT NULL,
		ReviewerId [int] NOT NULL,
		RestaurantId [int] NOT NULL,
		ReviewDateTime [smalldatetime] NOT NULL 
			CONSTRAINT [DF_tblReview_ReviewDateTime] DEFAULT GetDate(),
		Rating [int] NOT NULL,
		ReviewText [varchar](5000) NOT NULL,
		CONSTRAINT [PK_tblReview] PRIMARY KEY (
			[Id] ASC
		),
		CONSTRAINT [FK_tblReview_tblReviewer] FOREIGN KEY ([ReviewerId])
			REFERENCES [dbo].[tblReviewer]([Id]),
		CONSTRAINT [FK_tblReview_tblRestaurant] FOREIGN KEY ([RestaurantId])
			REFERENCES [dbo].[tblRestaurant]([Id]),
		CONSTRAINT [CK_tblReview_Rating] CHECK([Rating] >=1 AND [Rating] <= 5)
	);
END
GO

