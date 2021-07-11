using System.Collections.Generic;
using Game.Core.Model.Runner;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Behaviour.Runner
{
    public class RunnerComponentBehaviour : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Transform EdgeDetector;
        public Transform CollectableTransform;
        public Animator Animator;
        public List<RunnerParticle> Particles;
        public Text PlayerName;
    }
}
