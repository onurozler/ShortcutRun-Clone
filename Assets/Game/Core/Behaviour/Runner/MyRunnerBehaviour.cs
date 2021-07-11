using Game.Core.Model.Constants;
using Game.Core.Model.Input;
using Game.Core.Model.Runner;
using UnityEngine;
using Zenject;

namespace Game.Core.Behaviour.Runner
{
    public class MyRunnerBehaviour : RunnerBehaviourBase
    {
        private IInputModel _inputModel;
        private Camera _mainCamera;
        private Vector3 _cameraOffset;

        [Inject]
        private void Initialize(Camera mainCamera,IInputModel inputModel)
        {
            _inputModel = inputModel;
            _mainCamera = mainCamera;
            _cameraOffset = _mainCamera.transform.position - transform.position;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            transform.rotation = Quaternion.Euler(0,_mainCamera.transform.eulerAngles.y,0);
        }

        private void LateUpdate()
        {
            if(RunnerModel.CurrentState == RunnerState.Climbing || IsNotActive)
                return;
            
            if (_inputModel.IsTouching)
            {
                _cameraOffset = Quaternion.AngleAxis (_inputModel.GetMouseAxis(MouseAxis.X) * 2
                                                     , Vector3.up) * _cameraOffset;
            }
            _mainCamera.transform.position = transform.position + _cameraOffset;
            _mainCamera.transform.LookAt(transform.position + Vector3.up * RunnerConstants.MyRunnerCameraThreshold);
        }
    }
}
