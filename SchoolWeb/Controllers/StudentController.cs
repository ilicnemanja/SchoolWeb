using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWeb.Data;
using SchoolWeb.Models;

namespace SchoolWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        private bool StudentIDExists(string ID)
        {
            return (_db.Students?.Any(e => e.ID == ID)).GetValueOrDefault();
        }
        private bool StudentEmailExists(string Email)
        {
            return (_db.Students?.Any(e => e.Email == Email)).GetValueOrDefault();
        }


        // GET LIST
        public IActionResult Index()
        {
            IEnumerable<Student> objStudentList = _db.Students.ToList();
            return View(objStudentList);
        }

        // GET DETAIL
        public IActionResult Details(string ID)
        {
            List<Exam> exam = _db.Exams.Where(s => s.StudentID == ID).Include(e => e.Student).Include(e => e.Subject).ToList();

            if (exam == null)
            {
                return NotFound();
            }


            return View(exam);
        }


        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,FirstName,LastName,Email")] Student student)
        {
            if (StudentIDExists(student.ID))
            {
                TempData["error"] = "Student with this ID already exist!";
                return View(student);
            }

            if (StudentEmailExists(student.Email))
            {
                TempData["error"] = "Student with this Email already exist!";
                return View(student);
            }

            if (student.ID.Contains("/"))
            {
                student.ID = student.ID.Replace("/", "-");
            }

            if (ModelState.IsValid)
            {
                _db.Add(student);
                _db.SaveChanges();
                TempData["success"] = "Student created successfully!";
                       
            }
                
            return RedirectToAction(nameof(Index));
            
        }

        // GET EDIT
        public IActionResult Edit(string ID)
        {
            if (ID == null || _db.Students == null)
            {
                return NotFound();
            }

            var student = _db.Students.Find(ID);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string ID, string Email, [Bind("ID,FirstName,LastName,Email")] Student student)
        {
            if (ID != student.ID)
            {
                return NotFound();
            }

            if (StudentIDExists(student.ID) && ID != student.ID)
            {
                TempData["error"] = "Student with this ID already exist!";
                return View(student);
            }

            if (ModelState.IsValid)
            {

                _db.Update(student);
                _db.SaveChanges();
                TempData["success"] = "Student updated successfully!";
                

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        
        // GET DELETE
        public IActionResult Delete(string ID)
        {
            if (ID == null || _db.Students == null)
            {
                return NotFound();
            }

            var student = _db.Students.FirstOrDefault(m => m.ID == ID);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string ID)
        {
            //if (_db.Students == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            //}
            var student = _db.Students.Find(ID);
            if (student != null)
            {
                _db.Students.Remove(student);
                TempData["success"] = "Student deleted successfully!";
            }

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        // STUDENT SEARCH
        public IActionResult SearchedStudent(string ID)
        {
            IEnumerable<Student> objStudentList = _db.Students;


            if (!string.IsNullOrEmpty(ID))
            {
                objStudentList = objStudentList.Where(s => s.ID.Contains(ID));
            }

            return View(objStudentList.ToList());
        }


    }
}
