using Microsoft.AspNetCore.Mvc;
using Zink.MVC.Data;
using static Zink.MVC.Areas.Admin.Controllers.CategoryController;
using Zink.MVC.Enums;
using Microsoft.EntityFrameworkCore;
using Zink.MVC.Helper;
using Zink.MVC.Models;

namespace Zink.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public class ProductModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public decimal Price { get; set; }
            public int ProductStatusId { get; set; }
            public int CategoryId { get; set; }
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context.Products.Where(x => x.ProductStatusId == (int)ProductStatusEnum.Active).OrderByDescending(num => num.Id).Select(x => new ProductModel
            {
                Name = x.Name,
                CategoryId = x.CategoryId,
                Id = x.Id,
                Price = x.Price,
                Image = x.Image,
                Description = x.Description,
                ProductStatusId = x.ProductStatusId,

            }).ToListAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = _context.Categories.Where(x => x.CategoryStatusId == (int)CategoryStatusEnum.Active).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = _context.Categories.Where(x => x.CategoryStatusId == (int)CategoryStatusEnum.Active).ToList();
            if (product.Name == null)
            {
                ModelState.AddModelError("Name", "Mehsulun adi bos ola bilmez.");
                return View(product);
            }

            if (product.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Mehsulun sekli bos ola bilmez.");
                return View(product);
            }

            if (product.Description == null)
            {
                ModelState.AddModelError("Description", "Mehsul haqqinda melumat bos ola bilmez.");
                return View(product);
            }

            if(product.CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Kateqoriya secilmeyib");
                return View(product);
            }

            if(product.Price == null)
            {
                ModelState.AddModelError("Price", "Qiymet xanasi bos ola bilmez.");
                return View(product);
            }

            if(product.Price <= 0)
            {
                ModelState.AddModelError("Price", "Qiymet 0 ve ya menfi ola bilmez.");
                return View(product);
            }


            product.ProductStatusId = (int)ProductStatusEnum.Active;
            product.Image = FileManager.Save(_env.WebRootPath, "uploads/products", product.ImageFile);

            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = _context.Categories.Where(x => x.CategoryStatusId == (int)CategoryStatusEnum.Active).ToList();
            Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(product == null)
            {
                return RedirectToAction("index", "product");
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Product product)
        {
            ViewBag.Categories = _context.Categories.Where(x => x.CategoryStatusId == (int)CategoryStatusEnum.Active).ToList();

            Product existProduct = await _context.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync();

            if(existProduct == null)
            {
                return RedirectToAction("Index", "Product");
            }

            if(product.ImageFile != null)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/products", existProduct.Image);
                existProduct.Image = FileManager.Save(_env.WebRootPath, "uploads/products", product.ImageFile);
            }

            if (product.Name == null)
            {
                ModelState.AddModelError("Name", "Mehsulun adi bos ola bilmez.");
                return View(product);
            }

            if (product.Description == null)
            {
                ModelState.AddModelError("Description", "Mehsul haqqinda melumat bos ola bilmez.");
                return View(product);
            }

            if (product.CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Kateqoriya secilmeyib");
                return View(product);
            }

            if (product.Price == null)
            {
                ModelState.AddModelError("Price", "Qiymet xanasi bos ola bilmez.");
                return View(product);
            }

            if (product.Price <= 0)
            {
                ModelState.AddModelError("Price", "Qiymet 0 ve ya menfi ola bilmez.");
                return View(product);
            }

            existProduct.Name = product.Name;
            existProduct.Description = product.Description;
            existProduct.Price = product.Price;
            existProduct.CategoryId = product.CategoryId;
            await _context.SaveChangesAsync();

            return RedirectToAction("index", "product");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }

            product.ProductStatusId = (int)ProductStatusEnum.Deactive;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
