using System;
using DG.Tweening;
using Game.Core.Model.Runner;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Core.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] 
        private GameObject _playAgainText;
        
        [SerializeField] 
        private Text _starText;

        [SerializeField] 
        private Text _collectableText;
        
        [Inject(Id = "MyPlayer")]
        private IRunnerModel _runnerModel;
        
        public event Action OnStartButtonPressed;
        public event Action OnRestartButtonPressed;

        private void Awake()
        {
            _runnerModel.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged(RunnerState oldState, RunnerState newState)
        {
            if (newState == RunnerState.Died || newState == RunnerState.Finished)
            {
                _playAgainText.gameObject.SetActive(true);
            }
        }

        public void StartButtonPressed()
        {
            _starText.gameObject.SetActive(false);
            OnStartButtonPressed?.Invoke();
        }

        public void RestartButtonPressed()
        {
            _playAgainText.gameObject.SetActive(false);
            OnRestartButtonPressed?.Invoke();
        }

        private void OnDestroy()
        {
            _runnerModel.OnStateChanged -= OnStateChanged;
        }
    }
}
