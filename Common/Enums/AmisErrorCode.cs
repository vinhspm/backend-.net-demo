using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum AMISErrorCode
    {
        // ngoại lệ
        Exception = 001,
        // input sai
        InvalidInput = 002,
        // không được để trống
        NotNullInput = 003,
        // không được trùng lặp
        DuplicateInput = 004,
    }
}
