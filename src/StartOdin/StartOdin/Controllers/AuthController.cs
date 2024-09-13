using Microsoft.AspNetCore.Mvc;

namespace StartOdin.Controllers;

public class AuthController : Controller
{
    
    public IActionResult Register()
    {
        return View();
    }
    
    
}