using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using HataBildirimModel.Models;
using HataBildirimModel.Repositories;
using Microsoft.CodeAnalysis.Scripting;

public class AdminRepository : AdminInterface
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    // Admin giriş yapacak (username & password doğrulama)
    public async Task<Admin?> AuthenticateAsync(string username, string password)
    {
        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.UserName == username);
        if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.Password))
        {
            return null;  // Admin bulunamadı veya şifre yanlış
        }
        return admin;
    }
}
