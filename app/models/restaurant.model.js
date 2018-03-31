var mongoose = require('mongoose');

var RRSchema = mongoose.Schema({
    name: {type: String, required: true, unique: true},
    city: String,
    email: String,
    user: {
        name: String,
        review: String,
        rating: String
    }
}, {
    timestamps: true
});

var Restaurant =  mongoose.model('Restaurant', RRSchema);

module.exports = Restaurant;