using System.ComponentModel.DataAnnotations;

namespace ContactForm.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }
}