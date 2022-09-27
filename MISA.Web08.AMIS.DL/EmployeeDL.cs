using Dapper;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.DL
{
    public class EmployeeDL : IEmployeeDL
    {
        public IEnumerable<Employee> GetAllEmployees()
        {
            // khởi tạo kết nối tới db mysql
            string connectionString = DataContext.MySqlConnectionString;
            var mysqlConnection = new MySqlConnection(connectionString);
            string storedProcedureName = String.Format(Resource.Proc_GetAll, typeof(Employee).Name);

            var employees = mysqlConnection.Query<Employee>(
                storedProcedureName,
                commandType: System.Data.CommandType.StoredProcedure);
            return employees;
        }

        public Employee GetEmployeeById()
        {
            throw new NotImplementedException();
        }

        public int InsertEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
