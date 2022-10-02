using MISA.Web08.AMIS.Common;
using MISA.Web08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.AMIS.BL
{
    public class PositionBL : BaseBL<Position>, IPositionBL
    {
        #region Constructor
        
        public PositionBL(IBaseDL<Position> baseDL) : base(baseDL)
        {
        } 

        #endregion
    }
}
