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

        public  List<FoldersNote> getNotesList(int? id) 
        {
            IEnumerable<FoldersNote> objFoldersNotesList = note_db.FoldersNotes;
            List<FoldersNote> notesOfFolder = new List<FoldersNote>();

            foreach (var row in objFoldersNotesList)
            {
                if (id == row.Note_id)
                {
                    notesOfFolder.Add(row);
                }
            }
            return notesOfFolder;
        }

        public string getFolderTitle(int? id)
        {
            var notesObj = note_db.Notes.Find(id);

            if (notesObj == null)
            {
                return "";
            }

            return notesObj.Title;
        }
        public IActionResult Index(int? id)
        {
            ViewBag.Message = id;
            ViewBag.FolderName = getFolderTitle(id);
            return View(getNotesList(id));
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
                ViewBag.FolderName = getFolderTitle(obj.Note_id);
                return View("Index",getNotesList(obj.Note_id));
            }
            return View(obj);
        }

        public IActionResult DeleteNote(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var noteFromDb = note_db.FoldersNotes.Find(id);

            if (noteFromDb == null)
            {
                return NotFound();
            }
            ViewBag.Message = noteFromDb.Note_id;
            return View(noteFromDb);
        }

        //POST
        [HttpPost, ActionName("DeleteNote")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = note_db.FoldersNotes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            note_db.FoldersNotes.Remove(obj);
            note_db.SaveChanges();
            TempData["success"] = "Note deleted successfully";
            ViewBag.FolderName = getFolderTitle(obj.Note_id);
            return View("Index", getNotesList(obj.Note_id));
        }

        //GET
        public IActionResult EditNote(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            var noteFromDb = note_db.FoldersNotes.Find(id);
     
            if (noteFromDb == null)
            {
                return NotFound();
            }
            ViewBag.Message = noteFromDb.Note_id;
            return View(noteFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNote(FoldersNote obj)
        {
            obj.Type = "note";
            if (ModelState.IsValid)
            {
                note_db.FoldersNotes.Update(obj);
                note_db.SaveChanges();
                TempData["success"] = "Note updated successfully";
                ViewBag.FolderName = getFolderTitle(obj.Note_id);
                return View("Index", getNotesList(obj.Note_id));
            }
            return View(obj);
        }
    }
}
