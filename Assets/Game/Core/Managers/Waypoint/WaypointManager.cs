using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Core.Managers
{
    public class WaypointManager : MonoBehaviour, IWaypointManager
    {
        [SerializeField]
        private Transform[] wayPoints;

        public IList<Transform> Waypoints => wayPoints;
        
        public void Initialize()
        {
            wayPoints = GetComponentsInChildren<Transform>().Where(x=> x.gameObject != gameObject).ToArray();
        }

        private void OnDrawGizmos()
        {
            for (var index = 0; index < wayPoints.Length; index++)
            {
                if (index + 1 == wayPoints.Length)
                {
                    break;
                }
                
                var wayPoint = wayPoints[index];
                var target = wayPoints[index + 1];
                
                Gizmos.DrawLine(wayPoint.position,target.position);
            }
        }
    }

    [CustomEditor(typeof(WaypointManager),true)]
    public class WaypointManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var waypointManager = (WaypointManager) target;
            if (GUILayout.Button("Initialize Waypoints"))
            {
                waypointManager.Initialize();
            }
        }
    }
}
