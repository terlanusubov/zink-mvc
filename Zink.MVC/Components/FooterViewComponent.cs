using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zink.MVC.Data;
using Zink.MVC.Models;
using Zink.MVC.ViewModels;

namespace Zink.MVC.Components
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FooterViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterViewModel footerVM = new FooterViewModel
            {
                SocialMedia = await _context.SocialMedias.ToListAsync()
            };
            return View(footerVM);
        }
    }
}
