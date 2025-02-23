using Microsoft.AspNetCore.Mvc;

namespace BanHangMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/file-manager")]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
