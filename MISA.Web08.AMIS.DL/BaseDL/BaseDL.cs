using Dapper;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {

        /// <summary>
        /// xoá 1 bản ghi
        /// </summary>
        /// <param name="v_id"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public QueryResult DeleteRecord(Guid v_id)
        {
            DynamicParameters value = new DynamicParameters();
            value.Add("@v_Id", v_id);
            int affetecRows = 0;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var storedProcedureName = String.Format(Resource.Proc_Delete, typeof(T).Name); ;

                affetecRows = _connection.Execute(
                                storedProcedureName,
                                value,
                                commandType: System.Data.CommandType.StoredProcedure);
            }

            if (affetecRows > 0)
            {
                return QueryResult.Success;
            }
            else
            {
                return QueryResult.Fail;
            }
        }

        #region Method

        /// <summary>
        /// danh sách toàn bộ bản ghi 1 bảng
        /// </summary>
        /// <returns>danh sách toàn bộ bản ghi 1 bảng</returns>
        /// created by vinhkt(30/09/2022)
        public IEnumerable<T> GetAllRecords()
        {
            string storedProcedureName = String.Format(Resource.Proc_GetAll, typeof(T).Name);
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var records = _connection.Query<T>(
                                storedProcedureName,
                                commandType: System.Data.CommandType.StoredProcedure);
                return records;
            }
        }

        /// <summary>
        /// lấy chi tiết 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>record</returns>
        /// created by: vinhkt(30/09/2022)
        public T GetRecordById(Guid id)
        {
            DynamicParameters value = new DynamicParameters();
            value.Add("@v_id", id);
            var storedProcedureName = String.Format(Resource.Proc_Detail, typeof(T).Name); ;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var records = _connection.QueryFirstOrDefault<T>(
                                storedProcedureName,
                                value,
                                commandType: System.Data.CommandType.StoredProcedure);
                return records;
            }
        }

        /// <summary>
        /// thêm mới 1 bản ghi
        /// </summary>
        /// <param name="v_Columns"></param>
        /// <param name="v_Values"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public QueryResult InsertRecord(string v_Columns, string v_Values)
        {
            DynamicParameters values = new DynamicParameters();
            values.Add("@v_Columns", v_Columns);
            values.Add("@v_Values", v_Values);
            var affetecRows = 0;

            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {

                var storedProcedureName = String.Format(Resource.Proc_Insert, typeof(T).Name);

                affetecRows = _connection.Execute(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure);

            }

            if (affetecRows > 0)
            {
                return QueryResult.Success;
            }
            else
            {
                return QueryResult.Fail;
            }


        }

        /// <summary>
        /// thêm mới 1 bản ghi
        /// </summary>
        /// <param name="v_id"></param>
        /// <param name="v_Query"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public QueryResult UpdateRecord(Guid id, string v_Query)
        {
            var v_Id = $"'{id}'";
            DynamicParameters values = new DynamicParameters();
            values.Add("@v_Id", v_Id);
            values.Add("@v_Query", v_Query);
            var affetecRows = 0;

            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {

                var storedProcedureName = String.Format(Resource.Proc_Update, typeof(T).Name);

                affetecRows = _connection.Execute(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure);

            }
            if (affetecRows > 0)
            {
                return QueryResult.Success;
            }
            else
            {
                return QueryResult.Fail;
            }
        }

        #endregion
    }
}
