namespace Game.Core.Controller.Runner
{
    public interface IRunnerGroundController : IRunnerController
    {
        bool IsNonWalkableGroundDetected();
    }
}