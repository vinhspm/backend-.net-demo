using MISA.Web08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.DL
{
    public interface IEmployeeDL
    {
        public IEnumerable<Employee> GetAllEmployees();

        public Dictionary<string,object> GetEmployeesFilter(int v_Offset, int v_Limit, string v_Where);

        public string GetMaxEmployeeCode();

        public int GetCountEmployees();

        public Employee GetEmployeeById(Guid EmployeeId); 

        public int InsertEmployee(string v_Columns, string v_Values);

        public int UpdateEmployee(Guid v_EmployeeID, string v_Query);

        public int DeleteEmployeeById(Guid v_EmployeeID);
    }
}
