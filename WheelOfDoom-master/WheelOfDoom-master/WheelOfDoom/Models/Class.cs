using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfDoom.Models
{
    public class Class
    {
        public string ClassName;
        public List<Student> Students;

        public Class(string className,List<Student> students)
        {
            this.Students = students;
            this.ClassName = className;
        }
    }
}
