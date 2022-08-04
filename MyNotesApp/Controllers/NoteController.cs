using Microsoft.AspNetCore.Mvc;
using MyNotesApp.Data;
using MyNotesApp.Models;

namespace MyNotesApp.Controllers
{
    public class NoteController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext note_db;
        public NoteController(ApplicationDbContext db)
        {
            note_db = db;
        }

        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            IEnumerable<Note> objNotesList = note_db.Notes;
            List<Note> notesListTemp = objNotesList.ToList();
            List<Note> notesList = new List<Note>();
            foreach (Note note in notesListTemp)
            {
                if (note.Username == username)
                {
                    notesList.Add(note);
                }

            }
            notesList.Sort((x, y) => x.CreatedDateTime.CompareTo(y.CreatedDateTime));
            notesList.Reverse();
            if (HttpContext.Session.GetString("welcome_msg") == "true")
            {
                ViewBag.username = username;
                HttpContext.Session.SetString("welcome_msg", "false");
            }
            return View(notesList);
        }

        //GET
        public IActionResult CreateNote()
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNote(Note obj)
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            obj.Username = username;
            note_db.Notes.Add(obj);
            note_db.SaveChanges();
            TempData["success"] = "Note created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult CreateFolder()
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFolder(Note obj)
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            obj.Type = "folder";
            obj.Username = username;
            note_db.Notes.Add(obj);
            note_db.SaveChanges();
            TempData["success"] = "Folder created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var noteFromDb = note_db.Notes.Find(id);

            if (noteFromDb == null)
            {
                return NotFound();
            }
            ViewBag.Type = noteFromDb.Type;
            return View(noteFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            var obj = note_db.Notes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            note_db.Notes.Remove(obj);
            note_db.SaveChanges();
            TempData["success"] = "Deleted successfully";

            if (obj.Type == "folder") 
            {
                IEnumerable<FoldersNote> objFoldersNotesList = note_db.FoldersNotes;
                foreach (var row in objFoldersNotesList)
                {
                    if (id == row.Note_id)
                    {
                        note_db.FoldersNotes.Remove(row);
                    }
                }
            }
            note_db.SaveChanges();
            return RedirectToAction("Index");

        }

        //GET
        public IActionResult Edit(int? id)
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            if (id==null || id == 0)
            {
                return NotFound();
            }
            var NoteFromDb = note_db.Notes.Find(id);

            if (NoteFromDb == null)
            {
                return NotFound();
            }

            return View(NoteFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Note obj)
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }
            obj.Username = username;
            obj.Type = "note";
            obj.CreatedDateTime = DateTime.Now;
            note_db.Notes.Update(obj);
            note_db.SaveChanges();
            TempData["success"] = "Updated successfully";
            return RedirectToAction("Index");
        }
    }
}

