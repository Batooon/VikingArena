using Code.Infrastructure.Services;
using Code.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateMonster(MonsterTypeId typeId, Transform at);
        void CreateSpawner(Vector3 at, MonsterTypeId monsterTypeId);
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        void CreateEnemyRespawner(string sceneName);
    }
}