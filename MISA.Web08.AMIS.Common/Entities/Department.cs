using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.Common
{
    public class Department : BaseEntity
    {
        // id phòng ban
        public Guid DepartmentID { get; set; } = Guid.NewGuid();

        // mã phòng ban
        public string DepartmentCode { get; set; }

        // tên phòng ban
        public string DepartmentName { get; set; }


    }
}
