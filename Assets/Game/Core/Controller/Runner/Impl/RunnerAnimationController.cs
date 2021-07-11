using Game.Core.Behaviour.Runner;
using Game.Core.Model.Constants;
using Game.Core.Model.Runner;
using UnityEngine;

namespace Game.Core.Controller.Runner.Impl
{
    public class RunnerAnimationController : IRunnerAnimationController
    {
        private IRunnerModel _runnerModel;
        private Animator _runnerAnimator;
        
        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _runnerAnimator = runnerBehaviourBase.RunnerComponentBehaviour.Animator;
            _runnerModel = runnerBehaviourBase.RunnerModel;
            _runnerModel.OnStateChanged += OnStateChanged;
            _runnerModel.OnCollectableCountChanged += OnCollectableCountChanged;
            _runnerModel.OnSpeedChanged += OnSpeedChanged;
        }

        private void OnStateChanged(RunnerState oldState, RunnerState newState)
        {
            _runnerAnimator.SetBool(oldState.ToString(),false);
            _runnerAnimator.SetBool(newState.ToString(),true);
        }
        
        private void OnCollectableCountChanged(int collectableCount)
        {
            var hasCollectable = collectableCount > 0; 
            _runnerAnimator.SetLayerWeight(_runnerAnimator.GetLayerIndex(RunnerConstants.Animation.ArmLayer)
                ,hasCollectable ? 1 : 0);
        }
        
        private void OnSpeedChanged(bool isIncreased)
        {
            _runnerAnimator.SetFloat(RunnerConstants.Animation.RunSpeedParameter,isIncreased ? 2f : 1f);
        }

        public void Dispose()
        {
            _runnerModel.OnStateChanged -= OnStateChanged;
            _runnerModel.OnCollectableCountChanged -= OnCollectableCountChanged;
            _runnerModel.OnSpeedChanged -= OnSpeedChanged;
        }
    }
}