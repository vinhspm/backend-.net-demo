using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public class EmployeeBL : IEmployeeBL
    {
        #region Field
		
        private IEmployeeDL _employeeDL; 

	    #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
        }

	    #endregion
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeDL.GetAllEmployees();
        }

        public Employee GetEmployeeById()
        {
            return _employeeDL.GetEmployeeById();
        }
    }
}
