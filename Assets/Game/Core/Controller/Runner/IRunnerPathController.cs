using UnityEngine;

namespace Game.Core.Controller.Runner
{
    public interface IRunnerPathController : IRunnerController
    {
        bool IsTouchingGround { get; }
        bool IsEdgeDetected();
        void CreateWalkablePath(Transform collectableBase);
        void AddWalkablePath(Collision collision);
        void RemoveWalkablePath(Collision collision);
    }
}