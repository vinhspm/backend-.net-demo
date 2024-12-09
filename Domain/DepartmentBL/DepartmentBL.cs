using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        public DepartmentBL(IBaseDL<Department> baseDL) : base(baseDL)
        {
        }
    }
}
