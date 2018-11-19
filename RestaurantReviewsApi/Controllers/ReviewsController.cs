using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Serilog;
using RestaurantReviews.Model;
using RestaurantReviews.Services;

namespace RestaurantReviews.Api.Controllers
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A controller for handling Review API calls. </summary>
    ///-------------------------------------------------------------------------------------------------

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _service;

        /// -------------------------------------------------------------------------------------------------
        ///  <summary> Reviews Controller Constructor. </summary>
        /// 
        /// <param name="service"></param>
        /// -------------------------------------------------------------------------------------------------

        public ReviewsController(IReviewService service)
        {
            _service = service;
            Log.Verbose("ReviewsController Created with services");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>The GetAll method returns a collection of RestaurantItem objects.  MVC automatically 
        ///          serializes the object to JSON and writes the JSON into the body of the response message.
        ///          The response code for this method is 200, assuming there are no unhandled exceptions.
        ///          unhandled exceptions are translated into 5xx errors.
        ///          </summary>
        ///
        /// <returns> All Review Reviews as an ActionResult. </returns>
        ///-------------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult<List<Review>> GetAll()
        {
            List<Review> reviews = _service.GetReviews().ToList();

            // if reviews was instaniated and there are any records...
            if (reviews.Any())
            {
                // ...return the list
                return reviews;
            }
            else
            {
                // ...otherwise, return a 404 status code 
                // ControllerBase.NotFound Method does a return new NotFoundResult();
                return NotFound();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   (An Action that handles HTTP GET requests) gets by city. </summary>
        ///
        /// <param name="user"> The city. </param>
        ///
        /// <returns>   The Reviews by a given user. </returns>
        ///-------------------------------------------------------------------------------------------------

        [HttpGet("{user}", Name = "GetByUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<Review>> GetByUser([FromQuery]string user)
        {
            List<Review> reviews = _service.GetReviewsByUser(user);
            if (reviews.Count == 0)
            {
                // ControllerBase.NotFound Method
                // Returns a 404 status code as if you did the following:
                // return new NotFoundResult();
                return NotFound();
            }
            return Ok(reviews);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>An Action that handles HTTP POST requests) creates a new IActionResult.  MVC gets the 
        ///          value of the review item from the body of the HTTP request.  The CreatedAtRoute
        ///          method reurns a 201 response (201 is the standard response for an HTTP POST method that
        ///          creates a new resource on the server)
        ///          </summary>
        ///
        /// <param name="review">   The review. </param>
        ///
        /// <returns>An IActionResult. </returns>
        ///-------------------------------------------------------------------------------------------------

        [HttpPost]
        public IActionResult Create(Review review)
        {
            if (!ReviewExists(review.Id))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _service.CreateReview(review);

                try
                {
                    _service.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    Log.Error($"An Error Occurred on the Web API call. Error: {e.Message}");
                    // Returns a 409 status - The request could not 
                    // be understood by the server due to malformed 
                    // syntax. The client SHOULD NOT repeat the 
                    // request without modifications.
                    return Conflict();
                }
            }
            return Ok();
        }

        /// -------------------------------------------------------------------------------------------------
        ///  <summary>Update is similar to Create, except it uses HTTP PUT. The response is 204 (No Content).
        ///           According to the HTTP specification, a PUT request requires the client to send the 
        ///           entire updated entity, not just the deltas.  To support partial updates, we would need 
        ///           to use HTTP PATCH.
        ///           </summary>
        /// 
        ///  <param name="id">The Review record identifier. </param>
        ///  <param name="review">The ResturantReview json object to be updated. </param>
        /// <returns>An IActionResult. </returns>
        /// -------------------------------------------------------------------------------------------------

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(409)]
        public IActionResult Update(Guid id, [FromBody] Review review)
        {
            if (ReviewExists(review.Id))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _service.UpdateReview(review);

                try
                {
                    _service.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Returns a 409 status - The request could not 
                        // be understood by the server due to malformed 
                        // syntax. The client SHOULD NOT repeat the 
                        // request without modifications.
                        return Conflict();
                    }
                }

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>Deletes Review associated with the given ID. </summary>
    ///
    /// <param name="id">   The Review record identifier. </param>
    ///
    /// <returns>An IActionResult:
    ///          NotFound - status code 204
    ///          NoContent - status code 404</returns>
    ///-------------------------------------------------------------------------------------------------

    [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            var review = _service.GetReviewById(id);
            if (review == null)
            {
                // ControllerBase.NoContent Method
                // Returns a 404 status code as if you did the following:
                // return new NotFoundResult();
                return NotFound();
            }

            _service.DeleteReview(review);
            _service.SaveChanges();

            // From the ControllerBase.NoContent Method
            // Returns a 204 status - i.e. the server has fulfilled 
            // the request but does not need to return an entity-body
            return NoContent();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Queries if a given review exists. </summary>
        ///
        /// <param name="id">   The Review record identifier. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        private bool ReviewExists(Guid id)
        {
            return _service.GetReviewById(id) != null;
        }
    }
}
