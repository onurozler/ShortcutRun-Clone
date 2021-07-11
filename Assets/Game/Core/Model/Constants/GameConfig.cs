namespace Game.Core.Model.Constants
{
    public static class GameConfig
    {
        public const int WalkableLayer = 8;
        public const int NonWalkableLayer = 9;
        public const int FinishLayer = 10;
        public const int WalkableLayerMask = 1 << WalkableLayer;
    }
}
