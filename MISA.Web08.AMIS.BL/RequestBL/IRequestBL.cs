using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public interface IRequestBL : IBaseBL<Request>
    {
        #region Method
        /// <summary>
        /// lấy thông tin yêu cầu theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="requestFilter"></param>
        /// created: vinhkt(30/09/2022)
        /// <returns>danh sách yêu cầu theo filter và phân trang</returns>
        public PagingData GetRequestsFilter(int pageSize, int pageNumber, string requestFilter, RequestStatus requestStatus, Guid? departmentId);

        /// <summary>
        /// xoá nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các yêu cầu cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public ServiceResponse MultipleDelete(List<Guid> guids);

        /// <summary>
        /// duyệt nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các yêu cầu cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public ServiceResponse MultipleApprove(List<Guid> guids);

        /// <summary>
        /// từ chối nhiều yêu cầu trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các yêu cầu cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public ServiceResponse MultipleDenine(List<Guid> guids);

        /// <summary>
        /// xuất file excel các yêu cầu theo filter
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>file excel cần download</returns>
        public MemoryStream ExportAllRequestsFilter(string? requestFilter, RequestStatus status, Guid? departmentId);

        #endregion
    }
}
