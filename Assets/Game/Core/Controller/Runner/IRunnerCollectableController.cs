using Game.Core.Behaviour.Collectable;
using UnityEngine;

namespace Game.Core.Controller.Runner
{
    public interface IRunnerCollectableController : IRunnerController
    {
        void Collect(CollectableBase collectable);
        Transform Drop();
    }
}