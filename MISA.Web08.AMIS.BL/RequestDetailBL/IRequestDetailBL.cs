using MISA.Web08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public interface IRequestDetailBL : IBaseBL<RequestDetail>
    {
        public IEnumerable<RequestDetail> GetAllRecordById(Guid overTimeId);

        public void DeleteRecordByOverTimeId(Guid overTimeId);
    }
}
