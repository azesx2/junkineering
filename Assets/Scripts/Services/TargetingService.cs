using System;
using Core.Controllers;
using Core.Data;

namespace Core.Services
{
    public class TargetingService : ITargetingService
    {
        public event Action<IUnitController> Confirmed;
        public event Action<Team> TargetsChanged;
        public event Action<IUnitController> TargetSelected;

        private IUnitController _currentTarget;

        public void SetTargets(Team team)
        {
            TargetsChanged?.Invoke(team);
        }

        public void SelectTarget(IUnitController unitController)
        {
            _currentTarget = unitController;
            TargetSelected?.Invoke(unitController);
        }

        public void ConfirmTarget()
        {
            Confirmed?.Invoke(_currentTarget);
            _currentTarget = null;
            TargetSelected?.Invoke(null);
        }
    }
}
