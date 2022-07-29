using Microsoft.AspNetCore.Mvc;
using MyNotesApp.Data;
using MyNotesApp.Models;
using System.Diagnostics;

namespace MyNotesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext note_db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            note_db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        //POST
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(string email,string password)
        {
            var obj = note_db.Users.Find(email);
            if (obj == null)
            {
                return RedirectToAction("Login");
            }

            if (obj.Password == password)
            {
                ViewBag.email = email;
                return View("Index");
            }else {
                TempData["error"] = "Email or password are incorrect";
                return RedirectToAction("Login");
            }

        }

        public IActionResult Register()
        {
            return View();
        }

        //POST
        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPost(User obj)
        {
            var user = note_db.Users.Find(obj.Email);
            if (user == null)
            {
                if (ModelState.IsValid)
                {
                    note_db.Users.Add(obj);
                    note_db.SaveChanges();
                    TempData["success"] = "Account created successfully";
                    return RedirectToAction("Login");
                }
                TempData["error"] = "Please put reasonable values for username and password";
                return RedirectToAction("Register");
            }
            TempData["error"] = "Username already in use";
            return RedirectToAction("Register");
        }


        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}