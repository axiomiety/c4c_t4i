using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace PathDataModels
{
    public class SchoolService : ISchoolService
    {
        public SchoolService()
        {
            // Add logic here to connect to server code
        }

        public string AppName
        {
            get
            {
                return "Path";
            }
        }
        public string CopyRight
        {
            get
            {
                return "";
            }
        }
        public string Introduction
        {
            get
            {
                return "";
            }
        }
        public IAcademicYear AcademicYear
        {
            get
            {
                return null;
            }
        }
        public IList<ISchool> Schools
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IList<string> GetCountries()
        {
            var query = from ISchool school in Schools
                        select school.Country;
            return query.ToList<string>();
        }
        public IList<string> GetStates(string country)
        {
            var query = from ISchool school in Schools
                        where school.Country == country
                        select school.State;
            return query.ToList<string>();
        }
        public IList<string> GetCities(string country, string state)
        {
            var query = from ISchool school in Schools
                        where school.Country == country && school.State == state
                        select school.City;
            return query.ToList<string>();
        }
        public IList<ISchool> GetSchools(string country, string state, string city)
        {
            var query = from ISchool school in Schools
                        where school.Country == country && school.State == state && school.City == city
                        select school;
            return query.ToList<ISchool>();
        }
        public ISchool AddSchool(string name, SchoolType type, string community, string country, string state, string city)
        {
            School school = new School(this, name, type, community, country, state, city);
            school.Save();
            return school;
        }
        public ITeacher AddTeacher(string id, string name, string mail, string phone, IClass cls, IList<ISubject> subjects)
        {
            Teacher teacher = new Teacher(this, name, mail, phone);
            foreach(ISubject subject in subjects)
            {
                cls.AddTeacher(teacher, subject);
            }
            return teacher;
        }
        public ITeacher GetTeacher(string id)
        {
            throw new NotImplementedException();
        }

        public int Save(IStudent student) { return 0; }
        public int Save(ISchool school) { return 0; }
        public int Save(IClass cls) { return 0; }
        public int Save(ITeacher teacher) { return 0; }
        public int Save(IAcademicYear year) { return 0; }
        public int Save(ISubject subject) { return 0; }

    }
}
