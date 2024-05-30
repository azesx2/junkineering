using System;
using System.Collections.Generic;
using Core.Controllers;
using Core.Data;

namespace Core.Services
{
    public interface IMatchUnitsService
    {
        event Action<IReadOnlyList<IUnitController>> OrderChanged;

        IUnitController Next { get; }

        void Add(IUnitController unitController);
        void Remove(IUnitController unitController);
        void MoveNext();
        int Count(Team team);
    }
}
