using System;
using DG.Tweening;

namespace Game.Core.Helpers.TimingManager.Impl
{
    public class TweenTimingManager : ITimingManager
    {
        public IDisposable Delay(TimeSpan delay, Action action)
        {
            return new DelayedTweenAction(delay, action, false);
        }

        public IDisposable Interval(TimeSpan interval, Action action)
        {
            return new DelayedTweenAction(interval, action, true);
        }

        private class DelayedTweenAction : IDisposable
        {
            private readonly Tween _tween;
            
            public DelayedTweenAction(TimeSpan delay, Action action, bool looping)
            {
                _tween = DOVirtual.DelayedCall((float) delay.TotalSeconds, () => action());
                if (looping)
                {
                    _tween.SetLoops(-1);
                }
            }
            
            public void Dispose()
            {
                if (_tween.active)
                {
                    _tween.Kill();
                }
            }
        }
    }
}