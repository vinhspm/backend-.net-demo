using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.BL;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Resources;
using MISA.Web08.AMIS.Common;

namespace MISA.Web08.AMIS.API.Controllers
{
    public class RequestsController : BasesController<Request>
    {
        #region Field

        private IRequestBL _requestBL;

        #endregion

        #region Constructor

        public RequestsController(IRequestBL requestBL) : base(requestBL)
        {
            _requestBL = requestBL;
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
        public IActionResult GetRequestsFilter([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string? requestFilter, [FromQuery] RequestStatus status, [FromQuery] Guid? departmentId)
        {
            try
            {
                PagingData pagingData = _requestBL.GetRequestsFilter(pageSize, pageNumber, requestFilter, status, departmentId);

                return StatusCode(StatusCodes.Status200OK, pagingData);
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
                var result = _requestBL.MultipleDelete(guids);
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
        /// duyệt nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các nhân viên cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        [HttpPut("multiple-approve")]
        public IActionResult MultipleApprove([FromBody] List<Guid> guids)
        {
            try
            {
                var result = _requestBL.MultipleApprove(guids);
                return StatusCode(StatusCodes.Status200OK, result.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        AMISErrorCode.Exception,
                        Resource.DevMsg_ApproveFailed,
                        Resource.UserMsg_ApproveFailed,
                        ex.Message,
                        HttpContext.TraceIdentifier));

            }
        }

        /// <summary>
        /// từ chối nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các nhân viên cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        [HttpPut("multiple-denine")]
        public IActionResult MultipleDenine([FromBody] List<Guid> guids)
        {
            try
            {
                var result = _requestBL.MultipleDenine(guids);
                return StatusCode(StatusCodes.Status200OK, result.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        AMISErrorCode.Exception,
                        Resource.DevMsg_DenineFailed,
                        Resource.UserMsg_DenineFailed,
                        ex.Message,
                        HttpContext.TraceIdentifier));

            }
        }

        /// <summary>
        /// xuất file excel các request theo filter
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>file excel cần download</returns>
        [HttpGet]
        [Route("ExportAllRequestsFilter")]
        public IActionResult ExportAllRequestsFilter([FromQuery] string? requestFilter, [FromQuery] RequestStatus status, [FromQuery] Guid? departmentId)
        {
            try
            {
                MemoryStream requestSheetStream = _requestBL.ExportAllRequestsFilter(requestFilter, status, departmentId);

                return File(requestSheetStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh sách yêu cầu.xlsx");
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
