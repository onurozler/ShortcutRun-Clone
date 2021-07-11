using UnityEngine;

namespace Game.Core.Behaviour.Obstacle
{
    public class RotatingObstacle : MonoBehaviour
    {
        [SerializeField] 
        private Transform _rotatingSide;

        public float Speed = 50f;
        
        private void FixedUpdate()
        {
            _rotatingSide.Rotate(0,Speed * Time.deltaTime,0);
        }
    }
}
