using System;
using Game.Core.Model.Constants;
using UnityEngine;

namespace Game.Core.Model.Runner
{
    [CreateAssetMenu(fileName = "RunnerData", menuName = "Game/Create/RunnerData")]
    public class RunnerModel : ScriptableObject, IRunnerModel
    {
        [SerializeField] private string _runnerName;
        [SerializeField] private Color _color;
        
        public event Action<bool> OnSpeedChanged;
        public event Action<int> OnCollectableCountIncreased;
        public event Action<int> OnCollectableCountChanged;
        public event Action<RunnerState, RunnerState> OnStateChanged;

        public Color Color => _color;
        public bool HasCollectable => CollectableCount > 0;
        public int CollectableCount
        {
            get => _collectableCount;
            set
            {
                if (value > _collectableCount)
                {
                    OnCollectableCountIncreased?.Invoke(value);
                }
                
                _collectableCount = value;
                OnCollectableCountChanged?.Invoke(_collectableCount);
            } 
        }

        public string RunnerName => _runnerName;

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


        public void ResetData()
        {
            _currentState = RunnerState.Idle;
            _speed = RunnerConstants.NormalSpeed;
            _collectableCount = 0;
        }
    }
}