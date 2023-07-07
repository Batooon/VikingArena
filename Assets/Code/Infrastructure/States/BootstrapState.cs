using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.AssetManagement;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Progress;
using Code.Infrastructure.Services.StaticData;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Bootstrap";
        private const string PlaySceneName = "Main";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, AllServices services, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(PlaySceneName);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(new StandaloneInputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());

            RegisterStaticData();

            RegisterProgress();

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IProgressService>(),
                _services.Single<IInputService>()
            ));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private void RegisterProgress()
        {
            IProgressService progressService = new ProgressService();
            _services.RegisterSingle(progressService);
            progressService.Progress = new PlayerProgress
            {
                HeroStats =
                {
                    Damage = 1,
                    Cleavage = .5f,
                }
            };
        }
    }
}