using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public interface IClass : INamedEntity
    {
        IAcademicYear AcademicYear { get; set; }
        ISchool School { get; set; }
        Shift Shift { get; set; }
        int DailyInstructionHours { get; set; }
        IList<ISubject> Subjects { get; }
        IDictionary<ISubject, ITeacher> Teachers { get; set; }
        IList<IStudent> Students { get; set; }

        IStudent AddStudent(string rollNo, string name, Gender gender, DateTime dob, int yoi);
        IStudent AddStudent(IStudent student);
        IClass PromoteStudent(IStudent student, IAcademicYear year);
        ISubject AddSubject(string name);
        void RemoveSubject(ISubject subject);
        void AddTeacher(ITeacher teacher, ISubject subject);
        void RemoveTeacher(ITeacher teacher, ISubject subject);
        
    }
}
