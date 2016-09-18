using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DataModels
{
    public interface ISchoolService
    {
        string AppName { get; }
        string CopyRight { get; }
        string Introduction { get; }
        IAcademicYear AcademicYear { get; }
        IList<ISchool> Schools { get; }

        string Email { get; set; }
        ITeacher Teacher { get; set; }
        IClass Class { get; set; }
        IList<ISubject> Subjects { get; }
        ISchool School { get; }
        string Country { get; }
        string State { get; }
        string City { get; }

        IList<string> GetCountries();
        IList<string> GetStates(string country);
        IList<string> GetCities(string country, string state);
        IList<ISchool> GetSchools(string country, string state, string city);
        ISchool AddSchool(string name, SchoolType type, string community, string country, string state, string city);
        ITeacher GetTeacher(string mail);
        ITeacher MapTeacher(string mail, string name, string phone, IClass cls, IList<ISubject> subjects);

        int Save(IStudent student);
        int Save(ISchool school);
        int Save(IClass cls);
        int Save(ITeacher teacher);
        int Save(IAcademicYear year);
        int Save(ISubject subject);
    }
}
