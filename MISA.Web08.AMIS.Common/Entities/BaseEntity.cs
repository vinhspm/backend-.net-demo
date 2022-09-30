using MISA.Web08.AMIS.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.Common
{
    public class BaseEntity
    {
        // ngày tạo
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        //người tạo
        public string CreatedBy { get; set; } = Resource.DefaultUser;

        // ngày chỉnh sửa gần nhất
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        //người chỉnh sửa gần nhất
        public string ModifiedBy { get; set; } = Resource.DefaultUser;
    }
}
