using MISA.Web08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {

        /// <summary>
        /// lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// created: vinhkt(30/09/2022)
        /// <returns>danh sách nhân viên theo filter và phân trang</returns>
        public Dictionary<string,object> GetEmployeesFilter(int v_Offset, int v_Limit, string v_Where);

        /// <summary>
        /// lấy mã nhân viên lớn nhất
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>mã nhân viên lớn nhất</returns>
        public string GetMaxEmployeeCode();

        /// <summary>
        /// xoá nhiều nhân viên trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các nhân viên cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public int MultipleDelete(List<Guid> guids);

        /// <summary>
        /// xuất file excel các nhân viên theo filter
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>file excel cần download</returns>
        public List<Employee> ExportAllEmployeesFilter(string v_Where);
    }
}
