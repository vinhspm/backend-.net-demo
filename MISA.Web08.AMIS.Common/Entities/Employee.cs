using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MISA.Web08.AMIS.Common.Entities
{
    public class Employee : BaseEntity
    {
        // id nhân viên
        [PrimaryKey]
        public Guid? EmployeeID { get; set; }

        //mã nhân viên
        [NotEmpty, NotDuplicate]
        public string? EmployeeCode { get; set; }

        // tên nhân viên
        [NotEmpty]
        public string? FullName { get; set; }

        // giới tính
        public Gender? Gender { get; set; }
        
        // ngày sinh
        public DateTime? DateOfBirth { get; set; }

        // số điện thoại
        public string? PhoneNumber { get; set; }

        // email
        [EmailAddress]
        public string? Email { get; set; }

        // địa chỉ
        public string? Address { get; set; }

        // số cmnd
        public string? IdentityNumber { get; set; }

        // ngày cấp cmnd
        public DateTime? IdentityDate { get; set; }

        // nơi cấp cmnd
        public string? IdentityPlace { get; set; }

        //id phòng ban
        [MustExisted]
        public Guid DepartmentId { get; set; }

        // id vị trí
        [MustExisted]
        public Guid? PositionId { get; set; }

        // số tài khoản ngân hàng
        public string? BankAccount { get; set; }
        
        // chi nhánh ngân hàng
        public string? BankBranch { get; set; }
        
        // tên ngân hàng
        public string? BankName { get; set; }
        
        // số điện thoại cố định
        public string? HomePhoneNumber { get; set; }

    }
}
