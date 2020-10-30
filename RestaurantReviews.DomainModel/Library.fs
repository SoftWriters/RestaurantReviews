namespace RestaurantReviews.DomainModel

open System

type NonEmptyString = private NonEmptyString of string
module NonEmptyString =
    let create str = 
        if String.IsNullOrWhiteSpace(str) then
            "The given string must not be empty or whitespace" |> Error
        else
            NonEmptyString str |> Ok

    let unwrap (NonEmptyString str) = str


type Id = private Id of Guid
module Id = 
    let create id = 
        if id = Guid.Empty then
            "Id may not be an empty Guid." |> Error
        else
            Id id |> Ok

    let unwrap (Id id) = id


type Rating = private Rating of int
module Rating = 
    let lower = 0
    let upper = 5

    let create rating = 
        if rating < lower || rating > upper then
            String.Format("Rating must be between {0} and {1}", lower, upper) |> Error
        else
            Rating rating |> Ok

    let unwrap (Rating rating) = rating


type User = {
    Id: Id
    FirstName: NonEmptyString // require first name?
    LastName: string
}
module User =
    let unwrap user = {|
        Id = Id.unwrap user.Id
        FirstName = NonEmptyString.unwrap user.FirstName
        LastName = user.LastName
    |}


type Restaurant = {
    Id: Id
    Name: NonEmptyString
    City: NonEmptyString
}
module Restaurant =
    let unwrap restaurant = {| 
        Id = Id.unwrap restaurant.Id
        Name = NonEmptyString.unwrap restaurant.Name 
        City = NonEmptyString.unwrap restaurant.City 
    |}

    let unwrapMany restaurants = Seq.map unwrap restaurants


type Review = {
    Id: Id
    User: Id
    Restaurant: Id
    Rating: Rating
    ReviewText: string
}
module Review = 
    let unwrap review = {|
        Id = Id.unwrap review.Id
        User = Id.unwrap review.User
        Restaurant = Id.unwrap review.Restaurant
        Rating = Rating.unwrap review.Rating
        ReviewText = review.ReviewText
    |}

    let unwrapMany reviews = Seq.map unwrap reviews
