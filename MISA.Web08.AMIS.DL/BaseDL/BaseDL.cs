using Dapper;
using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Enums;
using MISA.Web08.AMIS.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {

        #region Method

        /// <summary>
        /// danh sách toàn bộ bản ghi 1 bảng
        /// </summary>
        /// <returns>danh sách toàn bộ bản ghi 1 bảng</returns>
        /// created by vinhkt(30/09/2022)
        public IEnumerable<T> GetAllRecords()
        {
            string storedProcedureName = String.Format(Resource.Proc_GetAll, typeof(T).Name);
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var records = _connection.Query<T>(
                                storedProcedureName,
                                commandType: System.Data.CommandType.StoredProcedure);
                return records;
            }
        }

        /// <summary>
        /// lấy chi tiết 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>record</returns>
        /// created by: vinhkt(30/09/2022)
        public T GetRecordById(Guid id)
        {

            var storedProcedureName = String.Format(Resource.Proc_Detail, typeof(T).Name); ;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                DynamicParameters value = new DynamicParameters();
                value.Add("@v_id", id);
                var records = _connection.QueryFirstOrDefault<T>(
                                storedProcedureName,
                                value,
                                commandType: System.Data.CommandType.StoredProcedure);
                return records;
            }
        }

        /// <summary>
        /// thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public ServiceResponse InsertRecord(T record)
        {
            var v_Columns = "";
            var v_Values = "";
            Guid recordId = Guid.NewGuid();
            foreach (PropertyInfo prop in record.GetType().GetProperties())
            {
                var primaryKeyAttribute = (PrimaryKey?)Attribute.GetCustomAttribute(prop, typeof(PrimaryKey));

                // gán giá trị cho id của bản ghi
                if (primaryKeyAttribute != null)
                {
                    prop.SetValue(record, recordId, null);
                }
                var fieldName = prop.Name;
                var fieldValue = prop.GetValue(record);
                if (fieldValue != null)
                {
                    // định dạng chuẩn cho datetime
                    if (fieldValue.GetType() == typeof(DateTime))
                    {
                        if (fieldName == "ModifiedDate")
                        {
                            fieldValue = DateTime.Now;
                        }
                        fieldValue = DateTime.Parse(fieldValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    // gán giá trị cho giới tính từ enum
                    if (fieldValue.GetType() == typeof(Gender))
                    {
                        fieldValue = (int)Enum.Parse(typeof(Gender), fieldValue.ToString());
                    }
                    // gán giá trị cho trạng thái từ enum
                    if (fieldValue.GetType() == typeof(RequestStatus))
                    {
                        fieldValue = (int)Enum.Parse(typeof(RequestStatus), fieldValue.ToString());
                    }
                    // gán giá trị cho thời gian làm việc từ enum
                    if (fieldValue.GetType() == typeof(WorkTime))
                    {
                        fieldValue = (int)Enum.Parse(typeof(WorkTime), fieldValue.ToString());
                    }
                    // gán giá trị v_Columns truyền vào procedure
                    if (v_Columns.Length > 0)
                    {
                        v_Columns = v_Columns + ", " + fieldName;
                    }
                    else
                    {
                        v_Columns = v_Columns + fieldName;
                    }

                    // gán giá trị v_Values truyền vào procedure
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
            DynamicParameters values = new DynamicParameters();
            values.Add("@v_Columns", v_Columns);
            values.Add("@v_Values", v_Values);
            var affetecRows = 0;

            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                _connection.Open();
                var trans = _connection.BeginTransaction();
                var storedProcedureName = String.Format(Resource.Proc_Insert, typeof(T).Name);
                try
                {
                    affetecRows = _connection.Execute(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure, transaction: trans);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
                finally
                {
                    _connection.Close();
                }

            }

            if (affetecRows > 0)
            {
                return new ServiceResponse(true, recordId);
            }
            else
            {
                return new ServiceResponse(false, Guid.Empty);
            }


        }

        /// <summary>
        /// sửa 1 bản ghi
        /// </summary>
        /// <param name="v_id"></param>
        /// <param name="v_Query"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public QueryResult UpdateRecord(Guid id, T record)
        {
            var v_Query = "";
            Guid recordId = id;
            foreach (PropertyInfo prop in record.GetType().GetProperties())
            {
                var primaryKeyAttribute = (PrimaryKey?)Attribute.GetCustomAttribute(prop, typeof(PrimaryKey));
                // gán giá trị cho id của record từ route truyền vào
                if (primaryKeyAttribute != null)
                {
                    prop.SetValue(record, recordId, null);
                }

                var fieldName = prop.Name;
                var fieldValue = prop.GetValue(record);
                if (fieldValue != null)
                {
                    //format ngày tháng để truyền vào db
                    if (fieldValue.GetType() == typeof(DateTime))
                    {
                        if(fieldName == "ModifiedDate")
                        {
                            fieldValue = DateTime.Now;
                        }
                        fieldValue = DateTime.Parse(fieldValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    //lấy giá trị của gender dựa trên enum
                    if (fieldValue.GetType() == typeof(Gender))
                    {
                        fieldValue = (int)Enum.Parse(typeof(Gender), fieldValue.ToString());
                    }
                    // gán giá trị cho trạng thái từ enum
                    if (fieldValue.GetType() == typeof(RequestStatus))
                    {
                        fieldValue = (int)Enum.Parse(typeof(RequestStatus), fieldValue.ToString());
                    }
                    // gán giá trị cho thời gian làm việc từ enum
                    if (fieldValue.GetType() == typeof(WorkTime))
                    {
                        fieldValue = (int)Enum.Parse(typeof(WorkTime), fieldValue.ToString());
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
            var affetecRows = 0;

            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                _connection.Open();
                var trans = _connection.BeginTransaction();
                var v_Id = $"'{id}'";
                DynamicParameters values = new DynamicParameters();
                values.Add("@v_Id", v_Id);
                values.Add("@v_Query", v_Query);
                var storedProcedureName = String.Format(Resource.Proc_Update, typeof(T).Name);
                try
                {
                    affetecRows = _connection.Execute(storedProcedureName, values, commandType: System.Data.CommandType.StoredProcedure, transaction: trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                finally
                {
                    _connection.Close();
                }




            }
            if (affetecRows > 0)
            {
                return QueryResult.Success;
            }
            else
            {
                return QueryResult.Fail;
            }
        }

        /// <summary>
        /// xoá 1 bản ghi
        /// </summary>
        /// <param name="v_id"></param>
        /// <returns></returns>
        /// created by: vinhkt(30/09/2022)
        public QueryResult DeleteRecord(Guid v_id)
        {

            int affetecRows = 0;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                _connection.Open();
                var trans = _connection.BeginTransaction();
                var storedProcedureName = String.Format(Resource.Proc_Delete, typeof(T).Name); ;
                DynamicParameters value = new DynamicParameters();
                value.Add("@v_Id", v_id);
                try
                {
                    affetecRows = _connection.Execute(
                                storedProcedureName,
                                value,
                                commandType: System.Data.CommandType.StoredProcedure, transaction: trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                finally
                {
                    _connection.Close();
                }

            }

            if (affetecRows > 0)
            {
                return QueryResult.Success;
            }
            else
            {
                return QueryResult.Fail;
            }
        }

        /// <summary>
        /// tìm bản ghi trùng dữ liệu
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public T FindDuplicate(string fieldName, string fieldValue)
        {
            T duplicateRecord;
            using (var _connection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var storedProcedureName = String.Format(Resource.Proc_Duplicate, typeof(T).Name);
                DynamicParameters values = new DynamicParameters();
                values.Add("@v_FieldName", fieldName);
                values.Add("@v_FieldValue", $"'{fieldValue}'");

                duplicateRecord = _connection.QueryFirstOrDefault<T>(
                                storedProcedureName,
                                values,
                                commandType: System.Data.CommandType.StoredProcedure);
                return duplicateRecord;
            }

        }
        #endregion
    }
}
