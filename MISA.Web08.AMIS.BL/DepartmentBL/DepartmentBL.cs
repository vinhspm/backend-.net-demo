using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL.DepartmentBL
{
    internal class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        public DepartmentBL(IBaseDL<Department> baseDL) : base(baseDL)
        {
        }
    }
}
