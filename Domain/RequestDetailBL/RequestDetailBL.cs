using Common.Entities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RequestDetailBL : BaseBL<RequestDetail>, IRequestDetailBL
    {
        #region Field
        
        private IRequestDetailDL _requestDetailDL; 

        #endregion

        #region Constructor

        public RequestDetailBL(IBaseDL<RequestDetail> baseDL, IRequestDetailDL requestDetailDL) : base(baseDL)
        {
            _requestDetailDL = requestDetailDL;
        }

        #endregion
        #region Method

        /// <summary>
        /// lấy tất cả request detail theo id của cha
        /// </summary>
        /// <param name="overTimeId"></param>
        /// <returns></returns>
        public IEnumerable<RequestDetail> GetAllRecordById(Guid overTimeId)
        {
            var employees = _requestDetailDL.GetAllRecordById(overTimeId);
            return employees;
        }

        /// <summary>
        /// xoá tất cả request detail theo id của cha
        /// </summary>
        /// <param name="overTimeId"></param>
        /// <returns></returns>
        public void DeleteRecordByOverTimeId(Guid overTimeId)
        {
            _requestDetailDL.DeleteRecordByOverTimeId(overTimeId);
        } 

        #endregion
    }
}
