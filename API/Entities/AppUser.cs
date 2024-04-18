using DatingApp.API.Data.Core.Extensions;

namespace DatingApp.API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }

        public string? KnownAs { get; set; }
        public string? Gender { get; set; }
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interests { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;

        public virtual ICollection<Photo>? Photos { get; set; }

        //public int GetAge => DateOfBirth.CalculateAge();
    }
}

