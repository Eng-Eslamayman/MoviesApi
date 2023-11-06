using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Dtos
{
    public class RegistrationUserDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
