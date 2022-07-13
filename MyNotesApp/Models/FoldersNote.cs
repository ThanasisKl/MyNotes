using System.ComponentModel.DataAnnotations;

namespace MyNotesApp.Models
{
    public class FoldersNote
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        public int Note_id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public string Type { get; set; } = "note";

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
