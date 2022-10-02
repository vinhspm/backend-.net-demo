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
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        private MySqlConnection _connection;

        public EmployeeDL()
        {
            _connection = new MySqlConnection(DataContext.MySqlConnectionString);
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


    }
}
