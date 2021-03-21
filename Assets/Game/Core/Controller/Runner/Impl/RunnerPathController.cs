using System.Collections.Generic;
using Game.Core.Behaviour.Collectable;
using Game.Core.Behaviour.Runner;
using Game.Core.Model.Constants;
using Game.Core.Model.Runner;
using UnityEngine;

namespace Game.Core.Controller.Runner.Impl
{
    public class RunnerPathController : IRunnerPathController
    {
        public bool IsTouchingGround => _groundCollisions.Count > 0;

        private IList<Collision> _groundCollisions;
        private Transform _edgeDetectorTransform;
        private IRunnerModel _runnerModel;
        private Transform _runnerTransform;
        private int _touchingCollectable;
        
        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _groundCollisions = new List<Collision>();

            _runnerTransform = runnerBehaviourBase.transform;
            _edgeDetectorTransform = runnerBehaviourBase.EdgeDetector;
            _runnerModel = runnerBehaviourBase.RunnerModel;
        }

        public void CreateWalkablePath(Transform collectableBase)
        {
            var targetPosition = _runnerTransform.position + _runnerTransform.forward * 0.2f;
            targetPosition.y = 0.2f;
            collectableBase.transform.SetParent(null);
            collectableBase.transform.position = targetPosition;
            collectableBase.transform.rotation = _runnerTransform.rotation;
            collectableBase.gameObject.layer = GameConfig.WalkableLayer;
        }

        public void AddWalkablePath(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CollectableBase collectableBase))
            {
                if (++_touchingCollectable > RunnerConstants.IncreaseSpeedThreshold)
                {
                    _runnerModel.Speed = RunnerConstants.RunnerIncreasedSpeed;
                }
            }
            else
            {
                _touchingCollectable = 0;
            }
            
            _groundCollisions.Add(collision);
        }

        public void RemoveWalkablePath(Collision collision)
        {
            _groundCollisions.Remove(collision);
        }

        public bool IsEdgeDetected()
        {
            return !_runnerModel.HasCollectable && Physics.Raycast(_edgeDetectorTransform.position, _edgeDetectorTransform.forward,
                RunnerConstants.EdgeDetectionDistance, GameConfig.WalkableLayerMask);
        }

    }
}