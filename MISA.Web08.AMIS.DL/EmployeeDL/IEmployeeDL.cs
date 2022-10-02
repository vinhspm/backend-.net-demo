using MISA.Web08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {

        public Dictionary<string,object> GetEmployeesFilter(int v_Offset, int v_Limit, string v_Where);

        public string GetMaxEmployeeCode();

        public int GetCountEmployees();

        public int MultipleDelete(List<Guid> guids);
    }
}
