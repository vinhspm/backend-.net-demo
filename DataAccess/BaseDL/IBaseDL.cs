using Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IBaseDL<T>
    {
        #region Method
        /// <summary>
        /// lấy danh sách toàn bộ bản ghi trong 1 bảng
        /// </summary>
        /// <returns>danh sách toàn bộ bản ghi</returns>
        /// created by: vinhkt(30/09/2022)
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse InsertRecord(T record);

        /// <summary>
        /// thêm mới 1 bản ghi
        /// </summary>
        /// <param name="v_id"></param>
        /// <param name="v_Query"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public QueryResult UpdateRecord(Guid v_id, T record);

        /// <summary>
        /// xoá 1 bản ghi
        /// </summary>
        /// <param name="v_id"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public QueryResult DeleteRecord(Guid v_id);

        /// <summary>
        /// lấy chi tiết 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>record</returns>
        /// created by: vinhkt(30/09/2022)
        public T GetRecordById(Guid id);

        /// <summary>
        /// tìm giá trị của field trong db bị trùng lặp hay không
        /// </summary>
        /// <param name="fieldName" name="fieldValue"></param>
        /// <returns>boolean</returns>
        /// created by: vinhkt(30/09/2022)
        public T FindDuplicate(string fieldName, string fieldValue);
        #endregion
    }
}
