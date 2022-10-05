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
        /// lấy danh sách nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// created by: vinhkt(30/09/2022)
        /// <returns>PagingData</returns>
        public PagingData GetEmployeesFilter(int pageSize, int pageNumber, string employeeFilter);

        /// <summary>
        /// lấy mã nhân viên mới
        /// created by: vinhkt(30/09/2022)
        /// </summary>
        /// <returns>string newEmployeeCode</returns>
        public string GetNewEmployeeCode();

        /// <summary>
        /// hàm xoá nhiều nhân viên
        /// created by: vinhkt(30/09/2022)
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        public ServiceResponse MultipleDelete(List<Guid> guids);

        /// <summary>
        /// xuất file excel tất cả nhân viên theo filter
        /// </summary>
        /// <param name="employeeFilter">string tìm kiếm nhân viên theo mã, tên</param>
        /// <returns></returns>
        public MemoryStream ExportAllEmployeesFilter(string employeeFilter);
    }
}
