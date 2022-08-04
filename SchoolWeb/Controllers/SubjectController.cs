using Microsoft.AspNetCore.Mvc;
using SchoolWeb.Data;
using SchoolWeb.Models;

namespace SchoolWeb.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubjectController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Subject> objSubjectList = _db.Subjects.ToList();
            return View(objSubjectList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subject obj)
        {
            if (ModelState.IsValid)
            {
                _db.Subjects.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Subject created successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var SubjectFromDb = _db.Subjects.Find(ID);

            if (SubjectFromDb == null)
            {
                return NotFound();
            }
            return View(SubjectFromDb);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Subject obj)
        {
            if (ModelState.IsValid)
            {
                _db.Subjects.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Subject updated successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int ID)
        {
            var obj = _db.Subjects.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Subjects.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Subject deleted successfully!";
            return RedirectToAction("Index");

        }

    }
}
