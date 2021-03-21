using System.Collections.Generic;
using Game.Core.Behaviour.Collectable;
using Game.Core.Behaviour.Runner;
using Game.Core.Model.Runner;
using UnityEngine;

namespace Game.Core.Controller.Runner.Impl
{
    public class RunnerCollectableController : IRunnerCollectableController
    {
        private Stack<CollectableBase> _collectables;
        private IRunnerModel _runnerModel;
        private Transform _collectableTransform;
        
        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _collectables = new Stack<CollectableBase>();
            _runnerModel = runnerBehaviourBase.RunnerModel;
            _collectableTransform = runnerBehaviourBase.CollectableTransform;
        }

        public void Collect(CollectableBase collectable)
        {
            if (_collectables.Contains(collectable))
                return;

            _runnerModel.CollectableCount++;
            _collectables.Push(collectable);
            collectable.transform.SetParent(_collectableTransform);
            collectable.Collider.isTrigger = false;

            float targetPositionY = 0f;
            if (_collectables.Count > 1)
            {
                var lastCollectable = _collectables.Peek();
                targetPositionY = (_runnerModel.CollectableCount - 1) * lastCollectable.transform.localScale.y;
            }
            collectable.transform.localPosition = new Vector3(0,targetPositionY,0);
            collectable.transform.localEulerAngles = Vector3.zero;
        }

        public Transform Drop()
        {
            if (_collectables.Count > 0)
            {
                _runnerModel.CollectableCount--;
                var collectable = _collectables.Pop();
                return collectable.transform;
            }

            return null;
        }
        
    }
}