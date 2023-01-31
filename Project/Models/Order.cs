using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Project.Models
    {
    public class Order
        {
     
        public int Id { get; set; }


       public Employee Employee { get; set; }
        public int ?EmployeeId { get; set; }

        public DateTime MyDate { get; set; }
        public Customer Customer{ get; set; }
        public int ?CustomerId { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        }
    }
