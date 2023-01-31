using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Shop_Project.Models
    {
    public class Category
        {
      
        public int Id { get; set; }
        /*  [Required(ErrorMessage = "Input Name")]
          [StringLength(15, MinimumLength = 2, ErrorMessage = "incorrect length name")]*/
      /*  [Remote("CheckEmptyString", "Products")]*/
        public string Name { get; set; }
      /*  [Required(ErrorMessage = "Input Salary")]
        [Range(0, 100, ErrorMessage = "Incorect Range 0-100")]*/
        public int Salary { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();



        }
    }
