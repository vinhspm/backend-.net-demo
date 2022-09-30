using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.BL;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;

namespace MISA.Web08.AMIS.API.Controllers
{
    public class EmployeesController : BasesController<Employee>
    {
        
        #region Field

		private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// created: vinhkt(30/09/2022)
        /// <returns></returns>
        [HttpGet]
        [Route("filter")]
        public IActionResult GetEmployeesFilter([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string? employeeFilter)
        {
            try
            {
                PagingData pagingData = _employeeBL.GetEmployeesFilter(pageSize, pageNumber, employeeFilter);

                return StatusCode(StatusCodes.Status200OK, pagingData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        /// <summary>
        /// lấy mã nhân viên mới
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns></returns>
        [HttpGet]
        [Route("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                string newEmployeeCode = _employeeBL.GetNewEmployeeCode();

                return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        } 

        #endregion
    }
}
