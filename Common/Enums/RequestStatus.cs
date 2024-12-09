using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum RequestStatus
    {
        // đã duyệt
        Approved = 1,

        //từ chối
        Waiting = 2,

        // chờ duyệt
        Denined = 3,

        // tất cả
        All = -1
    }
}
