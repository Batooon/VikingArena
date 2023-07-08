using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.StaticData;
using Code.Logic;
using Code.Logic.Camera;
using Code.UI;
using Code.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;

        private string _levelName;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IStaticDataService staticData, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticData = staticData;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _levelName = sceneName;

            _sceneLoader.Load(_levelName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitGameWorld()
        {
            GameObject hero = InitHero();

            InitEnemyRespawn();
            InitSpawners();

            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitEnemyRespawn()
        {
            _gameFactory.CreateEnemyRespawner(_levelName);
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
        }

        private void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);

        private GameObject InitHero() =>
            _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.MonsterTypeId);
            }
        }
    }
}