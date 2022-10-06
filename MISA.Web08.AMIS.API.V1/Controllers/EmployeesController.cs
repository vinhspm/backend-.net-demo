using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.BL;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Resources;
using System.IO;

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
        /// <returns>danh sách nhân viên theo filter và phân trang</returns>
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
        /// <returns>mã nhân viên mới</returns>
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

        /// <summary>
        /// xoá nhiều nhân viên trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các nhân viên cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        [HttpPut("multiple-delete")]
        public IActionResult MultipleDelete([FromBody] List<Guid> guids)
        {
            try
            {
                var result = _employeeBL.MultipleDelete(guids);
                return StatusCode(StatusCodes.Status200OK, result.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        AMISErrorCode.Exception,
                        Resource.DevMsg_DeleteFailed,
                        Resource.UserMsg_DeleteFailed,
                        ex.Message,
                        HttpContext.TraceIdentifier));

            }
        }

        /// <summary>
        /// xuất file excel các nhân viên theo filter
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>file excel cần download</returns>
        [HttpGet]
        [Route("ExportAllEmployeesFilter")]
        public IActionResult ExportAllEmployeesFilter([FromQuery] string? employeeFilter)
        {
            try
            {
                MemoryStream employeesSheetStream = _employeeBL.ExportAllEmployeesFilter(employeeFilter);

                return File(employeesSheetStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
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
