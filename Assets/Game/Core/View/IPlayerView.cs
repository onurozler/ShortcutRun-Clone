using System;

namespace Game.Core.View
{
    public interface IPlayerView
    {
        event Action OnStartButtonPressed;
        event Action OnRestartButtonPressed;
    }
}