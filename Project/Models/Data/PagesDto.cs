using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Project.Models.Data
    {
    [Table("tblPages")]
    public class PagesDto
        {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string ? Slug { get; set; }
        public string? Body { get; set; }
        public int ? Sorting { get; set; }
        public bool ? HasSidebar { get; set; }
        }
    }
