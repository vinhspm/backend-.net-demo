using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class EmployeeSheetProperties
    {
        #region MyRegion

        public static Dictionary<string, int> Width;
        public static Dictionary<string, XLAlignmentHorizontalValues> Align;

        #endregion


        #region Constructor
        static EmployeeSheetProperties()
        {
            Width = new Dictionary<string, int>
            {
                {"A", 15 },
                {"B", 20 },
                {"C", 11 },
                {"D", 14 },
                {"E", 14 },
                {"F", 20 },
                {"G", 30 },
                {"H", 20 },
                {"I", 14 },
                {"J", 25 },
                {"K", 25 },
            };

            Align = new Dictionary<string, XLAlignmentHorizontalValues>
            {
                {"A", XLAlignmentHorizontalValues.Left },
                {"B", XLAlignmentHorizontalValues.Left },
                {"C", XLAlignmentHorizontalValues.Left },
                {"D", XLAlignmentHorizontalValues.Center },
                {"E", XLAlignmentHorizontalValues.Left },
                {"F", XLAlignmentHorizontalValues.Center },
                {"G", XLAlignmentHorizontalValues.Left },
                {"H", XLAlignmentHorizontalValues.Left },
                {"I", XLAlignmentHorizontalValues.Left },
                {"J", XLAlignmentHorizontalValues.Left },
                {"K", XLAlignmentHorizontalValues.Left },
            };
        }

        #endregion
    }
}
