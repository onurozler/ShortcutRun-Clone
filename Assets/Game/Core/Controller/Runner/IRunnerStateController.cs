using Game.Core.Model.Runner;

namespace Game.Core.Controller.Runner
{
    public interface IRunnerStateController : IRunnerController
    {
        void SetState(RunnerState newState);
        void Clear();
    }
}