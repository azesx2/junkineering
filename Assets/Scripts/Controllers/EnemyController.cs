using Core.Data;
using Core.Services;
using Core.Views;

namespace Core.Controllers
{
    public class EnemyController : UnitController
    {
        public EnemyController(ITargetingService targetingService, UnitView unitView, UnitConfig unitConfig)
            : base(targetingService, unitView, unitConfig)
        {
        }
    }
}
