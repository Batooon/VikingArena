using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataEnemiesPath = "StaticData/Enemies";
        private const string StaticDataPlayerPath = "StaticData/Player/Hero";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private PlayerStaticData _player;

        public void Load()
        {
            LoadMonsters();
            LoadPlayer();
            LoadLevel();
        }

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;

        public PlayerStaticData GetPlayerData() =>
            _player;

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData) ? staticData : null;

        private void LoadLevel()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }

        private void LoadPlayer() =>
            _player = Resources.Load<PlayerStaticData>(StaticDataPlayerPath);

        private void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(StaticDataEnemiesPath)
                .ToDictionary(x => x.TypeId, x => x);
        }
    }
}