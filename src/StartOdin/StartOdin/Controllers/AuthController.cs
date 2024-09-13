using Microsoft.AspNetCore.Mvc;
using StartOdin.Domain.ViewModels.Auth;

namespace StartOdin.Controllers;

public class AuthController : Controller
{

    #region HTTP GET

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Страница авторизации пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    #endregion

    #region HTTP POST

    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Register(RegisterViewModel viewModel)
    {
        return Ok();
    }

    /// <summary>
    /// Авторизация пользователя в системе
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Login(LoginViewModel viewModel)
    {
        return Ok();
    }

    #endregion
    
}