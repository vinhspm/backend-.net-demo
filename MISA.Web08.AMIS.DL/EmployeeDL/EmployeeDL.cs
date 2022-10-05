using Dapper;
using MISA.Web08.AMIS.Common;
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

        /// <summary>
        /// phân trang nhân viên
        /// created: vinhkt(30/09/2022)
        /// </summary>
        /// <param name="v_Offset"></param>
        /// <param name="v_Limit"></param>
        /// <param name="v_Where"></param>
        /// <returns></returns>
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

        /// <summary>
        /// đếm tổng số nhân viên trong bảng
        /// created: vinhkt(30/09/2022)
        /// </summary>
        /// <returns></returns>
        public int GetCountEmployees()
        {
            string storedProcedureName = String.Format(Resource.Proc_GetAllCount, typeof(Employee).Name);

            var count = _connection.QueryFirstOrDefault<int>(
                storedProcedureName,
                commandType: System.Data.CommandType.StoredProcedure);
            return count;

        }

        /// <summary>
        /// lấy mã nhân viên lớn nhất
        /// created: vinhkt(30/09/2022)
        /// </summary>
        /// <returns></returns>
        public string GetMaxEmployeeCode()
        {

            string storedProcedureName = String.Format(Resource.Proc_employee_Max, typeof(Employee).Name);

            var maxEmployeeCode = _connection.QueryFirstOrDefault<string>(
                storedProcedureName,
                commandType: System.Data.CommandType.StoredProcedure);
            return maxEmployeeCode;
        }

        /// <summary>
        /// xoá nhiều nhân viên
        /// created: vinhkt(30/09/2022)
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        public int MultipleDelete(List<Guid> guids)
        {
            int affetecRows = 0;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                string idsQuery = "(";
                for(int i = 0; i < guids.Count; i++)
                {
                    if(i == guids.Count - 1)
                    {
                        idsQuery += $" '{guids[i]}')";
                    }
                    else
                    {
                        idsQuery += $"'{guids[i]}', ";
                    }
                }
                var storedProcedureName = String.Format(Resource.Proc_DeleteMultiple, typeof(Employee).Name); ;
                DynamicParameters value = new DynamicParameters();
                
                value.Add("@v_IdsQuery", idsQuery);
                affetecRows = _connection.Execute(
                                storedProcedureName,
                                value,
                                commandType: System.Data.CommandType.StoredProcedure);
                return affetecRows;
            }

            
        }

        /// <summary>
        /// query vào db lấy tất cả nhân viên theo filter
        /// </summary>
        /// <param name="v_Where"></param>
        /// <returns></returns>
        public List<Employee> ExportAllEmployeesFilter(string v_Where)
        {
            string storedProcedureName = Resource.proc_employee_GetAllFilter;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                DynamicParameters value = new DynamicParameters();

                value.Add("@v_Where", v_Where);
                var records = _connection.Query<Employee>(
                                storedProcedureName,
                                value,
                                commandType: System.Data.CommandType.StoredProcedure);
                return records.ToList<Employee>();
            }
        }
    }
}
