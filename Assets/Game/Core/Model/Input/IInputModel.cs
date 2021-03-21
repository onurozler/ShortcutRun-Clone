namespace Game.Core.Model.Input
{
    public interface IInputModel
    {
        bool IsTouching { get; }
        float GetMouseAxis(MouseAxis mouseAxis);
    }

    public enum MouseAxis
    {
        X,
        Y
    }
}