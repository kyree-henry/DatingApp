namespace DatingApp.API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
    }
}

