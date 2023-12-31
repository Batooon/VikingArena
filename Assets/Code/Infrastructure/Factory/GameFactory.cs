using Code.Enemy;
using Code.Hero;
using Code.Infrastructure.Services.AssetManagement;
using Code.Infrastructure.Services.EnemyRespawn;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Progress;
using Code.Infrastructure.Services.StaticData;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.UI;
using Code.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IProgressService _progressService;
        private readonly IInputService _inputService;
        private readonly IWindowService _windowService;

        private GameObject HeroGameObject { get; set; }
        private EnemyRespawner EnemyRespawner;

        public GameFactory(IStaticDataService staticData, IProgressService progressService,
            IInputService inputService, IWindowService windowService)
        {
            _staticData = staticData;
            _progressService = progressService;
            _inputService = inputService;
            _windowService = windowService;
        }

        public GameObject CreateHero(Vector3 at)
        {
            var heroData = _staticData.GetPlayerData();

            HeroGameObject = AssetProvider.Instantiate(heroData.Prefab, at);

            var health = HeroGameObject.GetComponent<IHealth>();
            health.Current = heroData.Hp;
            health.Max = heroData.Hp;

            var attack = HeroGameObject.GetComponent<HeroAttack>();
            attack.Construct(_inputService, _progressService.Progress.HeroStats);

            var death = HeroGameObject.GetComponent<HeroDeath>();
            death.Construct(_windowService);

            var animation = HeroGameObject.GetComponent<AnimateAlongPlayer>();
            animation.Construct(_inputService);

            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = AssetProvider.Instantiate(AssetPath.HudPath);

            hud.GetComponentInChildren<ScoreCounter>()
                .Construct(_progressService.Progress.WorldData);

            return hud;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Vector3 at)
        {
            var monsterData = _staticData.ForMonster(typeId);
            GameObject monster = Object.Instantiate(monsterData.Prefab, at, Quaternion.identity);

            var health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            monster.GetComponent<ActorUI>().Construct(health);

            var attack = monster.GetComponent<EnemyAttack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;
            attack.Cleavage = monsterData.Cleavage;

            var death = monster.GetComponent<EnemyDeath>();
            death.Construct(_progressService.Progress.WorldData);

            EnemyRespawner.AddSpawnedEnemy(monster);

            return monster;
        }

        public void CreateEnemyRespawner(string sceneName)
        {
            EnemyRespawner = AssetProvider.Instantiate(AssetPath.EnemyRespawner)
                .GetComponent<EnemyRespawner>();

            var terrainData = _staticData.ForLevel(sceneName).Terrain;
            EnemyRespawner.Construct(terrainData, HeroGameObject);
        }

        public void CreateSpawner(Vector3 at, MonsterTypeId monsterTypeId)
        {
            var spawner = AssetProvider.Instantiate(AssetPath.SpawnerPath, at)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.TypeId = monsterTypeId;
        }
    }
}