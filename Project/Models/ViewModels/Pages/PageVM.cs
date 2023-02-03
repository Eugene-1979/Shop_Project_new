namespace Shop_Project.Models.ViewModels.Pages
    {
    public class PageVM
        {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public string Sorting { get; set; }
        public bool HasSidebar { get; set; }
        }
    }
