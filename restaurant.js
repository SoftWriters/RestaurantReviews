//npm faker module for mocking the data 
var faker = require('faker')

function generateRestaurantList () {
  var restaurants = []


  for (var id = 0; id < 100; id++) {
    var rdmNum = faker.random.number({min:1, max:100})
    var restaurant_name = "Restaurant" + rdmNum
    var restaurant_city = faker.address.city()
    var restaurant_rating = faker.random.number({min:1, max:5})
    var restaurant_email = faker.internet.email()
    var restaurant_user = "User"+ rdmNum
    var restaurant_review = "Some Review " + rdmNum
       
    restaurants.push({
        "name": restaurant_name,
        "city": restaurant_city,
        "rating":restaurant_rating,
        "email":restaurant_email+"@somedomain.com",
        "user":restaurant_user, 
        "review":restaurant_review
    })
}
    return { "restaurants": restaurants }
}

module.exports = generateRestaurantList
 