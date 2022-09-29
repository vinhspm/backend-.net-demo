using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MISA.Web08.AMIS.Common.Entities
{
    public class Employee
    {
        [Key]
        public Guid EmployeeID { get; set; }

        [Required(ErrorMessage = "e004")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "e005")]
        public string FullName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public Gender? Gender { get; set; }
        
        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string IdentityNumber { get; set; }

        public DateTime? IdentityDate { get; set; }

        public string IdentityPlace { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid? PositionId { get; set; }

        public string BankAccount { get; set; }
        
        public string BankBranch { get; set; }
        
        public string BankName { get; set; }
        
        public string HomePhoneNumber { get; set; }

    }
}
