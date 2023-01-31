﻿using System.ComponentModel.DataAnnotations;

namespace Shop_Project.Models
    {
    public class Enrollment
        {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        public int Count { get; set; }       
      
        }
    }
