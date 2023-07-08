using System.Collections.Generic;
using Code.Enemy;
using Code.Logic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Infrastructure.Services.EnemyRespawn
{
    public class EnemyRespawner : MonoBehaviour
    {
        private List<EnemyDeath> _enemyDeathTrackers = new(10);

        private TerrainData _terrainData;
        private GameObject _hero;
        private float _respawnRadius = 25f;

        public void Construct(TerrainData terrainData, GameObject hero)
        {
            _terrainData = terrainData;
            _hero = hero;
        }

        private void OnDestroy()
        {
            foreach (EnemyDeath deathTracker in _enemyDeathTrackers)
            {
                deathTracker.Happened -= Respawn;
            }
        }

        public void AddSpawnedEnemy(GameObject enemy)
        {
            EnemyDeath deathTracker = enemy.GetComponent<EnemyDeath>();
            _enemyDeathTrackers.Add(deathTracker);
            deathTracker.Happened += Respawn;
        }

        private void Respawn(GameObject enemy)
        {
            enemy.transform.position = GetNewPosition();

            var health = enemy.GetComponent<IHealth>();
            health.Max += 1;
            health.Current = health.Max;

            var attack = enemy.GetComponent<EnemyAttack>();
            attack.Reset();

            var death = enemy.GetComponent<EnemyDeath>();
            death.Restore();

            enemy.SetActive(true);
            enemy.GetComponent<Follow>().enabled = true;
            attack.enabled = true;
        }

        private Vector3 GetNewPosition()
        {
            Vector2 randomizedPosition = Random.insideUnitCircle * _respawnRadius;
            Vector3 worldPosition = _hero.transform.position;
            worldPosition += new Vector3(randomizedPosition.x, 0, randomizedPosition.y);

            int mapX = (int)((worldPosition.x / _terrainData.size.x) * _terrainData.heightmapResolution);
            int mapZ = (int)((worldPosition.z / _terrainData.size.z) * _terrainData.heightmapResolution);

            float finalHeight = _terrainData.GetHeight(mapX, mapZ);
            worldPosition.y = finalHeight;
            return worldPosition;
        }
    }
}