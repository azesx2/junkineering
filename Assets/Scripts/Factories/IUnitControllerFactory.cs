using Core.Controllers;
using Core.Data;
using Core.Services;
using Core.Views;

namespace Core.Factories
{
    public interface IUnitControllerFactory
    {
        PlayerController CreatePlayerController(UnitView unitView, UnitConfig unitConfig);
        EnemyController CreateEnemyController(UnitView unitView, UnitConfig unitConfig);
    }
}
