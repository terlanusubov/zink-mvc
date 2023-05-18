using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Zink.MVC.Data;
using Zink.MVC.Enums;
using Zink.MVC.Models;
using Zink.MVC.ViewModels;

namespace Zink.MVC.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public HeaderViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var existItem = HttpContext.Request.Cookies["wishItemsList"];
            int itemCount = 0;

            if (existItem != null)
            {
                var items = JsonConvert.DeserializeObject<List<int>>(existItem);
                itemCount = items.Count();
            }


            HeaderViewModel headerVM = new HeaderViewModel
            {
                WishItems = itemCount,
                Categories = await _context.Categories.Where(x => x.CategoryStatusId == (int)CategoryStatusEnum.Active).ToListAsync(),
            };
            return View(headerVM);
        }
    }
}
