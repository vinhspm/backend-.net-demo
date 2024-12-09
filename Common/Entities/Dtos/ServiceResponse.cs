using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ServiceResponse
    {
        #region Field

        /// <summary>
        /// Thành công hay thất bại
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Dữ liệu đi kèm khi thành công hoặc thất bại
        /// </summary>
        public object Data { get; set; }

        #endregion

        #region Constructor

        public ServiceResponse(bool success, object data)
        {
            Success = success;
            Data = data;
        }

        #endregion

    }
}
