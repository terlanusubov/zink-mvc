namespace Zink.MVC.Models
{
    public class Certificate:BaseEntity
    {
        public string Name { get; set; }
        
        //Product
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
