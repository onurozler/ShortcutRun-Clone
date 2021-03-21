using Game.Core.Behaviour.Collectable;
using Game.Core.Controller.Runner;
using Game.Core.Helpers;
using Game.Core.Model.Constants;
using Game.Core.Model.Runner;
using UnityEngine;
using Zenject;

namespace Game.Core.Behaviour.Runner
{
    public abstract class RunnerBehaviourBase : MonoBehaviour
    {
        public IRunnerModel RunnerModel = new MyRunnerModel{Speed = 4};
        public Rigidbody Rigidbody;
        public Transform EdgeDetector;
        public Transform CollectableTransform;
        public Animator Animator;
        public ParticleSystem SpeedParticle;
        
        protected IRunnerPathController RunnerPathController;
        protected IRunnerStateController RunnerStateController;
        protected IRunnerCollectableController RunnerCollectableController;
        protected IRunnerAnimationController RunnerAnimationController;
        
        [Inject]
        private void Initialize(IRunnerPathController runnerPathController, IRunnerStateController runnerStateController,
            IRunnerCollectableController runnerCollectableController, IRunnerAnimationController runnerAnimationController)
        {
            RunnerPathController = runnerPathController;
            RunnerStateController = runnerStateController;
            RunnerCollectableController = runnerCollectableController;
            RunnerAnimationController = runnerAnimationController;
        }

        protected virtual void FixedUpdate()
        {
            transform.Translate(Vector3.forward  * (RunnerModel.Speed * Time.deltaTime),Space.Self);
            
            if (RunnerPathController.IsEdgeDetected())
            {
                RunnerStateController.SetState(RunnerState.Climbing);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CollectableBase collectableBase))
            {
                RunnerCollectableController.Collect(collectableBase);
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.CheckLayer(GameConfig.WalkableLayer))
            {
                RunnerPathController.AddWalkablePath(collision);
                RunnerStateController.SetState(RunnerState.Running);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.CheckLayer(GameConfig.WalkableLayer))
            {
                RunnerPathController.RemoveWalkablePath(collision);
                if (!RunnerPathController.IsTouchingGround)
                {
                    if (!RunnerModel.HasCollectable)
                    {
                        RunnerStateController.SetState(RunnerState.Jumping);
                    }
                    else
                    {
                        var collectable = RunnerCollectableController.Drop();
                        if (collectable != null)
                        {
                            RunnerPathController.CreateWalkablePath(collectable);
                        }
                    }
                }
            }
        }

    }
}
