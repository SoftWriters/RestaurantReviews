//Initiallising node modules
var express = require("express");
var bodyParser = require("body-parser");

// create express app
var app = express(); 

// Body Parser Middleware to parse requests of content-type - application/json
app.use(bodyParser.json()); 

// parse requests of content-type - application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }))

// Configuring the database
var dbConfig = require('./config/db.config.js');
var mongoose = require('mongoose');

mongoose.Promise = global.Promise;

mongoose.connect(dbConfig.url);


mongoose.connection.on('error', function() {
    console.log('Could not connect to the database. Please make sure you have MongoDB installed as ***PREREQUISITE*** Exiting now...');
    console.log("Please download mongoDB from https://www.mongodb.com/download-center#production");
    process.exit();
});

mongoose.connection.once('open', function() {
    console.log("Successfully connected to the database");
})

// define a simple route
app.get('/', function(req, res){
    res.json({"message": "Welcome to Restaurants Review application. Please select a Restaurants from the list, e.g., /restaurants"})
});


require('./app/routes/restaurant.routes.js')(app);
//Setting up server
 var server = app.listen(process.env.PORT || 8080, function () {
    var port = server.address().port;
    console.log("App now running on port", port);
 });






