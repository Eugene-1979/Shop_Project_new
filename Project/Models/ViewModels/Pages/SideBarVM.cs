using Shop_Project.Models.Data;

namespace Shop_Project.Models.ViewModels.Pages
{
    public class SideBarVM
    {
        public SideBarVM()
            {
            }

        public SideBarVM(SideBarDto sideBarDto)
            {
            Id = sideBarDto.Id;
            Body = sideBarDto.Body;
            }

        public int Id { get; set; }
        public string Body { get; set; }
        }
}
