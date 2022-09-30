using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.BL;
using MISA.Web08.AMIS.Common;

namespace MISA.Web08.AMIS.API.Controllers
{
    public class DepartmentsController : BasesController<Department>
    {
        #region Constructor
        
        public DepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {
        } 

        #endregion
    }
}
