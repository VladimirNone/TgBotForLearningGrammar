using Microsoft.AspNetCore.Mvc;

namespace EngTrainerTelegramBot.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
