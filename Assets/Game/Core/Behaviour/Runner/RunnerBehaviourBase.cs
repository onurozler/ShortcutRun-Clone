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
        [SerializeField] 
        private RunnerModel _runnerModel;

        private RunnerComponentBehaviour _runnerComponentBehaviour;

        public IRunnerModel RunnerModel => _runnerModel;
        public RunnerComponentBehaviour RunnerComponentBehaviour => _runnerComponentBehaviour ? _runnerComponentBehaviour 
            : _runnerComponentBehaviour = GetComponent<RunnerComponentBehaviour>();

        protected IRunnerPathController RunnerPathController;
        protected IRunnerStateController RunnerStateController;
        protected IRunnerCollectableController RunnerCollectableController;
        protected IRunnerAnimationController RunnerAnimationController;
        protected IRunnerGroundController RunnerGroundController;
        protected bool IsNotActive => RunnerModel.CurrentState == RunnerState.Idle
                                    || RunnerModel.CurrentState == RunnerState.Died
                                    || RunnerModel.CurrentState == RunnerState.Finished;

        [Inject]
        private void Initialize(IRunnerPathController runnerPathController, IRunnerStateController runnerStateController,
            IRunnerCollectableController runnerCollectableController, IRunnerAnimationController runnerAnimationController,
            IRunnerGroundController runnerGroundController)
        {
            RunnerModel.ResetData();
            
            RunnerPathController = runnerPathController;
            RunnerStateController = runnerStateController;
            RunnerCollectableController = runnerCollectableController;
            RunnerAnimationController = runnerAnimationController;
            RunnerGroundController = runnerGroundController;

            RunnerComponentBehaviour.PlayerName.text = RunnerModel.RunnerName;
        }

        protected virtual void FixedUpdate()
        {
            if(IsNotActive)
                return;
            
            transform.Translate(Vector3.forward  * (RunnerModel.Speed * Time.deltaTime),Space.Self);
            if (RunnerGroundController.IsNonWalkableGroundDetected())
            {
                if (!RunnerModel.HasCollectable && RunnerModel.CurrentState != RunnerState.Jumping)
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

        protected virtual void OnDrawGizmos()
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.DrawWireCube(Vector3.up,RunnerConstants.NonWalkableBoxCastExtends);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CollectableBase collectableBase))
            {
                RunnerCollectableController.Collect(collectableBase);
            }
            else if (other.CheckLayer(GameConfig.NonWalkableLayer))
            {
                RunnerStateController.SetState(RunnerState.Died);
            }
            else if (other.CheckLayer(GameConfig.FinishLayer))
            {
                RunnerStateController.SetState(RunnerState.Finished);
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if(IsNotActive)
                return;
            
            if (collision.CheckLayer(GameConfig.WalkableLayer))
            {
                RunnerPathController.AddWalkablePath(collision);
                RunnerStateController.SetState(RunnerState.Running);
            }
        }

        private void OnDestroy()
        {
            RunnerAnimationController.Clear();
            RunnerStateController.Clear();
        }
    }
}
