using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using BCrypt.Net;
using HataBildirimModel.Models;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    // Yeni kullanıcı kaydı
    public async Task RegisterAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        // Şifreyi hashle
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        // Kullanıcıyı veritabanına ekle
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    // Kullanıcıyı doğrulama (username ve password ile)
    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null; // Kimlik doğrulama başarısızsa null döner
        }

        return user; // Kimlik doğrulama başarılıysa kullanıcıyı döndürür
    }

    // Id ile kullanıcıyı getirme
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    }

    internal async Task GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}
