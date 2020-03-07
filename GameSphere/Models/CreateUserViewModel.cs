using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameSphere.Models
{
    public class CreateUserViewModel
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
