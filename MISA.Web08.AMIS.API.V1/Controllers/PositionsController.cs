using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.BL;
using MISA.Web08.AMIS.Common;

namespace MISA.Web08.AMIS.API.Controllers
{
    public class PositionsController : BasesController<Position>
    {
        #region Constructor

        public PositionsController(IBaseBL<Position> baseBL) : base(baseBL)
        {
        }

        #endregion
    }
}
