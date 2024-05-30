using System;
using Core.Data;
using Cysharp.Threading.Tasks;

namespace Core.Services
{
    public interface IMatchService
    {
        event Action<Team> GameEnded;

        UniTask SetupUnits();
        void StartGame();
    }
}
