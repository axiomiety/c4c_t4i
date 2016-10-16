using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Class : IClass
    {
        private string _name;

        public Class() { }
        public Class(ISchoolService service, int id, string grade, string section, IAcademicYear year, ISchool school, Shift shift, int hours)
        {
            Service = service;
            ID = id;
            Grade = grade;
            Section = section;
            AcademicYear = year;
            School = school;
            Shift = shift;
            DailyInstructionHours = hours;
            Subjects = new List<ISubject>();
            Teachers = new Dictionary<ISubject, ITeacher>();
            Students = new List<IStudent>();
        }
        public Class(ISchoolService service, string grade, string section, IAcademicYear year, ISchool school, Shift shift, int hours)
            : this(service, 0, grade, section, year, school, shift, hours)
        { }
        public Class(IClass otherClass, IAcademicYear year)
        {
            Service = otherClass.Service;
            Grade = otherClass.Grade;
            Section = otherClass.Section;
            Name = otherClass.Name;
            AcademicYear = year;
            School = otherClass.School;
            Shift = otherClass.Shift;
            DailyInstructionHours = otherClass.DailyInstructionHours;
            Subjects = new List<ISubject>();
            Teachers = new Dictionary<ISubject, ITeacher>();
            Students = new List<IStudent>();
            MisInfo = new Dictionary<string, object>();
        }

        public ISchoolService Service { get; set; }
        public int ID { get; set; }
        public string Grade { get; set; }
        public string Section { get; set; }
        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(_name)) return _name;
                return string.Format("{0}{1}", Grade, Section);
            }
            set
            {
                _name = value;
            }
        }
        public IAcademicYear AcademicYear { get; set; }
        public ISchool School { get; set; }
        public Shift Shift { get; set; }
        public int DailyInstructionHours { get; set; }
        public IList<ISubject> Subjects { get; }
        public IDictionary<ISubject, ITeacher> Teachers { get; set; }
        public IList<IStudent> Students { get; set; }
        public IDictionary<string, object> MisInfo { get; set; }

        public IStudent AddStudent(string rollNo, string name, Gender gender, DateTime dob, int yoi)
        {
            IStudent student = new Student(Service, rollNo, name, gender, dob, yoi);
            student.Save();
            Students.Add(student);
            return student;
        }

        public IStudent AddStudent(IStudent student)
        {
            Students.Add(student);
            return student;
        }

        public IClass PromoteStudent(IStudent student, IAcademicYear year)
        {
            IClass nextCls = School.GetNextClass(this, year);
            student.AddToClass(nextCls);
            return nextCls;
        }

        public ISubject AddSubject(string name)
        {
            ISubject subject = new Subject(Service, name);
            Subjects.Add(subject);
            return subject;
        }

        public void RemoveSubject(ISubject subject)
        {
            if(Subjects.Contains(subject))
            {
                Subjects.Remove(subject);
            }
        }

        public void AddTeacher(ITeacher teacher, ISubject subject)
        {
            if (Teachers.ContainsKey(subject))
            {
                Teachers[subject].RemoveSubject(subject, this);
                Teachers.Remove(subject);
            }
            Teachers.Add(subject, teacher);
            teacher.AddSubject(subject, this);
        }

        public void RemoveTeacher(ITeacher teacher, ISubject subject)
        {
            if(!Teachers.ContainsKey(subject))
            {
                throw new Exception("Given subject doesn't have a teacher yet");
            }
            Teachers.Remove(subject);
            teacher.RemoveSubject(subject, this);
        }

        public void Save()
        {
            ID = Service.Save(this);
        }
    }
}
