using System.ComponentModel.DataAnnotations;
namespace PRN221_MVC.Models {
    public class TwoFactor {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
