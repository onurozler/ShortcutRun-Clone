using System.Collections.Generic;
using System.Linq;
using Game.Core.Model.Runner;
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
        
        public static bool CheckLayer(this Collider collider, int layer)
        {
            return collider.gameObject.layer == layer;
        }

        public static bool CheckLayer(this RaycastHit raycastHit, int layer)
        {
            return raycastHit.collider.gameObject.layer == layer;
        }

        #endregion

        public static ParticleSystem GetParticle(this IEnumerable<RunnerParticle> runnerParticles, RunnerParticleType type)
        {
            return runnerParticles.FirstOrDefault(x => x.Key == type)?.Value;
        }
    }
}