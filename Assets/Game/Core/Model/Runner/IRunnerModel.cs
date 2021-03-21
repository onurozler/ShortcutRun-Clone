using System;

namespace Game.Core.Model.Runner
{
    public interface IRunnerModel
    {
        event Action<int> OnCollectableCountChanged;
        event Action<bool> OnSpeedChanged;
        event Action<RunnerState,RunnerState> OnStateChanged;
        
        RunnerState CurrentState { get; set; }
        bool HasCollectable { get; }
        int CollectableCount { get; set; }
        float Speed { get; set; }
    }
}
