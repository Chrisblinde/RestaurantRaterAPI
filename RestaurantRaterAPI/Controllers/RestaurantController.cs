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
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
       
        //-- Create (POST)
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if(ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
              await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest(ModelState);
        }

        //-- Read (GET)
        // Get by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if(restaurant != null)
            {
                return Ok(restaurant);
            }

            return NotFound();
        }

        // Get All
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        //-- Update (PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Restaurant updatedRestaurant)
        {
            if (ModelState.IsValid)
            {
                //Find and update the appropriate restaurant
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);
                if(restaurant != null)
                {
                    //Update the restaurant now that we found it
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Rating = updatedRestaurant.Rating;

                    await _context.SaveChangesAsync();

                    return Ok("Restaurant has been updated.");
                }

                //Didn't find the restaurant
                return NotFound();
            }

            // Return a bad request
            return BadRequest(ModelState);
        }
        //-- Delete (DELETE)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurantById(int id)
        {
            Restaurant entity = await _context.Restaurants.FindAsync();
            if(entity == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(entity);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was deleted.");
            }

            return InternalServerError();
        }
    }
}
