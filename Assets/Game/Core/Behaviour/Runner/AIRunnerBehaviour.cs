using System;
using DG.Tweening;
using Game.Core.Helpers.TimingManager;
using Game.Core.Managers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Core.Behaviour.Runner
{
    public class AIRunnerBehaviour : RunnerBehaviourBase
    {
        private IWaypointManager _waypointManager;
        private ITimingManager _timingManager;
        private Vector3 _targetPosition;
        private int _currentIndex;
        private float _targetThreshold;
        private IDisposable _directionCheckInterval;
        
        [Inject]
        private void Initialize(IWaypointManager waypointManager, ITimingManager timingManager)
        {
            _timingManager = timingManager;
            _waypointManager = waypointManager;
            _currentIndex = -1;
            ChangeWaypoint();
        }
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (Vector3.Distance(_targetPosition,transform.position) < _targetThreshold)
            {
                ChangeWaypoint();
            }
        }
        
        // Randomized waypoint selector logic
        private void ChangeWaypoint()
        {
            if (_currentIndex >= _waypointManager.Waypoints.Count - 1)
            {
                return;
            }
            
            _targetThreshold = Random.Range(2, 5);
            if (RunnerModel.HasCollectable)
            {
                var nextStep = Random.Range(1, 3);
                if (_currentIndex + nextStep >= _waypointManager.Waypoints.Count)
                {
                    _currentIndex++;
                }
                else
                {
                    if (nextStep != 1)
                    {
                        var currentPos = _waypointManager.Waypoints[_currentIndex].position;
                        var targetPos = _waypointManager.Waypoints[_currentIndex + nextStep].position;
                        var distance = Vector3.Distance(currentPos, targetPos);

                        var percentage = (int)(RunnerModel.CollectableCount * 100 / (distance + 25));
                        var stepRandom = Random.Range(0, 100);
                        _currentIndex += stepRandom <= percentage ? nextStep : 1;
                    }
                    else
                    {
                        _currentIndex++;
                    }
                }
            }
            else
            {
                _currentIndex++;
            }
            
            _targetPosition = _waypointManager.Waypoints[_currentIndex].position;
            
            // Obstacles may affect AI direction...
            _directionCheckInterval?.Dispose();
            _directionCheckInterval = _timingManager.Interval(TimeSpan.FromSeconds(Random.Range(0, 2)), () =>
            {
                _targetPosition.y = transform.position.y;
                transform.DOLookAt(_targetPosition, 0.5f);
            });
        }
    }
}