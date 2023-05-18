using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Zink.MVC.Data;
using Zink.MVC.Enums;
using Zink.MVC.Models;
using Zink.MVC.ViewModels;

namespace Zink.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? categoryId)
        {

            if(categoryId != null)
            {
                ProductViewModel productVMFilter = new ProductViewModel
                {
                    Products = await _context.Products.Include(x => x.Category).Where(x => x.ProductStatusId == (int)ProductStatusEnum.Active && x.Category.ParentId == categoryId).ToListAsync(),
                };

                return View(productVMFilter);
            }
            ProductViewModel productVM = new ProductViewModel
            {
                Products = await _context.Products.Include(x => x.Category).Where(x => x.ProductStatusId == (int)ProductStatusEnum.Active).ToListAsync(),
            };
            return View(productVM);
        }

        public async Task<IActionResult> Filter(List<int?> categoryId)
        {
            var products = await _context.Products.Include(x => x.Category).Where(x => (categoryId.Count == 0 || categoryId.Contains(x.CategoryId)) && x.ProductStatusId == (int)ProductStatusEnum.Active).ToListAsync();

            var categories = await _context.Categories.Where(x => x.CategoryStatusId == (int)CategoryStatusEnum.Active).ToListAsync();
            ProductViewModel productVM = new ProductViewModel
            {
                Products = products,
                Categories = categories,
                //Products = await _context.Products.Include(x => x.Category).Where(x => x.Category.Id == categoryId).ToListAsync(),
            };
            return View(productVM);
        }

        public async Task<IActionResult> Detail(int productId)
        {
            Product product = await _context.Products.Where(x=>x.Id==productId).FirstOrDefaultAsync();

            if (product == null) return RedirectToAction("index", "home");

            DetailViewModel detailVM = new DetailViewModel
            {
                Product = product,

            };
   
            return View(detailVM);
        }

        public IActionResult AddWishList(int productId)
        {
            if(!_context.Products.Where(c => c.ProductStatusId == (int)ProductStatusEnum.Active).Any(x => x.Id == productId))
            {
                return RedirectToAction("Index", "Product");
            }

            List<int> wishItems = new List<int>();

            string existWishItems = HttpContext.Request.Cookies["wishItemsList"];

            if(existWishItems != null)
            {
                wishItems = JsonConvert.DeserializeObject<List<int>>(existWishItems);

                foreach (var item in wishItems)
                {
                    if(item == productId)
                    {
                        return RedirectToAction("wishlist");
                    }
                }
            }

            

            wishItems.Add(productId);

            var productIdStr = JsonConvert.SerializeObject(wishItems);

            HttpContext.Response.Cookies.Append("wishItemsList", productIdStr );

            return RedirectToAction("wishlist");
        }

        public async Task<IActionResult> RemoveCookie(int id)
        {
            var existItem = HttpContext.Request.Cookies["wishItemsList"];

            var items = JsonConvert.DeserializeObject<List<int>>(existItem);

            foreach (var item in items)
            {
                if(item == id)
                {
                    items.Remove(item);
                    var productIdStr = JsonConvert.SerializeObject(items);
                    HttpContext.Response.Cookies.Append("wishItemsList", productIdStr);
                    return RedirectToAction("wishlist");
                }
            }
            


            return RedirectToAction("wishlist");
        }

        public async Task<IActionResult> WishList()
        {
            var wishIdsStr = HttpContext.Request.Cookies["wishItemsList"];

            List<int> wishItems = new List<int>();

            if(wishIdsStr != null)
            {
                wishItems = JsonConvert.DeserializeObject<List<int>>(wishIdsStr);
            }

            List<Product> products = new List<Product>();

            foreach (var item in wishItems)
            {
                products.Add(_context.Products.Include(x => x.Category).Where(x => x.Id == item).FirstOrDefault());
            }

            ProductViewModel productVM = new ProductViewModel
            {
                Products = products,
            };


            return View(productVM);
        }
    }
}