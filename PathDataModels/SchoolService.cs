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

        #region Properties

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
        public string Email { get; set; }
        public ITeacher Teacher { get; set; }
        public IClass Class { get; set; }
        public IList<ISubject> Subjects
        {
            get
            {
                return this.Class != null ? this.Class.Subjects : null;
            }
        }
        public ISchool School
        {
            get
            {
                return this.Class != null ? this.Class.School : null;
            }
        }
        public string Country
        {
            get
            {
                return this.School != null ? this.School.Country : "";
            }
        }
        public string State
        {
            get
            {
                return this.School != null ? this.School.State : "";
            }
        }
        public string City
        {
            get
            {
                return this.School != null ? this.School.City : "";
            }
        }

        #endregion

        #region Methods

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
        public ITeacher MapTeacher(string mail, string name, string phone, IClass cls, IList<ISubject> subjects)
        {
            Teacher teacher = new Teacher(this, name, mail, phone);
            foreach(ISubject subject in subjects)
            {
                cls.AddTeacher(teacher, subject);
            }
            return teacher;
        }
        public ITeacher GetTeacher(string mail)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Save Methods

        public int Save(IStudent student) { return 0; }
        public int Save(ISchool school) { return 0; }
        public int Save(IClass cls) { return 0; }
        public int Save(ITeacher teacher) { return 0; }
        public int Save(IAcademicYear year) { return 0; }
        public int Save(ISubject subject) { return 0; }

        #endregion
    }

    public class MockSchoolService : ISchoolService
    {
        List<ISchool> _schools = new List<ISchool>();
        IAcademicYear _year;

        public MockSchoolService()
        {
            School s1 = new School(this, 1, "School1", SchoolType.PrivateSchool, "Community1", "India", "MH", "Mumbai");
            School s2 = new School(this, 1, "School2", SchoolType.PrivateSchool, "Community2", "India", "TN", "Chennai");
            School s3 = new School(this, 1, "School3", SchoolType.PrivateSchool, "Community3", "Indonesia", "IN", "Jakarta");

            IAcademicYear ay1 = s1.CreateNewAcademicYear(null, "2015-2016", new DateTime(2015, 4, 1), new DateTime(2016, 3, 31), 200);
            IClass c1 = s1.AddClass(ay1, "4A", Shift.Morning, 6);
            IClass c2 = s1.AddClass(ay1, "5A", Shift.Morning, 4);
            s1.MoveToAcademicYear(ay1);

            IAcademicYear ay2 = s2.CreateNewAcademicYear(null, "2015-2016", new DateTime(2015, 4, 1), new DateTime(2016, 3, 31), 200);
            IClass c3 = s2.AddClass(ay2, "4A", Shift.Morning, 6);
            s2.MoveToAcademicYear(ay2);

            IAcademicYear ay3 = s3.CreateNewAcademicYear(null, "2015-2016", new DateTime(2015, 4, 1), new DateTime(2016, 3, 31), 200);
            IClass c4 = s3.AddClass(ay3, "1A", Shift.Morning, 6);
            s3.MoveToAcademicYear(ay3);

            ISubject sb1 = c1.AddSubject("Maths");
            ISubject sb2 = c1.AddSubject("English");
            ISubject sb3 = c2.AddSubject("Maths");
            ISubject sb4 = c2.AddSubject("English");

            ITeacher t1 = new Teacher(this, 1, "Teacher1", "mail1@abc.com", "Phone1");
            ITeacher t2 = new Teacher(this, 2, "Teacher2", "mail2@abc.com", "Phone2");

            c1.AddTeacher(t1, sb1);
            c2.AddTeacher(t2, sb2);

            c1.AddStudent("S1", "Student1", Gender.Male, new DateTime(1988, 6, 1), 1);
            c1.AddStudent("S2", "Student2", Gender.Male, new DateTime(1994, 8, 21), 1);

            _schools.Add(s1);
            _schools.Add(s2);
            _schools.Add(s3);
            _year = ay1;
        }

        public string AppName
        {
            get
            {
                return "Mock";
            }
        }
        public string CopyRight
        {
            get
            {
                return "Mock CopyRight";
            }
        }
        public string Introduction
        {
            get
            {
                return "This is a sample service to test the Path app";
            }
        }
        public IAcademicYear AcademicYear
        {
            get
            {
                return _year;
            }
        }
        public IList<ISchool> Schools
        {
            get
            {
                return _schools;
            }
        }

        public string Email { get; set; }
        public ITeacher Teacher { get; set; }
        public IClass Class { get; set; }
        public IList<ISubject> Subjects
        {
            get
            {
                return this.Class != null ? this.Class.Subjects : null;
            }
        }
        public ISchool School
        {
            get
            {
                return this.Class != null ? this.Class.School : null;
            }
        }
        public string Country
        {
            get
            {
                return this.School != null ? this.School.Country : "";
            }
        }
        public string State
        {
            get
            {
                return this.School != null ? this.School.State : "";
            }
        }
        public string City
        {
            get
            {
                return this.School != null ? this.School.City : "";
            }
        }



        public IList<string> GetCountries()
        {
            var query = from ISchool school in Schools
                        select school.Country;
            return query.Distinct().ToList<string>();
        }
        public IList<string> GetStates(string country)
        {
            var query = from ISchool school in Schools
                        where school.Country == country
                        select school.State;
            return query.Distinct().ToList<string>();
        }
        public IList<string> GetCities(string country, string state)
        {
            var query = from ISchool school in Schools
                        where school.Country == country && school.State == state
                        select school.City;
            return query.Distinct().ToList<string>();
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
        public ITeacher MapTeacher(string mail, string name, string phone, IClass cls, IList<ISubject> subjects)
        {
            Teacher teacher = new Teacher(this, name, mail, phone);
            foreach (ISubject subject in subjects)
            {
                cls.AddTeacher(teacher, subject);
            }
            return teacher;
        }
        public ITeacher GetTeacher(string mail)
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
