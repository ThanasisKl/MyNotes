using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyNotesApp.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Username { get; set; }


        public string Content { get; set; } = "";

        public string Type { get; set; } = "note";

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
