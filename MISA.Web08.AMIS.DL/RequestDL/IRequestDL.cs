using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.DL
{
    public interface IRequestDL: IBaseDL<Request>
    {
        #region Method

        /// <summary>
        /// lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="requestFilter"></param>
        /// created: vinhkt(30/09/2022)
        /// <returns>danh sách nhân viên theo filter và phân trang</returns>
        public Dictionary<string, object> GetRequestsFilter(int pageSize, int pageNumber, string requestFilter, RequestStatus requestStatus, Guid? departmentId);

        /// <summary>
        /// xoá nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các yêu cầu cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public int MultipleDelete(List<Guid> guids);

        /// <summary>
        /// duyệt, từ chối nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các yêu cầu cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public int MultipleChangeStatus(List<Guid> guids, RequestStatus status);

        /// <summary>
        /// xuất file excel các nhân viên theo filter
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>file excel cần download</returns>
        public List<Employee> ExportAllRequestsFilter(string requestFilter);

        #endregion
    }
}
