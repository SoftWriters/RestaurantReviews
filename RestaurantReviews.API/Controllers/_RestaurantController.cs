﻿//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc;
//using RestaurantReviews.API.Models;
//using RestaurantReviews.API.Repository;

//namespace RestaurantReviews.API.Controllers
//{
//    [Route("api/restaurants")]
//    [ApiController]
//    public class RestaurantController : ControllerBase
//    {
//        private readonly IRestaurantRepository _repository;

//        public RestaurantController(IRestaurantRepository repository)
//        {
//            this._repository = repository;
//        }

//        // GET api/restaurants
//        [HttpGet]
//        public ActionResult<IEnumerable<Review>> Get()
//        {
//            return Ok(_repository.GetAll());
//        }

//        // GET api/restaurants/5
//        [HttpGet("{id}")]
//        public ActionResult<Restaurant> Get(int id)
//        {
//            return Ok(_repository.GetById(id));
//        }

//        // POST api/restaurants
//        [HttpPost]
//        public void Post([FromBody] Restaurant restaurant)
//        {
//            _repository.Create(restaurant);
//        }

//        // PUT api/restaurants/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] Review restaurant)
//        {
//        }

//        // DELETE api/restaurants/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
