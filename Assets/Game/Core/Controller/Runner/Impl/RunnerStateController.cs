using System;
using Game.Core.Behaviour.Runner;
using Game.Core.Helpers.TimingManager;
using Game.Core.Model.Constants;
using Game.Core.Model.Runner;
using UnityEngine;
using Zenject;

namespace Game.Core.Controller.Runner.Impl
{
    public class RunnerStateController : IRunnerStateController
    {
        private Rigidbody _runnerRigidbody;
        private IRunnerModel _runnerModel;
        private ParticleSystem _speedParticle;
        
        [Inject]
        private ITimingManager _timingManager;

        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _runnerRigidbody = runnerBehaviourBase.Rigidbody;
            _runnerModel = runnerBehaviourBase.RunnerModel;
            _speedParticle = runnerBehaviourBase.SpeedParticle;

            _runnerModel.OnSpeedChanged += OnSpeedChanged;
        }
        
        public void SetState(RunnerState newState)
        {
            switch (newState)
            {
                case RunnerState.Idle:
                    break;
                case RunnerState.Running:
                    break;
                case RunnerState.Climbing:
                    _runnerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    _runnerModel.Speed = 0;
                    break;
                case RunnerState.Jumping:
                    if (_runnerModel.CurrentState == RunnerState.Jumping || _runnerRigidbody.velocity.y > 0) 
                        return;
                        
                    _runnerRigidbody.AddForce(Vector3.up * RunnerConstants.JumpPower);
                    break;
            }

            _runnerModel.CurrentState = newState;
        }
        
        
        private void OnSpeedChanged(bool isIncreased)
        {
            if (isIncreased)
            {
                _speedParticle.Play();
                _timingManager.Delay(TimeSpan.FromSeconds(RunnerConstants.RunnerIncreasedSpeedTime), () =>
                {
                    _speedParticle.Stop();
                    _runnerModel.Speed = RunnerConstants.RunnerNormalSpeed;
                });
            }
        }
    }
}