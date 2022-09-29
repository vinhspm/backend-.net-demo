using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.BL;
using MISA.Web08.AMIS.Common.Entities;

namespace MISA.Web08.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        
        #region Field

		private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion

        #region Method

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employees = _employeeBL.GetAllEmployees();

                return StatusCode(StatusCodes.Status200OK, employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpGet]
        [Route("filter")]
        public IActionResult GetEmployeesFilter([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string employeeFilter)
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

        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] Guid employeeId)
        {
            try
            {
                var employee = _employeeBL.GetEmployeeById(employeeId);

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost()]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {

            try
            {
                var result = _employeeBL.InsertEmployee(employee);

                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeId, [FromBody] Employee employee)
        {

            try
            {
                var result = _employeeBL.UpdateEmployee(employeeId, employee);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployeeById([FromRoute] Guid employeeId)
        {
            try
            {
                var employee = _employeeBL.DeleteEmployeeById(employeeId);

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

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
