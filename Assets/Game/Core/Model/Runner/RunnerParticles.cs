using System;
using UnityEngine;

namespace Game.Core.Model.Runner
{
    [Serializable]
    public class RunnerParticle
    {
        public RunnerParticleType Key;
        public ParticleSystem Value;
    }

    public enum RunnerParticleType
    {
        Fire,
        WaterSplash
    }
}