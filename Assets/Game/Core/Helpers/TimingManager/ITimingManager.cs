using System;

namespace Game.Core.Helpers.TimingManager
{
    public interface ITimingManager
    {
        IDisposable Delay(TimeSpan delay, Action action);
        IDisposable Interval(TimeSpan interval, Action action);
    }
}