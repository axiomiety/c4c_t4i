using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DataModels;
using System.ComponentModel;

namespace PathViewModels
{
    public class StudentsViewModel : INotifyPropertyChanged
    {
        ISchoolService _service;

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentsViewModel(ISchoolService service)
        {
            this._service = service;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ISchoolService Service
        {
            get
            {
                return _service;
            }
        }

        public IClass Class
        {
            get
            {
                return _service.Class;
            }
        }

        public IList<IStudent> Students
        {
            get
            {
                return this.Class.Students;
            }
        }

        public IList<string> StudentNames
        {
            get
            {
                return (from IStudent student in this.Students select student.Name).ToList();
            }
        }


        public void AddStudent(string rollNo, string name, string gender)
        {
            this.Class.AddStudent(rollNo, name, gender == "Male" ? Gender.Male : Gender.Female, DateTime.MinValue, -1);
            RaisePropertyChanged("Students");
        }

        public void UpdateStudent(IStudent student, string rollNo, string name, string gender)
        {
            student.RollNumber = rollNo;
            student.Name = name;
            student.Gender = gender == "Male" ? Gender.Male : Gender.Female;
            student.Save();
        }

        public void RemoveStudent(IStudent student)
        {

        }
    }
}