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
            notesOfFolder.Sort((x, y) => x.CreatedDateTime.CompareTo(y.CreatedDateTime));
            notesOfFolder.Reverse();
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
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = id;
            ViewBag.FolderName = getFolderTitle(id);
            return View(getNotesList(id));
        }

        public IActionResult CreateNote(int? id) 
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = id;
            return View();
        }

        //POST
        [HttpPost, ActionName("CreateNote")]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNote(FoldersNote obj)
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                obj.Id = 0;
                obj.CreatedDateTime = DateTime.Now;
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
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            var obj = note_db.FoldersNotes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            note_db.FoldersNotes.Remove(obj);
            note_db.SaveChanges();
            TempData["success"] = "Note deleted successfully";
            ViewBag.FolderName = getFolderTitle(obj.Note_id);
            ViewBag.Message = obj.Note_id;
            return View("Index", getNotesList(obj.Note_id));
        }

        //GET
        public IActionResult EditNote(int? id)
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
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                TempData["success"] = "Your session has expired. Please log-in to your account again";
                return RedirectToAction("Login", "Home");
            }

            obj.Type = "note";
            obj.CreatedDateTime = DateTime.Now;
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
