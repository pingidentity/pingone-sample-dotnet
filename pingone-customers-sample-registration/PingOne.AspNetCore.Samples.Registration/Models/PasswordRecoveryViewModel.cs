using System.ComponentModel.DataAnnotations;

namespace PingOne.AspNetCore.Samples.Registration.Models
{
    public class PasswordRecoveryViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Recovery code")]
        [DataType(DataType.Text)]
        [Required]
        public string RecoveryCode { get; set; }

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string PasswordRegex { get; set; }
    }
}
