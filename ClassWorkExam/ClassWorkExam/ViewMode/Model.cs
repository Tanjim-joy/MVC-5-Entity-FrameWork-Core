using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWorkExam.ViewMode
{
    public class TeacherCreate
    {
        public int TeacherId { get; set; }

        [Required, StringLength(40), Display(Name = "Teacher Name")]

        public string TeacherName { get; set; }

        [Required, Column(TypeName = "money"),]
        public decimal CourseFee { get; set; }

        [Required, Column(TypeName = "Date"), Display(Name = "Class Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfClass { get; set; }

        [Required]
        public IFormFile PIcture { get; set; }

        public bool Continued { get; set; }

        [Required, ForeignKey("Subject")]
        public int SubjectId { get; set; }


    }
    public class TeacherEdit
    {
        public int TeacherId { get; set; }

        [Required, StringLength(40), Display(Name = "Teacher Name")]

        public string TeacherName { get; set; }

        [Required, Column(TypeName = "money"),]
        public decimal CourseFee { get; set; }

        [Required, Column(TypeName = "Date"), Display(Name = "Class Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfClass { get; set; }

        public IFormFile PIcture { get; set; }

        public bool Continued { get; set; }

        [Required, ForeignKey("Subject")]
        public int SubjectId { get; set; }


    }
}
