using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.BL;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Resources;

namespace MISA.Web08.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// api lấy toàn bộ bản ghi trong một bảng
        /// </summary>
        /// <returns>danh sách toàn bộ bản ghi</returns>
        /// created by: vinhkt(30/09/2022)
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                var records = _baseBL.GetAllRecords();

                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                AMISErrorCode.Exception,
                "dev message",
                "user message",
                "more info",
                "traceid"));
            }
        }

        /// <summary>
        /// thêm mới một bản ghi vào bảng
        /// </summary>
        /// <param name="record"></param>
        /// <returns>id của bản ghi mới trong bảng</returns>
        [HttpPost()]
        public IActionResult InsertRecord([FromBody] T record)
        {

            try
            {
                var result = _baseBL.InsertRecord(record);
                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Data);

                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                       AMISErrorCode.InvalidInput,
                       Resource.DevMsg_ValidateFailed,
                       Resource.UserMsg_ValidateFailed,
                       result.Data,
                       HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        AMISErrorCode.Exception,
                        Resource.DevMsg_InsertFailed,
                        Resource.UserMsg_InsertFailed,
                        ex.Message,
                        HttpContext.TraceIdentifier));

            }
        }

        /// <summary>
        /// sửa một bản ghi vào bảng
        /// </summary>
        /// <param name="record"></param>
        /// <returns>số bản ghi bị thay đổi trong bảng</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateRecord([FromRoute] Guid id, [FromBody] T record)
        {

            try
            {
                var result = _baseBL.UpdateRecord(id, record);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);

                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                       AMISErrorCode.InvalidInput,
                       Resource.DevMsg_ValidateFailed,
                       Resource.UserMsg_ValidateFailed,
                       result.Data,
                       HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        AMISErrorCode.Exception,
                        Resource.DevMsg_UpdateFailed,
                        Resource.UserMsg_UpdateFailed,
                        ex.Message,
                        HttpContext.TraceIdentifier));

            }
        }

        /// <summary>
        /// xoá một bản ghi trong bảng
        /// </summary>
        /// <param name="id"></param>
        /// created by vinhkt(30/09/2022)
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRecordById([FromRoute] Guid id)
        {
            try
            {
                var result = _baseBL.DeleteRecordById(id);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                {
                    var error = new ErrorResult(
                       AMISErrorCode.InvalidInput,
                       Resource.DevMsg_ValidateFailed,
                       Resource.UserMsg_ValidateFailed,
                       result.Data,
                       HttpContext.TraceIdentifier);
                        
                    return StatusCode(StatusCodes.Status400BadRequest, error);
                }
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
        /// lấy chi tiết 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>record</returns>
        /// created by: vinhkt(30/09/2022)
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById([FromRoute] Guid id)
        {
            try
            {
                var result = _baseBL.GetRecordById(id);
                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                {
                    var error = new ErrorResult(
                       AMISErrorCode.InvalidInput,
                       Resource.DevMsg_CannotFind,
                       Resource.UserMsg_CannotFind,
                       result.Data,
                       HttpContext.TraceIdentifier);

                    return StatusCode(StatusCodes.Status400BadRequest, error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        AMISErrorCode.Exception,
                        Resource.DevMsg_CannotFind,
                        Resource.UserMsg_CannotFind,
                        ex.Message,
                        HttpContext.TraceIdentifier));

            }
        }


        #endregion
    }
}
