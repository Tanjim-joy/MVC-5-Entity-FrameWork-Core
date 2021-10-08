using Evedance_Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evedance_Exam.Controllers
{
   
    public class SubjectsController : Controller
    {
        readonly TeacherSubDbContext db;
        public SubjectsController(TeacherSubDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Subjects.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create (Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return PartialView("_CreatePartial", true);
            }
            return PartialView("_CreatePartial", false);
        }
        public IActionResult Edit(int id)
        {
            return View(db.Subjects.First(X => X.SubjectId == id));
        }
        [HttpPost]
        public IActionResult Edit (Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_CreatePartial", true);
            }
            return PartialView("_CreatePartial", false);
        }
        public IActionResult Delete(int id)
        {
            return View(db.Subjects.First(X => X.SubjectId == id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            var sub = new Subject { SubjectId = id };
            if (!db.Teachers.Any(A=>A.SubjectId == id))
            {
                db.Entry(sub).State = EntityState.Deleted;
                db.SaveChanges();
                return PartialView("_CreatePartial", true);
            }
            ModelState.AddModelError("", "Cannot delete. Subject has related Teacher.");
            return View(db.Subjects.First(x => x.SubjectId == id));
        }
    }
}
