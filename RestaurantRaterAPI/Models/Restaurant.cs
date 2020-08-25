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
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Rating { get; set; }

        //public bool  IsRecomended => Rating > 3.5;
        public bool IsRecomended
        {
            get
            {
                return Rating > 3.5;
            }
        }
    }
}
                            
        