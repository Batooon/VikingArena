using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        public MonsterTypeId TypeId;

        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            Spawn();
        }

        private void Spawn()
        {
            _gameFactory.CreateMonster(TypeId, at: transform.position);
        }
    }
}