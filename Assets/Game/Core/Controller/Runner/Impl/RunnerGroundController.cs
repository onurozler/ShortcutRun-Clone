using Game.Core.Behaviour.Runner;
using Game.Core.Helpers;
using Game.Core.Model.Constants;
using Game.Core.Model.Runner;
using UnityEngine;

namespace Game.Core.Controller.Runner.Impl
{
    public class RunnerGroundController : IRunnerGroundController
    {
        private Transform _runnerTransform;
        private Transform _edgeDetectorTransform;
        private IRunnerModel _runnerModel;

        public void Initialize(RunnerBehaviourBase runnerBehaviourBase)
        {
            _runnerTransform = runnerBehaviourBase.transform;
            _edgeDetectorTransform = runnerBehaviourBase.RunnerComponentBehaviour.EdgeDetector;
            _runnerModel = runnerBehaviourBase.RunnerModel;
        }

        public bool IsNonWalkableGroundDetected()
        {
            var isBoxCasted = Physics.BoxCast(_runnerTransform.position + Vector3.up, RunnerConstants.NonWalkableBoxCastExtends, Vector3.down, 
                out var raycastHit,_runnerTransform.rotation, RunnerConstants.NonWalkableCheckDistance);

            return isBoxCasted && raycastHit.CheckLayer(GameConfig.NonWalkableLayer) 
                               && _runnerModel.CurrentState != RunnerState.Climbing;
        }

        public void Dispose()
        {
        }
    }
}