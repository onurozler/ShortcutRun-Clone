using UnityEngine;

namespace Game.Core.Model.Constants
{
    public static class RunnerConstants
    {
        public const float JumpPower = 300f;
        public const float EdgeDetectionDistance = 0.5f;
        public const float MyRunnerCameraThreshold = 2f;
        public const float NonWalkableCheckDistance = 7f;
        public static readonly Vector3 NonWalkableBoxCastExtends = new Vector3(0.5f,1,0.2f);

        public const int IncreaseSpeedThreshold = 3;
        public const float NormalSpeed = 4f;
        public const float IncreasedSpeed = 6f;
        
        public const float CollectableSpace = 0.1f;
        
        public static class Animation
        {
            public static string ArmLayer = "Arms";
            public static string RunSpeedParameter = "RunningSpeed";
        }
    }
}