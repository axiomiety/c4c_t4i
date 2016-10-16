using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public interface ISchool : INamedEntity
    {
        SchoolType Type { get; }
        string Community { get; }
        string Country { get; }
        string State { get; }
        string City { get; }
        IDictionary<IAcademicYear, int> DaysYearly { get; }
        IDictionary<IAcademicYear, IList<IClass>> ClassesByYear { get; }
        IAcademicYear AcademicYear { get; }
        IList<IClass> Classes { get; }

        IAcademicYear CreateNewAcademicYear(IAcademicYear prevYear, string name, DateTime start, DateTime end, int days);
        IClass AddClass(IAcademicYear year, string grade, string section, Shift shift, int dailyHours);
        IClass GetNextClass(IClass cls, IAcademicYear year);
        void MoveToAcademicYear(IAcademicYear year);
    }
}
