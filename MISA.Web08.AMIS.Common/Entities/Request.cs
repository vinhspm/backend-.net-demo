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
        [ShowInSheetAttribute]
        public string? EmployeeCode { get; set; }

        // tên nhân viên
        [ShowInSheetAttribute]
        public string? FullName { get; set; }

        //id đơn vị
        [ShowInSheetAttribute]
        public Guid? DepartmentId { get; set; }

        //id vị trí
        [ShowInSheetAttribute]
        public Guid? PositionId { get; set; }

        // ngày nộp đơn
        [ShowInSheetAttribute]
        public DateTime? ApplyDate { get; set; }

        // làm thêm từ
        [ShowInSheetAttribute]
        public DateTime? FromDate { get; set; }

        // làm thêm đến
        [ShowInSheetAttribute]
        public DateTime? ToDate { get; set; }

        // nghỉ giữa ca từ
        public DateTime? BreakTimeFrom { get; set; }

        // nghỉ giữa ca đến
        public DateTime? BreakTimeTo { get; set; }

        // thời điểm làm thêm
        [ShowInSheetAttribute]
        public WorkTime? OverTimeInWorkingShift { get; set; }

        //lý do làm thêm
        [ShowInSheetAttribute]
        public string? Reason { get; set; }

        // id người duyệt
        public Guid? ApprovalToId { get; set; }

        // tên người duyệt
        [ShowInSheetAttribute]
        public string? ApprovalToName { get; set; }

        // trạng thái đơn
        [ShowInSheetAttribute]
        public RequestStatus? Status { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// hàm dịch các prop name thành tiếng việt
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> TranslatePropName()
        {
            return new Dictionary<string, string> {
                {"EmployeeCode", "Mã nhân viên" },
                {"FullName", "Tên nhân viên" },
                {"DepartmentId", "Đơn vị" },
                {"PositionId", "Vị trí" },
                {"ApplyDate", "Ngày nộp đơn" },
                {"FromDate", "Làm thêm từ"},
                {"ToDate", "Làm thêm đến"},
                {"OverTimeInWorkingShift", "Thời điểm làm thêm"},
                {"", "Ca làm thêm"},
                {"Reason", "Lý do làm thêm"},
                {"ApprovalToName", "Người duyệt"},
                {"Note", "Ghi chú"},
                {"Status", "Trạng thái"},

            };

        }
        #endregion

    }
}
