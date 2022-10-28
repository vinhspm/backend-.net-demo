using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.Common
{
    public class MultipleQueriesResult
    {
        // số bản ghi thành công
        public int SuccessQueries { get; set; }

        // số bản ghi thất bại
        public int FailedQueries { get; set; }

        public MultipleQueriesResult(int successQueries, int failedQueries)
        {
            SuccessQueries = successQueries;
            FailedQueries = failedQueries;
        }   
    }
}
