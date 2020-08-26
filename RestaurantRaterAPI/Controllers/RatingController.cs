using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //Create new ratings
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            //Check to see if the model is NOT valid
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Find the targeted restaurant
            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the ID of {model.Restaurant} doesn't exist.");
            }

            //The restaurant isn't null, so we can succesfully rate it
            _context.Rating.Add(model);
            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok($"You rated {restaurant.Name} successfully!");
            }

            return InternalServerError();
        }

        //Get a rating by ID ?
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Rating rating = await _context.Rating.FindAsync(id);

            
            if (rating != null)
            {
                return Ok(rating);
            }

            return NotFound();
        }

        //Get all ratings for a specific restaurant by the restaurant ID
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Rating> rating = await _context.Rating.ToListAsync();
            return Ok(rating);
        }
        //Update Rating
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating([FromUri] int id, [FromBody] Rating updatedRating)
        {
            if (ModelState.IsValid)
            {
                //Find and update the appropriate rating
                Rating rating = await _context.Rating.FindAsync(id);
                if (rating != null)
                {
                    //Update the restaurant now that we found it
                    rating.CleanlinessScore = updatedRating.CleanlinessScore;
                    rating.EnvironmentScore = updatedRating.EnvironmentScore;
                    rating.FoodScore = updatedRating.FoodScore;


                    await _context.SaveChangesAsync();

                    return Ok("Rating has been updated.");
                }

                //Didn't find the restaurant
                return NotFound();
            }

            // Return a bad request
            return BadRequest(ModelState);
        }





        //Delete Rating
    }
}
