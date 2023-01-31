﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Project.Models
    {
    public class Employee
        {
     
        public int Id { get; set; }
        [Required(ErrorMessage = "Input Name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "incorrect length name")]
        public string Name { get; set; }

        [Required]
        [Range(1,100000)]      
       public int Salary { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public virtual ICollection<Order> Orders { get; set; }=new List<Order>();
        }
    }
