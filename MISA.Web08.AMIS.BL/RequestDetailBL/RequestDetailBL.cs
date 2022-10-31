using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public class RequestDetailBL : BaseBL<RequestDetail>, IRequestDetailBL
    {
        private IRequestDetailDL _requestDetailDL;

        public RequestDetailBL(IBaseDL<RequestDetail> baseDL, IRequestDetailDL requestDetailDL) : base(baseDL)
        {
            _requestDetailDL = requestDetailDL;
        }

        public IEnumerable<RequestDetail> GetAllRecordById(Guid overTimeId)
        {
            var employees = _requestDetailDL.GetAllRecordById(overTimeId);
            return employees;
        }

        public void DeleteRecordByOverTimeId(Guid overTimeId)
        {
            _requestDetailDL.DeleteRecordByOverTimeId(overTimeId);
        }
    }
}
