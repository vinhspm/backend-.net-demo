using ClosedXML.Excel;
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
        private IRequestDetailBL _requestDetailBL;

        #endregion

        #region Constructor

        public RequestBL(IRequestDL requestDL, IEmployeeDL employeeDL, IDepartmentBL departmentBL, IPositionDL positionBL, IRequestDetailBL requestDetailBL) : base(requestDL)
        {
            _requestDL = requestDL;
            _employeeDL = employeeDL;
            _departmentBL = departmentBL;
            _positionBL = positionBL;
            _requestDetailBL = requestDetailBL;
        }

        #endregion
        public MemoryStream ExportAllRequestsFilter(string? requestFilter, RequestStatus status, Guid? departmentId)
        {
            var result = _requestDL.GetRequestsFilter(100000, 1, requestFilter, status, departmentId);
            List<Request> requests = (List<Request>)result["PageData"];
            var departments = _departmentBL.GetAllRecords().ToList();
            var positions = _positionBL.GetAllRecords().ToList();
            DataTable dt = new DataTable("Grid");

            // tạo header cho file excel
            foreach (PropertyInfo prop in typeof(Request).GetProperties())
            {
                var showInSheetAttribute = (ShowInSheetAttribute?)Attribute.GetCustomAttribute(prop, typeof(ShowInSheetAttribute));
                if (showInSheetAttribute != null)
                {
                    var column = new DataColumn(Request.TranslatePropName()[prop.Name]);
                    dt.Columns.Add(column);
                }
            }
            // add data vào file excel
            foreach (var emp in requests)
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
                            if (prop.Name == nameof(Request.Status))
                            {
                                if (fieldValue.ToString() == (RequestStatus.Approved).ToString())
                                {
                                    fieldValue = Resource.Status_Approve_VN;
                                }
                                if (fieldValue.ToString() == (RequestStatus.Waiting).ToString())
                                {
                                    fieldValue = Resource.Status_Waiting_VN;
                                }
                                if (fieldValue.ToString() == (RequestStatus.Denined).ToString())
                                {
                                    fieldValue = Resource.Status_Denined_VN;
                                }

                            }
                            else if (fieldValue.GetType() == typeof(DateTime))
                            {
                                fieldValue = DateTime.Parse(fieldValue.ToString()).ToString("dd/MM/yyyy HH:mm");
                            }
                            else if (prop.Name == nameof(Department.DepartmentId))
                            {
                                fieldValue = departments.Find(dpm => dpm.DepartmentId == emp.DepartmentId).DepartmentName;
                            }
                            else if (prop.Name == nameof(Position.PositionId))
                            {
                                fieldValue = positions.Find(pst => pst.PositionId == emp.PositionId).PositionName;
                            }
                            else if(prop.Name == nameof(Request.OverTimeInWorkingShift))
                            {
                                fieldValue = Request.TranslateOverTimeShiftValue((WorkTime)fieldValue);
                            }
                            else if (prop.Name == nameof(Request.WorkingShift))
                            {
                                fieldValue = Request.TranslateWorkShiftValue((WorkShifts)fieldValue);
                            }
                        }


                        row[Request.TranslatePropName()[prop.Name]] = fieldValue;
                    }
                }
                dt.Rows.Add(row);

            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                var xlWorkSheet = wb.Worksheets.Add(dt);
                var widthProps = RequestSheetProperties.Width;
                foreach (KeyValuePair<string, int> entry in RequestSheetProperties.Width)
                {
                    xlWorkSheet.Column(entry.Key).Width = entry.Value;
                }
                foreach (KeyValuePair<string, XLAlignmentHorizontalValues> entry in RequestSheetProperties.Align)
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

        /// <summary>
        /// hàm thêm thông tin vào bảng detail
        /// </summary>
        /// <param name="record"></param>
        /// <param name="recordId"></param>
        public override void InsertDetailData(Request record, Guid requestId) {
            var employees = record.Employees;
            _requestDetailBL.DeleteRecordByOverTimeId(requestId);
            foreach(RequestDetail employee in employees)
            {
                employee.RequestId = requestId;
                _requestDetailBL.InsertRecord(employee);
            }
        }

        public override void GetDetailData(Request request)
        {
            var employees = _requestDetailBL.GetAllRecordById((Guid) request.OverTimeId);
            request.Employees =(List<RequestDetail>) employees;
        }
        public PagingData GetRequestsFilter(int pageSize, int pageNumber, string requestFilter, RequestStatus requestStatus, Guid? departmentId)
        {
            try
            {
                // gọi đến dl để query vào db
                var result = _requestDL.GetRequestsFilter(pageSize, pageNumber, requestFilter, requestStatus, departmentId);
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
            int affectedRecords = _requestDL.MultipleDelete(guids);

            return new ServiceResponse(true, new MultipleQueriesResult(affectedRecords, guids.Count - affectedRecords));
        }

        public ServiceResponse MultipleApprove(List<Guid> guids)
        {
            int affectedRecords = _requestDL.MultipleChangeStatus(guids, RequestStatus.Approved);

            return new ServiceResponse(true, new MultipleQueriesResult(affectedRecords, guids.Count - affectedRecords));
        }

        public ServiceResponse MultipleDenine(List<Guid> guids)
        {
            int affectedRecords = _requestDL.MultipleChangeStatus(guids, RequestStatus.Denined);

            return new ServiceResponse(true, new MultipleQueriesResult(affectedRecords, guids.Count - affectedRecords));
        }
    }
}
