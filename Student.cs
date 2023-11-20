using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    [Serializable]
    public class Student
    {
        public Student()
        {
            FullName = "";
            Faculty = "";
            Group = "";
            Speciality = "";
            Marks = "";
        }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public string Group { get; set; }
        public string Speciality { get; set; }
        public string Marks { get; set; }
    }
}