using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.Common
{
    public class Position : BaseEntity
    {
        #region Field
        // id vị trí
        [PrimaryKey]
        public Guid? PositionId { get; set; }

        // mã vị trí
        [NotEmpty, NotDuplicate]
        public string? PositionCode { get; set; }

        // tên vị trí
        [NotEmpty]
        public string? PositionName { get; set; }

        #endregion

    }
}
