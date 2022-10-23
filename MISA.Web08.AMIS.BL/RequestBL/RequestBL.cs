using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public class RequestBL : BaseBL<Request>, IRequestBL
    {
        #region Field
        private IRequestDL _requestDL;
        private IEmployeeDL _employeeDL;
        private IDepartmentBL _departmentBL;
        private IPositionDL _positionBL;


        #endregion

        #region Constructor

        public RequestBL(IRequestDL requestDL, IEmployeeDL employeeDL, IDepartmentBL departmentBL, IPositionDL positionBL) : base(requestDL)
        {
            _requestDL = requestDL;
            _employeeDL = employeeDL;
            _departmentBL = departmentBL;
            _positionBL = positionBL;
        }

        #endregion
        public MemoryStream ExportAllEmployeesFilter(string requestFilter)
        {
            throw new NotImplementedException();
        }

        public PagingData GetRequestsFilter(int pageSize, int pageNumber, string requestFilter, RequestStatus requestStatus)
        {
            try
            {
                string employeeIdArray = "";
                //lấy danh sách nhân viên phù hợp với filter
                if (requestFilter != null)
                {
                    //List<Employee> employees = _employeeDL.ExportAllEmployeesFilter(requestFilter);

                    //// thêm id các nhân viên tìm được vào mảng để tìm trong db
                    //if(employees.Count > 0)
                    //{
                    //    Employee lastEmployee = employees.Last();
                    //    foreach (Employee employee in employees)
                    //    {
                    //        if (employee.Equals(lastEmployee))
                    //        {
                    //            employeeIdArray += $"'{employee.EmployeeId}'";
                    //        }
                    //        else
                    //        {
                    //            employeeIdArray += $"'{employee.EmployeeId}', ";

                    //        }

                    //    }
                    //} else
                    //{
                    //    return new PagingData(
                    //new List<Request>(), 0, 0, 0, 0);
                    //}

                }


                // gọi đến dl để query vào db
                var result = _requestDL.GetRequestsFilter(pageSize, pageNumber, requestFilter, requestStatus);
                Console.WriteLine(result);
                var totalRecord = result["Total"];
                int isAdditionalLastPage = Convert.ToInt32(totalRecord) % Convert.ToInt32(pageSize);
                if (isAdditionalLastPage > 0)
                {
                    isAdditionalLastPage = 1;
                }
                var totalPage = Convert.ToInt32(totalRecord) / Convert.ToInt32(pageSize) + isAdditionalLastPage;
                var resultArr = (List<Request>)result["PageData"];
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

        public ServiceResponse MultipleDelete(List<Guid> guids)
        {
            throw new NotImplementedException();
        }
    }
}
