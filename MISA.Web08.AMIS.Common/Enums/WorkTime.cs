using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.Common
{
    public enum WorkTime
    {
        // trước ca
        BeforeShift = 1,

        //sau ca
        AfterShift = 2,

        // nghỉ giữa ca
        ShiftBreak = 3,

        // ngày nghỉ
        WorkOffDay = 4
    }
}
