using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Student : IStudent
    {

        public Student() { }
        public Student(ISchoolService service, int id, string rollNo, string name, Gender gender, DateTime dob, int yoi, IList<IClass> classes = null)
        {
            Service = service;
            Name = name;
            RollNumber = rollNo;
            Gender = gender;
            DateOfBirth = dob;
            YOI = yoi;
            Classes = classes != null ? classes : new List<IClass>();
        }
        public Student(ISchoolService service, string rollNo, string name, Gender gender, DateTime dob, int yoi, IList<IClass> classes = null) :
            this(service, 0, rollNo, name, gender, dob, yoi, classes) {}

        public ISchoolService Service { get; set; }
        public int ID { get; set; }
        public string RollNumber { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int YOI { get; set; }

        public IList<IClass> Classes { get; private set; }

        public IDictionary<IAcademicYear, IClass> ClassesByAcademicYear
        {
            get
            {
                Dictionary<IAcademicYear, IClass> byYear = new Dictionary<IAcademicYear, IClass>();
                foreach(IClass cls in this.Classes)
                {
                    byYear.Add(cls.AcademicYear, cls);
                }
                return byYear;
            }
        }

        public IClass Class
        {
            get
            {
                foreach (IClass cls in this.Classes)
                {
                    if (cls.AcademicYear == Service.AcademicYear)
                    {
                        return cls;
                    }
                }
                return null;
            }
        }


        public void AddToClass(IClass cls)
        {
            cls.AddStudent(this);
            Classes.Add(cls);
        }

        public void Save()
        {
            ID = Service.Save(this);
        }
    }
}
