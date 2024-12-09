using Common;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller
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
