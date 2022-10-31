using Microsoft.AspNetCore.Http;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Enums;
using MISA.Web08.AMIS.Common.Resources;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{

    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        protected IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// lấy tất cả bản ghi của một bảng
        /// </summary>
        /// created by: vinhkt(30/09/2022)
        /// <returns></returns>
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// lấy chi tiết 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>record</returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse GetRecordById(Guid id)
        {
            T record = _baseDL.GetRecordById(id);
            if (record == null)
            {
                return new ServiceResponse(false, null);
            }
            else
            {
                GetDetailData(record);
                return new ServiceResponse(true, record);
            }
        }

        /// <summary>
        /// hàm lấy thôgn tin detail
        /// </summary>
        /// <param name="record"></param>
        public virtual void GetDetailData(T record) { }

        /// <summary>
        /// xoá một bản ghi trong bảng
        /// </summary>
        /// <returns>số bản ghi bị ảnh hưởng ( 1 )</returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse DeleteRecordById(Guid id)
        {
            try
            {
                var res = _baseDL.DeleteRecord(id);

                if (res == QueryResult.Success)
                {
                    return new ServiceResponse(true, res);
                }
                else
                {
                    return new ServiceResponse(false, QueryResult.Fail);
                }
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, e.Message);
            }


        }

        /// <summary>
        /// thêm một bản ghi vào bảng
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ServiceResponse InsertRecord(T record)
        {
            
            var validateResponse = ValidateFormBody(record, Guid.NewGuid());
            if (!validateResponse.Success)
            {
                return validateResponse;
            }
            
            else
            {
                try
                {
                    var res = _baseDL.InsertRecord(record);
                    if (res.Success)
                    {
                        InsertDetailData(record,(Guid) res.Data);
                        return new ServiceResponse(true, res.Data);
                    }
                    else
                    {
                        return new ServiceResponse(false, new ErrorResult(
                                AMISErrorCode.InvalidInput,
                                Resource.DevMsg_ValidateFailed,
                                Resource.UserMsg_ValidateFailed,
                                "",
                                ""));
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return new ServiceResponse(false, ex.Message);
                }
            }

        }

        /// <summary>
        /// hàm thêm thông tin vào bảng detail
        /// </summary>
        /// <param name="record"></param>
        /// <param name="recordId"></param>
        public virtual void InsertDetailData(T record, Guid recordId) { }


        /// <summary>
        /// sửa một bản ghi trong bảng
        /// </summary>
        /// <returns>số bản ghi bị ảnh hưởng ( 1 )</returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse UpdateRecord(Guid id, T record)
        {
            var validateResponse = ValidateFormBody(record, id);
            if (!validateResponse.Success)
            {
                return validateResponse;
            }
            else
            {
                try
                {
                    var res = _baseDL.UpdateRecord(id, record);
                    if (res == QueryResult.Success)
                    {
                        InsertDetailData(record, id);
                        return new ServiceResponse(true, res);
                    }
                    else
                    {
                        return new ServiceResponse(false, new ErrorResult(
                                AMISErrorCode.InvalidInput,
                                Resource.DevMsg_ValidateFailed,
                                Resource.UserMsg_ValidateFailed,
                                "",
                                ""));
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return new ServiceResponse(false, ex.Message);
                }
            }
        }

        /// <summary>
        /// validate dữ liệu đầu vào theo attribute đã khai báo
        /// </summary>
        /// created: vinhkt(01/10/2022)
        /// <param name="record"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse ValidateFormBody(T record, Guid? id)
        {
            var primaryKeyFieldName = "";
            foreach (PropertyInfo prop in record.GetType().GetProperties())
            {
                var primaryKeyAttribute = (PrimaryKey?)Attribute.GetCustomAttribute(prop, typeof(PrimaryKey));

                // lấy fieldname của property đánh dấu là primarykey
                if (primaryKeyAttribute != null)
                {
                    primaryKeyFieldName = prop.Name;
                }

                // lấy giá trị của các attribute
                var notEmptyAttribute = (NotEmpty?)Attribute.GetCustomAttribute(prop, typeof(NotEmpty));
                var notDuplicateAttribute = (NotDuplicate?)Attribute.GetCustomAttribute(prop, typeof(NotDuplicate));
                var emailAddressAttribute = (Email?)Attribute.GetCustomAttribute(prop, typeof(Email));
                var fieldName = prop.Name;
                var fieldValue = prop.GetValue(record);

                // validate property not null/empty
                if (notEmptyAttribute != null)
                {
                    if (fieldValue == null || fieldValue.ToString() == "")
                    {
                        return new ServiceResponse(false, new ErrorResult(
                            AMISErrorCode.NotNullInput,
                            Resource.DevMsg_ValidateFailed,
                            Resource.UserMsg_ValidateFailed,
                            fieldName,
                            ""));
                    }
                }

                // validate property không duplicate
                if (notDuplicateAttribute != null)
                {
                    T findDuplicate = _baseDL.FindDuplicate(fieldName, fieldValue.ToString());
                    if (findDuplicate != null && id != null &&
                        (Guid)findDuplicate.GetType().GetProperty(primaryKeyFieldName).GetValue(findDuplicate, null) != id)
                    {
                        return new ServiceResponse(false, new ErrorResult(
                            AMISErrorCode.DuplicateInput,
                            Resource.DevMsg_ValidateFailed,
                            Resource.UserMsg_ValidateFailed,
                            fieldName,
                            ""));
                    }
                }

                // validate email
                if (emailAddressAttribute != null)
                {
                    if (fieldValue == null || fieldValue.ToString() == "")
                    {
                        return new ServiceResponse(true, null);
                    }
                    else
                    {
                        int index = fieldValue.ToString().IndexOf('@');
                        bool isValidate = index > 0 &&
                                        index != fieldValue.ToString().Length - 1 &&
                                        index == fieldValue.ToString().LastIndexOf('@');
                        if (!isValidate)
                        {
                            return new ServiceResponse(false, new ErrorResult(
                                AMISErrorCode.InvalidInput,
                                Resource.DevMsg_ValidateFailed,
                                Resource.UserMsg_ValidateFailed,
                                fieldName,
                                ""));
                        }
                    }


                }

            }
            return new ServiceResponse(true, null);
        }

        #endregion
    }

}
