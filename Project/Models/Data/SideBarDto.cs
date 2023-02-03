using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Project.Models.Data
{
    [Table("tblSidebar")]
    public class SideBarDto
    {
    [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        }
}
