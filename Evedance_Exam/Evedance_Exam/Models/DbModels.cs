using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evedance_Exam.Models
{
    public class Subject
    {
        public Subject() { this.Teachers = new List<Teacher>(); }
        public int SubjectId { get; set; }

        [Required, StringLength(50), Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Required, StringLength(50), Display(Name = "Subject Code")]
        public string SubjectCode { get; set; }
        //nav
        public virtual ICollection<Teacher> Teachers {get;set; }

    }

    public class Teacher
    {
        public int TeacherId { get; set; }
        [Required, StringLength(45), Display(Name = "Teachers Name")]
        public string TeacherName { get; set; }

        [Required, Column(TypeName = "money"), Display(Name = "Course Fees"), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Coursefees { get; set; }

        [Required, Column(TypeName = "Date"), Display(Name = "Class Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ClassDate { get; set; }

        public bool Continue { get; set; }

        public string Picture { get; set; }

        [Required, ForeignKey("Subject")]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
    }
    //Create Database
    public class TeacherSubDbContext : DbContext 
    {
        public TeacherSubDbContext(DbContextOptions<TeacherSubDbContext> options) : base(options) { }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    } 

}
