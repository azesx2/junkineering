using System.Collections.Generic;
using System.Linq;
using Core.Config;
using Core.Data;
using Cysharp.Threading.Tasks;

namespace Core.DataProviders
{
    public class ScriptableObjectMatchConfigProvider : IMatchConfigProvider
    {
        private readonly MatchConfigScriptableObject _matchConfigScriptableObject;

        public ScriptableObjectMatchConfigProvider(MatchConfigScriptableObject matchConfigScriptableObject) =>
            _matchConfigScriptableObject = matchConfigScriptableObject;

        public UniTask<IReadOnlyList<UnitConfig>> GetPlayerUnits()
        {
            var unitsConfig =
                _matchConfigScriptableObject.PlayerUnits.Select(c =>
                    new UnitConfig(Team.Player, c.SpritePath, c.AttackPower, c.Hp, c.Initiative));

            return UniTask.FromResult((IReadOnlyList<UnitConfig>)unitsConfig.ToList());
        }

        public UniTask<IReadOnlyList<UnitConfig>> GetEnemyUnits()
        {
            var unitsConfig =
                _matchConfigScriptableObject.EnemyUnits.Select(c =>
                    new UnitConfig(Team.Enemy, c.SpritePath, c.AttackPower, c.Hp, c.Initiative));

            return UniTask.FromResult((IReadOnlyList<UnitConfig>)unitsConfig.ToList());
        }
    }
}
