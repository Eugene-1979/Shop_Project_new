using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shop_Project.Data
    {
    public class ApplicationDbContext : IdentityDbContext
        {
/*        List<ApplicationUser> users = new List<ApplicationUser>(); */

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
            {
            }
        }
    }