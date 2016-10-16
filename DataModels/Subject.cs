using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Subject : ISubject
    {
        public Subject() { }
        public Subject(ISchoolService service, int id, string name)
        {
            Service = service;
            ID = id;
            Name = name;
            MisInfo = new Dictionary<string, object>();
        }
        public Subject(ISchoolService service, string name) : this(service, 0, name) { }

        public int ID { get; set; }
        public string Name { get; set; }
        public ISchoolService Service { get; set; }
        public IDictionary<string, object> MisInfo { get; set; }

        public void Save()
        {
            ID = Service.Save(this);
        }
    }
}
