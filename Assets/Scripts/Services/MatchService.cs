using System;
using System.Collections.Generic;
using Core.Controllers;
using Core.Data;
using Core.DataProviders;
using Core.Services.UnitSpawnerService;
using Cysharp.Threading.Tasks;

namespace Core.Services
{
    public class MatchService : IMatchService
    {
        public event Action<Team> GameEnded;

        private readonly IMatchConfigProvider _matchConfigProvider;
        private readonly IMatchUnitsService _matchUnitsService;
        private readonly IUnitSpawner _unitSpawner;

        private IUnitController _previousUnit;

        public MatchService(IMatchConfigProvider matchConfigProvider, IMatchUnitsService matchUnitsService,
            IUnitSpawner unitSpawner)
        {
            _matchConfigProvider = matchConfigProvider;
            _unitSpawner = unitSpawner;
            _matchUnitsService = matchUnitsService;
        }

        public async UniTask SetupUnits()
        {
            var playerTask = SpawnUnits(_matchConfigProvider.GetPlayerUnits,
                _unitSpawner.SpawnPlayerUnit);

            var enemyTask = SpawnUnits(_matchConfigProvider.GetEnemyUnits,
                _unitSpawner.SpawnEnemyUnit);

            await UniTask.WhenAll(playerTask, enemyTask);

            return;

            async UniTask SpawnUnits<T>(Func<UniTask<IReadOnlyList<UnitConfig>>> getter,
                Func<UnitConfig, UniTask<T>> spawner) where T: IUnitController
            {
                var unitConfigs = await getter.Invoke();
                var tasks = new UniTask[unitConfigs.Count];
                for (int i = 0; i < unitConfigs.Count; i++)
                {
                    tasks[i] = Spawn(unitConfigs[i]);
                }

                await UniTask.WhenAll(tasks);

                return;

                async UniTask Spawn(UnitConfig unitConfig)
                {
                    var unit = await spawner.Invoke(unitConfig);
                    _matchUnitsService.Add(unit);
                    unit.PerformedAttack += OnUnitAttack;
                }
            }
        }

        public void StartGame() => NextTurn();

        private void OnUnitAttack(IUnitController attacker, IUnitController target)
        {
            target.TakeDamage(attacker.Config.AttackPower);
            if (!target.IsAlive)
            {
                _matchUnitsService.Remove(target);
                target.PerformedAttack += OnUnitAttack;
                target.Kill();
                target.Dispose();
            }

            if (CheckEndConditions())
            {
                return;
            }

            _matchUnitsService.MoveNext();
            NextTurn();
        }

        private void NextTurn()
        {
            _previousUnit?.EndTurn();
            var next = _matchUnitsService.Next;
            next.StartTurn();
            _previousUnit = next;
        }

        private bool CheckEndConditions()
        {
            if (_matchUnitsService.Count(Team.Player) == 0)
            {
                GameEnded?.Invoke(Team.Enemy);
                return true;
            }

            if (_matchUnitsService.Count(Team.Enemy) == 0)
            {
                GameEnded?.Invoke(Team.Player);
                return true;
            }

            return false;
        }
    }
}
