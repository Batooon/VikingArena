using Code.Enemy;
using Code.Hero;
using Code.Infrastructure.Services.AssetManagement;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Progress;
using Code.Infrastructure.Services.StaticData;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IProgressService _progressService;
        private readonly IInputService _inputService;

        private GameObject HeroGameObject { get; set; }

        public GameFactory(IAssets assets, IStaticDataService staticData, IProgressService progressService,
            IInputService inputService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _inputService = inputService;
        }

        public GameObject CreateHero(GameObject at)
        {
            var heroData = _staticData.GetPlayerData();

            HeroGameObject = _assets.Instantiate(heroData.Prefab, at.transform.position);

            var health = HeroGameObject.GetComponent<IHealth>();
            health.Current = heroData.Hp;
            health.Max = heroData.Hp;

            var attack = HeroGameObject.GetComponent<HeroAttack>();
            attack.Construct(_inputService, _progressService.Progress.HeroStats);

            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HudPath);

            hud.GetComponentInChildren<ScoreCounter>()
                .Construct(_progressService.Progress.WorldData);

            return hud;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform at)
        {
            var monsterData = _staticData.ForMonster(typeId);
            GameObject monster = Object.Instantiate(monsterData.Prefab, at.position, Quaternion.identity);

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

            return monster;
        }

        public void CreateSpawner(Vector3 at, MonsterTypeId monsterTypeId)
        {
            var spawner = _assets.Instantiate(AssetPath.SpawnerPath, at)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.TypeId = monsterTypeId;
        }
    }
}