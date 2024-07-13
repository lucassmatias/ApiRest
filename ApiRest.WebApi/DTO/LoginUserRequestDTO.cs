using System.ComponentModel.DataAnnotations;

namespace ApiRest.WebApi.DTO
{
    public class LoginUserRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}
