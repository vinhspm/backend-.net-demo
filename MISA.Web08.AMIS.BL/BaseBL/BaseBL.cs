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
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{

    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

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
            if(record == null)
            {
                return new ServiceResponse(false, null);
            }
            else
            {
                return new ServiceResponse(true, record);
            }
        }

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
            if(!validateResponse.Success)
            {
                return validateResponse;
            } else
            {
                try
                {
                    var v_Columns = "";
                    var v_Values = "";
                    Guid recordId = Guid.NewGuid();
                    foreach (PropertyInfo prop in record.GetType().GetProperties())
                    {
                        var primaryKeyAttribute = (PrimaryKey?)Attribute.GetCustomAttribute(prop, typeof(PrimaryKey));
                        if (primaryKeyAttribute != null)
                        {
                            prop.SetValue(record, recordId, null);
                        }
                        var fieldName = prop.Name;
                        var fieldValue = prop.GetValue(record);
                        if (fieldValue != null)
                        {
                            if (fieldValue.GetType() == typeof(DateTime))
                            {
                                fieldValue = DateTime.Parse(fieldValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            if (fieldValue.GetType() == typeof(Gender))
                            {
                                fieldValue = (int)Enum.Parse(typeof(Gender), fieldValue.ToString());
                            }
                            if (v_Columns.Length > 0)
                            {
                                v_Columns = v_Columns + ", " + fieldName;
                            }
                            else
                            {
                                v_Columns = v_Columns + fieldName;
                            }
                            if (v_Values.Length > 0)
                            {
                                v_Values = v_Values + ", " + $"'{fieldValue}'";
                            }
                            else
                            {
                                v_Values = v_Values + $"'{fieldValue}'";
                            }

                        }

                    }
                    v_Columns = $"({v_Columns})";
                    v_Values = $"({v_Values})";
                    var res = _baseDL.InsertRecord(v_Columns, v_Values);
                    if (res == QueryResult.Success)
                    {
                        return new ServiceResponse(true, recordId);
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
                    var v_Query = "";
                    Guid recordId = id;
                    foreach (PropertyInfo prop in record.GetType().GetProperties())
                    {
                        var primaryKeyAttribute = (PrimaryKey?)Attribute.GetCustomAttribute(prop, typeof(PrimaryKey));
                        if (primaryKeyAttribute != null)
                        {
                            prop.SetValue(record, recordId, null);
                        }

                        var fieldName = prop.Name;
                        var fieldValue = prop.GetValue(record);
                        if (fieldValue != null)
                        {
                            if (fieldValue.GetType() == typeof(DateTime))
                            {
                                fieldValue = DateTime.Parse(fieldValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            if (fieldValue.GetType() == typeof(Gender))
                            {
                                fieldValue = (int)Enum.Parse(typeof(Gender), fieldValue.ToString());
                            }
                            var fieldUpdateString = fieldName + " = " + $"\"{fieldValue}\"";
                            if (v_Query.Length > 0)
                            {
                                v_Query += ", " + fieldUpdateString;
                            }
                            else
                            {
                                v_Query += fieldUpdateString;
                            }

                        }

                    }
                    var res = _baseDL.UpdateRecord(id, v_Query);
                    if (res == QueryResult.Success)
                    {
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
                
                if (primaryKeyAttribute != null)
                {
                    primaryKeyFieldName = prop.Name;
                }
                var notEmptyAttribute = (NotEmpty?)Attribute.GetCustomAttribute(prop, typeof(NotEmpty));
                var notDuplicateAttribute = (NotDuplicate?)Attribute.GetCustomAttribute(prop, typeof(NotDuplicate));
                var mustExistAttribute = (MustExisted?)Attribute.GetCustomAttribute(prop, typeof(MustExisted));
                var emailAddressAttribute = (Email?)Attribute.GetCustomAttribute(prop, typeof(Email));
                var fieldName = prop.Name;
                var fieldValue = prop.GetValue(record);

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

                if (notDuplicateAttribute != null)
                {
                    T findDuplicate = _baseDL.FindDuplicate(fieldName, fieldValue.ToString());
                    if(findDuplicate != null && id != null &&
                        (Guid) findDuplicate.GetType().GetProperty(primaryKeyFieldName).GetValue(findDuplicate, null) != id)
                    {
                        return new ServiceResponse(false, new ErrorResult(
                            AMISErrorCode.DuplicateInput,
                            Resource.DevMsg_ValidateFailed,
                            Resource.UserMsg_ValidateFailed,
                            fieldName,
                            ""));
                    }
                }
                
                //if (mustExistAttribute != null)
                //{
                //    T findDuplicate = _baseDL.FindDuplicate(fieldName, fieldValue.ToString());
                //    if (findDuplicate == null)
                //    {
                //        return new ServiceResponse(false, new ErrorResult(
                //            AMISErrorCode.MustExistInput,
                //            Resource.DevMsg_ValidateFailed,
                //            Resource.UserMsg_ValidateFailed,
                //            fieldName,
                //            ""));
                //    }
                //}

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
