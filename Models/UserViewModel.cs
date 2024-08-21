using System.ComponentModel.DataAnnotations;

namespace _2022E_WebApp.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter valid phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
    }
}
