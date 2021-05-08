using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}