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
    open System.Text.RegularExpressions
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
    FirstName: NonEmptyString
    LastName: string
}

type Restaurant = {
    Id: Id
    Name: NonEmptyString
    City: NonEmptyString
}

type Review = {
    Id: Id
    User: User
    Restaurant: Restaurant
    Rating: Rating
    ReviewText: string
}
