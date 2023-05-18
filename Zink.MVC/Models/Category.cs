namespace Zink.MVC.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int CategoryStatusId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
