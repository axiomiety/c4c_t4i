using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using System.Windows;

namespace PathViewModels
{
    public class TeacherViewModel
    {
        private ISchoolService _service;

        public TeacherViewModel(ISchoolService service)
        {
            this._service = service;
            if(this._service.Class != null)
            {
                this.Class = this._service.Class;
                this.School = this._service.Class.School;
                this.Country = this._service.Class.School.Country;
                this.State = this._service.Class.School.State;
                this.City = this._service.Class.School.City;
            }
        }


        public IList<string> Countries
        {
            get
            {
                return this._service.GetCountries();
            }
        }

        public string Country { get; set; }

        public IList<string> States
        {
            get { return this._service.GetStates(this.Country); }
        }

        public string State { get; set; }
        public IList<string> Cities
        {
            get { return this._service.GetCities(this.Country, this.State); }
        }

        public string City { get; set; }


        public IList<ISchool> Schools
        {
            get { return this._service.GetSchools(this.Country, this.State, this.City); }
        }

        public ISchool School { get; set; }

        public IList<IClass> Classes
        {
            get { return this.School.Classes; }
        }

        public IClass Class { get; set; }
    }
}
