using System;
using Core.Controllers;
using Core.Data;

namespace Core.Services
{
    public interface ITargetingService
    {
        event Action<IUnitController> Confirmed;
        event Action<Team> TargetsChanged;
        event Action<IUnitController> TargetSelected;

        void SelectTarget(IUnitController unitController);
        void SetTargets(Team team);
        void ConfirmTarget();
    }
}
