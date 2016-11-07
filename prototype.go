package main

import (
	"database/sql"
	"gopkg.in/gorp.v1"
	"log"

	"github.com/gin-gonic/gin"
	_ "github.com/lib/pq"
)

//x name
//x id
//x location (street addr)
// lat
// long
// genre?
// phone
// fax
// website
// avg_rating

type Restaurant struct {
	Id         int64   `db:"gid" json:"id"`
	Name       string  `db:"name" json:"name"`
	Address    string  `db:"address" json:"address"`
	Latitude   float64 `db:"latitude" json:"latitude"`
	Longitude  float64 `db:"longitude" json:"longitude"`
	avg_rating int8    `db:"rating" json:"rating"`
}

type User struct {
	Id       int64  `db:"id" json:"id"`
	Username string `db:"username" json:"username"`
}

type Review struct {
	Id           int64  `db:"id" json:"id"`
	RestaurantId int64  `db:"restid" json"restid"`
	UserId       int64  `db:"userid" json"userid"`
	Title        string `db:"title" json:"title"`
	Content      string `db:"content" json:"content"`
	Rating       uint8  `db:"rating" json:"rating"`
}

type ReviewView struct {
	RestaurantName string 
	UserName       string 
	ReviewID       int64  
	ReviewTitle    string 
	ReviewContent  string 
	ReviewRating   uint8  
}

var dbmap = initDb()

func initDb() *gorp.DbMap {

	db, err := sql.Open("postgres", "postgresql://restdb:notm3@localhost/restdb?sslmode=disable")

	checkErr(err, "sql.Open failed")
	dbmap := &gorp.DbMap{Db: db, Dialect: gorp.PostgresDialect{}}
	dbmap.AddTableWithName(User{}, "User").SetKeys(true, "Id")
	dbmap.AddTableWithName(Review{}, "Review").SetKeys(true, "Id")

	err = dbmap.CreateTablesIfNotExists()
	checkErr(err, "Create tables failed")

	return dbmap
}

func checkErr(err error, msg string) {
	if err != nil {
		log.Fatalln(msg, err)
	}
}

func main() {
	r := gin.Default()

	v1 := r.Group("api/v1")
	{
		v1.GET("/restaurants", GetRestaurants)
		v1.GET("/restaurants/:id", GetRestaurant)
		// v1.POST("/restaurants", PostRestaurant)
		v1.GET("/restaurants/:id/reviews", GetReviewsByRestaurant)
		// v1.POST("/restaurants/:id/reviews", PostReview)
		// v1.DELETE("/restaurants/:id/reviews", DeleteReview)
		v1.GET("/users/:id/reviews", GetReviewsByUser)
		v1.GET("/users", GetUsers)
	}

	r.Run(":8080")
}

func GetUsers(c *gin.Context) {
	var users []User
	_, err := dbmap.Select(&users, "SELECT * FROM \"user\"")

	if err == nil {
		c.JSON(200, users)
	} else {
		c.JSON(404, gin.H{"error": err})
	}

	// curl -i http://localhost:8080/api/v1/users
}

func GetRestaurants(c *gin.Context) {
	var restaurants []Restaurant
	_, err := dbmap.Select(&restaurants, "SELECT gid, name, address FROM \"restaurants\"")

	if err == nil {
		c.JSON(200, restaurants)
	} else {
		c.JSON(404, gin.H{"error": err})
	}

}

func GetRestaurant(c *gin.Context) {
	var restaurant Restaurant
	restid := c.Params.ByName("id")
	err := dbmap.SelectOne(&restaurant, "SELECT gid, name, address, latitude, longitude FROM restaurants WHERE gid=$1 LIMIT 1", restid)

	if err == nil {
		c.JSON(200, restaurant)
	} else {
		c.JSON(404, gin.H{"error": err})
	}

}

func GetReviewsByRestaurant(c *gin.Context) {
	var reviews []Review
	restid := c.Params.ByName("id")
	_, err := dbmap.Select(&reviews, "SELECT * FROM review WHERE restid=$1", restid)

	if err == nil {
		c.JSON(200, reviews)
	} else {
		c.JSON(404, gin.H{"error": err})
	}
}

// RestaurantName string `db:"restaurant" json:"restaurant"`
// UserName       string `db:"username" json:"username"`
// ReviewID       int64  `db:"reviewid" json:"reviewid"`
// ReviewTitle    string `db:title" json:"title"`
// ReviewContent  string `db:content" json:"content"`
// ReviewRating   uint8  `db:"rating" json:"rating"`

func GetReviewsByUser(c *gin.Context) {
	var reviews []ReviewView
	userid := c.Params.ByName("id")
	_, err := dbmap.Select(&reviews, "SELECT rs.name RestaurantName, u.username UserName, rv.id ReviewID, rv.title ReviewTitle, rv.content ReviewContent, rv.rating ReviewRating FROM review rv, restaurants rs, \"user\" u WHERE u.id=$1 AND u.id = rv.userid and rs.gid = rv.id", userid)



	if err == nil {
		c.JSON(200, reviews)
	} else {
		c.JSON(404, gin.H{"error": err})
	}
}
