using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Teacher : ITeacher
    {
        public Teacher() { }
        public Teacher(ISchoolService service, int id, string name, string email, string phone)
        {
            Service = service;
            ID = id;
            Name = name;
            PhoneNumber = phone;
            Email = email;
            Subjects = new Dictionary<IClass, IList<ISubject>>();
            MisInfo = new Dictionary<string, object>();
        }
        public Teacher(ISchoolService service, string name, string email, string phone) :
            this(service, 0, name, email, phone)
        { }

        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ISchoolService Service { get; set; }
        public ISchool School { get; set; }
        public IDictionary<string, object> MisInfo { get; set; }

        public IDictionary<IClass, IList<ISubject>> Subjects { get; private set; }
        public IList<IClass> Classes
        {
            get
            {
                return new List<IClass>(Subjects.Keys);
            }
        }

        public void AddSubject(ISubject subject, IClass cls)
        {
            IList<ISubject> subs = Subjects.ContainsKey(cls) ? Subjects[cls] : new List<ISubject>();
            if (!subs.Contains(subject))
            {
                subs.Add(subject);
            }
            Subjects.Add(cls, subs);
        }

        public void RemoveSubject(ISubject subject, IClass cls)
        {
            IList<ISubject> subs = Subjects.ContainsKey(cls) ? Subjects[cls] : new List<ISubject>();
            if (subs.Contains(subject))
            {
                subs.Remove(subject);
            }
            Subjects[cls] = subs;
        }

        public void Save()
        {
            ID = Service.Save(this);
        }
    }
}
