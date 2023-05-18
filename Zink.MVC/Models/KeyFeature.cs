namespace Zink.MVC.Models
{
    public class KeyFeature:BaseEntity
    {
        public string Name { get; set; }
        
        //Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
