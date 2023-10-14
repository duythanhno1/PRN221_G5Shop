using Microsoft.AspNetCore.Identity;

namespace DAL.Entities {
    public class User : IdentityUser {
        public string Name { get; set; }
        public DateTime? DoB { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public bool? isDeleted { get; set; }
        public virtual IEnumerable<Orders>? Orders { get; set; }
        public virtual IEnumerable<Comment>? Comments { get; set; }
    }
}
