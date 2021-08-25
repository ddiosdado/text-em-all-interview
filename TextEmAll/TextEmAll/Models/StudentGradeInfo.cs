using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TextEmAll.Models
{
    public class StudentGradeInfo
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Gpa { get; set; }
    }
}