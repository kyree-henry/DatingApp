namespace DatingApp.API.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string? PhotoUrl { get; set; }
    }
}