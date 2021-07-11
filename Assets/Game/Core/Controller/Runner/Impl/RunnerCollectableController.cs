using System.Collections.Generic;
using DG.Tweening;
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
        private Sequence _collectSequence;
        
        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _collectables = new Stack<CollectableBase>();
            _runnerModel = runnerBehaviourBase.RunnerModel;
            _collectableTransform = runnerBehaviourBase.RunnerComponentBehaviour.CollectableTransform;
            _collectSequence = DOTween.Sequence();
        }

        public void Collect(CollectableBase collectable)
        {
            if (_collectables.Contains(collectable))
                return;

            _runnerModel.CollectableCount++;
            _collectables.Push(collectable);
            collectable.transform.SetParent(_collectableTransform);
            collectable.Collider.isTrigger = false;
            collectable.SetColor(_runnerModel.Color);

            float targetPositionY = 0f;
            if (_collectables.Count > 1)
            {
                var lastCollectable = _collectables.Peek();
                targetPositionY = (_runnerModel.CollectableCount - 1) * (lastCollectable.transform.localScale.y + 0.03f);
            }
            _collectSequence.Join(collectable.transform.DOLocalMove(new Vector3(0, targetPositionY, 0), 0.25f).SetEase(Ease.OutExpo));
            _collectSequence.Join(collectable.transform.DOLocalRotate(Vector3.zero, 0.25f));
            _collectSequence.Append(collectable.transform.DOShakeScale(0.15f,0.5f));
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