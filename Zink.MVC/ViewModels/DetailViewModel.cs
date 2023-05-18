using Zink.MVC.Models;

namespace Zink.MVC.ViewModels
{
    public class DetailViewModel
    {
        public Product Product { get; set; }
        public List<Product> RelatedProduct { get; set; }
    }
}
