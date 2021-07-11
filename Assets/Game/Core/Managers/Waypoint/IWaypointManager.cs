using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Managers
{
    public interface IWaypointManager
    {
        IList<Transform> Waypoints { get; }
    }
}