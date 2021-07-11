using System;
using UnityEngine;

namespace Game.Core.Model.Runner
{
    public interface IRunnerModel
    {
        event Action<int> OnCollectableCountIncreased; 
        event Action<int> OnCollectableCountChanged;
        event Action<bool> OnSpeedChanged;
        event Action<RunnerState,RunnerState> OnStateChanged;
        
        string RunnerName { get; }
        RunnerState CurrentState { get; set; }
        bool HasCollectable { get; }
        int CollectableCount { get; set; }
        float Speed { get; set; }
        Color Color { get; }

        void ResetData();
    }
}
