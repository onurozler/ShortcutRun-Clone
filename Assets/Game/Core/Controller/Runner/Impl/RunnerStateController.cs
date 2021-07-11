using System;
using DG.Tweening;
using Game.Core.Behaviour;
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
        private Transform _collectableTransform;
        private Transform _runnerTextTransform;
        private bool _isJumped;
        
        [Inject]
        private ITimingManager _timingManager;
        
        [Inject]
        private FinishGroundBase _finishGroundBase;
        

        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _isJumped = false;
            _runnerTextTransform = runnerBehaviourBase.RunnerComponentBehaviour.PlayerName.transform;
            _collectableTransform = runnerBehaviourBase.RunnerComponentBehaviour.CollectableTransform;
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
                case RunnerState.Finished:
                    
                    if (_runnerModel.HasCollectable)
                    {
                        _runnerModel.CollectableCount = 0;
                        _collectableTransform.gameObject.SetActive(false);
                    }
                    var finishSequence = DOTween.Sequence();
                    finishSequence.Append(_runnerRigidbody.transform.DOMove(_finishGroundBase.GetAvailablePosition(), 0.5f));
                    finishSequence.Join(_runnerRigidbody.transform.DORotate(new Vector3(0,90,0),0.5f));
                    finishSequence.Join(_runnerTextTransform.DORotate(new Vector3(0, 270, 0), 0.5f));
                    break;
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

        public void Clear()
        {
            _runnerModel.OnSpeedChanged -= OnSpeedChanged;
        }
    }
}