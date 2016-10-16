using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public interface INamedEntity
    {
        int ID { get; }
        string Name { get; }
        IDictionary<string, object> MisInfo { get; set; }
        ISchoolService Service { get; }

        void Save();
    }
}
