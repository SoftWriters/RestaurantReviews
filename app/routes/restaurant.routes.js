module.exports = function(app) {

    var restaurants = require('../controllers/restaurant.controller.js');

//Get a list of all the restaurants 
    app.get('/restaurants', restaurants.findAll);
////Get a list of restaurants by city
    app.get('/restaurants/city/:city', restaurants.findAllByCity);
////Post a restaurant that is not in the database
    app.post('/restaurants', restaurants.create);
//Post a review for a restaurant
    app.post('/restaurants/review', restaurants.createReviews);
//Get of a list of reviews by user
    app.get('/restaurants/review/:user', restaurants.findAllReviewesByUser);   
//Delete a review
    app.delete('/restaurants/deletereview/:user', restaurants.deleteUserReview);
  
}