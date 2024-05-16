using DatingApp.API.DTOs;
using DatingApp.API.Entities;
using DatingApp.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            using HMACSHA512 hmac = new();

            if (await UserExists(model.UserName)) return BadRequest("UserName is taken!");

            AppUser user = new()
            {
                UserName = model.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new UserDto
            {
                Token = _tokenService.CreateToken(user),
                UserName = model.UserName,
                PhotoUrl = user.Photos?.FirstOrDefault(a => a.IsMain)?.URL
            });
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto model)
        {
            try
            {
                AppUser? user = await _context.Users.Include(a => a.Photos).FirstOrDefaultAsync(a => a.UserName == model.UserName);

                if (user is null) return Unauthorized("Invalid login attempt!");

                using HMACSHA512 hmac = new(user.PasswordSalt);

                byte[] ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != user.PasswordHash[i])
                        return Unauthorized("Invalid login attempt!");
                }

                return Ok(new UserDto
                {
                    Token = _tokenService.CreateToken(user),
                    UserName = model.UserName,
                    PhotoUrl = user.Photos?.FirstOrDefault(a => a.IsMain)?.URL
                });

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(user => user.UserName == userName.ToLower());
        }

    }
}
