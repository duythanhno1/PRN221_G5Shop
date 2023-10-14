using System.ComponentModel.DataAnnotations;

namespace PRN221_MVC.Models {
    public class RegisterUserViewModel {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
