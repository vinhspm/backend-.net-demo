using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                var v_Columns = "";
                var v_Values = "";
                Guid recordId = new Guid();
                foreach (PropertyInfo prop in record.GetType().GetProperties())
                {
                    var primaryKeyAttribute = (PrimaryKey?)Attribute.GetCustomAttribute(prop, typeof(PrimaryKey));
                    if (primaryKeyAttribute != null)
                    {
                        recordId = Guid.NewGuid();
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
                return new ServiceResponse(false, Guid.Empty);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ServiceResponse(false, ex.Message);
            }
        }


        /// <summary>
        /// sửa một bản ghi trong bảng
        /// </summary>
        /// <returns>số bản ghi bị ảnh hưởng ( 1 )</returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse UpdateRecord(Guid id, T record)
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
                return new ServiceResponse(false, res);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ServiceResponse(false, ex.Message);
            }
        }

        #endregion
    }

}
