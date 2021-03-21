using Game.Core.Behaviour.Runner;
using Game.Core.Controller.Runner;
using Game.Core.Controller.Runner.Impl;
using Game.Core.Helpers.TimingManager;
using Game.Core.Helpers.TimingManager.Impl;
using Game.Core.Model.Input;
using UnityEngine;
using Zenject;

namespace Game.Core.Injection
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] 
        private Camera _mainCamera;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_mainCamera ? _mainCamera : Camera.main);

            Container.Bind<ITimingManager>().To<TweenTimingManager>().AsSingle();
            Container.Bind<IInputModel>().To<InputModel>().AsSingle();

            Container.BindInterfacesTo<RunnerCollectableController>()
                .FromMethod(RunnerControllerInjection<RunnerCollectableController>).AsTransient();
            
            Container.BindInterfacesTo<RunnerPathController>()
                .FromMethod(RunnerControllerInjection<RunnerPathController>).AsTransient();
            
            Container.BindInterfacesTo<RunnerStateController>()
                .FromMethod(RunnerControllerInjection<RunnerStateController>).AsTransient();

            Container.BindInterfacesTo<RunnerAnimationController>()
                .FromMethod(RunnerControllerInjection<RunnerAnimationController>).AsTransient();
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