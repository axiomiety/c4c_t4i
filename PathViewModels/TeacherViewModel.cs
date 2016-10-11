using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using System.Windows;


namespace PathViewModels
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        private ISchoolService _service;

        public TeacherViewModel(ISchoolService service)
        {
            this._service = service;
        }

        public IList<string> Countries
        {
            get
            {
                return this._service.GetCountries();
            }
        }

        private string _country;

        public string Country {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                RaisePropertyChanged("Country");
                RaisePropertyChanged("States");
                State = "";
            }
        }

        public IList<string> States
        {
            get { return this._service.GetStates(this.Country); }
        }

        private string _state;
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
                RaisePropertyChanged("Cities");
                City = "";
            }
        }

        public IList<string> Cities
        {
            get { return this._service.GetCities(this.Country, this.State); }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                RaisePropertyChanged("City");
                RaisePropertyChanged("Schools");
                School = null;
            }
        }

        public IList<ISchool> Schools
        {
            get { return this._service.GetSchools(this.Country, this.State, this.City); }
        }

        private ISchool _school;

        public ISchool School
        {
            get
            {
                return _school;
            }
            set
            {
                _school = value;
                RaisePropertyChanged("School");
                RaisePropertyChanged("Classes");
                Class = null;
            }
        }

        public IList<IClass> Classes
        {
            get { return this.School.Classes; }
        }

        public IClass Class { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
