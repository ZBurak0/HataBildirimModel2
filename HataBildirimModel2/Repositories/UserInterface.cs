using HataBildirimModel.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
public interface IUserRepository
{
    // Kullanıcı kaydı
    Task RegisterAsync(User user);

    // Kullanıcıyı doğrulama
    Task<User> AuthenticateAsync(string username, string password);

    // Kullanıcıyı Id ile getirme
    Task<User> GetUserByIdAsync(int id);

}



