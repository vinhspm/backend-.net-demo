using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Department : BaseEntity
    {
        #region Field
        // id phòng ban
        [PrimaryKey]
        public Guid? DepartmentId { get; set; }

        // mã phòng ban
        [NotEmpty, NotDuplicate]
        public string? DepartmentCode { get; set; }

        // tên phòng ban
        [NotEmpty]
        public string? DepartmentName { get; set; }

        #endregion


    }
}
