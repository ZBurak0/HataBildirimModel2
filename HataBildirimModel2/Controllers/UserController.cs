//using DocumentFormat.OpenXml.Spreadsheet;
using HataBildirimModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using HataBildirimModel2;
using System.Net.Mail;




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
    private string GenerateVerificationCode()
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
        // Şifremi unuttum sayfasını göster
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        //mail gpnderme bolumu 
        if (string.IsNullOrEmpty(email))
        {
            ViewData["ErrorMessage"] = "Lütfen bir e-posta giriniz.";
            return View();
        }
        Method klas = new Method();

        DataRow drKul = klas.GetDataRow("Select * from [Users] Where Email='" + email + "'");
        if (drKul != null)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("hatabildirimsistemi@gmail.com", "vbdekooqqdcexidu");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.Port = 587;
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true;
            mail.To.Add(email);
            mail.From = new MailAddress("hatabildirimsistemi@gmail.com");
            mail.Subject = "Şifre Hatırlatma";
            mail.Body = "Şifreniz:" + drKul["Password"].ToString() + "   " + "Kullanıcı Adınız:" + drKul["UserName"] + "";
            client.Send(mail);
        }
        return RedirectToAction("VerifyCode");
    }

    // 6 haneli rastgele kod üreten metod
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

            string correctCode = TempData["ResetCode"] as string;
            if (correctCode == null)
            {
                ViewData["ErrorMessage"] = "Onay kodu süresi dolmuş veya geçersiz.";
                return View();
            }

            if (enteredCode == correctCode)
            {
                return RedirectToAction("ResetPassword");
            }

            ViewData["ErrorMessage"] = "Geçersiz onay kodu.";
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "Kullanıcı bulunamadı.";
                return View();
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword); // Yeni şifreyi hashleyerek kaydet
            await _context.SaveChangesAsync();

            TempData["status"] = "Şifreniz başarıyla güncellendi.";
            return RedirectToAction("Login");
        }
    }

