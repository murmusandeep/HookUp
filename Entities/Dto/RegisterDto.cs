using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateOnly? DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Introduction { get; set; }

        [Required]
        public string LookingFor { get; set; }

        [Required]
        public string Interests { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string password { get; set; }
    }
}
