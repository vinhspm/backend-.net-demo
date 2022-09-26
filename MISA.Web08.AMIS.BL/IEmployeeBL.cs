using MISA.Web08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public interface IEmployeeBL
    {
        public IEnumerable<Employee>  GetAllEmployees();
    }
}
