using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AdminController : Controller
{
    private readonly AdminRepository _adminRepository;

    public AdminController(AdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    // Admin giriş sayfası
    public IActionResult Login()
    {
        return View();
    }

    // Admin giriş işlemi
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var admin = await _adminRepository.AuthenticateAsync(username, password);

        if (admin == null)
        {
            ViewData["ErrorMessage"] = "Geçersiz kullanıcı adı veya şifre.";
            return View();
        }

        // Giriş başarılı, admin paneline yönlendirme
        // (Örneğin, Dashboard'a yönlendirebilirsiniz)
        return RedirectToAction("Index", "Home");
    }
}
