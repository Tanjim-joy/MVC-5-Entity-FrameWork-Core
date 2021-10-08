using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWorkExam.Models
{
    public class Subject
    {
        public Subject() { this.Teachers = new List<Teacher>(); }
        public int SubjectId { get; set; }

        [Required, StringLength(40), Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Required, StringLength(40), Display(Name = "Subject Code")]
        public string SubCode { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Required, StringLength(40), Display(Name = "Teacher Name")]

        public string TeacherName { get; set; }

        [Required, Column(TypeName = "money"),]
        public decimal CourseFee { get; set; }

        [Required, Column(TypeName = "Date"), Display(Name = "Class Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfClass { get; set; }

        [Required]
        public string PIcture { get; set; }
        
        public bool Continued { get; set; }

        [Required, ForeignKey("Subject")]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

    }
    public class TeacherDbContext : DbContext
    {
        public TeacherDbContext(DbContextOptions<TeacherDbContext> options) : base(options) { }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
