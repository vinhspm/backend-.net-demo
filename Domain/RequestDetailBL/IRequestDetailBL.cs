using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRequestDetailBL : IBaseBL<RequestDetail>
    {
        #region Method

        /// <summary>
        /// lấy tất cả request detail theo id của cha
        /// </summary>
        /// <param name="overTimeId"></param>
        /// <returns></returns>
        public IEnumerable<RequestDetail> GetAllRecordById(Guid overTimeId);

        /// <summary>
        /// xoá tất cả request detail theo id của cha
        /// </summary>
        /// <param name="overTimeId"></param>
        /// <returns></returns>
        public void DeleteRecordByOverTimeId(Guid overTimeId); 

        #endregion
    }
}
