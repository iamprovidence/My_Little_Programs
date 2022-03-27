using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class LoginModel
    {
        public string ReturnUrl { get; init; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
