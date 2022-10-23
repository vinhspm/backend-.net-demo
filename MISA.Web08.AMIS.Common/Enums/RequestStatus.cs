using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.Common
{
    public enum RequestStatus
    {
        // đã duyệt
        Approved = 1,

        //từ chối
        Denined = 2,

        // chờ duyệt
        Waiting = 3,

        // tất cả
        All = -1
    }
}
