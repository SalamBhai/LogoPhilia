using TheLogoPhilia.Models;

namespace TheLogoPhilia.Authentication
{
    public interface IJWTTokenHandler
    {
        string GenerateToken(UserViewModel Model);
    }
}