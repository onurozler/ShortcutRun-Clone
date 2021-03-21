using System;

namespace Game.Core.Model.Runner
{
    public class MyRunnerModel : IRunnerModel
    {
        public event Action<bool> OnSpeedChanged;
        public event Action<int> OnCollectableCountChanged;
        public event Action<RunnerState, RunnerState> OnStateChanged;

        public bool HasCollectable => CollectableCount > 0;
        public int CollectableCount
        {
            get => _collectableCount;
            set
            {
                _collectableCount = value;
                OnCollectableCountChanged?.Invoke(_collectableCount);
            } 
        }

        public RunnerState CurrentState
        {
            get => _currentState;
            set
            {
                // Invoke Old State - New State
                if(_currentState != value)
                    OnStateChanged?.Invoke(_currentState,value);
                
                _currentState = value;
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                if(value > _speed)
                    OnSpeedChanged?.Invoke(true);
                else if(value < _speed)
                    OnSpeedChanged?.Invoke(false);
                    
                _speed = value;
            }
        }

        private float _speed;
        private RunnerState _currentState;
        private int _collectableCount;


        public MyRunnerModel()
        {
            _currentState = RunnerState.Idle;
        }
    }
}