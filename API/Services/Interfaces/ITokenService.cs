using DatingApp.API.Entities;

namespace DatingApp.API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}