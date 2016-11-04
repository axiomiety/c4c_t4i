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
    public class StudentViewModel : INotifyPropertyChanged
    {
        private ISchoolService _service;
        private IStudent _student;
        private string _rollNo;
        private string _name;
        private Gender _gender;

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentViewModel(IStudent student, ISchoolService service=null)
        {
            _student = student;
            if (service == null)
            {
                if (student == null)
                {
                    throw new Exception("SchoolService missing");
                }
                _service = student.Service;
            }
            else
            {
                _service = service;
            }
            if (_student != null)
            {
                _rollNo = _student.RollNumber;
                _name = _student.Name;
                _gender = _student.Gender;
            }
        }

        #region Properties

        public string RollNumber
        {
            get
            {
                return _rollNo;
            }
            set
            {
                if (value != _rollNo)
                {
                    _rollNo = value;
                    RaisePropertyChanged("RollNumber");
                    RaisePropertyChanged("RollNumberError");
                }
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                    RaisePropertyChanged("NameError");
                }
            }
        }

        public string Gender
        {
            get
            {
                return _gender.ToString();
            }
            set
            {
                if (value != _gender.ToString())
                {
                    _gender = (Gender)Enum.Parse(typeof(Gender), value);
                    RaisePropertyChanged("Gender");
                }
            }
        }

        #endregion

        #region Error Properties

        public string RollNumberError
        {
            get
            {
                if (string.IsNullOrEmpty(RollNumber) || RollNumber.Trim().Length == 0)
                {
                    return "Roll Number cannot be empty";
                }
                IList<string> rollNos = (from IStudent student in _service.School.Students
                                         where student != _student
                                         select student.RollNumber).ToList();
                if (rollNos.Contains(RollNumber))
                {
                    return "Roll Number has to be unique";
                }
                return null;
            }
        }

        public string NameError
        {
            get
            {
                if (string.IsNullOrEmpty(Name) || Name.Trim().Length == 0)
                {
                    return "Name cannot be empty";
                }
                return null;
            }
        }

        #endregion

        #region Property Changes

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion

        #region Persistence

        public void AddStudent(string rollNo, string name, string gender)
        {
            _service.Class.AddStudent(rollNo, name, gender == "Male" ? DataModels.Gender.Male : DataModels.Gender.Female, DateTime.MinValue, -1);
            RaisePropertyChanged("Students");
        }

        public void UpdateStudent(IStudent student, string rollNo, string name, string gender)
        {
            student.RollNumber = rollNo;
            student.Name = name;
            student.Gender = gender == "Male" ? DataModels.Gender.Male : DataModels.Gender.Female;
            student.Save();
        }

        public void RemoveStudent(IStudent student)
        {
            student.Class.RemoveStudent(student);
        }

        #endregion
    }
}