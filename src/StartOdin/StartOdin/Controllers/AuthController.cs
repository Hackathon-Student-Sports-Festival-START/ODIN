using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartOdin.Core.Database;
using StartOdin.Core.Helpers;
using StartOdin.Domain.Entities.Users;
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
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
        
    }

    /// <summary>
    /// Страница авторизации пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    #endregion

    #region HTTP POST

    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="model">Модель представления со страницы регистрации пользователя</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await DatabaseController.GetInstance().Participants
                .FirstOrDefaultAsync(x => x.Email == model.Email);
            
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "Данный пользователь уже зарегистрирован");
                return View(model);
            }

            var participant = new Participant()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = Sha256Helper.ToHash(model.Password),
                Role = model.Role,
                University = model.University,
                DateOfBirth = model.DateOfBirth,
            };
            
            DatabaseController.GetInstance().Participants.Add(participant);
            await DatabaseController.GetInstance().SaveChangesAsync();
            
            var claims = new List<Claim>()
            {
                new (ClaimTypes.Email, participant.Email),
                new (ClaimTypes.Sid, participant.Id.ToString()),
                new (ClaimTypes.Name, participant.FirstName),
                new (ClaimTypes.Surname, participant.LastName),
            };
            
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Срок действия подключения - 7 дней
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };
            
            // Основной метод регистрации пользователя в системе
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity), authProperties);
            
            return RedirectToAction("Index", "Home");
            
        }
        
        ModelState.AddModelError(string.Empty, "Проверьте правильность ввода данных");
        
        return View(model);
    }

    
    /// <summary>
    /// Выход пользователя из системы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Удаление кукисов
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
 
        // Redirect to the login page or home page
        return RedirectToAction("Index", "Home");
    }
    
    /// <summary>
    /// Авторизация пользователя в системе
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = DatabaseController.GetInstance().Participants
                .FirstOrDefault(x => x.Email == model.Email && x.Password == Sha256Helper.ToHash(model.Password));

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль!");
                return View(model);
            }
            
            // Добавляем почту, индентификатор пользователя в системе и его роль 
            var claims = new List<Claim>()
            {
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Sid, user.Id.ToString()),
                new (ClaimTypes.Name, user.FirstName),
                new (ClaimTypes.Surname, user.LastName),
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Срок действия подключения - 7 дней
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };

            // Основной метод регистрации пользователя в системе
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity), authProperties);
            
            // Отправялем пользователя на главную страницу
            return RedirectToAction("Index", "Home");
        }
        
        ModelState.AddModelError(string.Empty, "Проверьте правильность ввода логина и пароля");
        
        return View(model);
    }

    #endregion
    
}