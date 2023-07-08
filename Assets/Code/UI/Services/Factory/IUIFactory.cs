using Code.Infrastructure.Services;
using Code.Infrastructure.States;

namespace Code.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateMainMenu(GameStateMachine stateMachine);
        void CreateEndGameMenu(GameStateMachine stateMachine);
        void CreateUIRoot();
    }
}