using Core.Data;
using Core.Services;
using Core.Views;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using VContainer;

namespace Core
{
    public class GameplaySceneController : MonoBehaviour
    {
        [SerializeField] private Transform _gameField;
        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private TMP_Text _hintText;
        [SerializeField] private TMP_Text _gameOverText;

        private IMatchService _matchService;

        [Inject]
        private void Initialize(IMatchService matchService)
        {
            _matchService = matchService;
        }

        private void Awake()
        {
            _gameField.gameObject.SetActive(false);
            _gameplayView.gameObject.SetActive(false);
        }

        private void OnEnable() => _matchService.GameEnded += OnGameEnded;

        private void OnDisable()
        {
            _matchService.GameEnded -= OnGameEnded;
        }

        // ReSharper disable once Unity.IncorrectMethodSignature
        private async UniTaskVoid Start()
        {
            await _matchService.SetupUnits();

            _gameField.gameObject.SetActive(true);
            _gameplayView.gameObject.SetActive(true);

            _matchService.StartGame();
        }

        private void OnGameEnded(Team winner)
        {
            _hintText.gameObject.SetActive(false);
            _gameField.gameObject.SetActive(false);
            _gameplayView.gameObject.SetActive(false);
            _gameOverText.text = winner == Team.Player ? "You Win!" : "You Loose!";
            _gameOverText.gameObject.SetActive(true);
        }
    }
}
