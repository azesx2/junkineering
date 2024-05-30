using Core.Controllers;
using Core.Data;
using Cysharp.Threading.Tasks;

namespace Core.Services.UnitSpawnerService
{
    public interface IUnitSpawner
    {
        UniTask<PlayerController> SpawnPlayerUnit(UnitConfig unitConfig);
        UniTask<EnemyController> SpawnEnemyUnit(UnitConfig unitConfig);
    }
}
