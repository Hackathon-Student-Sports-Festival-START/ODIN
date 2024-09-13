using Microsoft.AspNetCore.Mvc;

namespace StartOdin.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}