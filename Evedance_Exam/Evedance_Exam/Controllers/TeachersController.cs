using Evedance_Exam.Models;
using Evedance_Exam.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Evedance_Exam.Controllers
{
    public class TeachersController : Controller
    {
        readonly TeacherSubDbContext db = null;
        private readonly IWebHostEnvironment env;

        public TeachersController(TeacherSubDbContext db, IWebHostEnvironment env) { this.db = db; this.env = env; }
        public IActionResult Index()
        {
            return View(db.Teachers.Include(x => x.Subject).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Subjects = db.Subjects.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create (TeacherVM teacher)
        {
            if (ModelState.IsValid)
            {
                var newTeacher = new Teacher
                {
                    Picture = "no-pic.png",
                    TeacherName = teacher.TeacherName,
                    Coursefees = teacher.Coursefees,
                    ClassDate = teacher.ClassDate,
                    Continue = teacher.Continue,
                    SubjectId = teacher.SubjectId
                };
                if (teacher.Picture != null && teacher.Picture.Length>0)
                {
                    string dir = Path.Combine(env.WebRootPath, "Uploads");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    String fileName = Guid.NewGuid() + Path.GetExtension(teacher.Picture.FileName);
                    string fullPath = Path.Combine(dir, fileName);
                    FileStream fd = new FileStream(fullPath, FileMode.Create);
                    teacher.Picture.CopyTo(fd);
                    fd.Flush();
                    newTeacher.Picture = fileName;
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
            var teacher = db.Teachers.First(s => s.TeacherId == id);
            ViewBag.CurrentPicture = teacher.Picture;
            return View(new TeacherEdit
            {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.TeacherName,
                Coursefees = teacher.Coursefees,
                ClassDate = teacher.ClassDate,
                Continue = teacher.Continue,
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
                teacherExists.Coursefees = teacher.Coursefees;
                teacherExists.Continue = teacher.Continue;
                teacherExists.ClassDate = teacher.ClassDate;
                teacherExists.SubjectId = teacher.SubjectId;
                if (teacher.Picture != null && teacher.Picture.Length > 0)
                {
                    string dir = Path.Combine(env.WebRootPath, "Uploads");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string filename = Guid.NewGuid() + Path.GetExtension(teacher.Picture.FileName);
                    string fullpath = Path.Combine(dir, filename);
                    FileStream fs = new FileStream(fullpath, FileMode.Create);
                    teacher.Picture.CopyTo(fs);
                    fs.Flush();
                    teacherExists.Picture = filename;
                }
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.Subjects = db.Subjects.ToList();
            ViewBag.currentPicture = teacherExists.Picture;
            return View(teacher);
        }
        public IActionResult Delete(int id)
        {
            var delete = db.Teachers.Include(t => t.Subject).First(s => s.TeacherId == id);
            ViewBag.CurrentPic = delete.Picture;
            return View(delete);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            var Teacher = new Teacher { TeacherId = id };
            ViewBag.CurrentPic = Teacher.Picture;
            db.Entry(Teacher).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
