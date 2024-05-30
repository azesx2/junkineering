using System;
using System.Collections.Generic;
using Core.Controllers;
using Core.Data;
using Core.Factories;
using Core.Utils;
using Core.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;

namespace Core.Services.UnitSpawnerService
{
    public class AddressableUnitSpawner : IUnitSpawner, IDisposable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IUnitControllerFactory _unitControllerFactory;
        private readonly PlayerUnitsView _playerUnitsView;
        private readonly EnemyUnitsView _enemyUnitsView;
        private readonly List<AsyncOperationHandle<GameObject>> _unitsHandles = new();

        public AddressableUnitSpawner(IObjectResolver objectResolver, IUnitControllerFactory unitControllerFactory,
            PlayerUnitsView playerUnitsView, EnemyUnitsView enemyUnitsView)
        {
            _objectResolver = objectResolver;
            _unitControllerFactory = unitControllerFactory;
            _playerUnitsView = playerUnitsView;
            _enemyUnitsView = enemyUnitsView;
        }

        public async UniTask<PlayerController> SpawnPlayerUnit(UnitConfig unitConfig)
        {
            var unitView = await SpawnGameObject();
            _playerUnitsView.AddUnit(unitView.transform);
            var controller = _unitControllerFactory.CreatePlayerController(unitView, unitConfig);
            await controller.Setup();
            return controller;
        }

        public async UniTask<EnemyController> SpawnEnemyUnit(UnitConfig unitConfig)
        {
            var unitView = await SpawnGameObject();
            _enemyUnitsView.AddUnit(unitView.transform);
            var controller = _unitControllerFactory.CreateEnemyController(unitView, unitConfig);
            await controller.Setup();
            return controller;
        }

        private async UniTask<UnitView> SpawnGameObject()
        {
            var handle =  Addressables.InstantiateAsync(AddressableLocationUtil.GetUnitPrefabPath());
            _unitsHandles.Add(handle);
            var unitGameObject = await handle.ToUniTask();
            _objectResolver.Inject(unitGameObject);
            return unitGameObject.GetComponent<UnitView>();
        }

        public void Dispose()
        {
            foreach (var unitHandle in _unitsHandles)
            {
                Addressables.Release(unitHandle);
            }
        }
    }
}
