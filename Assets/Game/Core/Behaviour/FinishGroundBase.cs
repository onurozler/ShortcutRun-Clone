using UnityEngine;

namespace Game.Core.Behaviour
{
    public class FinishGroundBase : MonoBehaviour
    {
        [SerializeField] private Transform[] _finishPositions;
        private int _index = 0;
        
        public Vector3 GetAvailablePosition()
        {
            return _finishPositions[_index++].position;
        }
    }
}