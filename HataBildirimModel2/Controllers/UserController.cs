//using DocumentFormat.OpenXml.Spreadsheet;
using HataBildirimModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;
using System.Threading.Tasks;
//using MailKit.Net.Smtp;
//using MailKit.Security;
//using MimeKit;




public class UserController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserRepository _userRepository;

    // Tek constructor ile hem _context hem de _userRepository'i Dependency Injection yoluyla alıyoruz
    public UserController(AppDbContext context, UserRepository userRepository)
    {
        _context = context;  // Dependency Injection ile DbContext'i alıyoruz
        _userRepository = userRepository; // UserRepository'i alıyoruz
    }
    public string GenerateVerificationCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString(); // 100000 ile 999999 arasında rastgele sayı üretir
    }
    // Kullanıcı giriş sayfasını açar
    [HttpGet]
    public IActionResult Login()
    {
        return View(); // Views/User/Login.cshtml dosyasını açar
    }

    // Kullanıcı giriş işlemi
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _userRepository.AuthenticateAsync(username, password);
        if (user == null)
        {
            ViewData["ErrorMessage"] = "Geçersiz kullanıcı adı veya şifre.";
            return View(); // Hata varsa tekrar giriş sayfasını göster
        }

        return RedirectToAction("Index", "Home"); // Başarılı giriş → Anasayfaya yönlendir
    }

    // Kullanıcı kayıt sayfasını açar
    [HttpGet]
    public IActionResult Register()
    {
        var units = _context.Units.ToList(); // Units tablosundaki tüm verileri al

        // Birimleri View'a gönder
        ViewBag.Units = units;
        return View(); // Views/User/Register.cshtml dosyasını açar
    }

    // Yeni kullanıcı kaydı
    [HttpPost]
    public IActionResult Register(User newUser)
    {
        if (_context.Units.Find(newUser.UnitId) == null)
        {
            ViewData["ErrorMessage"] = "Geçersiz birim ID.";
            return View(newUser); // Geçerli birim ID'si yoksa hata ver
        }
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

        _context.Users.Add(newUser);
        _context.SaveChanges();

        TempData["status"] = "Kullanıcı başarıyla kaydedildi.";
        return RedirectToAction("Index", "Home"); // Kullanıcı kaydedildikten sonra Anasayfaya yönlendir
    }
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpPost]

    public async Task<IActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            ViewData["ErrorMessage"] = "E-posta adresi boş olamaz.";
            return View();
        }
        string Code = GenerateVerificationCode();
        //maile onay kodu alma bölümü 
        /*MimeMessage mimeMessage = new MimeMessage();
        MailboxAddress mailboxAddressFrom = new MailboxAddress("HataBildirimAdmin", "hatabildirimsistemi@gmail.com");
        MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
        mimeMessage.From.Add(mailboxAddressFrom);
        mimeMessage.To.Add(mailboxAddressTo);

        var bodybuilder = new BodyBuilder();
        bodybuilder.TextBody = "Şifre sıfırlamak için onay kodunuz:" + Code;
        mimeMessage.Body = bodybuilder.ToMessageBody();
        mimeMessage.Subject = "HBS Şifre sıfırlama kodu";

        MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
        client.Connect("smtp.gmail.com", 465, SecureSocketOptions.StartTls);
        client.Authenticate("hatabildirimsistemi@gmail.com", "poki wuaz yiqz ret");
        client.Send(mimeMessage);
        client.Disconnect(true);*/
        return RedirectToAction("Index", "ForgotPassword");
    }
    [HttpGet]
    public IActionResult VerifyCode()
    {
        return View();
    }
    [HttpPost]
    public IActionResult VerifyCode(string enteredCode)
    {
        if (string.IsNullOrEmpty(enteredCode))
        {
            ViewData["ErrorMessage"] = "Onay kodu boş olamaz.";
            return View();
        }

        // Saklanan kodu al
        string correctCode = TempData["ResetCode"] as string;

        if (correctCode == null)
        {
            ViewData["ErrorMessage"] = "Onay kodu süresi dolmuş veya geçersiz.";
            return View();
        }

        // Kodları karşılaştır
        if (enteredCode == correctCode)
        {
            return RedirectToAction("ResetPassword");
        }
        else
        {
            ViewData["ErrorMessage"] = "Geçersiz onay kodu.";
            return View();
        }
    }

}
