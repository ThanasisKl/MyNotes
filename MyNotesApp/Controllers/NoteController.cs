using Microsoft.AspNetCore.Mvc;
using MyNotesApp.Data;
using MyNotesApp.Models;

namespace MyNotesApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext note_db;
        public NoteController(ApplicationDbContext db)
        {
            note_db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Note> objNotesList = note_db.Notes;
            List<Note> notesList = objNotesList.ToList();  //converts IEnumerable to List
            notesList.Sort((x, y) => x.CreatedDateTime.CompareTo(y.CreatedDateTime));
            notesList.Reverse();
            return View(notesList);
        }

        //GET
        public IActionResult CreateNote()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNote(Note obj)
        {
      
            if (ModelState.IsValid)
            {
                note_db.Notes.Add(obj);
                note_db.SaveChanges();
                TempData["success"] = "Note created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult CreateFolder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFolder(Note obj)
        {
            obj.Type = "folder";

            if (ModelState.IsValid)
            {
                note_db.Notes.Add(obj);
                note_db.SaveChanges();
                TempData["success"] = "Folder created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
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
            if (id==null || id == 0)
            {
                return NotFound();
            }
            var NoteFromDb = note_db.Notes.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

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
            obj.Type = "note";
            obj.CreatedDateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                note_db.Notes.Update(obj);
                note_db.SaveChanges();
                TempData["success"] = "Updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult ViewFolder()
        {

            IEnumerable<Note> objCategoryList = note_db.Notes;
            return View(objCategoryList.Reverse());
        }

    }
}

