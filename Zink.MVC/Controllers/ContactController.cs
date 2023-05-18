using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Zink.MVC.Data;
using Zink.MVC.Models;
using Zink.MVC.ServiceModel.Request;
using Zink.MVC.ViewModels;

namespace Zink.MVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ContactViewModel contactVM = new ContactViewModel
            {
                Contact = await _context.Contacts.FirstOrDefaultAsync(),
            };
            return View(contactVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(QuestionRequest request)
        {

            if (!ModelState.IsValid)
            {
                ContactViewModel contactVM = new ContactViewModel
                {
                    Contact = await _context.Contacts.FirstOrDefaultAsync(),
                    Question = request,
                };
                return View(contactVM);
            }

            Question question = new Question
            {
                Name = request.Name,
                Surname = request.Surname,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Message = request.Message,
            };

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return View();
        }
    }
}
