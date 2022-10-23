using Dapper;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
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
        public List<Employee> ExportAllRequestsFilter(string requestFilter)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetRequestsFilter(int pageSize, int pageNumber, string requestFilter, RequestStatus requestStatus)
        {
            // tính offset, gán giá trị cho string v_where
            int offset = pageSize * (pageNumber - 1);
            string v_Where = null;
            if (requestStatus == RequestStatus.All)
            {
                if (requestFilter != "" && requestFilter != null)
                {
                    v_Where = $"e.EmployeeCode LIKE \"%{requestFilter}%\" OR e.FullName LIKE \"%{requestFilter}%\"";
                }
            }
            else if(requestStatus != null)
            {
                if (requestFilter != "")
                {
                    v_Where = $"(e.EmployeeCode LIKE \"%{requestFilter}%\" OR e.FullName LIKE \"%{requestFilter}%\") AND Status={(int)Enum.Parse(typeof(RequestStatus), requestStatus.ToString())}";
                } else
                {
                    v_Where = $"Status={(int)Enum.Parse(typeof(RequestStatus), requestStatus.ToString())}";
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

        public int MultipleDelete(List<Guid> guids)
        {
            throw new NotImplementedException();
        }
    }
}
