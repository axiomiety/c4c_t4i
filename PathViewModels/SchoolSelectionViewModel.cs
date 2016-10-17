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
    public class SchoolSelectionViewModel : INotifyPropertyChanged
    {
        private ISchoolService _service;
        private string _country;
        private string _state;
        private string _city;
        private string _schoolName;
        private ISchool _school;

        public SchoolSelectionViewModel(ISchoolService service)
        {
            this._service = service;
        }

        #region Properties

        public ISchoolService Service
        {
            get
            {
                return _service;
            }
        }

        public IList<string> Countries
        {
            get
            {
                return this._service.GetCountries();
            }
        }

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

        public IList<string> SchoolNames
        {
            get
            {
                return (from ISchool school in Schools select school.Name).ToList<string>();
            }
        }

        public string SchoolName
        {
            get
            {
                return _schoolName;
            }
            set
            {
                _schoolName = value;
                List<ISchool> match = (from ISchool school in Schools where school.Name == value select school).ToList<ISchool>();
                if (match.Count != 0)
                {
                    School = match[0];
                }
                else
                {
                    School = null;
                }
                RaisePropertyChanged("SchoolName");
            }
        }

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

        #endregion

        #region Validation

        public string CountryError
        {
            get
            {
                if (Country.Trim().Length == 0)
                {
                    return "Country cannot be empty";
                }
                else if (!Countries.Contains(Country))
                {
                    return "Invalid Country";
                }
                return string.Empty;
            }
        }

        public string StateError
        {
            get
            {
                if (State.Trim().Length == 0)
                {
                    return "State cannot be empty";
                }
                else if (!States.Contains(State))
                {
                    return "Invalid State";
                }
                return string.Empty;
            }
        }

        public string CityError
        {
            get
            {
                if (City.Trim().Length == 0)
                {
                    return "City cannot be empty";
                }
                else if (!Cities.Contains(City))
                {
                    return "Invalid City";
                }
                return string.Empty;
            }
        }

        public string SchoolError
        {
            get
            {
                if (SchoolName.Trim().Length == 0)
                {
                    return "School cannot be empty";
                }
                else if (!SchoolNames.Contains(SchoolName))
                {
                    return "Invalid School";
                }
                return string.Empty;
            }
        }

        public string Errors
        {
            get
            {
                List<string> errors = new List<string> { CountryError, StateError, CityError, SchoolError };
                errors = (from string error in errors where !string.IsNullOrEmpty(error) select error).ToList<string>();
                return string.Join("\n", errors);
            }
        }


        #endregion

        #region Persistence

        public void Save()
        {
            _service.School = School;
        }

        #endregion

        #region Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
