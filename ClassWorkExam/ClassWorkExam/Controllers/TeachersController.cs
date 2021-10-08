using ClassWorkExam.Models;
using ClassWorkExam.ViewMode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWorkExam.Controllers
{
    public class TeachersController : Controller
    {
        readonly TeacherDbContext db = null;

        readonly IWebHostEnvironment env;
        public TeachersController(TeacherDbContext db, IWebHostEnvironment env) { this.db = db; this.env = env; }
        public IActionResult Index()
        {
            return View(db.Teachers.Include(x=>x.Subject).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Subjects = db.Subjects.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(TeacherCreate teacher)
        {
            if (ModelState.IsValid)
            {
                var newTeacher = new Teacher
                {
                    PIcture = "no-pic.png",
                    TeacherName = teacher.TeacherName,
                    CourseFee = teacher.CourseFee,
                    Continued = teacher.Continued,
                    DateOfClass = teacher.DateOfClass,
                    SubjectId = teacher.SubjectId
                };
                if (teacher.PIcture != null && teacher.PIcture.Length > 0)
                {
                    string dir = Path.Combine(env.WebRootPath, "Uploads");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    String fileName = Guid.NewGuid() + Path.GetExtension(teacher.PIcture.FileName);
                    string fullPath = Path.Combine(dir, fileName);
                    FileStream fd = new FileStream(fullPath, FileMode.Create);
                    teacher.PIcture.CopyTo(fd);
                    fd.Flush();
                    newTeacher.PIcture = fileName;
                }
                db.Teachers.Add(newTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Subjects = db.Teachers.ToList();
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Subjects = db.Subjects.ToList();
            var teacher = db.Teachers/*.Include(x => x.Subject)*/.First(s => s.TeacherId == id);
            ViewBag.CurrentPicture = teacher.PIcture;
            return View(new TeacherEdit
            {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.TeacherName,
                CourseFee = teacher.CourseFee,
                Continued = teacher.Continued,
                DateOfClass = teacher.DateOfClass,
                SubjectId = teacher.SubjectId
            });
        }
        [HttpPost]
        public IActionResult Edit(TeacherEdit teacher)
        {
            var teacherExists = db.Teachers.First(a => a.TeacherId == teacher.TeacherId);
            if (ModelState.IsValid)
            {
                teacherExists.TeacherName = teacher.TeacherName;
                teacherExists.CourseFee = teacher.CourseFee;
                teacherExists.Continued = teacher.Continued;
                teacherExists.DateOfClass = teacher.DateOfClass;
                teacherExists.SubjectId = teacher.SubjectId;
                if(teacher.PIcture != null && teacher.PIcture.Length > 0)
                {
                    string dir = Path.Combine(env.WebRootPath, "Uploads");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string filename = Guid.NewGuid() + Path.GetExtension(teacher.PIcture.FileName);
                    string fullpath = Path.Combine(dir, filename);
                    FileStream fs = new FileStream(fullpath, FileMode.Create);
                    teacher.PIcture.CopyTo(fs);
                    fs.Flush();
                    teacherExists.PIcture = filename;
                }
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.Subjects = db.Subjects.ToList();
            ViewBag.currentPicture = teacherExists.PIcture;
            return View(teacher);
        }
        public IActionResult Delete(int id)
        {
            var delete = db.Teachers.Include(t => t.Subject).First(s => s.TeacherId == id);
            ViewBag.CurrentPic = delete.PIcture;
            return View(delete);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            var Teacher = new Teacher { TeacherId = id };
            ViewBag.CurrentPic = Teacher.PIcture;
            db.Entry(Teacher).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
