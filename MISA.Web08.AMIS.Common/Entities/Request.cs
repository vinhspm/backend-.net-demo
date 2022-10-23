using MISA.Web08.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.Common.Entities
{
    public class Request : BaseEntity
    {
        #region Field
        // id request
        [PrimaryKey]
        public Guid? OverTimeId { get; set; }

        //id nhân viên
        [NotEmpty]
        public Guid? EmployeeId { get; set; }

        // mã nhân viên
        public string? EmployeeCode { get; set; }

        // tên nhân viên
        public string? FullName { get; set; }

        //id đơn vị
        public Guid? DepartmentId { get; set; }

        //id vị trí
        public Guid? PositionId { get; set; }

        // id người duyệt
        public Guid? ApprovalToId { get; set; }

        // tên người duyệt
        public string? ApprovalToName { get; set; }

        // trạng thái đơn
        public RequestStatus? Status { get; set; }

        // làm thêm từ
        public DateTime? FromDate { get; set; }

        // làm thêm đến
        public DateTime? ToDate { get; set; }

        // nghỉ giữa ca từ
        public DateTime? BreakTimeFrom { get; set; }

        // nghỉ giữa ca đến
        public DateTime? BreakTimeTo { get; set; }

        // ngày nộp đơn
        public DateTime? ApplyDate { get; set; }

        //lý do làm thêm
        public string? Reason { get; set; }

        // thời điểm làm thêm
        public WorkTime? OverTimeInWorkingShift { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// hàm dịch các prop name thành tiếng việt
        /// </summary>
        /// <returns></returns>
        //public static Dictionary<string, string> TranslatePropName()
        //{
        //    return new Dictionary<string, string> {
        //        {"EmployeeCode", "Mã nhân viên" },
        //        {"FullName", "Tên nhân viên" },
        //        {"Gender", "Giới tính" },
        //        {"DateOfBirth", "Ngày sinh" },
        //        {"IdentityNumber", "Số CMND" },
        //        {"IdentityDate", "Ngày cấp CMND" },
        //        {"DepartmentId", "Đơn vị" },
        //        {"PositionId", "Vị trí" },
        //        {"BankAccount", "Số tài khoản" },
        //        {"BankBranch", "Chi nhánh TK ngân hàng" },
        //        {"BankName", "Tên ngân hàng" },
        //    };

        //}
        #endregion

    }
}
