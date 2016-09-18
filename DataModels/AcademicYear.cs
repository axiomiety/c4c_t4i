using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class AcademicYear : IAcademicYear
    {
        public AcademicYear() { }
        public AcademicYear(ISchoolService service, int id, string name, DateTime startDate, DateTime endDate)
        {
            Service = service;
            ID = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
        public AcademicYear(ISchoolService service, string name, DateTime startDate, DateTime endDate)
            : this(service, 0, name, startDate, endDate)
        { }

        public ISchoolService Service { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void Save()
        {
            ID = Service.Save(this);
        }
    }
}
