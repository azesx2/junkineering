using Core.Controllers;
using Core.Data;
using Core.Services;
using Core.Views;

namespace Core.Factories
{
    public class UnitControllerFactory : IUnitControllerFactory
    {
        private readonly ITargetingService _targetingService;

        public UnitControllerFactory(ITargetingService targetingService)
        {
            _targetingService = targetingService;
        }

        public PlayerController CreatePlayerController(UnitView unitView, UnitConfig unitConfig) =>
            new(_targetingService, unitView, unitConfig);

        public EnemyController CreateEnemyController(UnitView unitView, UnitConfig unitConfig) =>
            new(_targetingService, unitView, unitConfig);
    }
}
