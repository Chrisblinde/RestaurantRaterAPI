using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    //Restaurant Entity (the class that gets stored in the database)
    public class Restaurant
    {
        //Primary Key
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Rating
        {
            get
            {
                //Calculate a total average score based on Ratings
                double totalAverageRating = 0;
                
                //Add all Ratings together to get total average Rating
                foreach(var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                //Return Average of Total if the count is above 0
                return (Ratings.Count > 0) ? Math.Round(totalAverageRating / Ratings.Count, 2) : 0;
            }
        }

        //Average Food Rating
        
        public double AverageFoodRating
        {
            get
            {
                double totalFoodscore = 0;

                foreach(var score in Ratings)
                {
                    totalFoodscore += score.FoodScore;
                }

                return (Ratings.Count > 0) ? Math.Round(totalFoodscore / Ratings.Count, 2) : 0;
            }
        }

        //Average Environment Rating
        
        public double AverageEnvironmentRating
        {
            get
            {
                double environmentScore = 0;
                foreach(var score in Ratings)
                {
                    environmentScore += score.EnvironmentScore;
                }

                return (Ratings.Count > 0) ? Math.Round(environmentScore / Ratings.Count, 2) : 0;
            }
        }

        //Average Cleanliness Rating
        
        public double AverageCleanlinessRating       
        {
            get
            {
                double cleanlinessScore = 0;
                foreach (var score in Ratings)
                {
                    cleanlinessScore += score.CleanlinessScore;
                }

                return (Ratings.Count > 0) ? Math.Round(cleanlinessScore / Ratings.Count, 2) : 0;
            }
        }




        //public bool  IsRecomended => Rating > 3.5;
        public bool IsRecomended
        {
            get
            {
                return Rating > 8.5;
            }
        }

        //All of the associated objects from the database
        //based on the Foreign Key relationship
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
       
    }
}
                            
        