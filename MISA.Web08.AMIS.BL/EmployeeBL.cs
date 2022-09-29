using MISA.Web08.AMIS.Common.Entities;
using MISA.Web08.AMIS.Common.Resources;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public class EmployeeBL : IEmployeeBL
    {
        #region Field
		
        private IEmployeeDL _employeeDL; 

	    #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion
        
        public IEnumerable<Employee> GetAllEmployees()
        {
            
            return _employeeDL.GetAllEmployees();
        }

        public PagingData GetEmployeesFilter(int pageSize, int pageNumber, string employeeFilter)
        {
            //var totalEmployeesCount = _employeeDL.GetCountEmployees();
            int offset = pageSize * (pageNumber - 1);
            string v_Where = "";
            if (employeeFilter != null)
            {
                v_Where = $"EmployeeCode LIKE \"%{employeeFilter}%\" OR FullName LIKE \"%{employeeFilter}%\"";
            }
            var result = _employeeDL.GetEmployeesFilter(offset, pageSize, v_Where);
            var totalRecord = result["Total"];
            var totalPage = Convert.ToInt32( totalRecord ) / Convert.ToInt32(pageSize);
            return new PagingData(
            result["PageData"], Convert.ToInt32(totalRecord), totalPage, pageNumber, pageSize);
        }

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

        public Employee GetEmployeeById(Guid employeeId)
        {
            return _employeeDL.GetEmployeeById(employeeId);
        }

        public int DeleteEmployeeById(Guid employeeId)
        {
            return _employeeDL.DeleteEmployeeById(employeeId);
        }

        public int InsertEmployee(Employee employee)
        {

            try
            {

                employee.EmployeeID = Guid.NewGuid();
                var v_Columns = new string("");
                var v_Values = new string("");
                foreach (PropertyInfo prop in employee.GetType().GetProperties())
                {
                    var fieldName = prop.Name;
                    var fieldValue = prop.GetValue(employee);
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

                return _employeeDL.InsertEmployee(v_Columns, v_Values);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public int UpdateEmployee(Guid employeeId, Employee employee)
        {

            try
            {
                var v_Query = "";
                employee.EmployeeID = employeeId;
                foreach (PropertyInfo prop in employee.GetType().GetProperties())
                {

                    var fieldName = prop.Name;
                    var fieldValue = prop.GetValue(employee);
                    if (fieldValue != null)
                    {
                        if (fieldValue.GetType() == typeof(DateTime))
                        {
                            fieldValue = DateTime.Parse(fieldValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            System.Diagnostics.Debug.WriteLine(fieldValue);
                        }
                        var fieldUpdateString = fieldName + " = " + $"\"{fieldValue}\"";
                        if(v_Query.Length > 0)
                        {
                            v_Query += ", " + fieldUpdateString;
                        } else
                        {
                            v_Query += fieldUpdateString;
                        }

                    }

                }
                return _employeeDL.UpdateEmployee(employeeId, v_Query);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
