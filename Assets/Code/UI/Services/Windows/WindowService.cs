using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.States;
using Code.UI.Services.Factory;

namespace Code.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private readonly GameStateMachine _stateMachine;

        public WindowService(GameStateMachine gameStateMachine, IUIFactory uiFactory)
        {
            _stateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.MainMenu:
                    _uiFactory.CreateMainMenu(_stateMachine);
                    break;
                case WindowId.EndGameMenu:
                    _uiFactory.CreateEndGameMenu(_stateMachine);
                    break;
            }
        }
    }
}