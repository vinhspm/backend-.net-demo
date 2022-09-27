using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.BL;

namespace MISA.Web08.AMIS.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        [Route("")]
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

        [HttpGet("{employeeId}")]
        [Route("")]
        public IActionResult GetEmployeeById([FromRoute] Guid employeeId)
        {
            try
            {
                var employees = _employeeBL.GetEmployeeById();

                return StatusCode(StatusCodes.Status200OK, employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
    }
}
