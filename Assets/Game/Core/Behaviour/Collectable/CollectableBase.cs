using UnityEngine;

namespace Game.Core.Behaviour.Collectable
{
    public class CollectableBase : MonoBehaviour
    {
        private Collider _collider;
        private Renderer _renderer;

        public Collider Collider => _collider ? _collider : _collider = GetComponent<Collider>();
        public Renderer Renderer => _renderer ? _renderer : _renderer = GetComponent<Renderer>();
        
        
        public void SetColor(Color color)
        {
            var materialPropertyBlock = new MaterialPropertyBlock();
            materialPropertyBlock.SetColor("_Color",color);
            Renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
