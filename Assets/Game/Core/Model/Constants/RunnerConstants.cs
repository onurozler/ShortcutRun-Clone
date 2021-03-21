namespace Game.Core.Model.Constants
{
    public static class RunnerConstants
    {
        public const float JumpPower = 300f;
        public const float EdgeDetectionDistance = 0.5f;
        public const float MyRunnerCameraThreshold = 2f;

        public const int IncreaseSpeedThreshold = 3;
        public const float RunnerNormalSpeed = 4f;
        public const float RunnerIncreasedSpeed = 6f;
        public const float RunnerIncreasedSpeedTime = 2f;
        
        public static class Animation
        {
            public static string ArmLayer = "Arms";
            public static string RunSpeedParameter = "RunningSpeed";
        }
    }
}