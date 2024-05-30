using System;
using Core.Data;
using Core.Services;
using Core.Views;
using Cysharp.Threading.Tasks;

namespace Core.Controllers
{
    public class UnitController : IUnitController
    {
        public event Action<IUnitController, IUnitController> PerformedAttack;
        public bool IsAlive => _currentHp > 0;
        public UnitConfig Config { get; }

        private readonly ITargetingService _targetingService;
        private readonly UnitView _unitView;

        private int _currentHp;
        private bool _canTarget;

        protected UnitController(ITargetingService targetingService, UnitView unitView, UnitConfig unitConfig)
        {
            _targetingService = targetingService;
            _unitView = unitView;
            Config = unitConfig;

            _currentHp = Config.Hp;
            _unitView.Clicked += OnClicked;
            _targetingService.TargetSelected += OnTargetSelected;
            _targetingService.TargetsChanged += OnTargetsChanged;
        }

        public async UniTask Setup()
        {
            await _unitView.Setup(Config.SpritePath, Config.Hp, Config.AttackPower);
        }

        public void StartTurn()
        {
            _unitView.ToggleSelected(true);
            _targetingService.Confirmed += OnTargetConfirmed;
            _targetingService.SetTargets(Config.Team == Team.Player ? Team.Enemy : Team.Player);
        }

        private void OnTargetConfirmed(IUnitController target)
        {
            _targetingService.Confirmed -= OnTargetConfirmed;
            PerformedAttack?.Invoke(this, target);
        }

        public void EndTurn() => _unitView.ToggleSelected(false);

        public void TakeDamage(int damage)
        {
            _currentHp = Math.Max(0, _currentHp - damage);
            _unitView.UpdateHealth(_currentHp);
        }

        public void Kill() => UnityEngine.Object.Destroy(_unitView.gameObject);
        public void ToggleTargeted(bool isTargeted) => _unitView.ToggleTarget(isTargeted);

        private void OnClicked()
        {
            if (_canTarget)
            {
                _targetingService.SelectTarget(this);
            }
        }

        private void OnTargetsChanged(Team team)
        {
            _canTarget = Config.Team == team;
        }

        private void OnTargetSelected(IUnitController target)
        {
            _unitView.ToggleTarget(target == this);
        }

        public void Dispose()
        {
            _unitView.Clicked -= OnClicked;
            _targetingService.TargetSelected -= OnTargetSelected;
            _targetingService.TargetsChanged -= OnTargetsChanged;
        }
    }
}
