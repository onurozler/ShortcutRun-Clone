using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Behaviour.Runner;
using Game.Core.Controller.Game;
using Game.Core.Controller.Game.Impl;
using Game.Core.Controller.Runner;
using Game.Core.Controller.Runner.Impl;
using Game.Core.Helpers.TimingManager;
using Game.Core.Helpers.TimingManager.Impl;
using Game.Core.Managers;
using Game.Core.Model.Input;
using Game.Core.Model.Runner;
using Game.Core.View;
using UnityEngine;
using Zenject;

namespace Game.Core.Injection
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] 
        private Camera _mainCamera;

        [SerializeField] 
        private PlayerView _playerView;

        [SerializeField] 
        private RunnerBehaviourBase _myRunner;

        [SerializeField] 
        private RunnerBehaviourBase[] _otherRunners;

        [SerializeField] 
        private WaypointManager _waypointManager;

        public override void InstallBindings()
        {
            Container.Bind<IWaypointManager>().FromInstance(_waypointManager);
            
            Container.BindInstance(_myRunner);
            Container.Bind<IRunnerModel>().WithId("MyPlayer").FromInstance(_myRunner.RunnerModel);
            
            Container.BindInstance(_mainCamera ? _mainCamera : Camera.main);
            Container.Bind<IPlayerView>().FromInstance(_playerView);

            Container.Bind<ITimingManager>().To<TweenTimingManager>().AsSingle();
            Container.Bind<IInputModel>().To<InputModel>().AsSingle();

            Container.BindInterfacesTo<RunnerCollectableController>()
                .FromMethod(RunnerControllerInjection<RunnerCollectableController>).AsTransient();
            
            Container.BindInterfacesTo<RunnerPathController>()
                .FromMethod(RunnerControllerInjection<RunnerPathController>).AsTransient();
            
            Container.BindInterfacesTo<RunnerGroundController>()
                .FromMethod(RunnerControllerInjection<RunnerGroundController>).AsTransient();
            
            Container.BindInterfacesTo<RunnerStateController>()
                .FromMethod(RunnerControllerInjection<RunnerStateController>).AsTransient();

            Container.BindInterfacesTo<RunnerAnimationController>()
                .FromMethod(RunnerControllerInjection<RunnerAnimationController>).AsTransient();
            
            Container.BindInterfacesTo<GameController>().AsSingle();
            Container.Bind<ILevelController>().To<LevelController>().AsSingle();

            Container.Bind<IEnumerable<IRunnerModel>>().FromInstance(_otherRunners.Select(x => x.RunnerModel));
        }

        private void OnDestroy()
        {
            Debug.Log("VAR");
        }

        #region Helpers

        private T RunnerControllerInjection<T>(InjectContext injectContext) 
            where T : IRunnerController, new()
        {
            var runnerController = new T();
            runnerController.Initialize(injectContext.ObjectInstance as RunnerBehaviourBase);
            injectContext.Container.Inject(runnerController);
            return runnerController;
        }

        #endregion
    }
}