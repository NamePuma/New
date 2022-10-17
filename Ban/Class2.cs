using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ban
{
    public class Specialities : INotifyPropertyChanged
    {
        private string qualification;
        private string specialityName;
        private string specialityId;

        public Specialities(int id, string specialityid, string specialityname, string qualification)
        {
            Id = id;
            SpecialityId = specialityid;
            SpecialityName = specialityname;
            Qualification = qualification;
        }


        public int Id { get; set; }
        public string SpecialityId { get => specialityId; set {
                specialityId = value;
                OnChange(this, new PropertyChangedEventArgs("SpecialityId"));
            } }
        public string SpecialityName { get => specialityName; set { specialityName = value;
                OnChange(this, new PropertyChangedEventArgs("SpecialityName"));
            } 
        }
        public string Qualification
        {
            get => qualification; set
            {
                qualification = value;
                OnChange(this, new PropertyChangedEventArgs("Qualification"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChange(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, e);
            }
        }
    }
}