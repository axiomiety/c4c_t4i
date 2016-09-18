using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public interface IStudent : INamedEntity
    {
        string RollNumber { get; set; }
        Gender Gender { get; set; }
        DateTime DateOfBirth { get; set; }
        // What is this?
        int YOI { get; set; }
        IList<IClass> Classes { get; }
        IDictionary<IAcademicYear, IClass> ClassesByAcademicYear { get; }
        IClass Class { get; }

        void AddToClass(IClass cls);
    }
}
