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
        public EmployeeDL()
        {
        }

        #region Method
        /// <summary>
        /// lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// created: vinhkt(30/09/2022)
        /// <returns>danh sách nhân viên theo filter và phân trang</returns>
        public Dictionary<string, object> GetEmployeesFilter(int pageSize, int pageNumber, string employeeFilter)
        {
            // tính offset, gán giá trị cho string v_where
            int offset = pageSize * (pageNumber - 1);
            string v_Where = "";
            if (employeeFilter != null)
            {
                v_Where = $"EmployeeCode LIKE \"%{employeeFilter}%\" OR FullName LIKE \"%{employeeFilter}%\"";
            }
            var storedProcedureName = Resource.Proc_employee_Filter;

            DynamicParameters values = new DynamicParameters();
            values.Add("@v_Offset", offset);
            values.Add("@v_Limit", pageSize);
            values.Add("@v_Where", v_Where);

            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var response = _connection.QueryMultiple(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure);
                var employees = response.Read<Employee>().ToList();
                var count = response.Read<int>().First();
                return new Dictionary<string, object>{
                    { "PageData", employees},
                    { "Total", count }
                };
            }

        }

        /// <summary>
        /// lấy mã nhân viên lớn nhất
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>mã nhân viên lớn nhất</returns>
        public string GetMaxEmployeeCode()
        {

            string storedProcedureName = String.Format(Resource.Proc_employee_Max, typeof(Employee).Name);
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var maxEmployeeCode = _connection.QueryFirstOrDefault<string>(
                storedProcedureName,
                commandType: System.Data.CommandType.StoredProcedure);
                return maxEmployeeCode;
            }


        }

        /// <summary>
        /// xoá nhiều nhân viên trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các nhân viên cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public int MultipleDelete(List<Guid> guids)
        {
            int affetecRows = 0;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                string idsQuery = "(";
                for (int i = 0; i < guids.Count; i++)
                {
                    if (i == guids.Count - 1)
                    {
                        idsQuery += $" '{guids[i]}')";
                    }
                    else
                    {
                        idsQuery += $"'{guids[i]}', ";
                    }
                }
                _connection.Open();
                var trans = _connection.BeginTransaction();

                var storedProcedureName = String.Format(Resource.Proc_DeleteMultiple, typeof(Employee).Name); ;
                DynamicParameters value = new DynamicParameters();

                value.Add("@v_IdsQuery", idsQuery);
                try
                {
                    affetecRows = _connection.Execute(
                                storedProcedureName,
                                value,
                                commandType: System.Data.CommandType.StoredProcedure,
                                transaction: trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                finally
                {
                    _connection.Close();
                }

                return affetecRows;
            }


        }

        /// <summary>
        /// xuất file excel các nhân viên theo filter
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>file excel cần download</returns>
        public List<Employee> ExportAllEmployeesFilter(string employeeFilter)
        {
            string v_Where = "";
            if (employeeFilter != null)
            {
                v_Where = $"EmployeeCode LIKE \"%{employeeFilter}%\" OR FullName LIKE \"%{employeeFilter}%\"";
            }
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
        #endregion
    }
}
