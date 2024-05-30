using System;
using System.Collections.Generic;
using System.Linq;
using Core.Controllers;
using Core.Data;

namespace Core.Services
{
    public class MatchUnitsService : IMatchUnitsService
    {
        public event Action<IReadOnlyList<IUnitController>> OrderChanged;

        private readonly List<IUnitController> _order = new();

        public IUnitController Next => _order.FirstOrDefault();

        public void Add(IUnitController unitController)
        {
            _order.Add(unitController);
            _order.Sort((a, b) => a.Config.Initiative.CompareTo(b.Config.Initiative));
            OrderChanged?.Invoke(_order);
        }

        public void Remove(IUnitController unitController)
        {
            _order.Remove(unitController);
            OrderChanged?.Invoke(_order);
        }

        public void MoveNext()
        {
            if (_order.Count <= 1)
            {
                return;
            }

            var next = _order.First();
            _order.Remove(next);
            _order.Add(next);
            OrderChanged?.Invoke(_order);
        }

        public int Count(Team team) => _order.Count(c => c.Config.Team == team);
    }
}
