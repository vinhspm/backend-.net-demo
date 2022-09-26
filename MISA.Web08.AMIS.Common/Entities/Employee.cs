using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Enums;

namespace MISA.Web08.AMIS.Common.Entities
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }


    }
}
