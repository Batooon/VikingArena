using System;

namespace Code.Infrastructure.Services.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public Stats HeroStats;

        public PlayerProgress()
        {
            WorldData = new WorldData();
            HeroStats = new Stats();
        }
    }
}