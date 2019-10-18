RestaurantReviews
=================

The Problem
--------------
We are in the midst of building a mobile application that will let restaurant patrons rate the restaurant in which they are eating. As part of the build, we need to develop a web API that will accept and store the ratings and other sundry data from a publicly accessible interface. 

*Project Tracking is at https://trello.com/b/y0AEDhOj/softwriters-restaurantreviews*

**Restaurants API**
 - POST -   `https://localhost:44353/api/Restaurants` - send JSON object in request body - for example: 
  ```json
{
	"Name": "Tony's Pizzeria",
	"City": "Boston"
}
  ```
 - GET -    `https://localhost:44353/api/Restaurants/{city}`
     example 
     `https://localhost:44353/api/Restaurants/Boston`
      - Response:
```json
[
    {
        "restaurantId": 1,
        "name": "Tonys",
        "city": "Boston"
    },
    {
        "restaurantId": 2,
        "name": "Max",
        "city": "Boston"
    },
    {
        "restaurantId": 11,
        "name": "Sams",
        "city": "Boston"
    }
]
```
**Reviews API**
 - GET -          `https://localhost:44353/api/Reviews/{username}`
     - Response:
  ``` json
  [
    {
        "reviewId": 37,
        "restaurant": {
            "restaurantId": 10,
            "name": "Pizzeria",
            "city": "Los Angeles"
        },
        "user": {
            "userId": 1,
            "userName": "user1"
        },
        "reviewText": "Restaurant is awesome"
    }
]
```
 - POST -   `https://localhost:44353/api/Reviews`  - Send JSON object in request body - for example: 
  
  ``` javascript
{
  "Restaurant": {
    "RestaurantId": 1
  },
  "User": {
    "UserId": 2
  },
  "ReviewText": "It was ok not excellent"
}
  ```
  - PUT -          `https://localhost:44353/api/Reviews/{id}` 
    - Send JSON object in request body - for example: 
  ``` javascript
{
	"ReviewId": 37,
	"ReviewText": "They apologized and I am very happy"
}
```
 - DELETE - `https://localhost:44353/api/Reviews/{id}`
    

  
