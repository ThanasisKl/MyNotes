using Microsoft.AspNetCore.Mvc;
using MyNotesApp.Data;
using MyNotesApp.Models;

namespace MyNotesApp.Controllers
{
    public class FolderController : Controller
    {
        private readonly ApplicationDbContext note_db;
        public FolderController(ApplicationDbContext db)
        {
            note_db = db;
        }
        public IActionResult Index(int? id)
        {
            IEnumerable<FoldersNote> objFoldersNotesList = note_db.FoldersNotes;
            List<FoldersNote> notesOfFolder = new List<FoldersNote>();

            foreach(var row in objFoldersNotesList)
            {
                if (id == row.Note_id) 
                {
                    notesOfFolder.Add(row);
                }
            }
            ViewBag.Message = id;
            return View(notesOfFolder);
        }

        public IActionResult CreateNote(int? id) 
        {
            ViewBag.Message = id;
            return View();
        }

        //POST
        [HttpPost, ActionName("CreateNote")]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNote(FoldersNote obj)
        {
            if (ModelState.IsValid)
            {
                obj.Id = 0;
                note_db.FoldersNotes.Add(obj);
                note_db.SaveChanges();
                TempData["success"] = "Note created successfully";
                ViewBag.Message = obj.Note_id;

                IEnumerable<FoldersNote> objFoldersNotesList = note_db.FoldersNotes;
                List<FoldersNote> notesOfFolder = new List<FoldersNote>();

                foreach (var row in objFoldersNotesList)
                {
                    if (obj.Note_id == row.Note_id)
                    {
                        notesOfFolder.Add(row);
                    }
                }

                return View("Index",notesOfFolder);
            }
            return View(obj);
        }


    }
}
