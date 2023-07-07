using System;

namespace Code.Infrastructure.Services.Progress
{
    [Serializable]
    public class WorldData
    {
        public Score Score;

        public WorldData()
        {
            Score = new Score();
        }
    }
}