namespace Zink.MVC.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int ProductStatusId { get; set; }

        //Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
