using System.ComponentModel.DataAnnotations;

namespace se_24.backend.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email or username is required")]
        public string LoginIdentifier { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
} 