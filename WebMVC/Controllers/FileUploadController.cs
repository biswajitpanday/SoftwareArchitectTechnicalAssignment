using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}