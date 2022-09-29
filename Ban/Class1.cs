using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ban
{
    public class Student
    {
        public Student(string name, string surname, string groupID)
        {
            Name = name;
            Surname = surname;
            GroupId = groupID;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string GroupId { get; set; }
         
    }
}
