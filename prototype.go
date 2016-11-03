package main

import "github.com/gin-gonic/gin"

type Restaurant struct {
	Id        int64  `db:"id" json:"id"`
	Firstname string `db:"firstname" json:"firstname"`
	Lastname  string `db:"lastname" json:"lastname"`
}

type User struct {
	Id        int64  `db:"id" json:"id"`
	Firstname string `db:"firstname" json:"firstname"`
	Lastname  string `db:"lastname" json:"lastname"`
}

/*
x /restaurants	get	lat/long
x/restaurants	post	restaurant
x/restaurants/#ID	get	restaurant

/restauarnts/#ID/reviews	get	min_stars
/restauarnts/#ID/reviews	post	review
/restauarnts/#ID/reviews	delete	review_id

/user/#ID/reviews	get
*/

func main() {
	r := gin.Default()


	v1 := r.Group("api/v1")
	{
		v1.GET("/restaurants", GetRestaurants)
		// v1.GET("/restaurants/:id", GetRestaurant)
		// v1.POST("/restaurants", PostRestaurant)
		// v1.GET("/restaurants/:id/reviews", GetReviews)
		// v1.POST("/restaurants/:id/reviews", PostReview)
		// v1.DELETE("/restaurants/:id/reviews", DeleteReview)
		// v1.GET("/users/:id/reviews", GetReviewsByUser)
	}

	r.Run(":8080")
}

func GetRestaurants(c *gin.Context) {
	c.JSON(200, gin.H{
		"message": "GetRestaurants",
	})
}
