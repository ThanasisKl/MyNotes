using System.ComponentModel.DataAnnotations;

namespace MyNotesApp.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
