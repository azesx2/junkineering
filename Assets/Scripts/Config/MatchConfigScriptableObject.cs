using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Config
{
    [CreateAssetMenu(fileName = "MatchConfig", menuName = "Config/MatchConfig", order = 1)]
    public class MatchConfigScriptableObject : ScriptableObject
    {
        [SerializeField] private UnitConfigScriptableObject[] _playerUnits;
        [SerializeField] private UnitConfigScriptableObject[] _enemyUnits;

        public IEnumerable<UnitConfigScriptableObject> PlayerUnits => _playerUnits;
        public IEnumerable<UnitConfigScriptableObject> EnemyUnits => _enemyUnits;

        [Serializable]
        public class UnitConfigScriptableObject
        {
            [field: SerializeField] public string SpritePath  { get; private set; }
            [field: SerializeField] public int AttackPower  { get; private set; }
            [field: SerializeField] public int Hp  { get; private set; }
            [field: SerializeField] public int Initiative  { get; private set; }
        }
    }
}
