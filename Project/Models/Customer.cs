using System.ComponentModel.DataAnnotations;

namespace Shop_Project.Models
    {
    public class Customer
        {
    
        public int Id { get; set; }
        [Required(ErrorMessage = "Input Name")]
        [StringLength(15,MinimumLength =2,ErrorMessage ="incorrect length name")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }



        public List<Order> Orders { get; set; }=new List<Order>();
   
   

        }
    }
