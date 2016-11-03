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
    public class ClassSelectionViewModel : INotifyPropertyChanged
    {
        ISchoolService _service;
        string _grade;
        string _section;

        public ClassSelectionViewModel(ISchoolService service)
        {
            _service = service;
        }

        #region Properties

        public List<string> Grades
        {
            get
            {
                return (from IClass cls in _service.School.Classes select cls.Grade).ToList<string>();
            }
        }

        public string Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                if (_grade != value)
                {
                    _grade = value == null ? Grades[0] : value;
                    RaisePropertyChanged("Grade");
                    RaisePropertyChanged("Sections");
                    Section = null;
                }
            }
        }

        public List<string> Sections
        {
            get
            {
                if (Grade.Length != 0)
                {
                    return (from IClass cls in _service.School.Classes where cls.Grade == Grade select cls.Section).Distinct().ToList<string>();
                }
                return new List<string>();
            }
        }

        public string Section
        {
            get
            {
                return _section;
            }
            set
            {
                if (_section != value)
                {
                    _section = value == null ? Sections[0] : value;
                    RaisePropertyChanged("Section");
                }
            }
        }

        public IClass Class
        {
            get
            {
                if(Grade.Length != 0 && Section.Length != 0)
                {
                    return (from IClass cls in _service.School.Classes where cls.Grade == Grade && cls.Section == Section select cls).ToList<IClass>()[0];
                }
                return null;
            }
        }

        #endregion

        #region Validation

        public string GradeError
        {
            get
            {
                if (string.IsNullOrEmpty(Grade) || Grade.Trim().Length == 0)
                {
                    return "Grade cannot be empty";
                }
                else if (!Grades.Contains(Grade))
                {
                    return "Invalid Grade";
                }
                return string.Empty;

            }
        }

        public string SectionError
        {
            get
            {
                if (string.IsNullOrEmpty(Section) || Section.Trim().Length == 0)
                {
                    return "Section cannot be empty";
                }
                else if (!Sections.Contains(Section))
                {
                    return "Invalid Section";
                }
                return string.Empty;

            }
        }

        public string Errors
        {
            get
            {
                List<string> errors = new List<string> { GradeError, SectionError };
                errors = (from string error in errors where !string.IsNullOrEmpty(error) select error).ToList<string>();
                return string.Join("\n", errors);
            }
        }

        #endregion

        #region Persistence

        public void Save()
        {
			_service.Class = Class;
            _service.Teacher.Classes.Add(Class);
        }

        #endregion

        #region Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}