using ClosedXML.Excel;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Resources;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// lấy danh sách nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// created by: vinhkt(30/09/2022)
        /// <returns>PagingData</returns>
        public PagingData GetEmployeesFilter(int pageSize, int pageNumber, string employeeFilter)
        {
            // tính offset, gán giá trị cho string v_where
            int offset = pageSize * (pageNumber - 1);
            string v_Where = "";
            if (employeeFilter != null)
            {
                v_Where = $"EmployeeCode LIKE \"%{employeeFilter}%\" OR FullName LIKE \"%{employeeFilter}%\"";
            }

            // gọi đến dl để query vào db
            var result = _employeeDL.GetEmployeesFilter(offset, pageSize, v_Where);

            var totalRecord = result["Total"];
            int isAdditionalLastPage = Convert.ToInt32(totalRecord) % Convert.ToInt32(pageSize);
            if (isAdditionalLastPage > 0)
            {
                isAdditionalLastPage = 1;
            }
            var totalPage = Convert.ToInt32(totalRecord) / Convert.ToInt32(pageSize) + isAdditionalLastPage;
            var resultArr = (List<Employee>)result["PageData"];
            var currentPageRecords = resultArr.Count;
            return new PagingData(
                result["PageData"], Convert.ToInt32(totalRecord), totalPage, pageNumber, currentPageRecords);
        }

        /// <summary>
        /// lấy mã nhân viên mới
        /// created by: vinhkt(30/09/2022)
        /// </summary>
        /// <returns>string newEmployeeCode</returns>
        public string GetNewEmployeeCode()
        {
            string maxEmployeeCode = _employeeDL.GetMaxEmployeeCode();
            int newEmployeeCodeNumber = Int32.Parse(maxEmployeeCode.Substring(2, maxEmployeeCode.Length - 2));
            newEmployeeCodeNumber += 1;
            string newEmployeeCodePrefix = Resource.New_EmployeeCode_Prefix;
            string newEmployeeCode = newEmployeeCodePrefix + newEmployeeCodeNumber.ToString();
            System.Diagnostics.Debug.WriteLine(newEmployeeCode);

            return newEmployeeCode;
        }

        /// <summary>
        /// hàm xoá nhiều nhân viên
        /// created by: vinhkt(30/09/2022)
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        public ServiceResponse MultipleDelete(List<Guid> guids)
        {
            int affectedRecords = _employeeDL.MultipleDelete(guids);

            return new ServiceResponse(true, new MultipleDeleteResult(affectedRecords, guids.Count - affectedRecords));

        }

        /// <summary>
        /// xuất file excel tất cả nhân viên theo filter
        /// </summary>
        /// <param name="employeeFilter">string tìm kiếm nhân viên theo mã, tên</param>
        /// <returns></returns>
        public MemoryStream ExportAllEmployeesFilter(string employeeFilter)
        {
            //List<Department> departments = _baseDL.GetAllRecords<Department>();
            string v_Where = "";
            if (employeeFilter != null)
            {
                v_Where = $"EmployeeCode LIKE \"%{employeeFilter}%\" OR FullName LIKE \"%{employeeFilter}%\"";
            }
            List<Employee> employees = _employeeDL.ExportAllEmployeesFilter(v_Where);
            DataTable dt = new DataTable("Grid");
            Employee e = new Employee();
            foreach (PropertyInfo prop in e.GetType().GetProperties())
            {
                var showInSheetAttribute = (ShowInSheetAttribute?)Attribute.GetCustomAttribute(prop, typeof(ShowInSheetAttribute));
                if (showInSheetAttribute != null)
                {
                    dt.Columns.Add(new DataColumn(Employee.TranslatePropName()[prop.Name]));
                }
            }
            foreach (var emp in employees)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo prop in emp.GetType().GetProperties())
                {
                    var showInSheetAttribute = (ShowInSheetAttribute?)Attribute.GetCustomAttribute(prop, typeof(ShowInSheetAttribute));
                    if (showInSheetAttribute != null)
                    {
                        row[Employee.TranslatePropName()[prop.Name]] = prop.GetValue(emp);
                    }
                }
                dt.Rows.Add(row);
                
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return stream;
                }
            }
        }

    }

    #endregion
}
