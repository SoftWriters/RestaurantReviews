var Restaurant = require('../models/restaurant.model.js');

var rdmNum = Math.floor(Math.random() * 100) + 1

exports.create = function(req, res) {
// Create and Save a new Note for Restaurant
    if(!req.body.name) {
        return res.status(400).send({message: "Restaurant name cannot be empty"});
    }
//|| "Untitled Restaurant"
    
    var restaurant = new Restaurant({name: req.body.name , city: req.body.city, rating: req.body.rating, email:             req.body.email || "someRestaurantEmail@somedomain.com", user: { name: req.body.user.name || "Anonymous" , review: req.body.user.review, rating: req.body.user.rating }});

    restaurant.save(function(err, data) {
        if (err) {
            console.log(err);
            res.status(500).send({message: "Restaurant name already exist"});
        } else {
            res.send(data);
        }
    });
};



exports.createReviews = function(req, res) {
// Create and Save a new Note for Restaurant
    if(!(req.body.name && req.body.user.review)) {
        return res.status(400).send({message: "Restaurant name and review comment cannot be empty"});
    }
//    console.log(rdmNum);
    
    var restaurant = new Restaurant({name: req.body.name || "Untitled Restaurant", city: req.body.city, rating: req.body.rating, email:             req.body.email || "someRestaurantEmail@somedomain.com", user: { name: req.body.user.name || "Anonymous" , review: req.body.user.review, rating: req.body.user.rating }});


    restaurant.save(function(err, data) {
        if(err) {
            console.log(err);
            res.status(500).send({message: "Some error occurred while creating the Restaurant review"});
        } else {
            res.send(data);
        }
    });
};


exports.findAll = function(req, res) {
    // Retrieve and return all restaurants from the database.
    Restaurant.find(function(err, restaurants){
        if(err) {
            console.log(err);
            res.status(500).send({message: "Some error occurred while retrieving Restaurant."});
        } else {
            res.send(restaurants);
        }
    });
};

exports.findAllByCity = function(req, res) {
    var query = { city: req.params.city };
    // Retrieve and return all restaurants from the database.
//    Restaurant.find({} ,{ _id: 0, name: 1}, function(err, restaurants){
    Restaurant.find({city:req.params.city},{ _id: 0, name: 1}, function(err, restaurants){

        if(err) {
            console.log(err);
            if(err.kind === 'ObjectId') {
                return res.status(404).send({message: "No Restaurants found in city " + req.params.city});                
            }
            res.status(500).send({message: "Some error occurred while retrieving Restaurant."});
        } 
        
        if(!restaurants) {
            return res.status(404).send({message: "No Restaurants found in city " + req.params.city});            
        }

        res.send(restaurants);
    });
};


exports.findAllReviewesByUser = function(req, res) {
    // Retrieve and return all restaurants from the database.
//    console.log(req.params.user)
    Restaurant.find({ 'user.name' : req.params.user }, { _id: 0, 'user': 1 }, function(err, restaurants){
        if(err) {
            console.log(err);
            if(err.kind === 'ObjectId') {
                return res.status(404).send({message: "No Restaurants reviews found for user " + req.params.user});                
            }
            res.status(500).send({message: "Some error occurred while retrieving Restaurant."});
        } 
        
        if(!restaurants) {
            return res.status(404).send({message: "No Restaurants reviews found for user " + req.params.user});            
        }

        res.send(restaurants)
        
    });
};

exports.deleteUserReview = function(req, res) {
//    Restaurant.findOneAndUpdate({ 'user.name' : req.params.user }, { _id: 0, 'user.review': 1 } ,function(err, restaurants){
//    Restaurant.findOneAndRemove({ 'user.name' : req.params.user } ,function(err, restaurants){
//    Restaurant.findOneAndUpdate({ 'user.name' : req.params.user },{$set:{'user.review':""}}, {new: true},function(err, restaurants){
    Restaurant.update({ 'user.name' : req.params.user },{$set:{'user.review':""}}, {multi: true},function(err, restaurants){    
        if(err) {
            console.log(err);
            if(err.kind === 'ObjectId') {
                return res.status(404).send({message: "No Restaurants reviews found for user " + req.params.user});                
            }
            res.status(500).send({message: "Some error occurred while retrieving Restaurant."});
        } 
        
        if(!restaurants) {
            return res.status(404).send({message: "No Restaurants reviews found for user " + req.params.user});            
        }
//        console.log(restaurants)
        res.send("Review successfully deleted!");

    });
};


exports.updateUserReview = function(req, res) {
    Restaurant.findOneAndUpdate({ 'user.review' : "" },{$set:{'user.review':req.params.user.review}}, {new: true} ,function(err, restaurants){    
        if(err) {
            console.log(err);
            if(err.kind === 'ObjectId') {
                return res.status(404).send({message: "No Restaurants reviews found for user " + req.params.user});                
            }
            res.status(500).send({message: "Some error occurred while retrieving Restaurant."});
        } 
        
        if(!restaurants) {
            return res.status(404).send({message: "No Restaurants reviews found for user " + req.params.user});            
        }
//        console.log(restaurants)
        res.send("Review successfully deleted!");

    });
};