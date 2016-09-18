using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public interface IAcademicYear: INamedEntity
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
