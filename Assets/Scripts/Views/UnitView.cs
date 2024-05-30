using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Views
{
    public class UnitView : MonoBehaviour
    {
        public event Action Clicked;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _selection;
        [SerializeField] private Transform _target;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _attackText;

        private AsyncOperationHandle<Sprite> _spriteHandle;

        public async UniTask Setup(string spritePath, int hp, int attack)
        {
            _spriteHandle = Addressables.LoadAssetAsync<Sprite>(spritePath);
            var sprite = await _spriteHandle.ToUniTask();
            _spriteRenderer.sprite = sprite;

            _attackText.text = $"{attack}";
            UpdateHealth(hp);
        }

        public void UpdateHealth(int health)
        {
            _healthText.text = $"{health}";
        }

        private void Awake()
        {
            ToggleSelected(false);
            ToggleTarget(false);
        }

        private void OnDestroy()
        {
            if (_spriteHandle.IsValid())
            {
                Addressables.Release(_spriteHandle);
            }
        }

        private void OnMouseDown() => Clicked?.Invoke();

        public void ToggleSelected(bool isSelected) => _selection.gameObject.SetActive(isSelected);
        public void ToggleTarget(bool isTargeted) => _target.gameObject.SetActive(isTargeted);
    }
}
