using UnityEngine;

namespace Game.Core.Behaviour.Collectable
{
    public class CollectableBase : MonoBehaviour
    {
        private Collider _collider;

        public Collider Collider => _collider ? _collider : _collider = GetComponent<Collider>();
    }
}
