using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
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
                            
        