using Core.Controllers;
using Core.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core.Views
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;

        private ITargetingService _targetingService;

        [Inject]
        private void Initialize(ITargetingService targetingService)
        {
            _targetingService = targetingService;
            _targetingService.TargetSelected += OnTargetSelected;
        }

        private void Awake() => _attackButton.interactable = false;
        private void OnEnable() => _attackButton.onClick.AddListener(OnAttackClicked);
        private void OnDisable() => _attackButton.onClick.RemoveListener(OnAttackClicked);

        private void OnDestroy()
        {
            _targetingService.TargetSelected -= OnTargetSelected;
        }

        private void OnTargetSelected(IUnitController target)
        {
            _attackButton.interactable = target != null;
        }

        private void OnAttackClicked()
        {
            _targetingService.ConfirmTarget();
        }
    }
}
