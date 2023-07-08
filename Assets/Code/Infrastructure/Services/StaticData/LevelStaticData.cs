using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public TerrainData Terrain;

        public List<EnemySpawnerData> EnemySpawners;
    }
}