using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MISA.Web08.AMIS.Common.Entities
{
    public class Employee : BaseEntity
    {
        // id nhân viên
        [PrimaryKey]
        public Guid? EmployeeId { get; set; }

        //mã nhân viên
        [NotEmpty, NotDuplicate, ShowInSheetAttribute]
        public string? EmployeeCode { get; set; }

        // tên nhân viên
        [NotEmpty, ShowInSheetAttribute]
        public string? FullName { get; set; }

        // giới tính
        [ShowInSheetAttribute]
        public Gender? Gender { get; set; }

        // ngày sinh
        [ShowInSheetAttribute]
        public DateTime? DateOfBirth { get; set; }

        // số điện thoại
        public string? PhoneNumber { get; set; }

        // email
        [Email]
        public string? Email { get; set; }

        // địa chỉ
        public string? Address { get; set; }

        // số cmnd
        [ShowInSheetAttribute]
        public string? IdentityNumber { get; set; }

        // ngày cấp cmnd
        [ShowInSheetAttribute]
        public DateTime? IdentityDate { get; set; }

        // nơi cấp cmnd
        public string? IdentityPlace { get; set; }

        //id phòng ban
        [NotEmpty, ShowInSheetAttribute]
        public Guid? DepartmentId { get; set; }

        // id vị trí
        [ShowInSheetAttribute]
        public Guid? PositionId { get; set; }

        // số tài khoản ngân hàng
        [ShowInSheetAttribute]
        public string? BankAccount { get; set; }

        // chi nhánh ngân hàng
        [ShowInSheetAttribute]
        public string? BankBranch { get; set; }

        // tên ngân hàng
        [ShowInSheetAttribute]
        public string? BankName { get; set; }
        
        // số điện thoại cố định
        public string? HomePhoneNumber { get; set; }

        public static Dictionary<string, string> TranslatePropName()
        {
            return new Dictionary<string, string> {
                {"EmployeeCode", "Mã nhân viên" },
                {"FullName", "Tên nhân viên" },
                {"Gender", "Giới tính" },
                {"DateOfBirth", "Ngày sinh" },
                {"IdentityNumber", "Số CMND" },
                {"IdentityDate", "Ngày cấp CMND" },
                {"DepartmentId", "Đơn vị" },
                {"PositionId", "Vị trí" },
                {"BankAccount", "Số tài khoản" },
                {"BankBranch", "Chi nhánh TK ngân hàng" },
                {"BankName", "Tên ngân hàng" },
            };
            
        }

    }
}
