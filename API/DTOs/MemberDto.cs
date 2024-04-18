using DatingApp.API.Entities;

namespace DatingApp.API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public int Age { get; set; }

        public string? PhotoUrl { get; set; }
        public string? KnownAs { get; set; }
        public string? Gender { get; set; }
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interests { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }

        public ICollection<PhotoDto>? Photos { get; set; }
    }
}
