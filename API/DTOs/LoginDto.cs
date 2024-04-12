using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public record LoginDto
    {
        [Required]
        public string UserName { get; set; } = default!;

        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; } = default!;
    }
}