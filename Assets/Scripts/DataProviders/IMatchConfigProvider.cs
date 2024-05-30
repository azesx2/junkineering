using System.Collections.Generic;
using Core.Data;
using Cysharp.Threading.Tasks;

namespace Core.DataProviders
{
    public interface IMatchConfigProvider
    {
        UniTask<IReadOnlyList<UnitConfig>> GetPlayerUnits();
        UniTask<IReadOnlyList<UnitConfig>> GetEnemyUnits();
    }
}
