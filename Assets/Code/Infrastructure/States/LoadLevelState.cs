using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.StaticData;
using Code.Logic;
using Code.Logic.Camera;
using Code.UI;
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

        private string _levelName;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticData = staticData;
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
            InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject hero = InitHero();

            InitSpawners();

            InitHud(hero);
            CameraFollow(hero);
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