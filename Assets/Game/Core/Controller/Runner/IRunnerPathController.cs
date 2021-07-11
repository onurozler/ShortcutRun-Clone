using UnityEngine;

namespace Game.Core.Controller.Runner
{
    public interface IRunnerPathController : IRunnerController
    {
        void CreateWalkablePath(Transform collectableTransform);
        void AddWalkablePath(Collision collision);
    }
}