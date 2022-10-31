using Dapper;
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
    public interface IRequestDetailDL : IBaseDL<RequestDetail>
    {
        public IEnumerable<RequestDetail> GetAllRecordById(Guid overTimeId);
        public int DeleteRecordByOverTimeId(Guid overTimeId);

    }
}
