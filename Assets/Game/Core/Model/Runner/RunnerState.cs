namespace Game.Core.Model.Runner
{
    public enum RunnerState
    {
        Idle,
        Running,
        Jumping,
        Climbing, // TODO: Climbing Mechanic
        Died,
        Finished
    }
}