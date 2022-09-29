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
        private MySqlConnection _connection;

        public EmployeeDL()
        {
            _connection = new MySqlConnection(DataContext.MySqlConnectionString);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            
            string storedProcedureName = String.Format(Resource.Proc_GetAll, typeof(Employee).Name);

            var employees = _connection.Query<Employee>(
                storedProcedureName,
                commandType: System.Data.CommandType.StoredProcedure);
            return employees;
        }

        public Dictionary<string, object> GetEmployeesFilter(int v_Offset, int v_Limit, string v_Where)
        {
            var storedProcedureName = Resource.Proc_employee_Filter;

            DynamicParameters values = new DynamicParameters();
            values.Add("@v_Offset", v_Offset);
            values.Add("@v_Limit", v_Limit);
            values.Add("@v_Where", v_Where);


            var response = _connection.QueryMultiple(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure);
            var employees = response.Read<Employee>().ToList();
            var count = response.Read<int>().First();
            return new Dictionary<string, object>{
                { "PageData", employees},
                { "Total", count }
            };
        }

        public int GetCountEmployees()
        {
            string storedProcedureName = String.Format(Resource.Proc_GetAllCount, typeof(Employee).Name);

            var count = _connection.QueryFirstOrDefault<int>(
                storedProcedureName,
                commandType: System.Data.CommandType.StoredProcedure);
            return count;

        }
        public string GetMaxEmployeeCode()
        {

            string storedProcedureName = String.Format(Resource.Proc_employee_Max, typeof(Employee).Name);

            var maxEmployeeCode = _connection.QueryFirstOrDefault<string>(
                storedProcedureName,
                commandType: System.Data.CommandType.StoredProcedure);
            return maxEmployeeCode;
        }

        public Employee GetEmployeeById(Guid employeeId)
        {
            var storedProcedureName = String.Format(Resource.Proc_Detail, typeof(Employee).Name); ;

            DynamicParameters value = new DynamicParameters();
            value.Add(Resource.proc_emp_id, employeeId);

            var employee = _connection.QueryFirstOrDefault<Employee>(storedProcedureName, value, commandType: System.Data.CommandType.StoredProcedure);

            return employee;
        }

        public int DeleteEmployeeById(Guid employeeId)
        {
            var storedProcedureName = String.Format(Resource.Proc_Delete, typeof(Employee).Name); ;

            DynamicParameters value = new DynamicParameters();
            value.Add(Resource.proc_emp_id, employeeId);

            var res =  _connection.Execute(storedProcedureName, value, commandType: System.Data.CommandType.StoredProcedure);
            return res;
        }

        public int InsertEmployee(string v_Columns, string v_Values)
        {
                var storedProcedureName = Resource.Proc_employee_Insert;

                DynamicParameters values = new DynamicParameters();
                values.Add("@v_Columns", v_Columns);
                values.Add("@v_Values", v_Values);
                return _connection.Execute(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure);
            
        }

        public int UpdateEmployee(Guid employeeId, string v_Query)
        {
            var v_EmployeeID = $"'{employeeId}'";
            var storedProcedureName = Resource.Proc_employee_Update;
            DynamicParameters values = new DynamicParameters();
            values.Add("@v_EmployeeID", v_EmployeeID);
            values.Add("@v_Query", v_Query);
            return _connection.Execute(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
