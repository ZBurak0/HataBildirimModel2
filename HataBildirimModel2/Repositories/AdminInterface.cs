using HataBildirimModel.Models;

namespace HataBildirimModel.Repositories
{
    public interface AdminInterface
    {
        Task<Admin?> AuthenticateAsync(string username, string password);
    }
}

