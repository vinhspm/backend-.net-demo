using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.Common
{
    public static class RequestSheetProperties
    {
        #region MyRegion

        public static Dictionary<string, int> Width;
        public static Dictionary<string, XLAlignmentHorizontalValues> Align;

        #endregion


        #region Constructor
        static RequestSheetProperties()
        {
            Width = new Dictionary<string, int>
            {
                {"A", 11 },
                {"B", 20 },
                {"C", 30 },
                {"D", 20 },
                {"E", 20 },
                {"F", 20 },
                {"G", 20 },
                {"H", 20 },
                {"I", 30 },
                {"J", 20 },
                {"K", 12 },

            };

            Align = new Dictionary<string, XLAlignmentHorizontalValues>
            {
                {"A", XLAlignmentHorizontalValues.Left },
                {"B", XLAlignmentHorizontalValues.Left },
                {"C", XLAlignmentHorizontalValues.Left },
                {"D", XLAlignmentHorizontalValues.Left },
                {"E", XLAlignmentHorizontalValues.Center },
                {"F", XLAlignmentHorizontalValues.Center },
                {"G", XLAlignmentHorizontalValues.Center },
                {"H", XLAlignmentHorizontalValues.Left },
                {"I", XLAlignmentHorizontalValues.Left },
                {"J", XLAlignmentHorizontalValues.Left },
                {"K", XLAlignmentHorizontalValues.Left },

            };
        }

        #endregion
    }
}
