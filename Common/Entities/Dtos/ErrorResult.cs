using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class ErrorResult
    {
        #region Field

        // mã lỗi
        public AMISErrorCode Code { get; set; }
        
        // message cho dev đọc
        public string DevMsg { get; set; }
        
        // message cho user đọc
        public string UserMsg { get; set; }

        // thông tin thêm
        public object MoreInfo { get; set; }

        // id để truy lỗi
        public string TraceId { get; set; }

        #endregion

        #region Constructor

        public ErrorResult(AMISErrorCode code, string devMsg, string userMsg, object moreInfo, string traceId)
        {
            Code = code;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }

        #endregion
    }
}
