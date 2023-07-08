using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.AssetManagement;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Progress;
using Code.Infrastructure.Services.StaticData;
using Code.UI.Services.Factory;
using Code.UI.Services.Windows;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Menu";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;

        public BootstrapState(GameStateMachine stateMachine, AllServices services, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();

            _uiFactory = _services.Single<IUIFactory>();
            _windowService = _services.Single<IWindowService>();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitMainMenu();
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

            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IProgressService>()
            ));

            _services.RegisterSingle<IWindowService>(new WindowService(_stateMachine, _services.Single<IUIFactory>()));

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IProgressService>(),
                _services.Single<IInputService>(),
                _services.Single<IWindowService>()
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
            progressService.Progress = new PlayerProgress();
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitMainMenu() =>
            _windowService.Open(WindowId.MainMenu);
    }
}