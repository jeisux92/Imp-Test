using System.ComponentModel.DataAnnotations;

namespace Impexium.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This fields is required")]
        [MaxLength(200, ErrorMessage = "Max 320 characters")]
        [MinLength(5, ErrorMessage = "Min 10 characters")]
        [EmailAddress(ErrorMessage = "This field mus be an email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This fields is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters")]
        [MinLength(11, ErrorMessage = "Min 11 characters")]
        [DataType(DataType.Password, ErrorMessage = "This field mus be a valid password")]
        public string Password { get; set; }
    }
}
