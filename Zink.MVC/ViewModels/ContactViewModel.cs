using Zink.MVC.Models;
using Zink.MVC.ServiceModel.Request;

namespace Zink.MVC.ViewModels
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; }
        public QuestionRequest Question { get; set; }
    }
}
