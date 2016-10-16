using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public interface ITeacher : INamedEntity
    {
        string Email { get; }
        string PhoneNumber { get; }
        ISchool School{ get; set; }
        IDictionary<IClass, IList<ISubject>> Subjects { get; }
        IList<IClass> Classes { get; }

        void AddSubject(ISubject subject, IClass cls);
        void RemoveSubject(ISubject subject, IClass cls);
    }
}
