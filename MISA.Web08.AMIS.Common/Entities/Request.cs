using MISA.Web08.AMIS.Common.Enums;
using MISA.Web08.AMIS.Common.Resources;
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
        [ShowInSheetAttribute, NotColumn]
        public string? EmployeeCode { get; set; }

        // tên nhân viên
        [ShowInSheetAttribute, NotColumn]
        public string? FullName { get; set; }

        //id đơn vị
        [ShowInSheetAttribute, NotColumn]
        public Guid? DepartmentId { get; set; }

        // tên đơn vị
        [NotColumn]
        public string? DepartmentName { get; set; }

        //id vị trí
        [ShowInSheetAttribute, NotColumn]
        public Guid? PositionId { get; set; }

        [NotColumn]
        public string? PositionName { get; set; }

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

        [ShowInSheet]
        public WorkShifts? WorkingShift { get; set; }

        //lý do làm thêm
        [ShowInSheetAttribute]
        public string? Reason { get; set; }

        // id người duyệt
        public Guid? ApprovalToId { get; set; }

        // tên người duyệt
        [ShowInSheetAttribute, NotColumn]
        public string? ApprovalToName { get; set; }

        // trạng thái đơn
        [ShowInSheetAttribute]
        public RequestStatus? Status { get; set; }

        [NotColumn]
        public List<RequestDetail>? Employees { get; set; }

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
                {"WorkingShift", "Ca làm thêm"},
                {"Reason", "Lý do làm thêm"},
                {"ApprovalToName", "Người duyệt"},
                {"Note", "Ghi chú"},
                {"Status", "Trạng thái"},

            };

        }

        public static string TranslateOverTimeShiftValue(WorkTime workTime )
        {
            if(workTime.ToString() == (WorkTime.AfterShift).ToString())
            {
                return Resource.WorkTime_AfterShift_VN;
            }
            else if(workTime.ToString() == (WorkTime.BeforeShift).ToString())
            {
                return Resource.WorkTime_BeforeShift_VN;
            }
            else if(workTime.ToString() == (WorkTime.ShiftBreak).ToString())
            {
                return Resource.WorkTime_ShiftBreak_VN;
            } else if(workTime.ToString() == (WorkTime.WorkOffDay).ToString())
            {
                return Resource.WorkTime_WorkOffDay_VN; ;
            }
            return null;
        }

        public static string TranslateWorkShiftValue(WorkShifts workShift)
        {
            if (workShift.ToString() == WorkShifts.OneTimeShift.ToString())
            {
                return Resource.WorkShift_OneTimeShift_VN;
            }
            return null;
        }

        #endregion

    }
}
