using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Input;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";

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
            _sceneLoader.Load(InitialSceneName);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(new StandaloneInputService());
        }
    }
}