using System;
using Core.Data;

namespace Core.Controllers
{
    public interface IUnitController : IDisposable
    {
        event Action<IUnitController, IUnitController> PerformedAttack;

        bool IsAlive { get; }
        UnitConfig Config { get; }

        void StartTurn();
        void EndTurn();
        void TakeDamage(int damage);
        void Kill();
    }
}
