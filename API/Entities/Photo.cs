using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.API.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string URL { get; set; } = default!;
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; } = default!;
    }
}