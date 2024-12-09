using Common;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller
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
