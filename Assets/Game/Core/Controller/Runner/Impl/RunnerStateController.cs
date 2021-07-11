using System;
using Game.Core.Behaviour.Runner;
using Game.Core.Helpers;
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
        private ParticleSystem _deathParticle;
        private bool _isJumped;
        
        [Inject]
        private ITimingManager _timingManager;

        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _isJumped = false;
            _runnerRigidbody = runnerBehaviourBase.RunnerComponentBehaviour.Rigidbody;
            _runnerModel = runnerBehaviourBase.RunnerModel;
            _speedParticle = runnerBehaviourBase.RunnerComponentBehaviour.Particles.GetParticle(RunnerParticleType.Fire);
            _deathParticle = runnerBehaviourBase.RunnerComponentBehaviour.Particles.GetParticle(RunnerParticleType.WaterSplash);
            
            _runnerModel.OnSpeedChanged += OnSpeedChanged;
        }
        
        public void SetState(RunnerState newState)
        {
            if(_runnerModel.CurrentState == newState)
                return;
            
            switch (newState)
            {
                case RunnerState.Idle:
                    break;
                case RunnerState.Running:
                    if(_isJumped)
                        return;
                    break;
                // case RunnerState.Climbing: TODO: Climbing
                //     _runnerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                //     _runnerModel.Speed = 0;
                //     break;
                case RunnerState.Jumping:
                    _isJumped = true;
                    _timingManager.Delay(TimeSpan.FromSeconds(0.5f), () => _isJumped = false);
                    _runnerRigidbody.AddForce(Vector3.up * RunnerConstants.JumpPower);
                    break;
                case RunnerState.Died:
                    _deathParticle.gameObject.transform.SetParent(null);
                    _deathParticle.Play();
                    _timingManager.Delay(TimeSpan.FromSeconds(3f),
                        () => _runnerRigidbody.constraints = RigidbodyConstraints.FreezeAll);
                    break;
            }

            _runnerModel.CurrentState = newState;
        }
        
        
        private void OnSpeedChanged(bool isIncreased)
        {
            if (isIncreased)
            {
                _speedParticle.Play();
            }
            else
            {
                _speedParticle.Stop();
            }
        }

        public void Dispose()
        {
        }
    }
}