using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Enums;
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
        private IDepartmentBL _departmentBL;
        private IPositionDL _positionBL;


        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL, IDepartmentBL departmentBL, IPositionDL positionBL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
            _departmentBL = departmentBL;
            _positionBL = positionBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// created: vinhkt(30/09/2022)
        /// <returns>danh sách nhân viên theo filter và phân trang</returns>
        public PagingData GetEmployeesFilter(int pageSize, int pageNumber, string employeeFilter)
        {
            ServiceResponse res = ValidateSqlInject(employeeFilter);
            if(!res.Success)
            {
                return new PagingData(new List<Employee>(), 0, 0, 0, 0);
            }
            try
            {

                // gọi đến dl để query vào db
                var result = _employeeDL.GetEmployeesFilter(pageSize, pageNumber, employeeFilter);

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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// lấy mã nhân viên mới
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>mã nhân viên mới</returns>
        public string GetNewEmployeeCode()
        {
            try
            {
                string maxEmployeeCode = _employeeDL.GetMaxEmployeeCode();
                int newEmployeeCodeNumber = Int32.Parse(maxEmployeeCode.Substring(2, maxEmployeeCode.Length - 2));
                newEmployeeCodeNumber += 1;
                string newEmployeeCodePrefix = Resource.New_EmployeeCode_Prefix;
                string newEmployeeCode = newEmployeeCodePrefix + newEmployeeCodeNumber.ToString();
                System.Diagnostics.Debug.WriteLine(newEmployeeCode);

                return newEmployeeCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// xoá nhiều nhân viên trong bảng
        /// </summary>
        /// <param name="ids">mảng các id của các nhân viên cần xoá</param>
        /// created by vinhkt(30/09/2022)
        /// <returns>số bản ghi được xoá thành công, số bản ghi xoá thất bại</returns>
        public ServiceResponse MultipleDelete(List<Guid> guids)
        {
            int affectedRecords = _employeeDL.MultipleDelete(guids);

            return new ServiceResponse(true, new MultipleDeleteResult(affectedRecords, guids.Count - affectedRecords));

        }

        /// <summary>
        /// xuất file excel các nhân viên theo filter
        /// </summary>
        /// created: vinhkt(30/09/2022)
        /// <returns>file excel cần download</returns>
        public MemoryStream ExportAllEmployeesFilter(string employeeFilter)
        {
            ServiceResponse res = ValidateSqlInject(employeeFilter);
            if (!res.Success)
            {
                DataTable dtFake = new DataTable("Grid");
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var xlWorkSheet = wb.Worksheets.Add(dtFake);
                    var widthProps = EmployeeSheetProperties.Width;
                    foreach (KeyValuePair<string, int> entry in EmployeeSheetProperties.Width)
                    {
                        xlWorkSheet.Column(entry.Key).Width = entry.Value;
                    }
                    foreach (KeyValuePair<string, XLAlignmentHorizontalValues> entry in EmployeeSheetProperties.Align)
                    {
                        xlWorkSheet.Column(entry.Key).Style.Alignment.SetHorizontal(entry.Value);
                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream;
                    }
                }
            }
            List<Employee> employees = _employeeDL.ExportAllEmployeesFilter(employeeFilter);
            var departments = _departmentBL.GetAllRecords().ToList();
            var positions = _positionBL.GetAllRecords().ToList();
            DataTable dt = new DataTable("Grid");

            // tạo header cho file excel
            foreach (PropertyInfo prop in typeof(Employee).GetProperties())
            {
                var showInSheetAttribute = (ShowInSheetAttribute?)Attribute.GetCustomAttribute(prop, typeof(ShowInSheetAttribute));
                if (showInSheetAttribute != null)
                {
                    var column = new DataColumn(Employee.TranslatePropName()[prop.Name]);
                    dt.Columns.Add(column);
                }
            }
            // add data vào file excel
            foreach (var emp in employees)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo prop in emp.GetType().GetProperties())
                {
                    var showInSheetAttribute = (ShowInSheetAttribute?)Attribute.GetCustomAttribute(prop, typeof(ShowInSheetAttribute));
                    if (showInSheetAttribute != null)
                    {

                        var fieldName = prop.Name;
                        var fieldValue = prop.GetValue(emp);

                        //format data các loại cho file excel
                        if (fieldValue != null)
                        {
                            if (prop.Name == nameof(Employee.Gender))
                            {
                                if (fieldValue.ToString() == (Gender.Male).ToString())
                                {
                                    fieldValue = Resource.Gender_Male_VN;
                                }
                                if (fieldValue.ToString() == (Gender.Female).ToString())
                                {
                                    fieldValue = Resource.Gender_Female_VN;
                                }
                                if (fieldValue.ToString() == (Gender.Other).ToString())
                                {
                                    fieldValue = Resource.Gender_Other_VN;
                                }
                            }
                            else if (fieldValue.GetType() == typeof(DateTime))
                            {
                                fieldValue = DateTime.Parse(fieldValue.ToString()).ToString("dd/MM/yyyy");
                            }
                            else if (prop.Name == nameof(Department.DepartmentId))
                            {
                                fieldValue = departments.Find(dpm => dpm.DepartmentId == emp.DepartmentId).DepartmentName;
                            }
                            else if (prop.Name == nameof(Position.PositionId))
                            {
                                fieldValue = positions.Find(pst => pst.PositionId == emp.PositionId).PositionName;
                            }
                        }


                        row[Employee.TranslatePropName()[prop.Name]] = fieldValue;
                    }
                }
                dt.Rows.Add(row);

            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                var xlWorkSheet = wb.Worksheets.Add(dt);
                var widthProps = EmployeeSheetProperties.Width;
                foreach (KeyValuePair<string, int> entry in EmployeeSheetProperties.Width)
                {
                    xlWorkSheet.Column(entry.Key).Width = entry.Value;
                }
                foreach (KeyValuePair<string, XLAlignmentHorizontalValues> entry in EmployeeSheetProperties.Align)
                {
                    xlWorkSheet.Column(entry.Key).Style.Alignment.SetHorizontal(entry.Value);
                }

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
