using DatingApp.API.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingApp.API.Data.Core
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            string? userData = await File.ReadAllTextAsync("Data/Core/UserSeedData.json");
            List<AppUser> users = JsonSerializer.Deserialize<List<AppUser>>(userData)!;
            if (users.Count == 0) return;

            foreach (AppUser user in users)
            {
                using HMACSHA512 hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
