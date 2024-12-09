using Common.Entities;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RequestDetailDL : BaseDL<RequestDetail>, IRequestDetailDL
    {

        #region Method

        /// <summary>
        /// lấy tất cả request detail theo id của cha
        /// </summary>
        /// <param name="overTimeId"></param>
        /// <returns></returns>
        public IEnumerable<RequestDetail> GetAllRecordById(Guid overTimeId)
        {
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                var storedProcedureName = "Proc_RequestDetail_Detail";
                DynamicParameters values = new DynamicParameters();
                values.Add("@v_RequestId", overTimeId);
                var record = mysqlConnection.Query<RequestDetail>(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure);
                return record;
            }

        }

        /// <summary>
        /// xoá tất cả request detail theo id của cha
        /// </summary>
        /// <param name="overTimeId"></param>
        /// <returns></returns>
        public int DeleteRecordByOverTimeId(Guid overTimeId)
        {
            int affetecRows = 0;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {

                _connection.Open();
                var trans = _connection.BeginTransaction();

                var storedProcedureName = "proc_requestDetail_DeleteAll";
                DynamicParameters value = new DynamicParameters();

                value.Add("@v_OverTimeId", overTimeId);
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
