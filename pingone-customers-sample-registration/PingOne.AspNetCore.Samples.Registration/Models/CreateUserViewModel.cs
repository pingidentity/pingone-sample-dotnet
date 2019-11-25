using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PingOne.AspNetCore.Samples.Registration.Models
{
    public class CreateUserViewModel
    {
        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Population")]
        public string Population { get; set; }

        public List<SelectListItem> AllPopulations { get; set; }
        public string PasswordRegex { get; set; }
    }
}
