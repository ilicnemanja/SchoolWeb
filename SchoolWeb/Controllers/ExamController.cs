using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolWeb.Data;
using SchoolWeb.Models;


namespace SchoolWeb.Controllers
{
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExamController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(Subject obje)
        {
            var objExamList = _db.Exams.Include(e => e.Student).Include(e => e.Subject);
            return View(objExamList.ToList());
        }

        // GET CREATE
        public IActionResult Create()
        {
            ViewData["StudentID"] = _db.Students.Select(s => new SelectListItem { Value = s.ID.ToString(), 
                Text = s.FirstName.ToString() + " " + s.LastName.ToString() + $" ({s.ID})" }).ToList();
            ViewData["SubjectName"] = _db.Subjects.Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.Name.ToString() }).ToList();
            return View();
        }

        // POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exam exam)
        {

            if (ModelState.IsValid)
            {
                _db.Exams.Add(exam);
                _db.SaveChanges();
                TempData["success"] = "Exam created successfully!";
                return RedirectToAction(nameof(Index));
            }
            
            return View(exam);
        }

        // GET EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || _db.Exams == null)
            {
                return NotFound();
            }

            var exam = _db.Exams.Find(id);

            if (exam == null)
            {
                return NotFound();
            }

            ViewData["StudentID"] = _db.Students.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.FirstName.ToString() + " " + s.LastName.ToString() + $" ({s.ID})"
            }).ToList();

            ViewData["SubjectName"] = _db.Subjects.Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.Name.ToString() }).ToList();

            return View(exam);
        }

        // POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Exam exam)
        {

            if (ModelState.IsValid)
            {
                _db.Exams.Update(exam);
                _db.SaveChanges();
                TempData["success"] = "Exam created successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(exam);
        }

        // POST DELETE
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int ID)
        {
            var obj = _db.Exams.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Exams.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Exam deleted successfully!";
            return RedirectToAction("Index");

        }

    }
}
