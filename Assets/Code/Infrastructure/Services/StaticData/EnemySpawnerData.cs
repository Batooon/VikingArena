using System;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;

        public EnemySpawnerData(MonsterTypeId monsterTypeId, Vector3 position)
        {
            MonsterTypeId = monsterTypeId;
            Position = position;
        }
    }
}