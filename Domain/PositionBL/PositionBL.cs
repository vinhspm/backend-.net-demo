using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
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
