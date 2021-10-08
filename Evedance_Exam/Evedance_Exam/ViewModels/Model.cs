using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evedance_Exam.ViewModels
{
    public class TeacherVM
    {
        public int TeacherId { get; set; }

        [Required, StringLength(40), Display(Name = "Teacher Name")]

        public string TeacherName { get; set; }

        [Required, Column(TypeName = "money"), Display(Name = "Course Fees"), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Coursefees { get; set; }

        [Required, Column(TypeName = "Date"), Display(Name = "Class Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ClassDate { get; set; }

        public bool Continue { get; set; }

        public IFormFile Picture { get; set; }

        [Required, ForeignKey("Subject")]
        public int SubjectId { get; set; }
    }

    public class TeacherEdit
    {
        public int TeacherId { get; set; }

        [Required, StringLength(40), Display(Name = "Teacher Name")]

        public string TeacherName { get; set; }

        [Required, Column(TypeName = "money"), Display(Name = "Course Fees"), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Coursefees { get; set; }

        [Required, Column(TypeName = "Date"), Display(Name = "Class Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ClassDate { get; set; }

        public bool Continue { get; set; }

        public IFormFile Picture { get; set; }

        [Required, ForeignKey("Subject")]
        public int SubjectId { get; set; }
    }
}
