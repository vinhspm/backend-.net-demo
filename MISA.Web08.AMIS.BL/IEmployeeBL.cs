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

        public PagingData GetEmployeesFilter(int pageSize, int pageNumber, string employeeFilter);

        public string GetNewEmployeeCode();
        
        public Employee GetEmployeeById(Guid employeeId); 

        public int InsertEmployee(Employee employee); 

        public int UpdateEmployee(Guid employeeId, Employee employee);

        public int DeleteEmployeeById(Guid employeeId);

    }
}
