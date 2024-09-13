using Microsoft.AspNetCore.Mvc;

namespace StartOdin.Controllers;

public class PersonalAccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    #region Team

    public IActionResult Team()
    {
        return View();
    }

    #endregion
}