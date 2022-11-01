using Dapper;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Enums;
using MISA.Web08.AMIS.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.DL
{
    public class RequestDL : BaseDL<Request>, IRequestDL
    {

        #region Method

        /// <summary>
        /// lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="requestFilter"></param>
        /// created: vinhkt(30/09/2022)
        /// <returns>danh sách nhân viên theo filter và phân trang</returns>
        public Dictionary<string, object> GetRequestsFilter(int pageSize, int pageNumber, string requestFilter, RequestStatus requestStatus, Guid? departmentId)
        {
            // tính offset, gán giá trị cho string v_where
            int offset = pageSize * (pageNumber - 1);
            string v_Where = null;
            if (requestStatus == RequestStatus.All)
            {
                if (requestFilter != "" && requestFilter != null)
                {
                    if (departmentId == null)
                    {
                        v_Where = $"e.EmployeeCode LIKE \"%{requestFilter}%\" OR e.FullName LIKE \"%{requestFilter}%\"";
                    }
                    else
                    {
                        v_Where = $"(e.EmployeeCode LIKE \"%{requestFilter}%\" OR e.FullName LIKE \"%{requestFilter}%\") AND e.DepartmentId=\"{departmentId}\"";
                    }

                }
                else
                {
                    if (departmentId == null)
                    {
                        v_Where = null;
                    }
                    else
                    {
                        v_Where = $"e.DepartmentId=\"{departmentId}\"";
                    }
                }
            }
            else if (requestStatus != null)
            {
                if (requestFilter != "")
                {
                    if (departmentId != null)
                    {
                        v_Where = $"(e.EmployeeCode LIKE \"%{requestFilter}%\" OR e.FullName LIKE \"%{requestFilter}%\") AND Status={(int)Enum.Parse(typeof(RequestStatus), requestStatus.ToString())} AND e.DepartmentId=\"{departmentId}\"";
                    }
                    else
                    {
                        v_Where = $"(e.EmployeeCode LIKE \"%{requestFilter}%\" OR e.FullName LIKE \"%{requestFilter}%\") AND Status={(int)Enum.Parse(typeof(RequestStatus), requestStatus.ToString())}";
                    }
                }
                else
                {
                    if (departmentId == null)
                    {
                        v_Where = $"Status={(int)Enum.Parse(typeof(RequestStatus), requestStatus.ToString())}";
                    }
                    else
                    {
                        v_Where = $"Status={(int)Enum.Parse(typeof(RequestStatus), requestStatus.ToString())} AND e.DepartmentId=\"{departmentId}\"";

                    }
                }


            }

            var storedProcedureName = Resource.Proc_request_Filter;

            DynamicParameters values = new DynamicParameters();
            values.Add("@v_Offset", offset);
            values.Add("@v_Limit", pageSize);
            values.Add("@v_Where", v_Where);

            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var response = _connection.QueryMultiple(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure);
                var requests = response.Read<Request>().ToList();
                var count = response.Read<int>().First();
                return new Dictionary<string, object>{
                    { "PageData", requests},
                    { "Total", count }
                };
            }

        }

        /// <summary>
        /// xoá nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các yêu cầu cần xoá</param>
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

                var storedProcedureName = String.Format(Resource.Proc_DeleteMultiple, typeof(Request).Name); ;
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
        /// duyệt, từ chối nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các yêu cầu cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public int MultipleChangeStatus(List<Guid> guids, RequestStatus status)
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

                var storedProcedureName = String.Format(Resource.Proc_ChangeStatusMultiple, typeof(Request).Name); ;
                DynamicParameters value = new DynamicParameters();

                value.Add("@v_IdsQuery", idsQuery);
                value.Add("@v_Status", (int)Enum.Parse(typeof(RequestStatus), status.ToString()));
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

        #endregion
    }
}
