using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class School : ISchool
    {
        public School() { }
        public School(ISchoolService service, int id, string name, SchoolType type, string community,
            string country, string state, string city)
        {
            Service = service;
            ID = id;
            Name = name;
            Type = type;
            Community = community;
            Country = country;
            State = state;
            City = city;
            DaysYearly = new Dictionary<IAcademicYear, int>();
            ClassesByYear = new Dictionary<IAcademicYear, IList<IClass>>();
            MisInfo = new Dictionary<string, object>();
        }

        public School(ISchoolService service, string name, SchoolType type, string community,
            string country, string state, string city)
            : this(service, 0, name, type, community, country, state, city)
        { }


        public int ID { get; set; }
        public string Name { get; set; }
        public ISchoolService Service { get; }
        public SchoolType Type { get; }
        public string Community { get; }
        public string Country { get; }
        public string State { get; }
        public string City { get; }
        public IDictionary<IAcademicYear, int> DaysYearly { get;  }
        public IDictionary<IAcademicYear, IList<IClass>> ClassesByYear { get; }
        public IAcademicYear AcademicYear { get; private set; }
        public IDictionary<string, object> MisInfo { get; set; }
        public IList<IClass> Classes
        {
            get
            {
                if (ClassesByYear.ContainsKey(AcademicYear))
                    return ClassesByYear[AcademicYear];
                return new List<IClass>();
            }
        }

        public IList<IStudent> Students
        {
            get
            {
                return Classes.SelectMany<IClass, IStudent>((IClass cls) => cls.Students).ToList();
            }
        }

        public IAcademicYear CreateNewAcademicYear(IAcademicYear prevYear, string name, DateTime start, DateTime end, int days)
        {
            if (prevYear != null && !ClassesByYear.ContainsKey(prevYear))
            {
                throw new Exception("Given academic year is not part of this school");
            }
            foreach(IAcademicYear year in ClassesByYear.Keys)
            {
                if (year.Name == name && year.StartDate == start && year.EndDate == end)
                {
                    throw new Exception("Given new academic year is already part of this school");
                }
            }
            IAcademicYear newYear = new AcademicYear(Service, name, start, end);
            DaysYearly.Add(newYear, days);
            List<IClass> newClasses = new List<IClass>();
            ClassesByYear.Add(newYear, newClasses);
            if (prevYear != null)
            {
                foreach (IClass prevCls in ClassesByYear[prevYear])
                {
                    newClasses.Add(new Class(prevCls, newYear));
                }
            }
            return newYear;
        }

        public void MoveToAcademicYear(IAcademicYear year)
        {
            AcademicYear = year;
        }

        public IClass AddClass(IAcademicYear year, string grade, string section, Shift shift, int dailyHours)
        {
            if (!ClassesByYear.ContainsKey(year))
            {
                throw new Exception("Given academic year is not part of this school");
            }
            foreach (IClass cls in ClassesByYear[year])
            {
                if (cls.Grade == grade && cls.Section == section && cls.Shift == shift && cls.DailyInstructionHours == dailyHours)
                {
                    throw new Exception("Given class is already part of this school");
                }
            }
            IClass newCls = new Class(Service, grade, section, year, this, shift, dailyHours);
            ClassesByYear[year].Add(newCls);
            return newCls;
        }

        public IClass GetNextClass(IClass cls, IAcademicYear year)
        {
            if (!ClassesByYear.ContainsKey(year))
            {
                throw new Exception("Given academic year is not part of this school");
            }
            // TODO: Add logic to get next class
            return null;
        }

        public void Save()
        {
            ID = Service.Save(this);
        }
    }
}
