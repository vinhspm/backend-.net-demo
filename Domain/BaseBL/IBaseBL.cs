using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{ 
    public interface IBaseBL<T>
    {
        #region Method

        /// <summary>
        /// lấy danh sách toàn bộ bản ghi trong 1 bảng
        /// </summary>
        /// <returns>danh sách toàn bộ bản ghi</returns>
        /// created by: vinhkt(30/09/2022)
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// thêm mới 1 bản ghi vào bảng
        /// </summary>
        /// <returns>id của bản ghi</returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse InsertRecord(T record);

        /// <summary>
        /// sửa một bản ghi trong bảng
        /// </summary>
        /// <returns>số bản ghi bị ảnh hưởng ( 1 )</returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse UpdateRecord(Guid id, T record);

        /// <summary>
        /// xoá một bản ghi trong bảng
        /// </summary>
        /// <returns>số bản ghi bị ảnh hưởng ( 1 )</returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse DeleteRecordById(Guid id);

        /// <summary>
        /// lấy chi tiết 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>record</returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse GetRecordById(Guid id); 

        #endregion
    }
}
