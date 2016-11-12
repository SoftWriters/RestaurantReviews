package main

/* 
*  Jay Palat
*  jay@palat.net
*  MIT Licensed
*/

import (
	"database/sql"
	"gopkg.in/gorp.v1"
	"log"
	"strconv"

	"github.com/gin-gonic/gin"
	_ "github.com/lib/pq"
)



type Restaurant struct {
	Id         int64   `db:"gid" json:"id"`
	Name       string  `db:"name" json:"name"`
	Address    string  `db:"address" json:"address"`
	Latitude   float64 `db:"latitude" json:"latitude"`
	Longitude  float64 `db:"longitude" json:"longitude"`
	Avg_rating int8    `db:"avg_rating" json:"avg_rating"`
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
	Rating       int64  `db:"rating" json:"rating"`
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
	dbmap.AddTableWithName(Restaurant{}, "Restaurants").SetKeys(true, "Id")

	err = dbmap.CreateTablesIfNotExists()
	checkErr(err, "Create tables failed")

	return dbmap
}

func Cors() gin.HandlerFunc {
	return func(c *gin.Context) {
		c.Writer.Header().Add("Access-Control-Allow-Origin", "*")
		c.Next()
	}
}

func checkErr(err error, msg string) {
	if err != nil {
		log.Fatalln(msg, err)
	}
}

func main() {
	r := gin.Default()
	r.Use(Cors())

	v1 := r.Group("api/v1")
	{
		v1.GET("/restaurants", GetRestaurants)
		v1.GET("/restaurants/:id", GetRestaurant)
		v1.POST("/restaurants", PostRestaurant)
		v1.GET("/restaurants/:id/reviews", GetReviewsByRestaurant)
		v1.POST("/restaurants/:id/reviews", PostReview)
		v1.GET("/users/:id/reviews", GetReviewsByUser)
		v1.GET("/users", GetUsers)
		v1.GET("/reviews", GetReviews)
		v1.GET("/reviews/:id", GetReview)
		v1.DELETE("/reviews/:id/", DeleteReview)

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
	latitude := c.Query("lat")
	longitude := c.Query("lng")
	var restaurants []Restaurant

	if latitude == "" || longitude == "" {
		_, err := dbmap.Select(&restaurants, "SELECT gid, name, address FROM \"restaurants\"")
		if err == nil {
			c.JSON(200, restaurants)
		} else {
			c.JSON(404, gin.H{"error": err})
		}
	} else {
		_, err := dbmap.Select(&restaurants, "SELECT gid, name, address FROM restaurants WHERE GeometryType(ST_Centroid(the_geom)) = 'POINT' AND ST_Distance_Sphere( ST_Point(ST_X(ST_Centroid(the_geom)), ST_Y(ST_Centroid(the_geom))), (ST_MakePoint($1, $2))) <= 1609.34", latitude, longitude)
		if err == nil {
			c.JSON(200, restaurants)
		} else {
			c.JSON(404, gin.H{"error": err, "lat": latitude, "lng": longitude})
		}

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

func PostRestaurant(c *gin.Context) {
	var restaurant Restaurant
	c.Bind(&restaurant)

	log.Println(restaurant.Name)
	log.Println(restaurant.Address)

	if restaurant.Name != "" {
		if restaurant.Address != "" {
			log.Println("Got address")
			content := &Restaurant{0, restaurant.Name, restaurant.Address, 0, 0, 0}
			zerr := dbmap.Insert(content)
			checkErr(zerr, "Error adding restaurant.")
			if zerr == nil {
				c.JSON(201, content)
			} else {
				c.JSON(400, gin.H{"dmap error": zerr})
			}

			log.Println("Postinsert")

		} else {
			c.JSON(400, gin.H{"Error: missing address": restaurant})
		}
	} else {
		c.JSON(400, gin.H{"Error: missing Name": restaurant})
	}
}


func GetReviews(c *gin.Context){
	var reviews []ReviewView
	_, err := dbmap.Select(&reviews, "SELECT rs.name RestaurantName, u.username UserName, rv.id ReviewID, rv.title ReviewTitle, rv.content ReviewContent, rv.rating ReviewRating FROM review rv, restaurants rs, \"user\" u WHERE  u.id = rv.userid and rs.gid = rv.restid")

	if err == nil {
		c.JSON(200, reviews)
	} else {
		c.JSON(404, gin.H{"error": err})
	}

}

func GetReview(c *gin.Context){
	var review ReviewView
	reviewId := c.Params.ByName("id")

	err := dbmap.SelectOne(&review, "SELECT rs.name RestaurantName, u.username UserName, rv.id ReviewID, rv.title ReviewTitle, rv.content ReviewContent, rv.rating ReviewRating FROM review rv, restaurants rs, \"user\" u WHERE rv.id = $1 AND u.id = rv.userid and rs.gid = rv.restid ", reviewId)

	if err == nil {
		c.JSON(200, review)
	} else {
		c.JSON(404, gin.H{"error": err})
	}

}

func PostReview(c *gin.Context) {
	var review Review
	c.Bind(&review)

	restaurantId, _ := strconv.ParseInt(c.Params.ByName("id"),10,64)
	userId, _ := strconv.ParseInt(c.PostForm("uid"),10,64)
	title := c.PostForm("title")
	reviewContent := c.PostForm("content")

	rating, _ := strconv.ParseInt( c.PostForm("rating"),10,8)

	log.Println("restid: %d userid: %d", restaurantId, userId);
	log.Println("Title: %s \n %s", title, reviewContent);

	content := &Review{0, restaurantId, userId, title, reviewContent, rating}
	err := dbmap.Insert(content)
	if err == nil {
		c.JSON(201, content)
	} else {
		c.JSON(400, gin.H{"dmap error": err})
	}
}

func DeleteReview(c *gin.Context){


	reviewId, _ := strconv.ParseInt(c.Params.ByName("id"),10,64)
	userId, _ := strconv.ParseInt(c.PostForm("uid"),10,64)
	
	_, err := dbmap.Exec("DELETE from review where id=$1 and userid=$2", reviewId, userId)
    if err == nil {
		c.JSON(202, "Item marked for deletion.")
	} else {
		c.JSON(400, gin.H{"dmap error": err})
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

func GetReviewsByUser(c *gin.Context) {
	var reviews []ReviewView
	userid := c.Params.ByName("id")
	_, err := dbmap.Select(&reviews, "SELECT rs.name RestaurantName, u.username UserName, rv.id ReviewID, rv.title ReviewTitle, rv.content ReviewContent, rv.rating ReviewRating FROM review rv, restaurants rs, \"user\" u WHERE u.id=$1 AND u.id = rv.userid and rs.gid = rv.restid", userid)

	if err == nil {
		c.JSON(200, reviews)
	} else {
		c.JSON(404, gin.H{"error": err})
	}
}
