using Dal.Entities;

namespace BL.Api;

public interface IBLUser
{
    Task<User?> Login(string username, string phone);
    public Task SignUp(User user);
}
