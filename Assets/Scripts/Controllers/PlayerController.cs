using Core.Data;
using Core.Services;
using Core.Views;

namespace Core.Controllers
{
    public class PlayerController : UnitController
    {
        public PlayerController(ITargetingService targetingService, UnitView unitView, UnitConfig unitConfig)
            : base(targetingService, unitView, unitConfig)
        {
        }
    }
}
