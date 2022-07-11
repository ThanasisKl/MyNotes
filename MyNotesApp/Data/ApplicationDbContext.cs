using Microsoft.EntityFrameworkCore;
using MyNotesApp.Models;

namespace MyNotesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }
    }
}
