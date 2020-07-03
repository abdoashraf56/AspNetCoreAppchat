using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreChatapp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    
    }
}