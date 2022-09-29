using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ban
{
    public class Group
    {
        public Group(string groupid, int course, int speciality ,string nameSpet)
        {
            GroupID = groupid;
            Course = course;
            Speciality = speciality;
            NameSpet = nameSpet;

        }


        public string GroupID { get; set; }
        public int Course { get; set; }
        public int Speciality { get; set; }
        public string NameSpet { get; set; }

    }
}
