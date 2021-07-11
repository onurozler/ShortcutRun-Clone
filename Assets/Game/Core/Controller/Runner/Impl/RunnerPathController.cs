using Game.Core.Behaviour.Collectable;
using Game.Core.Behaviour.Runner;
using Game.Core.Model.Constants;
using Game.Core.Model.Runner;
using UnityEngine;

namespace Game.Core.Controller.Runner.Impl
{
    public class RunnerPathController : IRunnerPathController
    {
        private IRunnerModel _runnerModel;
        private Transform _runnerTransform;
        private int _touchingCollectable;

        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _runnerTransform = runnerBehaviourBase.transform;
            _runnerModel = runnerBehaviourBase.RunnerModel;
        }

        public void CreateWalkablePath(Transform collectableTransform)
        {
            var targetPosition = _runnerTransform.position + _runnerTransform.forward * RunnerConstants.CollectableSpace;
            targetPosition.y = 0.2f;
            collectableTransform.SetParent(null);
            collectableTransform.position = targetPosition;
            collectableTransform.rotation = _runnerTransform.rotation;
            collectableTransform.gameObject.layer = GameConfig.WalkableLayer;
        }

        public void AddWalkablePath(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CollectableBase collectableBase))
            {
                if (++_touchingCollectable > RunnerConstants.IncreaseSpeedThreshold)
                {
                    _runnerModel.Speed = RunnerConstants.IncreasedSpeed;
                }
            }
            else
            {
                _runnerModel.Speed = RunnerConstants.NormalSpeed;
                _touchingCollectable = 0;
            }
        }
    }
}