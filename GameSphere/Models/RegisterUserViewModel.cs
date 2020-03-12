using System.ComponentModel.DataAnnotations;

namespace GameSphere.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Game { get; set; }
        [Required]
        public string Console { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public bool Privacy { get; set; }
    }
}
