using UnityEngine;

namespace Game.Core.Helpers
{
    public static class Extensions
    {
        #region Physics
        public static bool CheckLayer(this Collision collision, int layer)
        {
            return collision.gameObject.layer == layer;
        }

        #endregion

    }
}