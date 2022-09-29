using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ban
{
    public class Specialities
    {

        public Specialities(int id, string specialityid, string specialityname, string qualification)
        {
            Id = id;
            SpecialityId = specialityid;
            SpecialityName = specialityname;
            Qualification = qualification;
        }


        public int Id { get; set; }
        public string SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public string Qualification { get; set; }
    }
}