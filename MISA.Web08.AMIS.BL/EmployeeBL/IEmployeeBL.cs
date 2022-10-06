using ClosedXML.Excel;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public interface IEmployeeBL: IBaseBL<Employee>
    {
        /// <summary>
        /// lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// created: vinhkt(30/09/2022)
        /// <returns>danh sách nhân viên theo filter và phân trang</returns>
        public PagingData GetEmployeesFilter(int pageSize, int pageNumber, string employeeFilter);

        /// <summary>
        /// lấy mã nhân viên mới
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>mã nhân viên mới</returns>
        public string GetNewEmployeeCode();

        /// <summary>
        /// xoá nhiều nhân viên trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các nhân viên cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public ServiceResponse MultipleDelete(List<Guid> guids);

        /// <summary>
        /// xuất file excel các nhân viên theo filter
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>file excel cần download</returns>
        public MemoryStream ExportAllEmployeesFilter(string employeeFilter);
    }
}
