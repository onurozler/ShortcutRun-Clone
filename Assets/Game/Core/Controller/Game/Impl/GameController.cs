using System.Collections.Generic;
using Game.Core.Behaviour.Runner;
using Game.Core.Model.Runner;
using Game.Core.View;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Core.Controller.Game.Impl
{
    public class GameController : IGameController
    {
        private readonly IPlayerView _playerView;
        private readonly IRunnerModel _myRunnerModel;
        private IEnumerable<RunnerBehaviourBase> _AIRunners;

        [Inject]
        private void Initialize(IEnumerable<RunnerBehaviourBase> runnerModels)
        {
            _AIRunners = runnerModels;
        }
        
        public GameController(IPlayerView playerView,RunnerBehaviourBase myRunnerBehaviourBase)
        {
            _playerView = playerView;
            _playerView.OnStartButtonPressed += StartGame;
            _playerView.OnRestartButtonPressed += RestartGame;
            _myRunnerModel = myRunnerBehaviourBase.RunnerModel;
        }

        private void StartGame()
        {
            _myRunnerModel.CurrentState = RunnerState.Running;
            foreach (var aiRunner in _AIRunners)
            {
                aiRunner.RunnerModel.CurrentState = RunnerState.Running;
            }
        }
        
        private void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void Dispose()
        {
            _playerView.OnStartButtonPressed -= StartGame;
            _playerView.OnRestartButtonPressed -= RestartGame;
        }
    }
}