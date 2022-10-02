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
        [PrimaryKey]
        public Guid? DepartmentId { get; set; }

        // mã phòng ban
        [NotEmpty, NotDuplicate]
        public string? DepartmentCode { get; set; }

        // tên phòng ban
        [NotEmpty]
        public string? DepartmentName { get; set; }


    }
}
