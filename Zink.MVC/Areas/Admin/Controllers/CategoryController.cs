using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zink.MVC.Data;
using Zink.MVC.Enums;
using Zink.MVC.Helper;
using Zink.MVC.Models;

namespace Zink.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public class CategoryModel
        {
            public int Id { get; set; }
            public int ParentId { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public int CategoryStatusId { get; set; }
        }
        public async Task<IActionResult> Index()
        {
            var result = await _context.Categories.Where(x => x.CategoryStatusId == (int)CategoryStatusEnum.Active).OrderByDescending(num => num.Id).Select(x => new CategoryModel
            {
                Name = x.Name,
                ParentId = x.ParentId,
                Id = x.Id,
                Image = x.Image,
                Description = x.Description,
                CategoryStatusId = x.CategoryStatusId,
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
        public async Task<IActionResult> Create(Category category)
        {
            ViewBag.Categories = _context.Categories.Where(x => x.CategoryStatusId == (int)CategoryStatusEnum.Active).ToList();
          
            if (category.Name == null)
            {
                ModelState.AddModelError("Name", "Kategoriyanin adi bos ola bilmez.");
                return View(category);
            }

            if(category.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Kategoriyanin sekli bos ola bilmez.");
                return View(category);
            }

            if(category.Description == null)
            {
                ModelState.AddModelError("Description", "Kategoriya haqqinda melumat bos ola bilmez.");
                return View(category);
            }

            category.CategoryStatusId = (int)CategoryStatusEnum.Active;
            await _context.Categories.AddAsync(category);

            category.Image = FileManager.Save(_env.WebRootPath, "uploads/categories", category.ImageFile);
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index" , "Category");
        }


        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            var category = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(category == null)
            {
                return RedirectToAction("index", "Category");
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category category)
        {
            ViewBag.Categories = _context.Categories.ToList();
            var existCategory = await _context.Categories.Where(x => x.Id == category.Id).FirstOrDefaultAsync();

            if(existCategory == null)
            {
                return RedirectToAction("index", "category");
            }
            if (category.Name == null)
            {
                ModelState.AddModelError("Name", "Kategoriyanin adi bos ola bilmez.");
                return View(category);
            }

            if (category.Description == null)
            {
                ModelState.AddModelError("Description", "Kategoriya haqqinda melumat bos ola bilmez.");
                return View(category);
            }

            if (category.ImageFile != null)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/categories", existCategory.Image);
                existCategory.Image = FileManager.Save(_env.WebRootPath, "uploads/categories", category.ImageFile);
            }

            existCategory.Name = category.Name;
            existCategory.Description = category.Description;
            existCategory.ParentId = category.ParentId;

            await _context.SaveChangesAsync();
            return RedirectToAction("index", "category");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return RedirectToAction("Index", "Category");
            }

            category.CategoryStatusId = (int)CategoryStatusEnum.Deactive;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
