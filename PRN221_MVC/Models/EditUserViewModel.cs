using System.ComponentModel.DataAnnotations;

namespace PRN221_MVC.Models {
    public class EditUserViewModel {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public DateTime? DoB { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }

    }
}
