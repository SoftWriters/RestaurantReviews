﻿namespace RestaurantReviews.DomainModel

open System


type NonEmptyString = private NonEmptyString of string
module NonEmptyString =
    let create str = 
        if String.IsNullOrWhiteSpace(str) then
            "The given string must not be empty or whitespace" |> Error
        else
            NonEmptyString str |> Ok


type Id = private Id of Guid
module Id = 
    let create id = 
        if id = Guid.Empty then
            "Id may not be an empty Guid." |> Error
        else
            Id id |> Ok


type Rating = private Rating of int
module Rating = 
    let lower = 0
    let upper = 5

    let create rating = 
        if rating < lower || rating > upper then
            String.Format("Rating must be between {0} and {1}", lower, upper) |> Error
        else
            Rating rating |> Ok


type User = {
    Id: Id
    FirstName: NonEmptyString
    LastName: string
    Reviews: Review seq
}
and Review = {
    Id: Id
    User: User
    RestaurantName: NonEmptyString
}


type Restaurant = {
    Id: Id
    Name: NonEmptyString
    City: NonEmptyString
    Reviews: Review seq
}