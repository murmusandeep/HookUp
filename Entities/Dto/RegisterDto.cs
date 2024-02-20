using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
    public class RegisterDto
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
